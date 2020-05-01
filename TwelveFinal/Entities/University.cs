using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Linq;
using System.Threading.Tasks;

namespace TwelveFinal.Entities
{
    public class University : DataEntity
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Website { get; set; }
        public List<University_Majors> University_Majors { get; set; }
    }

    public class UniversityFilter : FilterEntity
    {
        public GuidFilter Id { get; set; }
        public StringFilter Code { get; set; }
        public StringFilter Name { get; set; }
        public UniversityOrder OrderBy { get; set; }
        public UniversityFilter() : base()
        {

        }
    }

    [JsonConverter(typeof(StringEnumConverter))]
    public enum UniversityOrder
    {
        CX,
        Code,
        Name
    }
}
