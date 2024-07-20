using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace ContactBookApplication.Models
{
    public class Contactbook
    {
        internal Contactbook? filterContact;

        [Key]
        [DisplayName("ContactId")]
        public int ContactId {  get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        public string Email { get; set; }

        [Required]
        public string PhoneNumber { get; set; }
        public string Company {  get; set; }

        public string FileName { get; set; }

    }
}
