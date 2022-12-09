namespace Final_.Net.Areas.Admin.ViewModels.WhyChoose
{
    public class WhyChooseCreateVM
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Text { get; set; }
        public string? PhotoPath { get; set; }
        public IFormFile Photo { get; set; }
        public string? Descriprtion { get; set; }
    }
}
