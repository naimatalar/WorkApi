using System;
using System.Collections.Generic;
using System.Reflection.Metadata;
using System.Text;

namespace Work.Core
{
   public class Geo :BaseEntity
    {
        public double lat { get; set; }
        public double lng { get; set; }
        public Address Address { get; set; }
        public string AddressId { get; set; }
    }
}
