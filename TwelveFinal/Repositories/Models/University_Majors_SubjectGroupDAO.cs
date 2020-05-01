using System;
using System.Collections.Generic;

namespace TwelveFinal.Repositories.Models
{
    public partial class University_Majors_SubjectGroupDAO
    {
        public Guid Id { get; set; }
        public long CX { get; set; }
        public Guid University_MajorsId { get; set; }
        public double? Benchmark { get; set; }
        public int? Quantity { get; set; }
        public string Note { get; set; }
        public Guid SubjectGroupId { get; set; }

        public virtual SubjectGroupDAO SubjectGroup { get; set; }
        public virtual University_MajorsDAO University_Majors { get; set; }
    }
}
