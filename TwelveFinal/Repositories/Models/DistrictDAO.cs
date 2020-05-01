using System;
using System.Collections.Generic;

namespace TwelveFinal.Repositories.Models
{
    public partial class DistrictDAO
    {
        public DistrictDAO()
        {
            Towns = new HashSet<TownDAO>();
        }

        public Guid Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public Guid ProvinceId { get; set; }

        public virtual ProvinceDAO Province { get; set; }
        public virtual ICollection<TownDAO> Towns { get; set; }
    }
}
