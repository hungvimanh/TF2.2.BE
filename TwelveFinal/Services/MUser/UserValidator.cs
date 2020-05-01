using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TwelveFinal.Common;
using TwelveFinal.Entities;
using TwelveFinal.Repositories;

namespace TwelveFinal.Services.MUser
{
    public interface IUserValidator : IServiceScoped
    {
        Task<bool> Create(User user);
        Task<bool> Update(User user);
        Task<bool> Delete(User user);
        Task<bool> ChangePassword(User user);
    }
    public class UserValidator : IUserValidator
    {
        public enum ErrorCode
        {
            Invalid,
            Duplicate,
            NotExisted
        }
        private IUOW UOW;


        public UserValidator(IUOW UOW)
        {
            this.UOW = UOW;
        }

        public async Task<bool> Create(User user)
        {
            bool IsValid = true;
            IsValid &= await IsExisted(user);
            IsValid &= await ValidateEmail(user);
            return IsValid;
        }

        public async Task<bool> Update(User user)
        {
            bool IsValid = true;
            IsValid &= await ValidateEmail(user);
            return IsValid;
        }

        public async Task<bool> Delete(User user)
        {
            bool IsValid = true;
            IsValid &= await IsExisted(user);
            return IsValid;
        }

        public async Task<bool> ChangePassword(User user)
        {
            bool IsValid = true;
            IsValid &= await ValidatePassword(user);
            return IsValid;
        }
        private async Task<bool> IsExisted(User user)
        {
            //Kiểm tra username đã tồn tại hay chưa??
            //Nếu tồn tại thì trả ra lỗi Username đã được sử dụng
            if(await UOW.UserRepository.Get(new UserFilter
            {
                Username = user.Username
            }) == null)
            {
                user.AddError(nameof(UserValidator), nameof(user.Username), ErrorCode.Duplicate);
                return user.IsValidated;
            }
            return true;
        }

        private async Task<bool> ValidateEmail(User user)
        {
            //Validate Format cuả Email
            if (!IsValidEmail(user.Email))
            {
                user.AddError(nameof(UserValidator), nameof(user.Email), ErrorCode.Invalid);
            }

            return user.IsValidated;
        }

        private async Task<bool> ValidatePassword(User user)
        {
            //Kiểm tra tính hợp lệ của Password
            //Độ dài của Password từ 8-16 kí tự
            //chứa các chữ cái in hoa và in thường từ A - Z, các chữ số từ 0 - 9 và các kĩ tự đặc biệt
            if (string.IsNullOrEmpty(user.Password) || user.Password.Length > 50)
            {
                user.AddError(nameof(UserValidator), nameof(user.Password), ErrorCode.Invalid);
                return user.IsValidated;
            }

            var rule = new Regex(@"^(?=.{8,16}$)(?=.*?[a-z])(?=.*?[A-Z])(?=.*?[0-9]).*$");
            if (!rule.IsMatch(user.Password))
            {
                user.AddError(nameof(UserValidator), nameof(user.Password), ErrorCode.Invalid);
                return user.IsValidated;
            }
            return true;
        }

        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }
    }
}
