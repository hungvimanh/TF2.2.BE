using System;
using System.Collections.Generic;

namespace TwelveFinal.Repositories.Models
{
    public partial class MajorsDAO
    {
        public MajorsDAO()
        {
            Aspirations = new HashSet<AspirationDAO>();
            University_Majors = new HashSet<University_MajorsDAO>();
        }

        public Guid Id { get; set; }
        public long CX { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }

        public virtual ICollection<AspirationDAO> Aspirations { get; set; }
        public virtual ICollection<University_MajorsDAO> University_Majors { get; set; }
    }
}
