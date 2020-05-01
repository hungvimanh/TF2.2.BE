using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TwelveFinal.Controller.DTO
{
    public class FormDTO : DataDTO
    {
        public Guid Id { get; set; }
        public Guid StudentId { get; set; }
        public string Name { get; set; }
        public bool Gender { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string Identify { get; set; }
        public DateTime Dob { get; set; }
        public string Image { get; set; }
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

        public bool? Graduated { get; set; }
        public Guid ClusterContestId { get; set; }
        public string ClusterContestCode { get; set; }
        public string ClusterContestName { get; set; }
        public Guid RegisterPlaceOfExamId { get; set; }
        public string RegisterPlaceOfExamCode { get; set; }
        public string RegisterPlaceOfExamName { get; set; }
        public bool? Maths { get; set; }
        public bool? Literature { get; set; }
        public string Languages { get; set; }
        public bool? NaturalSciences { get; set; }
        public bool? SocialSciences { get; set; }
        public bool? Physics { get; set; }
        public bool? Chemistry { get; set; }
        public bool? Biology { get; set; }
        public bool? History { get; set; }
        public bool? Geography { get; set; }
        public bool? CivicEducation { get; set; }

        public string ExceptLanguages { get; set; }
        public int? Mark { get; set; }
        public int? ReserveMaths { get; set; }
        public int? ReservePhysics { get; set; }
        public int? ReserveChemistry { get; set; }
        public int? ReserveLiterature { get; set; }
        public int? ReserveHistory { get; set; }
        public int? ReserveGeography { get; set; }
        public int? ReserveBiology { get; set; }
        public int? ReserveCivicEducation { get; set; }
        public int? ReserveLanguages { get; set; }

        public string PriorityType { get; set; }
        public string Area { get; set; }
        public int Status { get; set; }

        public List<AspirationDTO> Aspirations { get; set; }
    }
}
