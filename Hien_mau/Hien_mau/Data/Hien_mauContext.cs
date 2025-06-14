using Hien_mau.Models;
using Microsoft.EntityFrameworkCore;

namespace Hien_mau.Data
{
    public class Hien_mauContext(DbContextOptions<Hien_mauContext> options) : DbContext(options)
    {
        public DbSet<User> Users { get; set; }
        public DbSet<News> News { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<BloodArticle> BloodArticles { get; set; } 

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Cấu hình quan hệ nhiều-nhiều News và Tag
            modelBuilder.Entity<News>()
                 .HasMany(b => b.Tags)
                 .WithMany(t => t.Posts)
                 .UsingEntity<Dictionary<string, object>>(
                     "NewsTags", // Đổi từ BlogPostTags thành NewsTags
                     j => j.HasOne<Tag>().WithMany().HasForeignKey("TagID"),
                     j => j.HasOne<News>().WithMany().HasForeignKey("PostID")
                 );
            // Cấu hình quan hệ nhiều-nhiều BloodArticle và Tag
            modelBuilder.Entity<BloodArticle>()
                .HasMany(a => a.Tags)
                .WithMany(t => t.Articles)
                .UsingEntity<Dictionary<string, object>>(
                    "ArticleTags",
                    j => j.HasOne<Tag>().WithMany().HasForeignKey("TagID"),
                    j => j.HasOne<BloodArticle>().WithMany().HasForeignKey("ArticleID")
                );
        }
    }
}
