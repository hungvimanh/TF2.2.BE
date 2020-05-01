using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TwelveFinal.Entities
{
    public class Ethnic : DataEntity    //Dân tộc
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
    }

    public class EthnicFilter : FilterEntity
    {
        public GuidFilter Id { get; set; }
        public StringFilter Code { get; set; }
        public StringFilter Name { get; set; }
        public EthnicOrder OrderBy { get; set; }
        public EthnicFilter() : base()
        {

        }
    }

    public enum EthnicOrder
    {
        CX,
        Code,
        Name
    }
}
