namespace ClientApiContactbookApplication.ViewModels
{
    public class ContactbookViewModel
    {
        public int ContactId { get; set; }


        public string Name { get; set; }

        public string Email { get; set; }


        public string PhoneNumber { get; set; }
        public string Company { get; set; }

        public string FileName { get; set; }

        public byte[] File { get; set; }

        public DateTime? BirthDate { get; set; }

        public string Gender { get; set; }

        public bool Favourite { get; set; }

        public int CountryId { get; set; }
        public int StateId { get; set; }

        //for return iamgepriview
        public string? ImagePreview
        {
            get
            {
                if (File != null && File.Length > 0)
                {
                    return Convert.ToBase64String(File);
                }
                return null;
            }
        }

        public StateContactbookViewModel State { get; set; }
        public CountryContactbookViewModel Country { get; set; }

    }
}
