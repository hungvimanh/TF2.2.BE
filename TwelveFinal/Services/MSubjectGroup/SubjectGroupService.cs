using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TwelveFinal.Entities;
using TwelveFinal.Repositories;

namespace TwelveFinal.Services.MSubjectGroup
{
    public interface ISubjectGroupService : IServiceScoped
    {
        Task<SubjectGroup> Create(SubjectGroup subjectGroup);
        Task<SubjectGroup> Get(Guid Id);
        Task<List<SubjectGroup>> List(SubjectGroupFilter subjectGroupFilter);
        Task<SubjectGroup> Update(SubjectGroup subjectGroup);
        Task<SubjectGroup> Delete(SubjectGroup subjectGroup);
    }
    public class SubjectGroupService : ISubjectGroupService
    {
        private readonly IUOW UOW;
        private readonly ISubjectGroupValidator SubjectGroupValidator;

        public SubjectGroupService(
            IUOW UOW,
            ISubjectGroupValidator SubjectGroupValidator
            )
        {
            this.UOW = UOW;
            this.SubjectGroupValidator = SubjectGroupValidator;
        }

        public async Task<SubjectGroup> Create(SubjectGroup subjectGroup)
        {
            subjectGroup.Id = Guid.NewGuid();
            if (!await SubjectGroupValidator.Create(subjectGroup))
                return subjectGroup;

            try
            {
                await UOW.Begin();
                await UOW.SubjectGroupRepository.Create(subjectGroup);
                await UOW.Commit();
                return await Get(subjectGroup.Id);
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                throw new MessageException(ex);
            }
        }

        public async Task<SubjectGroup> Delete(SubjectGroup subjectGroup)
        {
            if (!await SubjectGroupValidator.Delete(subjectGroup))
                return subjectGroup;

            try
            {
                await UOW.Begin();
                await UOW.SubjectGroupRepository.Delete(subjectGroup.Id);
                await UOW.Commit();
                return subjectGroup;
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                throw new MessageException(ex);
            }
        }

        public async Task<SubjectGroup> Get(Guid Id)
        {
            if (Id == Guid.Empty) return null;
            SubjectGroup subjectGroup = await UOW.SubjectGroupRepository.Get(Id);
            return subjectGroup;
        }

        public async Task<List<SubjectGroup>> List(SubjectGroupFilter subjectGroupFilter)
        {
            return await UOW.SubjectGroupRepository.List(subjectGroupFilter);
        }

        public async Task<SubjectGroup> Update(SubjectGroup subjectGroup)
        {
            if (!await SubjectGroupValidator.Update(subjectGroup))
                return subjectGroup;

            try
            {
                await UOW.Begin();
                await UOW.SubjectGroupRepository.Update(subjectGroup);
                await UOW.Commit();
                return await Get(subjectGroup.Id);
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                throw new MessageException(ex);
            }
        }
    }
}
