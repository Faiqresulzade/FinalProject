using DataAcces.Contexts;
using Final_.Net.Services.Abstract;
using Final_.Net.ViewModel.Home;
using Microsoft.EntityFrameworkCore;

namespace Final_.Net.Services.Concrete
{
    public class DepartmentsService : IDepartmentsService
    {
        private readonly AppDbContext _appDbContext;

        public DepartmentsService(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public async Task<HomeIndexVM> IndexAsync()
        {
            var model = new HomeIndexVM
            {
                MedicalDepartments = await _appDbContext.MedicalDepartments.ToListAsync(),
            };
            return model;
        }
    }
}
