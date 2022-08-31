using Parad.Models;
using System.Collections.Generic;

namespace Parad.ViewModels
{
    public class DetailsVM
    {
        public AppUser User { get; set; }
        public Image Image { get; set; }
        public Comment Comment { get; set; }
        public Like Like { get; set; }
        public List<TagImage> TagImages { get; set; }
        public List<Comment> Comments { get; set; }
        public List<Image> Images { get; set; }
    }
}
