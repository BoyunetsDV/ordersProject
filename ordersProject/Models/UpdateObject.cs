using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ordersProject.Models
{
    public class UpdateObject
    {
        public long Id { get; set; }
        public int InvoiceNumber { get; set; }
        public string Status { get; set; }
    }
}
