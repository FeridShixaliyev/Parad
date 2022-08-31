using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Parad.Models
{
    public class AppUser:IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ProfileImage { get; set; }
        public DateTime StartDate { get; set; }
        [NotMapped]
        public IFormFile ProfileImageFile { get; set; }
        public List<Image> Collection { get; set; }
        [NotMapped]
        public ICollection<Like> Likes { get; set; }
        public ICollection<Favorite> Favorites { get; set; }
    }
}
