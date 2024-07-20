﻿using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ApiContactbookApplication.Dtos
{
    public class UserDetailDto
    {
        public int UserId { get; set; }

        [Required(ErrorMessage = "First Name is Required.")]
        [StringLength(15)]
        [DisplayName("First Name")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last Name is Required.")]
        [StringLength(15)]
        [DisplayName("Last Name")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Login Id is Required.")]
        [StringLength(15)]
        [DisplayName("Login ID")]
        public string LoginId { get; set; }

        [Required(ErrorMessage = "Email Address is Required.")]
        [StringLength(50)]
        [EmailAddress]
        [RegularExpression(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$", ErrorMessage = "Invalid email format.")]
        [DisplayName("Email Address")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Contact Number is Required.")]
        [StringLength(15)]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\(?\d{3}\)?[-.\s]?\d{3}[-.\s]?\d{4}$", ErrorMessage = "Invalid contact number.")]
        [DisplayName("Contact Number")]
        public string ContactNumber { get; set; }

        public string? FileName { get; set; }

        public byte[]? File { get; set; }
    }
}
