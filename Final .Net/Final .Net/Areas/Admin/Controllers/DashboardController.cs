using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace Final_.Net.Areas.Admin.Controllers
{
    public class DashboardController:Controller
    {
        [Area("admin")]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> Index()
        {
            return View();
        }
    }
}
