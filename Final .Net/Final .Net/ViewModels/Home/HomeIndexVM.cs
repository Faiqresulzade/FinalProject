using Core.Entities;

namespace Final_.Net.ViewModel.Home
{
    public class HomeIndexVM
    {
        public List<OurVision>? OurVisions { get; set; }
        public AboutUs GetAboutUs { get; set; }
        public List<MedicalDepartments> MedicalDepartments { get; set; }
        public WhyChoose? WhyChoose { get; set; }
    }
}
