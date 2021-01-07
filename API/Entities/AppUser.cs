using System.ComponentModel.DataAnnotations;

namespace API.Entities
{
    public class AppUser
    {
        public int Id {get; set;}

        [Required]
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "E-mail is not valid")]
        public string UserName {get; set;}

        public string FirstName {get; set;}

        public string LastName {get; set;}

        public bool Active = true;

        public byte[] PasswordHash { get; set;}

        public byte[] PasswordSalt { get; set;}
    }
}