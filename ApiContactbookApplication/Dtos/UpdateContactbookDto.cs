using System.ComponentModel.DataAnnotations;

namespace ApiContactbookApplication.Dtos
{
    public class UpdateContactbookDto
    {
        [Required(ErrorMessage = "Contact id is required.")]
        public int ContactId { get; set; }

        [Required(ErrorMessage = "Name is required.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Email address is required.")]
        [EmailAddress]
        [RegularExpression(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$", ErrorMessage = "Invalid email format.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Contact number is required.")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\(?\d{3}\)?[-.\s]?\d{3}[-.\s]?\d{4}$", ErrorMessage = "Invalid contact number.")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Company name is required.")]
        public string Company { get; set; }

        public string? FileName { get; set; }
        public byte[]? File { get; set; }

        public DateTime? BirthDate { get; set; }


        [Required(ErrorMessage = "Gender is required.")]
        public string Gender { get; set; }

        public bool Favourite { get; set; }

        [Required(ErrorMessage = "Country id is required.")]
        public int CountryId { get; set; }

        [Required(ErrorMessage = "State id is required.")]
        public int StateId { get; set; }
    }
}
