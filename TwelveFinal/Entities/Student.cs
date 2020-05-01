using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TwelveFinal.Entities
{
    public class Student : DataEntity
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
        public byte[] Image { get; set; }
        public double? Maths { get; set; }
        public double? Literature { get; set; }
        public double? Languages { get; set; }
        public double? Physics { get; set; }
        public double? Chemistry { get; set; }
        public double? Biology { get; set; }
        public double? History { get; set; }
        public double? Geography { get; set; }
        public double? CivicEducation { get; set; }

        public int Status { get; set; }
        public bool? Graduated { get; set; }
        public double? GraduationMark { get; set; }
        //public Student() : base()
        //{
        //    GraduationMark = 0;
        //}
    }

    public class StudentFilter : FilterEntity
    {
        public GuidFilter Id { get; set; }
        public StringFilter Name { get; set; }
        public GuidFilter ProvinceId { get; set; }
        public StringFilter Identify { get; set; }
        public StringFilter Email { get; set; }
        public GuidFilter HighSchoolId { get; set; }
        public bool? Gender { get; set; }
        public DateTimeFilter Dob { get; set; }
        public int? Status { get; set; }
        public StudentOrder OrderBy { get; set; }
        public StudentFilter() : base()
        {

        }
    }
    //[JsonConverter(typeof(StringEnumConverter))]
    public enum StudentOrder
    {
        CX,
        Name,
        Identify,
    }
}
