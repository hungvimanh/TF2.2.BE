using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TwelveFinal.Entities;
using TwelveFinal.Repositories.Models;

namespace TwelveFinal.Repositories
{
    public interface ITownRepository
    {
        Task<bool> Create(Town town);
        Task<int> Count(TownFilter townFilter);
        Task<List<Town>> List(TownFilter townFilter);
        Task<Town> Get(Guid Id);
        Task<bool> Update(Town town);
        Task<bool> Delete(Guid Id);
    }
    public class TownRepository : ITownRepository
    {
        private readonly TFContext tFContext;
        public TownRepository(TFContext _tFContext)
        {
            tFContext = _tFContext;
        }

        private IQueryable<TownDAO> DynamicFilter(IQueryable<TownDAO> query, TownFilter townFilter)
        {
            if (townFilter == null)
                return query.Where(q => 1 == 0);
            query = query.Where(q => q.DistrictId == townFilter.DistrictId);

            if (townFilter.Id != null)
                query = query.Where(q => q.Id, townFilter.Id);
            if (townFilter.Name != null)
                query = query.Where(q => q.Name, townFilter.Name);
            if (townFilter.Code != null)
                query = query.Where(q => q.Code, townFilter.Code);
            return query;
        }
        private IQueryable<TownDAO> DynamicOrder(IQueryable<TownDAO> query, TownFilter townFilter)
        {
            switch (townFilter.OrderType)
            {
                case OrderType.ASC:
                    switch (townFilter.OrderBy)
                    {
                        case TownOrder.Code:
                            query = query.OrderBy(q => q.Code);
                            break;
                        case TownOrder.Name:
                            query = query.OrderBy(q => q.Name);
                            break;
                        default:
                            query = query.OrderBy(q => q.CX);
                            break;
                    }
                    break;
                case OrderType.DESC:
                    switch (townFilter.OrderBy)
                    {
                        case TownOrder.Code:
                            query = query.OrderByDescending(q => q.Code);
                            break;
                        case TownOrder.Name:
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
            query = query.Skip(townFilter.Skip).Take(townFilter.Take);
            return query;
        }
        private async Task<List<Town>> DynamicSelect(IQueryable<TownDAO> query)
        {

            List<Town> towns = await query.Select(q => new Town()
            {
                Id = q.Id,
                DistrictId = q.DistrictId,
                DistrictCode = q.District.Code,
                DistrictName = q.District.Name,
                Name = q.Name,
                Code = q.Code
            }).ToListAsync();
            return towns;
        }

        public async Task<int> Count(TownFilter townFilter)
        {
            IQueryable<TownDAO> townDAOs = tFContext.Town;
            townDAOs = DynamicFilter(townDAOs, townFilter);
            return await townDAOs.CountAsync();
        }

        public async Task<List<Town>> List(TownFilter townFilter)
        {
            if (townFilter == null) return new List<Town>();
            IQueryable<TownDAO> townDAOs = tFContext.Town;
            townDAOs = DynamicFilter(townDAOs, townFilter);
            townDAOs = DynamicOrder(townDAOs, townFilter);
            var towns = await DynamicSelect(townDAOs);
            return towns;
        }

        public async Task<bool> Create(Town town)
        {
            TownDAO TownDAO = new TownDAO
            {
                Id = town.Id,
                Code = town.Code,
                Name = town.Name,
                DistrictId = town.DistrictId,
            };

            tFContext.Town.Add(TownDAO);
            await tFContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Delete(Guid Id)
        {
            await tFContext.Student.Where(p => p.TownId.Equals(Id)).DeleteFromQueryAsync();
            await tFContext.Town.Where(t => t.Id.Equals(Id)).DeleteFromQueryAsync();
            return true;
        }

        public async Task<Town> Get(Guid Id)
        {
            Town Town = await tFContext.Town.Where(t => t.Id.Equals(Id)).Select(t => new Town
            {
                Id = t.Id,
                Code = t.Code,
                Name = t.Name,
                DistrictId = t.DistrictId,
                DistrictCode = t.District.Code,
                DistrictName = t.District.Name
            }).FirstOrDefaultAsync();

            return Town;
        }

        public async Task<bool> Update(Town town)
        {
            await tFContext.Town.Where(t => t.Id.Equals(town.Id)).UpdateFromQueryAsync(t => new TownDAO
            {
                Code = town.Code,
                Name = town.Name,
                DistrictId = town.DistrictId
            });

            return true;
        }
    }
}
