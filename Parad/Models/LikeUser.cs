

namespace Parad.Models
{
    public class LikeUser
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public AppUser User { get; set; }
        public int LikeId { get; set; }
        public Like Like { get; set; }
    }
}
