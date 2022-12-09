namespace Final_.Net.Areas.Admin.ViewModels.LastestNews
{
    public class LastestNewsCreateVM
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Time { get; set; }    
        public string? PhotoPath { get; set; }
        public IFormFile Photo { get; set; }
    }
}
