using System.ComponentModel.DataAnnotations;

namespace Final_.Net.ViewModels.Account
{
    public class AccountLoginVM
    {
        [Required]
        public string UserName { get; set; }
        [Required, DataType(DataType.Password)]
        public string PassWord { get; set; }
        public string? ReturnUrl { get; set; }
    }
}
