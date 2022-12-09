namespace Final_.Net.Areas.Admin.ViewModels.Doctors
{
    public class DoctorsCreateVM
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string? PhotoName { get; set; }
        public IFormFile Photo { get; set; }
        public string Name { get; set; }
        public string Position { get; set; }
        public string FacebookLink { get; set; }
        public string TwitterLink { get; set; }
        public string LinkedinLink { get; set; }
    }
}
