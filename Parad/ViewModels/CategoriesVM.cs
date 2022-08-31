using Parad.Models;
using System.Collections.Generic;

namespace Parad.ViewModels
{
    public class CategoriesVM
    {
        public AppUser AppUser { get; set; }
        public List<Category> Categories { get; set; }
        public List<Image> Images { get; set; }
    }
}
