using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ClientApiContactbookApplication.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Username is required.")]
        [DisplayName("Username")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        [DataType(DataType.Password)]
        [DisplayName("Password")]
        public string Password { get; set; }
    }
}
