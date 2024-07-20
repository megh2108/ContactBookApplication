using System.ComponentModel.DataAnnotations;

namespace ContactBookApplication.ViewModels
{
    public class ContactbookViewModel
    {
        [Required(ErrorMessage ="Name is required.")]
        [StringLength(50)]
        public string Name { get; set; }

        //[Required(ErrorMessage ="Email is required.")]
        public string Email { get; set; }

        [Required(ErrorMessage ="Phone number is required.")]
        public string PhoneNumber { get; set; }

        //[Required(ErrorMessage ="Company is required.")]
        public string Company { get; set; }

        public string FileName { get; set; } = string.Empty;

        //[Required(ErrorMessage = "File is required.")]
        public IFormFile File { get; set; }
    }
}
