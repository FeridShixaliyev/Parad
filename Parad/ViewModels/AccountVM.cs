using Parad.Models;
using System.Collections.Generic;

namespace Parad.ViewModels
{
    public class AccountVM
    {
        public AppUser AppUser { get; set; }
        public List<Image> Images { get; set; }
        public List<Like> Likes { get; set; }
    }
}
