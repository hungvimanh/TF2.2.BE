using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TwelveFinal.Common;
using TwelveFinal.Entities;
using TwelveFinal.Repositories;

namespace TwelveFinal.Services.MForm
{
    public interface IFormValidator : IServiceScoped
    {
        Task<bool> Approve(Form form);
        Task<bool> Save(Form form);
        Task<bool> IsExisted(Form form);
        Task<bool> Delete(Form form);
    }
    public class FormValidator : IFormValidator
    {
        private IUOW UOW;
        private ICurrentContext CurrentContext;
        public enum ErrorCode
        {
            Duplicate,
            NotExisted,
            Invalid,
            IsApproved,
        }

        public FormValidator(IUOW _UOW, ICurrentContext CurrentContext)
        {
            UOW = _UOW;
            this.CurrentContext = CurrentContext;
        }

        public async Task<bool> Approve(Form form)
        {
            bool IsValid = true;
            IsValid &= await StatusValidation(form);
            return IsValid;
        }

        public async Task<bool> Save(Form form)
        {
            bool IsValid = true;
            IsValid &= await FormApproved(form);
            //IsValid &= await GraduationValidate(form);
            //if (form.Aspirations.Any())
            //{
            //    IsValid &= await SequenceValidate(form.Aspirations);
            //}
            return IsValid;
        }

        public async Task<bool> Delete(Form form)
        {
            bool IsValid = true;
            IsValid &= await IsExisted(form);
            if (!IsValid)
            {
                form.AddError(nameof(FormValidator), "Form", ErrorCode.NotExisted);
            }
            return IsValid;
        }

        public async Task<bool> IsExisted(Form form)
        {
            //Kiểm tra Form đã tồn tại hay chưa?
            if(await UOW.FormRepository.Get(CurrentContext.StudentId) == null)
            {
                return false;
            }
            return true;
        }

        private async Task<bool> FormApproved(Form form)
        {
            //Nếu form đã được Approve thì không thể approve lại nữa
            if(form.Status == 2)
            {
                form.AddError(nameof(FormValidator), "Form", ErrorCode.IsApproved);
            }
            return form.IsValidated;
        }

        private async Task<bool> StatusValidation(Form form)
        {
            //Validate Trạng thái 
            //0: Nếu Phiếu ĐKDT chưa được tạo
            //1: Phiếu đang ở trạng thái chờ duyệt => cho phép duyệt
            //2 || 3: Phiếu đã được duyệt, 2 là duyệt nhận, 3 là duyệt từ chối
            if (form.Status == 0 || form.Status == null)
            {
                form.AddError(nameof(FormValidator), "Form", ErrorCode.NotExisted);
            }

            if (form.Status == 2 || form.Status == 3)
            {
                form.AddError(nameof(FormValidator), "Form", ErrorCode.IsApproved);
            }

            return form.IsValidated;
        }

        #region Validate thông tin xét tốt nghiệp
        //private async Task<bool> GraduationValidate(Form form)
        //{
        //    //Kiểm tra số điểm các môn bảo lưu nếu có 
        //    if (form.ReserveMaths != null && !(form.ReserveMaths >= 0 && form.ReserveMaths <= 10))
        //    {
        //        form.AddError(nameof(FormValidator), nameof(form.ReserveMaths), ErrorCode.Invalid);
        //    }

        //    if (form.ReservePhysics != null && !(form.ReservePhysics >= 0 && form.ReservePhysics <= 10))
        //    {
        //        form.AddError(nameof(FormValidator), nameof(form.ReservePhysics), ErrorCode.Invalid);
        //    }

        //    if (form.ReserveChemistry != null && !(form.ReserveChemistry >= 0 && form.ReserveChemistry <= 10))
        //    {
        //        form.AddError(nameof(FormValidator), nameof(form.ReserveChemistry), ErrorCode.Invalid);
        //    }

        //    if (form.ReserveLiterature != null && !(form.ReserveLiterature >= 0 && form.ReserveLiterature <= 10))
        //    {
        //        form.AddError(nameof(FormValidator), nameof(form.ReserveLiterature), ErrorCode.Invalid);
        //    }

        //    if (form.ReserveHistory != null && !(form.ReserveHistory >= 0 && form.ReserveHistory <= 10))
        //    {
        //        form.AddError(nameof(FormValidator), nameof(form.ReserveHistory), ErrorCode.Invalid);
        //    }

        //    if (form.ReserveGeography != null && !(form.ReserveGeography >= 0 && form.ReserveGeography <= 10))
        //    {
        //        form.AddError(nameof(FormValidator), nameof(form.ReserveGeography), ErrorCode.Invalid);
        //    }

        //    if (form.ReserveBiology != null && !(form.ReserveBiology >= 0 && form.ReserveBiology <= 10))
        //    {
        //        form.AddError(nameof(FormValidator), nameof(form.ReserveBiology), ErrorCode.Invalid);
        //    }

        //    if (form.ReserveCivicEducation != null && !(form.ReserveCivicEducation >= 0 && form.ReserveCivicEducation <= 10))
        //    {
        //        form.AddError(nameof(FormValidator), nameof(form.ReserveCivicEducation), ErrorCode.Invalid);
        //    }

        //    if (form.ReserveLanguages != null && !(form.ReserveLanguages >= 0 && form.ReserveLanguages <= 10))
        //    {
        //        form.AddError(nameof(FormValidator), nameof(form.ReserveLanguages), ErrorCode.Invalid);
        //    }

        //    return form.IsValidated;
        //}
        #endregion

        #region Validate thông tin xét tuyển đại học
        //private async Task<bool> SequenceValidate(List<Aspiration> aspirations)
        //{
        //    //Kiểm tra thứ tự nguyện vọng hợp lệ
        //    //Sắp xếp List thứ tự nguyện vọng từ bé đến lớn
        //    aspirations = aspirations.OrderBy(a => a.Sequence).ToList();
        //    //Nguyện vọng cao nhất phải có Sequence = 1
        //    if (aspirations.First().Sequence != 1)
        //    {
        //        aspirations.First().AddError(nameof(FormValidator), "Sequence", ErrorCode.Invalid);
        //        return false;
        //    }

        //    //Sequence của các nguyện vọng sau phải hơn nguyện vọng trước 1
        //    var listSequence = aspirations.Select(a => a.Sequence).ToList();
        //    for (int i = 1; i < listSequence.Count; i++)
        //    {
        //        if (listSequence[i] != listSequence[i - 1] + 1)
        //        {
        //            aspirations[i].AddError(nameof(FormValidator), "Sequence", ErrorCode.Invalid);
        //        }
        //    }

        //    bool IsValid = true;
        //    aspirations.ForEach(e => IsValid &= e.IsValidated);
        //    return IsValid;
        //}
        #endregion
    }
}
