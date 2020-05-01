using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TwelveFinal.Entities;
using TwelveFinal.Repositories.Models;

namespace TwelveFinal.Repositories
{
    public interface IDistrictRepository
    {
        Task<bool> Create(District district);
        Task<int> Count(DistrictFilter districtFilter);
        Task<District> Get(Guid Id);
        Task<List<District>> List(DistrictFilter districtFilter);
        Task<bool> Update(District district);
        Task<bool> Delete(Guid Id);
    }
    public class DistrictRepository : IDistrictRepository
    {
        private readonly TFContext tFContext;
        public DistrictRepository(TFContext _tFContext)
        {
            tFContext = _tFContext;
        }

        private IQueryable<DistrictDAO> DynamicFilter(IQueryable<DistrictDAO> query, DistrictFilter districtFilter)
        {
            if (districtFilter == null)
                return query.Where(q => 1 == 0);
            query = query.Where(q => q.ProvinceId == districtFilter.ProvinceId);

            if (districtFilter.Id != null)
                query = query.Where(q => q.Id, districtFilter.Id);
            if (districtFilter.Name != null)
                query = query.Where(q => q.Name, districtFilter.Name);
            if (districtFilter.Code != null)
                query = query.Where(q => q.Code, districtFilter.Code);
            return query;
        }
        private IQueryable<DistrictDAO> DynamicOrder(IQueryable<DistrictDAO> query, DistrictFilter districtFilter)
        {
            switch (districtFilter.OrderType)
            {
                case OrderType.ASC:
                    switch (districtFilter.OrderBy)
                    {
                        case DistrictOrder.Code:
                            query = query.OrderBy(q => q.Code);
                            break;
                        case DistrictOrder.Name:
                            query = query.OrderBy(q => q.Name);
                            break;
                    }
                    break;
                case OrderType.DESC:
                    switch (districtFilter.OrderBy)
                    {
                        case DistrictOrder.Code:
                            query = query.OrderByDescending(q => q.Code);
                            break;
                        case DistrictOrder.Name:
                            query = query.OrderByDescending(q => q.Name);
                            break;
                    }
                    break;
            }
            query = query.Skip(districtFilter.Skip).Take(districtFilter.Take);
            return query;
        }
        private async Task<List<District>> DynamicSelect(IQueryable<DistrictDAO> query)
        {
            List<District> districts = await query.Select(q => new District()
            {
                Id = q.Id,
                ProvinceId = q.ProvinceId,
                ProvinceCode = q.Province.Code,
                ProvinceName = q.Province.Name,
                Name = q.Name,
                Code = q.Code
            }).ToListAsync();
            return districts;
        }

        public async Task<int> Count(DistrictFilter districtFilter)
        {
            IQueryable<DistrictDAO> districtDAOs = tFContext.District;
            districtDAOs = DynamicFilter(districtDAOs, districtFilter);
            return await districtDAOs.CountAsync();
        }

        public async Task<List<District>> List(DistrictFilter districtFilter)
        {
            if (districtFilter == null) return new List<District>();
            IQueryable<DistrictDAO> districtDAOs = tFContext.District;
            districtDAOs = DynamicFilter(districtDAOs, districtFilter);
            districtDAOs = DynamicOrder(districtDAOs, districtFilter);
            var districts = await DynamicSelect(districtDAOs);
            return districts;
        }

        public async Task<bool> Create(District district)
        {
            DistrictDAO DistrictDAO = new DistrictDAO
            {
                Id = district.Id,
                Code = district.Code,
                Name = district.Name,
                ProvinceId = district.ProvinceId
            };

            tFContext.District.Add(DistrictDAO);
            await tFContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Delete(Guid Id)
        {
            await tFContext.Town.Where(t => t.DistrictId.Equals(Id)).DeleteFromQueryAsync();
            await tFContext.District.Where(d => d.Id.Equals(Id)).DeleteFromQueryAsync();
            return true;
        }

        public async Task<District> Get(Guid Id)
        {
            District District = await tFContext.District.Where(d => d.Id.Equals(Id)).Select(d => new District
            {
                Id = d.Id,
                Code = d.Code,
                Name = d.Name,
                ProvinceId = d.ProvinceId,
                ProvinceCode = d.Province.Code,
                ProvinceName = d.Province.Name,
            }).FirstOrDefaultAsync();

            return District;
        }

        public async Task<bool> Update(District district)
        {
            await tFContext.District.Where(t => t.Id.Equals(district.Id)).UpdateFromQueryAsync(t => new DistrictDAO
            {
                Code = district.Code,
                Name = district.Name,
                ProvinceId = district.ProvinceId
            });

            return true;
        }
    }
}
