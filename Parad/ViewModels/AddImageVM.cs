using Parad.Models;
using System.Collections.Generic;

namespace Parad.ViewModels
{
    public class AddImageVM
    {
        public AppUser AppUser { get; set; }
        public Image Image { get; set; }
        public List<Category> Categories { get; set; }
    }
}
