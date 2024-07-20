using System.ComponentModel.DataAnnotations;

namespace ApiContactbookApplication.Models
{
    public class Country
    {
        [Key]
        public int CountryId {  get; set; }


        public string CountryName { get; set; }

        public ICollection<State> State {  get; set; }
        public ICollection<Contactbook> Contactbook {  get; set; }

    }
}
