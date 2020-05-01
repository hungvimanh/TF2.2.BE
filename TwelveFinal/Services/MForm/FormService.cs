using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TwelveFinal.Common;
using TwelveFinal.Entities;
using TwelveFinal.Repositories;

namespace TwelveFinal.Services.MForm
{
    public interface IFormService : IServiceScoped
    {
        Task<Form> ApproveAccept(Form form);
        Task<Form> ApproveDeny(Form form);
        Task<Form> Save(Form form);
        Task<Form> Get(Guid StudentId);
        Task<Form> Delete(Form form);
    }
    public class FormService : IFormService
    {
        private readonly IUOW UOW;
        private readonly IFormValidator FormValidator;
        private readonly ICurrentContext CurrentContext;

        public FormService(IUOW UOW, IFormValidator FormValidator, ICurrentContext CurrentContext)
        {
            this.UOW = UOW;
            this.FormValidator = FormValidator;
            this.CurrentContext = CurrentContext;
        }

        public async Task<Form> ApproveAccept(Form form)
        {
            if(!await FormValidator.Approve(form))
                return form;

            try
            {
                await UOW.Begin();
                //Chuyển trạng thái từ chờ duyệt sang đã duyệt
                form.Status = 2;
                await UOW.FormRepository.Approve(form);
                await UOW.StudentRepository.UpdateStatus(new Student { Id = form.StudentId, Status = form.Status });
                await UOW.Commit();
                return await Get(form.StudentId);
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                throw new MessageException(ex);
            }
        }

        public async Task<Form> ApproveDeny(Form form)
        {
            if (!await FormValidator.Approve(form))
                return form;

            try
            {
                await UOW.Begin();
                //Chuyển trạng thái từ chờ duyệt sang đã từ chối
                form.Status = 3;
                await UOW.FormRepository.Approve(form);
                await UOW.StudentRepository.UpdateStatus(new Student { Id = form.StudentId, Status = form.Status });
                await UOW.Commit();
                return await Get(form.StudentId);
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                throw new MessageException(ex);
            }
        }

        public async Task<Form> Save(Form form)
        {
            //Nếu Form chưa tồn tại với user
            //Tạo mới form
            //Update nếu form đã tồn tại
            if (!await FormValidator.IsExisted(form))
                return await Create(form);
            return await Update(form);
        }

        private async Task<Form> Create(Form form)
        {
            form.Id = Guid.NewGuid();
            form.Aspirations.ForEach(a => a.Id = Guid.NewGuid());
            if(!await FormValidator.Save(form))
                return form;

            try
            {
                await UOW.Begin();
                //Chuyển trạng thái từ chưa đăng ký sang chờ duyệt
                form.Status = 1;
                await UOW.FormRepository.Create(form);
                await UOW.StudentRepository.UpdateStatus(new Student { Id = CurrentContext.StudentId, Status = form.Status });
                await UOW.Commit();
                return await Get(CurrentContext.StudentId);
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                throw new MessageException(ex);
            }
        }

        public async Task<Form> Get(Guid StudentId)
        {
            if (StudentId == Guid.Empty) return null;
            Form form = await UOW.FormRepository.Get(StudentId);
            return form;
        }

        private async Task<Form> Update(Form form)
        {
            form.Aspirations.ForEach(a => a.Id = Guid.NewGuid());
            if (!await FormValidator.Save(form))
                return form;

            try
            {
                await UOW.Begin();
                //Chuyển trạng thái về chờ duyệt
                //Học sinh có thể cập nhật phiếu đăng ký dự thi kể cả khi phiếu đã được duyệt
                //Khi đó trạng thái sẽ chuyển từ đã duyệt về chờ duyệt
                form.Status = 1;
                await UOW.FormRepository.Update(form);
                await UOW.StudentRepository.UpdateStatus(new Student { Id = CurrentContext.StudentId, Status = 1 });
                await UOW.Commit();
                return await Get(CurrentContext.StudentId);
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                throw new MessageException(ex);
            }
        }

        public async Task<Form> Delete(Form form)
        {
            if (!await FormValidator.Delete(form))
                return form;

            try
            {
                await UOW.Begin();
                await UOW.FormRepository.Delete(form.Id);
                //Chuyển trạng thái về chưa đăng ký
                await UOW.StudentRepository.UpdateStatus(new Student { Id = CurrentContext.StudentId, Status = 0 });
                await UOW.Commit();
                return await Get(CurrentContext.StudentId);
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                throw new MessageException(ex);
            }
        }
    }
}
