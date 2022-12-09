namespace Final_.Net.Areas.Admin.ViewModels.OurVision
{
    public class OurVisionUpdateVM
    {

        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? PhotoName { get; set; }
        public IFormFile? Photo { get; set; }
        public DateTime ModifiedAt { get; set; }
    }
}
