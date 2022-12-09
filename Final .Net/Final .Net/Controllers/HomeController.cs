using DataAcces.Contexts;
using Final_.Net.ViewModel.Home;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Final_.Net.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _appDbContext;

        public HomeController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public async Task<IActionResult> Index()
        {
            var model = new HomeIndexVM
            {
                OurVisions = await _appDbContext.OurVisions.ToListAsync(),
                GetAboutUs =  await _appDbContext.GetAbouts.FirstOrDefaultAsync(),
                MedicalDepartments=await _appDbContext.MedicalDepartments.ToListAsync(),
                WhyChoose=await _appDbContext.WhyChoose.FirstOrDefaultAsync()
            };
            return View(model);
        }
    }
}