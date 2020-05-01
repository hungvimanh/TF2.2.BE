using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TwelveFinal.Controller.DTO
{
    public class SubjectGroupDTO : DataDTO
    {
        public Guid? Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
    }

    public class SubjectGroupFilterDTO : FilterDTO
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public SubjectGroupFilterDTO() : base()
        {

        }
    }
}
