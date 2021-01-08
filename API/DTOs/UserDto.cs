using System.ComponentModel.DataAnnotations;

namespace API.DTOs
{
    public class UserDto
    {
        [Required]
        public string UserName {get; set;}

        public string FirstName {get; set;}

        public string LastName {get; set;}

        public string Token { get; set; }

         public bool Active { get; set; } = true;

    }
}