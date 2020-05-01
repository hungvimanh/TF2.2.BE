using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Linq;
using System.Threading.Tasks;

namespace TwelveFinal.Entities
{
    public class Majors : DataEntity        //Ngành học
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
    }

    public class MajorsFilter : FilterEntity
    {
        public GuidFilter Id { get; set; }
        public StringFilter Code { get; set; }
        public StringFilter Name { get; set; }
        public MajorsOrder OrderBy { get; set; }
        public MajorsFilter() : base()
        {

        }
    }

    [JsonConverter(typeof(StringEnumConverter))]
    public enum MajorsOrder
    {
        CX,
        Code,
        Name
    }
}
