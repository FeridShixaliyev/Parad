using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Parad.Models;

namespace Parad.DAL
{
    public class AppDbContext: IdentityDbContext<AppUser>
    {
        public AppDbContext(DbContextOptions options):base(options)
        {
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<Like> Likes { get; set; }
        public DbSet<LikeUser> LikeUsers { get; set; }
        public DbSet<Favorite> Favorites { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<CommentReport> CommentReports { get; set; }
        public DbSet<ReasonReport> ReasonReports { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<TagImage> TagImages { get; set; }
        public DbSet<Slider> Sliders { get; set; }
    }
}
