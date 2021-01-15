using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using API.Extensions;
using Microsoft.AspNetCore.Identity;

namespace API.Entities
{
    public class AppUser : IdentityUser<int>
    {
        // public int Id {get; set;}

        // public string Username {get; set;}

        public string FirstName {get; set;}

        public string LastName {get; set;}

        public bool Active { get; set; } = true;

        // public byte[] PasswordHash { get; set;}

        // public byte[] PasswordSalt { get; set;}

        public string KnownAs { get; set; }

        public DateTime DateOfBirth { get; set; }

        public DateTime Created { get; set; } = DateTime.Now;

        public DateTime LastActive { get; set; } = DateTime.Now;

        public string Gender { get; set; }

        public string Introduction { get; set; }

        public string LookingFor { get; set; }

        public string Interests { get; set; }

        public string City { get; set; }

        public string Country { get; set; }

        public ICollection<Photo> Photos { get; set; }


        // public int GetAge()
        // {
        //     return DateOfBirth.CalculateAge();
        // }

        public ICollection<UserLike> LikedByUsers { get; set; }
        public ICollection<UserLike> LikedUsers { get; set; }

        public ICollection<Message> MessagesSent { get; set; }
        public ICollection<Message> MessagesReceived { get; set; }

        public ICollection<AppUserRole> UserRoles { get; set; }

    }
}