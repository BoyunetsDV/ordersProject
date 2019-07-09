using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ordersProject.Models
{
    public class OrderArticle
    {
        [Key]
        public int Id { get; set; }
        public string ArtNum { get; set; }
        public string Title { get; set; }
        public double Amount { get; set; }
        public double BrutPrice { get; set; }

        public OrderArticle()
        {
        }
        public OrderArticle(XElement element)
        {
            ArtNum = element.Element("artnum").Value;
            Title = element.Element("title").Value;
            Amount = double.Parse(element.Element("amount").Value);
            BrutPrice = double.Parse(element.Element("brutprice").Value);
        }
    }
}
