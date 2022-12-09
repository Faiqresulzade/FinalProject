using Core.Constants;
using System.ComponentModel.DataAnnotations;

namespace Final_.Net.Areas.Admin.ViewModels.Pricing
{
    public class PricingUpdateVM
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
        public PricingPosition Status { get; set; }
    }
}
