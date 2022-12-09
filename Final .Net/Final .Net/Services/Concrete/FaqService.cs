using DataAcces.Contexts;
using Final_.Net.Areas.Admin.ViewModels.Pricing;
using Final_.Net.Services.Abstract;
using Final_.Net.ViewModels.Faq;
using Microsoft.EntityFrameworkCore;

namespace Final_.Net.Services.Concrete
{
    public class FaqService : IFaqService
    {
        private readonly AppDbContext _appDbContext;

        public FaqService(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public async Task<FaqIndexVM> IndexAsync()
        {
            var model = new FaqIndexVM
            {
                Pricings = await _appDbContext.PricingsComponent.ToListAsync()
            };
            return model;
        }
    }
}
