using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

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
    }
}
