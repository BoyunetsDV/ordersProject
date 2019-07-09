using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ordersProject.Models
{
    public class Country
    {
        [Key]
        public int Key { get; set; }
        public string Geo { get; set; }
    }
}
