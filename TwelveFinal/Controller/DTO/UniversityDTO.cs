using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TwelveFinal.Entities;

namespace TwelveFinal.Controller.DTO
{
    public class UniversityDTO : DataDTO
    {
        public Guid? Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Website { get; set; }
        public List<University_Majors_SubjectGroupDTO> University_Majors_SubjectGroups { get; set; } 
    }

    public class UniversityFilterDTO : FilterDTO
    {
        public Guid? Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Year { get; set; }
        public UniversityFilterDTO() : base()
        {

        }
    }
}
