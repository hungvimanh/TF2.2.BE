using System;
using System.Collections.Generic;
using System.Text;
using TwelveFinal.Repositories.Models;

namespace DataSeeding
{
    public class StudentInit : CommonInit
    {
        public StudentInit(TFContext tFContext) : base(tFContext)
        {

        }
        public void Init()
        {
            List<StudentDAO> studentDAOs = new List<StudentDAO>();

            StudentDAO hocsinh1 = new StudentDAO
            {
                Id = CreateGuid("Hùng Vi Mạnh"),
                Name = "Hùng Vi Mạnh",
                Dob = new DateTime(1999, 06, 05),
                Gender = true,
                Identify = "091955291",
                Email = "hungvimanh.cntt@gmail.com",
                Status = 0
            };
            studentDAOs.Add(hocsinh1);
            StudentDAO hocsinh2 = new StudentDAO
            {
                Id = CreateGuid("Vi Mạnh Hùng"),
                Name = "Vi Mạnh Hùng",
                Dob = new DateTime(1999, 06, 05),
                Gender = true,
                Identify = "091955291",
                Email = "hungvimanh.cntt@gmail.com",
                Status = 0
            };
            studentDAOs.Add(hocsinh2);
            StudentDAO hocsinh3 = new StudentDAO
            {
                Id = CreateGuid("Khang ĐM"),
                Name = "Khang ĐM",
                Dob = new DateTime(1999, 08, 08),
                Gender = true,
                Identify = "456456456",
                Email = "xcvxcvx@gmail.com",
                Status = 0
            };
            studentDAOs.Add(hocsinh3);
            StudentDAO hocsinh4 = new StudentDAO
            {
                Id = CreateGuid("Linh ML"),
                Name = "Linh ML",
                Dob = new DateTime(1999, 08, 09),
                Gender = false,
                Identify = "45634536",
                Email = "sdfsdfsd@gmail.com",
                Status = 0
            };
            studentDAOs.Add(hocsinh4);

            DbContext.AddRange(studentDAOs);
        }
    }
}
