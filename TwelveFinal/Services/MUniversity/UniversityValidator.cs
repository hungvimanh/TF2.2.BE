using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TwelveFinal.Entities;
using TwelveFinal.Repositories;

namespace TwelveFinal.Services.MUniversity
{
    public interface IUniversityValidator : IServiceScoped
    {
        Task<bool> Create(University University);
        Task<bool> Update(University University);
        Task<bool> Delete(University University);
    }
    public class UniversityValidator : IUniversityValidator
    {
        private IUOW UOW;
        public enum ErrorCode
        {
            NotExisted,
            Duplicate
        }

        public UniversityValidator(IUOW _UOW)
        {
            UOW = _UOW;
        }

        public async Task<bool> Create(University university)
        {
            bool IsValid = true;
            IsValid &= await CodeValidate(university);
            return IsValid;
        }

        public async Task<bool> Delete(University university)
        {
            bool IsValid = true;
            IsValid &= await IsExisted(university);
            return IsValid;
        }

        public async Task<bool> Update(University university)
        {
            bool IsValid = true;
            IsValid &= await IsExisted(university);
            IsValid &= await CodeValidate(university);
            return IsValid;
        }

        private async Task<bool> IsExisted(University university)
        {
            //Kiểm tra sự tồn tại của Trường CĐ-ĐH
            if (await UOW.UniversityRepository.Get(university.Id) == null)
            {
                university.AddError(nameof(UniversityValidator), nameof(university.Name), ErrorCode.NotExisted);
            }
            return university.IsValidated;
        }

        private async Task<bool> CodeValidate(University university)
        {
            //Kiểm tra Code nhập vào có tồn tại trong Db
            UniversityFilter filter = new UniversityFilter
            {
                Id = new GuidFilter { NotEqual = university.Id },
                Code = new StringFilter { Equal = university.Code }
            };

            var count = await UOW.UniversityRepository.Count(filter);
            if (count > 0)
            {
                university.AddError(nameof(UniversityValidator), nameof(university.Code), ErrorCode.Duplicate);
            }
            return university.IsValidated;
        }
    }
}
