using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TwelveFinal.Entities;
using TwelveFinal.Repositories.Models;

namespace TwelveFinal.Repositories
{
    public interface IHighSchoolRepository
    {
        Task<bool> Create(HighSchool highSchool);
        Task<int> Count(HighSchoolFilter highSchoolFilter);
        Task<List<HighSchool>> List(HighSchoolFilter highSchoolFilter);
        Task<HighSchool> Get(Guid Id);
        Task<bool> Update(HighSchool highSchool);
        Task<bool> Delete(Guid Id);
    }
    public class HighSchoolRepository : IHighSchoolRepository
    {
        private readonly TFContext tFContext;
        public HighSchoolRepository(TFContext _tFContext)
        {
            tFContext = _tFContext;
        }

        private IQueryable<HighSchoolDAO> DynamicFilter(IQueryable<HighSchoolDAO> query, HighSchoolFilter highSchoolFilter)
        {
            if (highSchoolFilter == null)
                return query.Where(q => 1 == 0);
            query = query.Where(q => q.ProvinceId == highSchoolFilter.ProvinceId);

            if (highSchoolFilter.Id != null)
                query = query.Where(q => q.Id, highSchoolFilter.Id);
            if (highSchoolFilter.Name != null)
                query = query.Where(q => q.Name, highSchoolFilter.Name);
            if (highSchoolFilter.Code != null)
                query = query.Where(q => q.Code, highSchoolFilter.Code);
            return query;
        }
        private IQueryable<HighSchoolDAO> DynamicOrder(IQueryable<HighSchoolDAO> query, HighSchoolFilter highSchoolFilter)
        {
            switch (highSchoolFilter.OrderType)
            {
                case OrderType.ASC:
                    switch (highSchoolFilter.OrderBy)
                    {
                        case HighSchoolOrder.Code:
                            query = query.OrderBy(q => q.Code);
                            break;
                        case HighSchoolOrder.Name:
                            query = query.OrderBy(q => q.Name);
                            break;
                        default:
                            query = query.OrderBy(q => q.CX);
                            break;
                    }
                    break;
                case OrderType.DESC:
                    switch (highSchoolFilter.OrderBy)
                    {
                        case HighSchoolOrder.Code:
                            query = query.OrderByDescending(q => q.Code);
                            break;
                        case HighSchoolOrder.Name:
                            query = query.OrderByDescending(q => q.Name);
                            break;
                        default:
                            query = query.OrderByDescending(q => q.CX);
                            break;
                    }
                    break;
                default:
                    query = query.OrderBy(q => q.CX);
                    break;
            }
            query = query.Skip(highSchoolFilter.Skip).Take(highSchoolFilter.Take);
            return query;
        }
        private async Task<List<HighSchool>> DynamicSelect(IQueryable<HighSchoolDAO> query)
        {

            List<HighSchool> highSchools = await query.Select(q => new HighSchool()
            {
                Id = q.Id,
                Name = q.Name,
                Code = q.Code,
                ProvinceId = q.ProvinceId,
                ProvinceCode = q.Province.Code,
                ProvinceName = q.Province.Name,
                Address = q.Address,
            }).ToListAsync();
            return highSchools;
        }

        public async Task<int> Count(HighSchoolFilter highSchoolFilter)
        {
            IQueryable<HighSchoolDAO> highSchoolDAOs = tFContext.HighSchool;
            highSchoolDAOs = DynamicFilter(highSchoolDAOs, highSchoolFilter);
            return await highSchoolDAOs.CountAsync();
        }

        public async Task<bool> Create(HighSchool highSchool)
        {
            HighSchoolDAO HighSchoolDAO = new HighSchoolDAO
            {
                Id = highSchool.Id,
                Code = highSchool.Code,
                Name = highSchool.Name,
                ProvinceId = highSchool.ProvinceId,
                Address = highSchool.Address,
            };

            tFContext.HighSchool.Add(HighSchoolDAO);
            await tFContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Delete(Guid Id)
        {
            await tFContext.Student.Where(h => h.HighSchoolId.Equals(Id)).DeleteFromQueryAsync();
            await tFContext.Form.Where(h => h.RegisterPlaceOfExamId.Equals(Id)).DeleteFromQueryAsync();
            await tFContext.HighSchool.Where(h => h.Id.Equals(Id)).DeleteFromQueryAsync();
            return true;
        }

        public async Task<HighSchool> Get(Guid Id)
        {
            HighSchool HighSchool = await tFContext.HighSchool.Where(h => h.Id.Equals(Id)).Select(h => new HighSchool
            {
                Id = h.Id,
                Code = h.Code,
                Name = h.Name,
                ProvinceId = h.ProvinceId,
                ProvinceCode = h.Province.Code,
                ProvinceName = h.Province.Name,
                Address = h.Address,
            }).FirstOrDefaultAsync();

            return HighSchool;
        }

        public async Task<List<HighSchool>> List(HighSchoolFilter highSchoolFilter)
        {
            if (highSchoolFilter == null) return new List<HighSchool>();
            IQueryable<HighSchoolDAO> highSchoolDAOs = tFContext.HighSchool;
            highSchoolDAOs = DynamicFilter(highSchoolDAOs, highSchoolFilter);
            highSchoolDAOs = DynamicOrder(highSchoolDAOs, highSchoolFilter);
            var highSchools = await DynamicSelect(highSchoolDAOs);
            return highSchools;
        }

        public async Task<bool> Update(HighSchool highSchool)
        {
            await tFContext.HighSchool.Where(t => t.Id.Equals(highSchool.Id)).UpdateFromQueryAsync(t => new HighSchoolDAO
            {
                Code = highSchool.Code,
                Name = highSchool.Name,
                ProvinceId = highSchool.ProvinceId,
                Address = highSchool.Address,
            });

            return true;
        }
    }
}
