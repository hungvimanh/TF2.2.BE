using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Linq;
using System.Threading.Tasks;

namespace TwelveFinal.Entities
{
    public class Province : DataEntity      //Tỉnh/thành phố
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public List<District> Districts { get; set; }
        public List<HighSchool> HighSchools { get; set; }
    }

    public class ProvinceFilter : FilterEntity
    {
        public GuidFilter Id { get; set; }
        public StringFilter Code { get; set; }
        public StringFilter Name { get; set; }
        public ProvinceOrder OrderBy { get; set; }
        public ProvinceFilter() : base()
        {

        }
    }
    [JsonConverter(typeof(StringEnumConverter))]

    public enum ProvinceOrder
    {
        CX,
        Code,
        Name
    }
}
