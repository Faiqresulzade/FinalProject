using System.ComponentModel.DataAnnotations;

namespace Final_.Net.Areas.Admin.ViewModels.Account
{
    public class AccountLoginVM
    {
        [Required]
        public string Username { get; set; }
        [Required, DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
