using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Parad.Models
{
    public class Like
    {
        public int Id { get; set; }
        public int LikeCount { get; set; }
        public int ImageId { get; set; }
        public Image Image { get; set; }
        [NotMapped]
        public ICollection<AppUser> Users { get; set; }
    }
}
