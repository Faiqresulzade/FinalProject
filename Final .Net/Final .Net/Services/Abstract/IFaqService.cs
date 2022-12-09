using Final_.Net.Areas.Admin.ViewModels.Pricing;
using Final_.Net.ViewModels.Faq;

namespace Final_.Net.Services.Abstract
{
    public interface IFaqService
    {
        Task<FaqIndexVM> IndexAsync();
    }
}
