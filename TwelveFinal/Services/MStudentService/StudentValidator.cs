using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TwelveFinal.Common;
using TwelveFinal.Entities;
using TwelveFinal.Repositories;

namespace TwelveFinal.Services.MStudentService
{
    public interface IStudentValidator : IServiceScoped
    {
        Task<bool> Create(Student student);
        Task<bool> BulkInsert(List<Student> students);
        Task<bool> Update(Student student);
        Task<bool> Delete(Student student);
        Task<bool> ViewMark(Student student);
    }
    public class StudentValidator : IStudentValidator
    {
        private readonly IUOW UOW;
        public StudentValidator(IUOW UOW)
        {
            this.UOW = UOW;
        }

        public enum ErrorCode
        {
            NotExisted,
            Duplicate,
            Invalid
        }
        public Task<bool> BulkInsert(List<Student> students)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> Create(Student student)
        {
            bool IsValid = true;
            IsValid &= await ValidateIdentify(student);
            IsValid &= await ValidateEmail(student);
            return IsValid;
        }

        public async Task<bool> Delete(Student student)
        {
            bool IsValid = true;
            IsValid &= await IsExisted(student);
            return IsValid;
        }

        public async Task<bool> Update(Student student)
        {
            bool IsValid = true;
            IsValid &= await IsExisted(student);
            IsValid &= await GraduationValidate(student);
            return IsValid;
        }

        public async Task<bool> ViewMark(Student student)
        {
            bool IsValid = true;
            IsValid &= await ValidateViewMark(student);
            return IsValid;
        }

        private async Task<bool> IsExisted(Student student)
        {
            //Validate Existed
            if(await UOW.StudentRepository.Get(student.Id) == null)
            {
                student.AddError(nameof(StudentValidator), nameof(student.Identify), ErrorCode.NotExisted);
            }

            return student.IsValidated;
        }

        private async Task<bool> ValidateEmail(Student student)
        {
            //Validate Format cuả Email
            if (!Utils.IsValidEmail(student.Email))
            {
                student.AddError(nameof(StudentValidator), nameof(student.Email), ErrorCode.Invalid);
            }

            if(await UOW.StudentRepository.Count(new StudentFilter { Id = new GuidFilter { NotEqual = student.Id }, Email = new StringFilter { Equal = student.Email} }) != 0)
            {
                student.AddError(nameof(StudentValidator), nameof(student.Email), ErrorCode.Duplicate);
            }
            return student.IsValidated;
        }

        private async Task<bool> ValidateIdentify(Student student)
        {
            //Validate số CMND Unique
            
            StudentFilter filter = new StudentFilter
            {
                Id = new GuidFilter { NotEqual = student.Id },
                Identify = new StringFilter { Equal = student.Identify }
            };

            //Đếm số record có Identify qua filter
            if (await UOW.StudentRepository.Count(filter) != 0)
            {
                student.AddError(nameof(StudentValidator), nameof(student.Identify), ErrorCode.Duplicate);
            }
            return student.IsValidated;
        }

        private async Task<bool> ValidateViewMark(Student student)
        {
            if(!student.Maths.HasValue || !student.Literature.HasValue || !student.Languages.HasValue)
            {
                student.AddError(nameof(StudentValidator), "Mark", ErrorCode.Invalid);
            }
            return student.IsValidated;
        }

        private async Task<bool> GraduationValidate(Student student)
        {
            //Kiểm tra số điểm các môn nếu có 
            if (student.Maths != null && !(student.Maths >= 0 && student.Maths <= 10))
            {
                student.AddError(nameof(StudentValidator), nameof(student.Maths), ErrorCode.Invalid);
            }

            if (student.Physics != null && !(student.Physics >= 0 && student.Physics <= 10))
            {
                student.AddError(nameof(StudentValidator), nameof(student.Physics), ErrorCode.Invalid);
            }

            if (student.Chemistry != null && !(student.Chemistry >= 0 && student.Chemistry <= 10))
            {
                student.AddError(nameof(StudentValidator), nameof(student.Chemistry), ErrorCode.Invalid);
            }

            if (student.Literature != null && !(student.Literature >= 0 && student.Literature <= 10))
            {
                student.AddError(nameof(StudentValidator), nameof(student.Literature), ErrorCode.Invalid);
            }

            if (student.History != null && !(student.History >= 0 && student.History <= 10))
            {
                student.AddError(nameof(StudentValidator), nameof(student.History), ErrorCode.Invalid);
            }

            if (student.Geography != null && !(student.Geography >= 0 && student.Geography <= 10))
            {
                student.AddError(nameof(StudentValidator), nameof(student.Geography), ErrorCode.Invalid);
            }

            if (student.Biology != null && !(student.Biology >= 0 && student.Biology <= 10))
            {
                student.AddError(nameof(StudentValidator), nameof(student.Biology), ErrorCode.Invalid);
            }

            if (student.CivicEducation != null && !(student.CivicEducation >= 0 && student.CivicEducation <= 10))
            {
                student.AddError(nameof(StudentValidator), nameof(student.CivicEducation), ErrorCode.Invalid);
            }

            if (student.Languages != null && !(student.Languages >= 0 && student.Languages <= 10))
            {
                student.AddError(nameof(StudentValidator), nameof(student.Languages), ErrorCode.Invalid);
            }

            return student.IsValidated;
        }

    }
}
