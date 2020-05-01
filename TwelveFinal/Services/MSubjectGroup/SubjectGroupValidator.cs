using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TwelveFinal.Entities;
using TwelveFinal.Repositories;

namespace TwelveFinal.Services.MSubjectGroup
{
    public interface ISubjectGroupValidator : IServiceScoped
    {
        Task<bool> Create(SubjectGroup subjectGroup);
        Task<bool> Update(SubjectGroup subjectGroup);
        Task<bool> Delete(SubjectGroup subjectGroup);
    }
    public class SubjectGroupValidator : ISubjectGroupValidator
    {
        private IUOW UOW;
        public enum ErrorCode
        {
            NotExisted,
            Duplicate
        }

        public SubjectGroupValidator(IUOW _UOW)
        {
            UOW = _UOW;
        }

        public async Task<bool> Create(SubjectGroup subjectGroup)
        {
            bool IsValid = true;
            IsValid &= await CodeValidate(subjectGroup);
            return IsValid;
        }

        public async Task<bool> Delete(SubjectGroup subjectGroup)
        {
            bool IsValid = true;
            IsValid &= await IsExisted(subjectGroup);
            return IsValid;
        }

        public async Task<bool> Update(SubjectGroup subjectGroup)
        {
            bool IsValid = true;
            IsValid &= await IsExisted(subjectGroup);
            IsValid &= await CodeValidate(subjectGroup);
            return IsValid;
        }

        private async Task<bool> IsExisted(SubjectGroup subjectGroup)
        {
            //Kiểm tra Khối xét tuyển đã tồn tại chưa?
            if (await UOW.SubjectGroupRepository.Get(subjectGroup.Id) == null)
            {
                subjectGroup.AddError(nameof(SubjectGroupValidator), nameof(subjectGroup.Name), ErrorCode.NotExisted);
            }
            return subjectGroup.IsValidated;
        }

        private async Task<bool> CodeValidate(SubjectGroup subjectGroup)
        {
            //Kiểm tra sự trùng lặp Code
            SubjectGroupFilter filter = new SubjectGroupFilter
            {
                Id = new GuidFilter { NotEqual = subjectGroup.Id },
                Code = new StringFilter { Equal = subjectGroup.Code }
            };

            var count = await UOW.SubjectGroupRepository.Count(filter);
            if (count > 0)
            {
                subjectGroup.AddError(nameof(SubjectGroupValidator), nameof(subjectGroup.Code), ErrorCode.Duplicate);
            }
            return subjectGroup.IsValidated;
        }
    }
}
