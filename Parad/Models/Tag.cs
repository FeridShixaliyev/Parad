using System.Collections.Generic;

namespace Parad.Models
{
    public class Tag
    {
        public int Id { get; set; }
        public string TagName { get; set; }
        public ICollection<TagImage> TagImages { get; set; }
    }
}
