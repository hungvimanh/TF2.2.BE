using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Linq;
using System.Threading.Tasks;

namespace TwelveFinal.Entities
{
    public class HighSchool : DataEntity
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public Guid ProvinceId { get; set; }
        public string ProvinceCode { get; set; }
        public string ProvinceName { get; set; }
    }

    public class HighSchoolFilter : FilterEntity
    {
        public GuidFilter Id { get; set; }
        public StringFilter Code { get; set; }
        public StringFilter Name { get; set; }
        public Guid ProvinceId { get; set; }

        public HighSchoolOrder OrderBy { get; set; }
        public HighSchoolFilter() : base()
        {

        }
    }

    [JsonConverter(typeof(StringEnumConverter))]

    public enum HighSchoolOrder
    {
        CX,
        Code,
        Name
    }
}
