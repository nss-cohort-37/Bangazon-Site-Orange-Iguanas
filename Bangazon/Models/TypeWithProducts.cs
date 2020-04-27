using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bangazon.Models
{
    public class TypeWithProducts : ProductType
    {
        public int TypeId { get; set; }
        public string TypeName { get; set; }
        public int ProductCount { get; set; }
    }
}
