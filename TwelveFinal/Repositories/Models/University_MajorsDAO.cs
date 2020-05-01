using System;
using System.Collections.Generic;

namespace TwelveFinal.Repositories.Models
{
    public partial class University_MajorsDAO
    {
        public University_MajorsDAO()
        {
            University_Majors_SubjectGroups = new HashSet<University_Majors_SubjectGroupDAO>();
        }

        public long CX { get; set; }
        public Guid UniversityId { get; set; }
        public Guid MajorsId { get; set; }
        public Guid Id { get; set; }
        public string Year { get; set; }

        public virtual MajorsDAO Majors { get; set; }
        public virtual UniversityDAO University { get; set; }
        public virtual ICollection<University_Majors_SubjectGroupDAO> University_Majors_SubjectGroups { get; set; }
    }
}
