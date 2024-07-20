using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ClientApiContactbookApplication.ViewModels
{
    public class PasswordViewModel
    {
        [Required(ErrorMessage = "Username is required.")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        [DataType(DataType.Password)]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,}$",
 ErrorMessage = "The password must be at least 8 characters long and contain at least 1 uppercase letter, 1 number, and 1 special character.")]
        [DisplayName("Enter new password")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Confirm password is required.")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        [DisplayName("Enter confirm Password")]
        public string ConfirmPassword { get; set; }
    }
}
