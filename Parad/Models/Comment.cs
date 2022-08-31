using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Parad.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public DateTime CommentDate { get; set; }
        public string UserId { get; set; }
        public AppUser User { get; set; }
        public int ImageId { get; set; }
        public Image Image { get; set; }
    }
}
