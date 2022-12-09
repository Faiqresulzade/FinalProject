using Core.Entities.Base;

namespace Core.Entities
{
    public class OurVision : BaseEntity
    {
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? PhotoPath { get; set; }
    }
}
