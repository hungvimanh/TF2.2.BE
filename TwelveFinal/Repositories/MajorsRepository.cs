using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TwelveFinal.Entities;
using TwelveFinal.Repositories.Models;

namespace TwelveFinal.Repositories
{
    public interface IMajorsRepository
    {
        Task<bool> Create(Majors majors);
        Task<List<Majors>> List(MajorsFilter majorsFilter);
        Task<int> Count(MajorsFilter majorsFilter);
        Task<Majors> Get(Guid Id);
        Task<bool> Update(Majors majors);
        Task<bool> Delete(Guid Id);
    }
    public class MajorsRepository : IMajorsRepository
    {
        private readonly TFContext tFContext;
        public MajorsRepository(TFContext _tFContext)
        {
            tFContext = _tFContext;
        }

        private IQueryable<MajorsDAO> DynamicFilter(IQueryable<MajorsDAO> query, MajorsFilter majorsFilter)
        {
            if (majorsFilter == null)
                return query.Where(q => 1 == 0);

            if (majorsFilter.Id != null)
                query = query.Where(q => q.Id, majorsFilter.Id);
            if (majorsFilter.Name != null)
                query = query.Where(q => q.Name, majorsFilter.Name);
            if (majorsFilter.Code != null)
                query = query.Where(q => q.Code, majorsFilter.Code);
            return query;
        }
        private IQueryable<MajorsDAO> DynamicOrder(IQueryable<MajorsDAO> query, MajorsFilter majorsFilter)
        {
            switch (majorsFilter.OrderType)
            {
                case OrderType.ASC:
                    switch (majorsFilter.OrderBy)
                    {
                        case MajorsOrder.Code:
                            query = query.OrderBy(q => q.Code);
                            break;
                        case MajorsOrder.Name:
                            query = query.OrderBy(q => q.Name);
                            break;
                        default:
                            query = query.OrderBy(q => q.CX);
                            break;
                    }
                    break;
                case OrderType.DESC:
                    switch (majorsFilter.OrderBy)
                    {
                        case MajorsOrder.Code:
                            query = query.OrderByDescending(q => q.Code);
                            break;
                        case MajorsOrder.Name:
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
            query = query.Skip(majorsFilter.Skip).Take(majorsFilter.Take);
            return query;
        }
        private async Task<List<Majors>> DynamicSelect(IQueryable<MajorsDAO> query)
        {

            List<Majors> Majorss = await query.Select(q => new Majors()
            {
                Id = q.Id,
                Name = q.Name,
                Code = q.Code
            }).ToListAsync();
            return Majorss;
        }

        public async Task<int> Count(MajorsFilter majorsFilter)
        {
            IQueryable<MajorsDAO> majorsDAOs = tFContext.Majors;
            majorsDAOs = DynamicFilter(majorsDAOs, majorsFilter);
            return await majorsDAOs.CountAsync();
        }

        public async Task<List<Majors>> List(MajorsFilter majorsFilter)
        {
            if (majorsFilter == null) return new List<Majors>();
            IQueryable<MajorsDAO> majorsDAOs = tFContext.Majors;
            majorsDAOs = DynamicFilter(majorsDAOs, majorsFilter);
            majorsDAOs = DynamicOrder(majorsDAOs, majorsFilter);
            var majorss = await DynamicSelect(majorsDAOs);
            return majorss;
        }

        public async Task<bool> Create(Majors majors)
        {
            MajorsDAO majorsDAO = new MajorsDAO
            {
                Id = majors.Id,
                Code = majors.Code,
                Name = majors.Name
            };

            tFContext.Majors.Add(majorsDAO);
            await tFContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Delete(Guid Id)
        {
            await tFContext.Majors.Where(m => m.Id.Equals(Id)).DeleteFromQueryAsync();
            return true;
        }

        public async Task<Majors> Get(Guid Id)
        {
            Majors Majors = await tFContext.Majors.Where(m => m.Id.Equals(Id)).Select(m => new Majors
            {
                Id = m.Id,
                Code = m.Code,
                Name = m.Name
            }).FirstOrDefaultAsync();

            return Majors;
        }

        public async Task<bool> Update(Majors majors)
        {
            await tFContext.Majors.Where(m => m.Id.Equals(majors.Id)).UpdateFromQueryAsync(m => new MajorsDAO
            {
                Code = majors.Code,
                Name = majors.Name
            });

            return true;
        }
    }
}
