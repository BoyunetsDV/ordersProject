using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ordersProject.Models
{
    public class Payment
    {
        [Key]
        public int Id { get; set; }
        public string MethodName { get; set; }
        public double Amount { get; set; }

        public Payment()
        {
        }
        public Payment(XElement element)
        {
            MethodName = element.Element("method-name").Value;
            Amount = double.Parse(element.Element("amount").Value);
        }
    }
}
