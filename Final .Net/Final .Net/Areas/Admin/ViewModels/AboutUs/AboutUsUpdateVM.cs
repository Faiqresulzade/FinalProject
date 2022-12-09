namespace Final_.Net.Areas.Admin.ViewModels.AboutUs
{
    public class AboutUsUpdateVM
    {
        public int Id { get; set; }
        public string? SubTitle { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? SignuturePath { get; set; }
        public IFormFile? SignaturePhoto { get; set; }
        public string? MainPhotoPath { get; set; }
        public IFormFile? MainPhoto { get; set; }
        public string? SecondPhotoPath { get; set; }
        public IFormFile? SecondPhoto { get; set; }
        public DateTime ModifiedAt { get; set; }
    }
}
