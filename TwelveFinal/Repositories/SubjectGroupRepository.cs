using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TwelveFinal.Entities;
using TwelveFinal.Repositories.Models;

namespace TwelveFinal.Repositories
{
    public interface ISubjectGroupRepository
    {
        Task<bool> Create(SubjectGroup subjectGroup);
        Task<SubjectGroup> Get(Guid Id);
        Task<int> Count(SubjectGroupFilter subjectGroupFilter);
        Task<List<SubjectGroup>> List(SubjectGroupFilter subjectGroupFilter);
        Task<bool> Update(SubjectGroup subjectGroup);
        Task<bool> Delete(Guid Id);
    }
    public class SubjectGroupRepository : ISubjectGroupRepository
    {
        private readonly TFContext tFContext;
        public SubjectGroupRepository(TFContext _tFContext)
        {
            tFContext = _tFContext;
        }

        private IQueryable<SubjectGroupDAO> DynamicFilter(IQueryable<SubjectGroupDAO> query, SubjectGroupFilter subjectGroupFilter)
        {
            if (subjectGroupFilter == null)
                return query.Where(q => 1 == 0);

            if (subjectGroupFilter.Id != null)
                query = query.Where(q => q.Id, subjectGroupFilter.Id);
            if (subjectGroupFilter.Name != null)
                query = query.Where(q => q.Name, subjectGroupFilter.Name);
            if (subjectGroupFilter.Code != null)
                query = query.Where(q => q.Code, subjectGroupFilter.Code);
            return query;
        }
        private IQueryable<SubjectGroupDAO> DynamicOrder(IQueryable<SubjectGroupDAO> query, SubjectGroupFilter subjectGroupFilter)
        {
            switch (subjectGroupFilter.OrderType)
            {
                case OrderType.ASC:
                    switch (subjectGroupFilter.OrderBy)
                    {
                        case SubjectGroupOrder.Code:
                            query = query.OrderBy(q => q.Code);
                            break;
                        case SubjectGroupOrder.Name:
                            query = query.OrderBy(q => q.Name);
                            break;
                        default:
                            query = query.OrderBy(q => q.CX);
                            break;
                    }
                    break;
                case OrderType.DESC:
                    switch (subjectGroupFilter.OrderBy)
                    {
                        case SubjectGroupOrder.Code:
                            query = query.OrderByDescending(q => q.Code);
                            break;
                        case SubjectGroupOrder.Name:
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
            query = query.Skip(subjectGroupFilter.Skip).Take(subjectGroupFilter.Take);
            return query;
        }
        private async Task<List<SubjectGroup>> DynamicSelect(IQueryable<SubjectGroupDAO> query)
        {

            List<SubjectGroup> subjectGroups = await query.Select(q => new SubjectGroup()
            {
                Id = q.Id,
                Name = q.Name,
                Code = q.Code
            }).ToListAsync();
            return subjectGroups;
        }

        public async Task<int> Count(SubjectGroupFilter subjectGroupFilter)
        {
            IQueryable<SubjectGroupDAO> subjectGroupDAOs = tFContext.SubjectGroup;
            subjectGroupDAOs = DynamicFilter(subjectGroupDAOs, subjectGroupFilter);
            return await subjectGroupDAOs.CountAsync();
        }

        public async Task<List<SubjectGroup>> List(SubjectGroupFilter subjectGroupFilter)
        {
            if (subjectGroupFilter == null) return new List<SubjectGroup>();
            IQueryable<SubjectGroupDAO> subjectGroupDAOs = tFContext.SubjectGroup;
            subjectGroupDAOs = DynamicFilter(subjectGroupDAOs, subjectGroupFilter);
            subjectGroupDAOs = DynamicOrder(subjectGroupDAOs, subjectGroupFilter);
            var subjectGroups = await DynamicSelect(subjectGroupDAOs);
            return subjectGroups;
        }

        public async Task<bool> Create(SubjectGroup subjectGroup)
        {
            SubjectGroupDAO subjectGroupDAO = new SubjectGroupDAO
            {
                Id = subjectGroup.Id,
                Code = subjectGroup.Code,
                Name = subjectGroup.Name
            };

            tFContext.SubjectGroup.Add(subjectGroupDAO);
            await tFContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Delete(Guid Id)
        {
            await tFContext.University_Majors_SubjectGroup.Where(t => t.SubjectGroupId.Equals(Id)).DeleteFromQueryAsync();
            await tFContext.SubjectGroup.Where(d => d.Id.Equals(Id)).DeleteFromQueryAsync();
            return true;
        }

        public async Task<SubjectGroup> Get(Guid Id)
        {
            SubjectGroup subjectGroup = await tFContext.SubjectGroup.Where(d => d.Id.Equals(Id)).Select(d => new SubjectGroup
            {
                Id = d.Id,
                Code = d.Code,
                Name = d.Name
            }).FirstOrDefaultAsync();

            return subjectGroup;
        }

        public async Task<bool> Update(SubjectGroup subjectGroup)
        {
            await tFContext.SubjectGroup.Where(t => t.Id.Equals(subjectGroup.Id)).UpdateFromQueryAsync(t => new SubjectGroupDAO
            {
                Code = subjectGroup.Code,
                Name = subjectGroup.Name
            });

            return true;
        }
    }
}
