﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ordersProject.Models
{
    public class UpdateObject
    {
        public string Type { get; set; }
        public Dictionary<int,string> KeyValuePairs { get; set; }
    }
}