using System.ComponentModel.DataAnnotations;

namespace ApiContactbookApplication.Dtos
{
    public class ContactbookDtoSP
    {
        public int ContactId { get; set; }

        [Required]
        public string Name { get; set; }

        public string Email { get; set; }

        [Required]
        public string PhoneNumber { get; set; }

        public string Company { get; set; }

        public string FileName { get; set; }

        public byte[] File { get; set; }
        public DateTime? BirthDate { get; set; }


        public string Gender { get; set; }

        public bool Favourite { get; set; }

        public string CountryName { get; set; }
        public string StateName { get; set; }

      
    }
}
