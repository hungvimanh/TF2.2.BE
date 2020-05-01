using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TwelveFinal.Entities;
using TwelveFinal.Repositories;

namespace TwelveFinal.Services.MMajors
{
    public interface IMajorsValidator : IServiceScoped
    {
        Task<bool> Create(Majors majors);
        Task<bool> Update(Majors majors);
        Task<bool> Delete(Majors majors);
    }
    public class MajorsValidator : IMajorsValidator
    {
        private IUOW UOW;
        public enum ErrorCode
        {
            NotExisted,
            Duplicate
        }

        public MajorsValidator(IUOW _UOW)
        {
            UOW = _UOW;
        }

        public async Task<bool> Create(Majors majors)
        {
            bool IsValid = true;
            IsValid &= await CodeValidate(majors);
            return IsValid;
        }

        public async Task<bool> Delete(Majors majors)
        {
            bool IsValid = true;
            IsValid &= await IsExisted(majors);
            return IsValid;
        }

        public async Task<bool> Update(Majors majors)
        {
            bool IsValid = true;
            IsValid &= await IsExisted(majors);
            IsValid &= await CodeValidate(majors);
            return IsValid;
        }

        private async Task<bool> IsExisted(Majors majors)
        {
            //Kiểm tra sự tồn tại trong DB
            if (await UOW.MajorsRepository.Get(majors.Id) == null)
            {
                majors.AddError(nameof(MajorsValidator), nameof(majors.Name), ErrorCode.NotExisted);
            }
            return majors.IsValidated;
        }

        private async Task<bool> CodeValidate(Majors majors)
        {
            //Kiểm tra sự trùng lặp Code
            MajorsFilter filter = new MajorsFilter
            {
                Id = new GuidFilter { NotEqual = majors.Id },
                Code = new StringFilter { Equal = majors.Code }
            };

            var count = await UOW.MajorsRepository.Count(filter);
            if (count > 0)
            {
                majors.AddError(nameof(MajorsValidator), nameof(majors.Code), ErrorCode.Duplicate);
            }
            return majors.IsValidated;
        }
    }
}
