using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TwelveFinal.Entities;

namespace TwelveFinal.Controller.DTO
{
    public class University_Majors_SubjectGroupDTO : DataDTO
    {
        public Guid Id { get; set; }
        public Guid University_MajorsId { get; set; }
        public Guid UniversityId { get; set; }
        public string UniversityName { get; set; }
        public string UniversityCode { get; set; }
        public Guid MajorsId { get; set; }
        public string MajorsCode { get; set; }
        public string MajorsName { get; set; }
        public Guid SubjectGroupId { get; set; }
        public string SubjectGroupCode { get; set; }
        public string SubjectGroupName { get; set; }
        public string Year { get; set; }
        public double? Benchmark { get; set; }
        public int? Quantity { get; set; }
        public string Note { get; set; }
    }

    public class University_Majors_SubjectGroupFilterDTO : FilterDTO
    {
        public Guid Id { get; set; }
        public Guid? University_MajorsId { get; set; }
        public string Year { get; set; }
        public Guid? UniversityId { get; set; }
        public string UniversityName { get; set; }
        public string UniversityCode { get; set; }
        public Guid? MajorsId { get; set; }
        public string MajorsCode { get; set; }
        public string MajorsName { get; set; }
        public double? Benchmark { get; set; }
        public Guid? SubjectGroupId { get; set; }
        public string SubjectGroupCode { get; set; }
        public University_Majors_SubjectGroupOrder OrderBy { get; set; }
        public University_Majors_SubjectGroupFilterDTO(): base()
        {

        }
    }
}
