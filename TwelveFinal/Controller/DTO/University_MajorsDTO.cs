using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TwelveFinal.Entities;

namespace TwelveFinal.Controller.DTO
{
    public class University_MajorsDTO : DataDTO
    {
        public Guid Id { get; set; }
        public Guid UniversityId { get; set; }
        public string UniversityCode { get; set; }
        public string UniversityName { get; set; }
        public string UniversityAddress { get; set; }
        public Guid MajorsId { get; set; }
        public string MajorsCode { get; set; }
        public string MajorsName { get; set; }
        public string Year { get; set; }
    }

    public class University_MajorsFilterDTO : FilterDTO
    {
        public Guid Id { get; set; }
        public Guid? UniversityId { get; set; }
        public string UniversityCode { get; set; }
        public string UniversityName { get; set; }
        public Guid? MajorsId { get; set; }
        public string MajorsCode { get; set; }
        public string MajorsName { get; set; }
        public string Year { get; set; }

        public University_MajorsOrder OrderBy { get; set; }
        public University_MajorsFilterDTO() : base()
        {

        }
    }
}
