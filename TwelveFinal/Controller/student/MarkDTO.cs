using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TwelveFinal.Controller.student
{
    public class MarkDTO : DataDTO
    {
        public Guid StudentId { get; set; }
        public string Identify { get; set; }
        public string Name { get; set; }
        public DateTime Dob { get; set; }
        public string Email { get; set; }
        public double? Maths { get; set; }
        public double? Literature { get; set; }
        public double? Languages { get; set; }
        public double? Physics { get; set; }
        public double? Chemistry { get; set; }
        public double? Biology { get; set; }
        public double? History { get; set; }
        public double? Geography { get; set; }
        public double? CivicEducation { get; set; }
        public bool? Graduated { get; set; }
        public double? GraduationMark { get; set; }
    }
}
