using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TwelveFinal.Entities;

namespace TwelveFinal.Controller.DTO
{
    public class StudentDTO : DataDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public bool Gender { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string Identify { get; set; }
        public DateTime Dob { get; set; }
        public Guid? EthnicId { get; set; }
        public string EthnicCode { get; set; }
        public string EthnicName { get; set; }
        public Guid? HighSchoolId { get; set; }
        public string HighSchoolCode { get; set; }
        public string HighSchoolName { get; set; }
        public string PlaceOfBirth { get; set; }
        public Guid? TownId { get; set; }
        public string TownCode { get; set; }
        public string TownName { get; set; }
        public Guid? DistrictId { get; set; }
        public string DistrictCode { get; set; }
        public string DistrictName { get; set; }
        public Guid? ProvinceId { get; set; }
        public string ProvinceCode { get; set; }
        public string ProvinceName { get; set; }
        public string Image { get; set; }
        public int Status { get; set; }
    }

    public class StudentFilterDTO : FilterDTO
    {
        public Guid? Id { get; set; }
        public string Identify { get; set; }
        public string Name { get; set; }
        public bool? Gender { get; set; }
        public DateTime? Dob { get; set; }
        public Guid? ProvinceId { get; set; } 
        public Guid? HighSchoolId { get; set; }
        public int? Status { get; set; }
        public StudentOrder OrderBy { get; set; }
        public StudentFilterDTO() : base()
        {

        }
    }
}
