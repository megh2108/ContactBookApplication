using System.ComponentModel.DataAnnotations;

namespace ApiContactbookApplication.Models
{
    public class State
    {
        [Key]
        public int StateId { get; set; }

        public string StateName { get; set; }

        public int CountryId {  get; set; }

        public Country Country { get; set; }

        public ICollection<Contactbook> Contactbook { get; set; }

    }
}
