using System.ComponentModel.DataAnnotations;

namespace API.DTOs
{
    public class UserDto
    {
        
        public string Username {get; set;}

        public string KnownAs {get; set;}

        public string Gender {get; set;}

        public string Token { get; set; }

         public bool Active { get; set; } = true;

         public string PhotoUrl { get; set; }

    }
}