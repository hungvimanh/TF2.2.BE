using System;
using System.Collections.Generic;

namespace TwelveFinal.Repositories.Models
{
    public partial class ProvinceDAO
    {
        public ProvinceDAO()
        {
            Districts = new HashSet<DistrictDAO>();
            Forms = new HashSet<FormDAO>();
            HighSchools = new HashSet<HighSchoolDAO>();
        }

        public Guid Id { get; set; }
        public long CX { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }

        public virtual ICollection<DistrictDAO> Districts { get; set; }
        public virtual ICollection<FormDAO> Forms { get; set; }
        public virtual ICollection<HighSchoolDAO> HighSchools { get; set; }
    }
}
