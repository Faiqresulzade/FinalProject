using Final_.Net.Areas.Admin.Services.Abstract;
using Final_.Net.Services.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace Final_.Net.Controllers
{
    public class FaqController:Controller
    {
        private readonly IFaqService _faqService;

        public FaqController(IFaqService faqService)
        {
            _faqService = faqService;
        }
        public async Task<IActionResult> Index()
        {
            var pricing=await _faqService.IndexAsync();
            if(pricing!=null)return View(pricing);
            return View();
        }
    }
}
