using Core.Constants;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Build.Framework;

namespace Final_.Net.Areas.Admin.ViewModels.Pricing
{
    public class PricingCreateVM
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Text { get; set; }
        [Required]
        public string Price { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public PricingPosition  Status { get; set; }
    }
}
