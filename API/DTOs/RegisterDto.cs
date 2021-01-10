using System;
using System.ComponentModel.DataAnnotations;

namespace API.DTOs
{
    public class RegisterDto
    {
        [Required]
        public string Username {get; set;}

        public string FirstName {get; set;}

        public string LastName {get; set;}

        [Required]
        [StringLength(16, MinimumLength=4)]
        public string Password {get; set;}

        // // [Compare("Password", ErrorMessage = "Passwords must match")]
        // [Display(Name = "Confirm Password")]
        // public string ConfirmPassword { get; set; }

         public bool Active { get; set; } = true;
    }
}