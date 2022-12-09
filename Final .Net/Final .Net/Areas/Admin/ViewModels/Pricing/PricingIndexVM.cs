using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Final_.Net.Areas.Admin.ViewModels.Pricing
{
    public class PricingIndexVM
    {
        public List<Core.Entities.Pricing> Pricings { get; set; }

        #region Filter
        public string? Title { get; set; }
        [Display(Name = "Create at Start")]
        public DateTime? CreateAtStart { get; set; }
        [Display(Name = "Create at End")]
        public DateTime? CreateAtEnd { get; set; }
        #endregion
    }
}
