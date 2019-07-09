using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ordersProject.Models
{

    public class Country
    {
        [Key]
        public int Key { get; set; }
        public string Geo { get; set; }
        public Country()
        {
        }
        public Country(XElement element)
        {
            Geo = element.Element("geo").Value;
        }
    }
}
