﻿using Core.Constants;
using Core.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class Pricing:BaseEntity
    {
        public string Title { get; set; }
        public string Text { get; set; }
        public string Price { get; set; }
        public string Description { get; set; }
        public PricingPosition Status { get; set; }
    }
}
