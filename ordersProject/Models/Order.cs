using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ordersProject.Models
{
    public class Order
    {
        [Key]
        public int Oxid { get; set; }
        public DateTime OrderDate { get; set; }
        public BillingAddress BillingAddress { get; set; }
        public List<Payment> Payments { get; set; }
        public List<OrderArticle> Articles { get; set; }
        public string Status { get; set; }
        public int InvoiceNumber { get; set; }

    }
}
