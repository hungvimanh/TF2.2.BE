using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TwelveFinal.Controller.DTO
{
    public class ForgotPasswordDTO : DataDTO
    {
        public string Username { get; set; }
        public string Email { get; set; }
    }
}
