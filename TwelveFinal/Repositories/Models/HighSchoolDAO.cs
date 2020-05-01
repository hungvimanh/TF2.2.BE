using System;
using System.Collections.Generic;

namespace TwelveFinal.Repositories.Models
{
    public partial class HighSchoolDAO
    {
        public HighSchoolDAO()
        {
            Forms = new HashSet<FormDAO>();
            Students = new HashSet<StudentDAO>();
        }

        public Guid Id { get; set; }
        public long CX { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public Guid ProvinceId { get; set; }
        public string Address { get; set; }

        public virtual ProvinceDAO Province { get; set; }
        public virtual ICollection<FormDAO> Forms { get; set; }
        public virtual ICollection<StudentDAO> Students { get; set; }
    }
}
