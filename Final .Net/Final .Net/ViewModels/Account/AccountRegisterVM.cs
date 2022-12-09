using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Final_.Net.ViewModels.Account
{
    public class AccountRegisterVM
    {
        [Microsoft.Build.Framework.Required, MaxLength(50)]
        public string FullName { get; set; }
        [Microsoft.Build.Framework.Required, MaxLength(50), DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Microsoft.Build.Framework.Required, MaxLength(50), DataType(DataType.Password)]
        public string PassWord { get; set; }
        [Microsoft.Build.Framework.Required, MaxLength(50), DataType(DataType.Password), Display(Name = "Confirm PassWord"), Compare(nameof(PassWord))]
        public string ConfirmPassWord { get; set; }
        [Microsoft.Build.Framework.Required, MaxLength(50)]
        public string UserName { get; set; }
    }
}
