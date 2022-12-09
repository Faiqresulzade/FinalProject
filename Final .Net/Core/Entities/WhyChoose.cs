using Core.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class WhyChoose:BaseEntity
    {
        public string? Title { get; set; }
        public string? Text { get; set; }
        public string? Photo { get; set; }
        public string? Descriprtion { get; set; }
    }
}
