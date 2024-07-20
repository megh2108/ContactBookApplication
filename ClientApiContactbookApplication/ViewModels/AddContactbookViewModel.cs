using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ClientApiContactbookApplication.ViewModels
{
    public class AddContactbookViewModel
    {
        [Required(ErrorMessage = "Name is required.")]
        [DisplayName("Name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Email address is required.")]
        [EmailAddress]
        [RegularExpression(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$", ErrorMessage = "Invalid email format.")]
        [DisplayName("Email address")]
        public string Email { get; set; }

      

        [Required(ErrorMessage = "Contact number is required.")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\(?\d{3}\)?[-.\s]?\d{3}[-.\s]?\d{4}$", ErrorMessage = "Invalid contact number.")]
        [DisplayName("Phone number")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Company name is required.")]
        [DisplayName("Company name")]
        public string Company { get; set; }

        public string? FileName { get; set; } = string.Empty;

        [DisplayName("Image")]
        public byte[]? File { get; set; }



        [DisplayName("Birthdate")]
        public DateTime? BirthDate { get; set; }



        [Required(ErrorMessage = "Gender is required.")]
        [DisplayName("Gender")]
        public string Gender { get; set; }

        [DisplayName("Favourite")]
        public bool Favourite { get; set; }

        [Required(ErrorMessage = "Country name is required.")]
        [DisplayName("Country name")]
        public int CountryId { get; set; }

        [Required(ErrorMessage = "State name is required.")]
        [DisplayName("State name")]
        public int StateId { get; set; }

        public List<StateContactbookViewModel>? StateContactbook { get; set; }
        public List<CountryContactbookViewModel>? CountryContactbook { get; set; }

    }
}
