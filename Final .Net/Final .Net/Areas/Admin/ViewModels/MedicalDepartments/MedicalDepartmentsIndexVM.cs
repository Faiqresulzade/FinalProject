namespace Final_.Net.Areas.Admin.ViewModels.MedicalDepartments
{
    public class MedicalDepartmentsIndexVM
    {
        public MedicalDepartmentsIndexVM()
        {
            MedicalDepartments = new List<Core.Entities.MedicalDepartments>();
        }
        public List<Core.Entities.MedicalDepartments> MedicalDepartments { get; set; }
    }
}
