using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TwelveFinal.Controller.DTO
{
    public class RegisterDTO : DataDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public bool Gender { get; set; }
        public DateTime Dob { get; set; }
        public Guid? EthnicId { get; set; }
        public string EthnicName { get; set; }
        public string PlaceOfBirth { get; set; }
        public Guid? HighSchoolId { get; set; }
        public string HighSchoolName { get; set; }
        public string Identify { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
    }
}
