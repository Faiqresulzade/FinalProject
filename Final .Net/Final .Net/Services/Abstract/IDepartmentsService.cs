using Final_.Net.ViewModel.Home;

namespace Final_.Net.Services.Abstract
{
    public interface IDepartmentsService
    {
        Task<HomeIndexVM> IndexAsync();
    }
}
