using Core.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class AboutUs:BaseEntity
    {
        public string? SubTitle { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? SignuturePath { get; set; }
        public string? MainPhotoPath { get; set; }
        public string? SecondPhotoPath { get; set; }
    }
}
