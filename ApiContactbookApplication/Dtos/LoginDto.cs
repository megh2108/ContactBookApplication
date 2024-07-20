﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace ApiContactbookApplication.Dtos
{
    public class LoginDto
    {
        [Required(ErrorMessage = "Username is required.")]
        [DisplayName("Username")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        [DataType(DataType.Password)]
        [DisplayName("Password")]
        public string Password { get; set; }
    }
}
