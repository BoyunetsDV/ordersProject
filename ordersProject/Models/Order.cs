using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ordersProject.Models
{
    public class Order
    {
        [Key]
        public int Id { get; set; }
        public long Oxid { get; set; }
        public DateTime OrderDate { get; set; }
        public BillingAddress BillingAddress { get; set; }
        public List<Payment> Payments { get; set; }
        public List<OrderArticle> Articles { get; set; }
        public string Status { get; set; } = null;
        public int InvoiceNumber { get; set; } = 0;

        public Order()
        {
        }
        public Order(XElement element)
        {
            Oxid = long.Parse(element.Element("oxid").Value);
            OrderDate = DateTime.Parse(element.Element("orderdate").Value);
            BillingAddress = new BillingAddress(element.Element("billingaddress"));

            Payments = new List<Payment>();
            foreach(var paymentElement in element.Element("payments").Elements("payment"))
                Payments.Add(new Payment(paymentElement));

            Articles = new List<OrderArticle>();
            foreach (var articleElement in element.Element("articles").Elements("orderarticle"))
                Articles.Add(new OrderArticle(articleElement));
        }
    }
}
