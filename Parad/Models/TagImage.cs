using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Parad.Models
{
    public class TagImage
    {
        public int Id { get; set; }
        public int TagId { get; set; }
        public Tag Tag { get; set; }
        public int ImageId { get; set; }
        public Image Image { get; set; }
    }
}
