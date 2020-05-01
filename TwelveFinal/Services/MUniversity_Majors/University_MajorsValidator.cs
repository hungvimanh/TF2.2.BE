using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TwelveFinal.Entities;
using TwelveFinal.Repositories;

namespace TwelveFinal.Services.MUniversity_Majors_Majors
{
    public interface IUniversity_MajorsValidator : IServiceScoped
    {
        Task<bool> Create(University_Majors university_Majors);
        Task<bool> Update(University_Majors university_Majors);
        Task<bool> Delete(University_Majors university_Majors);
    }
    public class University_MajorsValidator : IUniversity_MajorsValidator
    {
        private IUOW UOW;
        public enum ErrorCode
        {
            NotExisted,
            Duplicate
        }

        public University_MajorsValidator(IUOW _UOW)
        {
            UOW = _UOW;
        }

        public async Task<bool> Create(University_Majors university_Majors)
        {
            bool IsValid = true;
            IsValid &= await CodeValidate(university_Majors);
            return IsValid;
        }

        public async Task<bool> Delete(University_Majors university_Majors)
        {
            bool IsValid = true;
            IsValid &= await IsExisted(university_Majors);
            return IsValid;
        }

        public async Task<bool> Update(University_Majors university_Majors)
        {
            bool IsValid = true;
            IsValid &= await IsExisted(university_Majors);
            return IsValid;
        }

        private async Task<bool> IsExisted(University_Majors university_Majors)
        {
            //Kiểm tra sự tồn tại trong DB
            if (await UOW.University_MajorsRepository.Get(university_Majors.Id) == null)
            {
                university_Majors.AddError(nameof(University_MajorsValidator), nameof(university_Majors.MajorsName), ErrorCode.NotExisted);
            }
            return university_Majors.IsValidated;
        }

        private async Task<bool> CodeValidate(University_Majors university_Majors)
        {
            //Kiểm tra Code nhập vào đã tồn tại trong Db chưa
            University_MajorsFilter filter = new University_MajorsFilter
            {
                MajorsId = university_Majors.MajorsId,
                UniversityId = university_Majors.UniversityId,
                Year = new StringFilter { Equal = university_Majors.Year }
            };

            var count = await UOW.University_MajorsRepository.Count(filter);
            if (count > 0)
            {
                university_Majors.AddError(nameof(University_MajorsValidator), nameof(university_Majors.MajorsName), ErrorCode.Duplicate);
            }
            return university_Majors.IsValidated;
        }
    }
}
