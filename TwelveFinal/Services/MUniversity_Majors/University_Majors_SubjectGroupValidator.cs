using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TwelveFinal.Entities;
using TwelveFinal.Repositories;

namespace TwelveFinal.Services.MUniversity_Majors
{
    public interface IUniversity_Majors_SubjectGroupValidator : IServiceScoped
    {
        Task<bool> Create(University_Majors_SubjectGroup university_Majors_SubjectGroup);
        Task<bool> Delete(University_Majors_SubjectGroup university_Majors_SubjectGroup);
    }
    public class University_Majors_SubjectGroupValidator : IUniversity_Majors_SubjectGroupValidator
    {
        private IUOW UOW;
        public University_Majors_SubjectGroupValidator(IUOW UOW)
        {
            this.UOW = UOW;
        }

        public enum ErrorCode
        {
            Duplicate,
            NotExisted
        }

        public async Task<bool> Create(University_Majors_SubjectGroup university_Majors_SubjectGroup)
        {
            bool IsValid = true;
            IsValid &= await DuplicateValidation(university_Majors_SubjectGroup);
            return IsValid;
        }

        public async Task<bool> Delete(University_Majors_SubjectGroup university_Majors_SubjectGroup)
        {
            bool IsValid = true;
            IsValid &= await IsExisted(university_Majors_SubjectGroup);
            return IsValid;
        }

        private async Task<bool> IsExisted(University_Majors_SubjectGroup university_Majors_SubjectGroup)
        {
            if(await UOW.University_Majors_SubjectGroupRepository.Get(university_Majors_SubjectGroup.Id) == null)
            {
                university_Majors_SubjectGroup.AddError(nameof(University_Majors_SubjectGroupValidator), nameof(University_Majors_SubjectGroup), ErrorCode.NotExisted);
            }
            return university_Majors_SubjectGroup.IsValidated;
        }

        private async Task<bool> DuplicateValidation(University_Majors_SubjectGroup university_Majors_SubjectGroup)
        {
            University_Majors_SubjectGroupFilter filter = new University_Majors_SubjectGroupFilter
            {
                SubjectGroupId = university_Majors_SubjectGroup.SubjectGroupId,
                University_MajorsId = university_Majors_SubjectGroup.University_MajorsId
            };

            if(await UOW.University_Majors_SubjectGroupRepository.Count(filter) > 0)
            {
                university_Majors_SubjectGroup.AddError(nameof(University_Majors_SubjectGroupValidator), nameof(university_Majors_SubjectGroup), ErrorCode.Duplicate);
            }

            return university_Majors_SubjectGroup.IsValidated;
        }
    }
}
