using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace ApiContactbookApplication.Models
{
    public class Contactbook
    {
        [Key]
        [DisplayName("ContactId")]
        public int ContactId { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        public string Email { get; set; }

        [Required]
        public string PhoneNumber { get; set; }
        public string Company { get; set; }

        public string FileName {  get; set; }

        public byte[] File { get; set; }

        public DateTime? BirthDate { get; set; }

        public string Gender { get; set; }

        public bool Favourite {  get; set; }
        
        public int StateId { get; set; }
        public int CountryId { get; set; }

        public State State { get; set; }

        public Country Country { get; set; }
    }
}
