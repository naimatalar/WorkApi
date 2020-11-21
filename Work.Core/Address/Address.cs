using System;
using System.Collections.Generic;
using System.Text;

namespace Work.Core
{
    public class Address :BaseEntity
    {
     
        public string Street { get; set; }
        public string Suite { get; set; }
        public string City { get; set; }
        public string ZipCode { get; set; }
  
        public ICollection<Geo> Users { get; set; }
    }
}
