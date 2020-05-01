using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TwelveFinal.Entities
{
    public class Aspiration : DataEntity
    {
        public Guid Id { get; set; }
        public Guid UniversityId { get; set; }
        public string UniversityCode { get; set; }
        public string UniversityName { get; set; }
        public string UniversityAddress { get; set; }
        public Guid MajorsId { get; set; }
        public string MajorsCode { get; set; }
        public string MajorsName { get; set; }
        public Guid SubjectGroupId { get; set; }
        public string SubjectGroupCode { get; set; }
        public string SubjectGroupName { get; set; }
        public Guid FormId { get; set; }
        public int Sequence { get; set; }
    }
}
