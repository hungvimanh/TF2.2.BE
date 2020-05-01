using System;
using System.Collections.Generic;

namespace TwelveFinal.Repositories.Models
{
    public partial class AspirationDAO
    {
        public Guid Id { get; set; }
        public long CX { get; set; }
        public int Sequence { get; set; }
        public Guid UniversityId { get; set; }
        public Guid MajorsId { get; set; }
        public Guid SubjectGroupId { get; set; }
        public Guid FormId { get; set; }

        public virtual FormDAO Form { get; set; }
        public virtual MajorsDAO Majors { get; set; }
        public virtual SubjectGroupDAO SubjectGroup { get; set; }
        public virtual UniversityDAO University { get; set; }
    }
}
