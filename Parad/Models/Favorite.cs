

namespace Parad.Models
{
    public class Favorite
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public AppUser User { get; set; }
        public int ImageId { get; set; }
        public Image Image { get; set; }
    }
}
