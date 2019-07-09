using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ordersProject.Models
{
    public class OrderArticle
    {
        [Key]
        public string ArtNum { get; set; }
        public string Title { get; set; }
        public double Amount { get; set; }
        public double BrutPrice { get; set; }
    }
}
