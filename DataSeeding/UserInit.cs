using System;
using System.Collections.Generic;
using System.Text;
using TwelveFinal.Repositories.Models;

namespace DataSeeding
{
    public class UserInit : CommonInit
    {
        public UserInit(TFContext tFContext) : base(tFContext)
        {

        }

        public void Init()
        {
            List<UserDAO> userDAOs = new List<UserDAO>();
            UserDAO Admin = new UserDAO
            {
                Id = Guid.NewGuid(),
                Username = "admin",
                Password = "admin",
                IsAdmin = true
            };

            userDAOs.Add(Admin);
            for (int i = 1; i < 11; i++)
            {
                UserDAO user = new UserDAO
                {
                    Id = Guid.NewGuid(),
                    Username = "hocsinh" + i.ToString(),
                    Password = "hocsinh" + i.ToString(),
                    IsAdmin = false
                };
                userDAOs.Add(user);
            }
            userDAOs[1].StudentId = CreateGuid("Hùng Vi Mạnh");
            userDAOs[2].StudentId = CreateGuid("Vi Mạnh Hùng");
            userDAOs[3].StudentId = CreateGuid("Khang ĐM");
            userDAOs[4].StudentId = CreateGuid("Linh ML");

            DbContext.User.AddRange(userDAOs);
        }
    }


}
