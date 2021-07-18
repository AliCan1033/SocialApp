using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ServerApp.Models;

namespace ServerApp.Data
{
    public class SocialContext:IdentityDbContext<User,Role,int>
    {
        public SocialContext(DbContextOptions<SocialContext> options):base(options)
        {
            
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<UserToUser> UserToUSers { get; set; }
        
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            
            builder.Entity<UserToUser>()
                            .HasKey(k =>new {k.UserId,k.FollowerId});//primary keyleri belirledik 
            builder.Entity<UserToUser>()
                            .HasOne(l => l.User)
                            .WithMany(a => a.Followers)
                            .HasForeignKey(l =>l.FollowerId);//bir userin birden çok takipçisi olabilir
            builder.Entity<UserToUser>()
                            .HasOne(l => l.Follower)
                            .WithMany(a =>a.Followings)
                            .HasForeignKey(l => l.UserId);//bir takipçi birden fazla kullanıcıyı takip edeblir
    
        }

    }
}