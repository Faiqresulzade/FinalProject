namespace Final_.Net.Areas.Admin.ViewModels.MedicalDepartments
{
    public class MedicalDepartmentsCreateVM
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string? PhotoName { get; set; }
        public IFormFile Photo { get; set; }
    }
}
