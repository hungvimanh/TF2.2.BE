using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using TwelveFinal.Controller;
using TwelveFinal.Controller.form;
using TwelveFinal.Controller.majors;
using TwelveFinal.Repositories;

namespace TwelveFinal.Common
{
    public class PermissionRequirement : IAuthorizationRequirement
    {
        public PermissionRequirement()
        {
        }
    }
    public class PermissionHandler : AuthorizationHandler<PermissionRequirement>
    {
        private IUOW UOW;
        private ICurrentContext CurrentContext;
        // CurrentContext để lưu thông tin trạng thái của 1 phiên đăng nhập
        // thông tin về permission bảo gồm 2 phần: 1 phần trong jwt, 1 phần do hệ thống kiểm tra và lấy ra
        public PermissionHandler(ICurrentContext CurrentContext, IUOW UOW)
        {
            this.CurrentContext = CurrentContext;
            this.UOW = UOW;
        }
        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionRequirement requirement)
        {

            var types = context.User.Claims.Select(c => c.Type).ToList();
            // kiểm tra có thông tin user trong jwt ko
            // ClaimTypes.NameIdentifier == unique_name
            // c.Type là key của đoạn json
            if (!context.User.HasClaim(c => c.Type == ClaimTypes.NameIdentifier))
            {
                context.Fail();
                return;
            }
            Guid UserId = Guid.TryParse(context.User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier).Value, out Guid u) ? u : Guid.Empty;
            Guid StudentId = Guid.TryParse(context.User.FindFirst(c => c.Type == "studentId")?.Value, out Guid e) ? e : Guid.Empty;
            bool IsAdmin = bool.TryParse(context.User.FindFirst(c => c.Type == "isAdmin").Value, out bool b) ? b : false;
            // lấy ra mvcContext, phục vụ cho việc lấy ra url path
            var mvcContext = context.Resource as AuthorizationFilterContext;

            var HttpContext = mvcContext.HttpContext;
            string url = HttpContext.Request.Path.Value;

            CurrentContext.UserId = UserId;
            CurrentContext.StudentId = StudentId;
            CurrentContext.IsAdmin = IsAdmin;

            //Phân quyền
            switch (IsAdmin)
            {
                case true:
                    if (url.StartsWith("/" + StudentRoute.Default))
                    {
                        context.Fail();
                    }
                    break;
                case false:
                    if (url.StartsWith("/" + AdminRoute.Default))
                    {
                        context.Fail();
                    }
                    break;
                default:
                    break;
            }
            //if (!IsAdmin)
            //{
                
            //}

            //if (IsAdmin)
            //{
                
            //}

            context.Succeed(requirement);
        }
    }
}
