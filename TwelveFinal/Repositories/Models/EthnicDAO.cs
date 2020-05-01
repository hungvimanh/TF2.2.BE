using System;
using System.Collections.Generic;

namespace TwelveFinal.Repositories.Models
{
    public partial class EthnicDAO
    {
        public EthnicDAO()
        {
            Students = new HashSet<StudentDAO>();
        }

        public Guid Id { get; set; }
        public long CX { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }

        public virtual ICollection<StudentDAO> Students { get; set; }
    }
}
