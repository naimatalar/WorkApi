using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Work.Api.Models.RequestModel
{
    public class AddressRequestModel
    {
        public string Street { get; set; }
        public string Suite { get; set; }
        public string City { get; set; }
        public string ZipCode { get; set; }
        public GeoRequestModel GeoRequestModel { get; set; }
    }
}
