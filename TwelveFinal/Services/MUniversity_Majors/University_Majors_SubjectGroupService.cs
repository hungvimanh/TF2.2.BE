using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TwelveFinal.Entities;
using TwelveFinal.Repositories;

namespace TwelveFinal.Services.MUniversity_Majors
{
    public interface IUniversity_Majors_SubjectGroupService : IServiceScoped
    {
        Task<University_Majors_SubjectGroup> Get(Guid Id);
        Task<List<University_Majors_SubjectGroup>> List(University_Majors_SubjectGroupFilter university_Majors_SubjectGroupFilter);
        Task<University_Majors_SubjectGroup> Create(University_Majors_SubjectGroup university_Majors_SubjectGroup);
        Task<University_Majors_SubjectGroup> Delete(University_Majors_SubjectGroup university_Majors_SubjectGroup);
    }
    public class University_Majors_SubjectGroupService : IUniversity_Majors_SubjectGroupService
    {
        private readonly IUOW UOW;
        private readonly IUniversity_Majors_SubjectGroupValidator University_Majors_SubjectGroupValidator;

        public University_Majors_SubjectGroupService(IUOW UOW, IUniversity_Majors_SubjectGroupValidator University_Majors_SubjectGroupValidator)
        {
            this.UOW = UOW;
            this.University_Majors_SubjectGroupValidator = University_Majors_SubjectGroupValidator;
        }

        public async Task<University_Majors_SubjectGroup> Create(University_Majors_SubjectGroup university_Majors_SubjectGroup)
        {
            university_Majors_SubjectGroup.Id = Guid.NewGuid();
            if (!await University_Majors_SubjectGroupValidator.Create(university_Majors_SubjectGroup))
                return university_Majors_SubjectGroup;
            try
            {
                await UOW.Begin();
                await UOW.University_Majors_SubjectGroupRepository.Create(university_Majors_SubjectGroup);
                await UOW.Commit();
                return await Get(university_Majors_SubjectGroup.Id);
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                throw new MessageException(ex);
            }
        }

        public async Task<University_Majors_SubjectGroup> Delete(University_Majors_SubjectGroup university_Majors_SubjectGroup)
        {
            if (!await University_Majors_SubjectGroupValidator.Delete(university_Majors_SubjectGroup))
                return university_Majors_SubjectGroup;
            try
            {
                await UOW.Begin();
                await UOW.University_Majors_SubjectGroupRepository.Delete(university_Majors_SubjectGroup.Id);
                await UOW.Commit();
                return university_Majors_SubjectGroup;
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                throw new MessageException(ex);
            }
        }

        public async Task<University_Majors_SubjectGroup> Get(Guid Id)
        {
            if (Id == Guid.Empty) return null;
            University_Majors_SubjectGroup u_M_Y_S = await UOW.University_Majors_SubjectGroupRepository.Get(Id);
            return u_M_Y_S;
        }

        public async Task<List<University_Majors_SubjectGroup>> List(University_Majors_SubjectGroupFilter university_Majors_SubjectGroupFilter)
        {
            return await UOW.University_Majors_SubjectGroupRepository.List(university_Majors_SubjectGroupFilter);
        }
    }
}
