using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TwelveFinal.Entities
{
    public class SubjectGroup : DataEntity
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
    }

    public class SubjectGroupFilter : FilterEntity
    {
        public GuidFilter Id { get; set; }
        public StringFilter Code { get; set; }
        public StringFilter Name { get; set; }

        public SubjectGroupOrder OrderBy { get; set; }
        public SubjectGroupFilter() : base()
        {

        }
    }

    public enum SubjectGroupOrder
    {
        CX,
        Code,
        Name
    }
}
