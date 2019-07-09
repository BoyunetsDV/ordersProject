using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ordersProject.Models
{
    public class Payment
    {
        [Key]
        public int Id { get; set; }
        public string MethodName { get; set; }
        public double Amount { get; set; }

    }
}
