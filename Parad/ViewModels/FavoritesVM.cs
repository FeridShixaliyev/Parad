using Parad.Models;
using System.Collections.Generic;

namespace Parad.ViewModels
{
    public class FavoritesVM
    {
        public AppUser User { get; set; }
        public List<Favorite> Favorites { get; set; }
        //public List<Image> Images { get; set; }
    }
}
