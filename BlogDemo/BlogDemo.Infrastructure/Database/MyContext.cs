using BlogDemo.Core.Entities;
using BlogDemo.Infrastructure.Database.EntityConfigurations;
using Microsoft.EntityFrameworkCore;

namespace BlogDemo.Infrastructure.Database
{
    /// <summary>
    /// EF数据迁移配置
    /// </summary>
    public class MyContext:DbContext
    {
        public MyContext(DbContextOptions<MyContext> options):base(options)
        {
                
        }

        public DbSet<Post> Posts { get; set; }
        public  DbSet<PostImage> PostImages { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new PostConfiguration());
            modelBuilder.ApplyConfiguration(new PostImageConfiguration());
        }
    }
}