using Core.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class Doctors :BaseEntity
    {
        public string Title { get; set; }
        public string PhotoName { get; set; }
        public string Name { get; set; }
        public string Position { get; set; }
        public string FacebookLink { get; set; }
        public string TwitterLink { get; set; }
        public string LinkedinLink { get; set; }
    }
}
