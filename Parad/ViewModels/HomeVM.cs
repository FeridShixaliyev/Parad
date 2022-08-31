using Parad.Models;
using System.Collections.Generic;

namespace Parad.ViewModels
{
    public class HomeVM
    {
        public List<AppUser> AppUser { get; set; }
        public List<Slider> Sliders { get; set; }
        public List<Image> Images { get; set; }
        public List<Like> Likes { get; set; }
        public List<Category> Categories { get; set; }
    }
}
