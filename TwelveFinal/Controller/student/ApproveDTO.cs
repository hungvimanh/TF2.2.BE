using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TwelveFinal.Controller.student
{
    public class ApproveDTO : DataDTO
    {
        public Guid StudentId { get; set; }
        public Guid Id { get; set; }
        public int Status { get; set; }
    }
}
