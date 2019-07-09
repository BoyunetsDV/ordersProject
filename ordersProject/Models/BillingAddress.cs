using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace ordersProject.Models
{
    public class BillingAddress
    {
        [Key]
        public int Id { get; set; }
        public string BillEmail { get; set; }
        public string BillFName { get; set; }
        public string BillStreet { get; set; }
        public string BillStreetNr { get; set; }
        public string BillCity { get; set; }
        public Country Country { get; set; }
        public string BillZip { get; set; }

        public BillingAddress()
        {
        }
        public BillingAddress(XElement element)
        {
            BillEmail = element.Element("billemail").Value;
            BillFName = element.Element("billfname").Value;
            BillStreet = element.Element("billstreet").Value;
            BillStreetNr = element.Element("billstreetnr").Value;
            BillCity = element.Element("billcity").Value;
            Country = new Country(element.Element("country"));
            BillZip = element.Element("billzip").Value;
        }
    }
}
