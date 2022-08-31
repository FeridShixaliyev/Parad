using Parad.Models;
using System.Collections.Generic;

namespace Parad.ViewModels
{
    public class ImageAndTagsVM
    {
        public AppUser User { get; set; }
        public Image Image { get; set; }
        public List<int> TagIds { get; set; }
        
    }
}
