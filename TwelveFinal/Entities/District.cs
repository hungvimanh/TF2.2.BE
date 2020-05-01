using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TwelveFinal.Entities
{
    public class District : DataEntity  //Quận/Huyện
    {
        public Guid Id { get; set; }
        public string Code { get; set; }    //Mã quận/huyện
        public string Name { get; set; }    //Tên quận/huyện
        public Guid ProvinceId { get; set; }    //Id tỉnh/ thành phố huyện đó trực thuộc
        public string ProvinceCode { get; set; }    //Mã tỉnh
        public string ProvinceName { get; set; }    //Tên tỉnh
        public List<Town> Towns { get; set; }   //Danh sách các phường/xã đặc biệt khó khăn trực thuộc tỉnh
    }

    public class DistrictFilter : FilterEntity
    {
        public GuidFilter Id { get; set; }
        public StringFilter Code { get; set; }
        public StringFilter Name { get; set; }
        public Guid ProvinceId { get; set; }
        public StringFilter ProvinceCode { get; set; }
        public StringFilter ProvinceName { get; set; }
        public DistrictOrder OrderBy { get; set; }
        public DistrictFilter() : base()
        {

        }
    }

    [JsonConverter(typeof(StringEnumConverter))]

    public enum DistrictOrder
    {
        CX,
        Code,
        Name
    }
}
