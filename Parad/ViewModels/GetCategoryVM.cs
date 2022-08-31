using Parad.Models;
using System.Collections.Generic;

namespace Parad.ViewModels
{
    public class GetCategoryVM
    {
        public AppUser AppUser { get; set; }
        public Category Category { get; set; }
        public List<Image> Images { get; set; }
        public List<Like> Likes { get; set; }
    }
}
