using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TwelveFinal.Controller.DTO
{
    public class TownDTO : DataDTO
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public Guid DistrictId { get; set; }
    }

    public class TownFilterDTO : FilterDTO
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public Guid DistrictId { get; set; }
        public TownFilterDTO() : base()
        {

        }
    }
}
