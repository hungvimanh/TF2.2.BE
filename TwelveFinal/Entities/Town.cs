using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Linq;
using System.Threading.Tasks;

namespace TwelveFinal.Entities
{
    public class Town : DataEntity
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public Guid DistrictId { get; set; }
        public string DistrictCode { get; set; }
        public string DistrictName { get; set; }
    }

    public class TownFilter : FilterEntity
    {
        public GuidFilter Id { get; set; }
        public StringFilter Code { get; set; }
        public StringFilter Name { get; set; }
        public Guid DistrictId { get; set; }
        public StringFilter DistrictCode { get; set; }
        public StringFilter DistrictName { get; set; }
        public TownOrder OrderBy { get; set; }
        public TownFilter() : base()
        {
        }
    }
    [JsonConverter(typeof(StringEnumConverter))]

    public enum TownOrder
    {
        CX,
        Code,
        Name
    }
}
