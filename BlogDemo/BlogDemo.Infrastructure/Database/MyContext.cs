using BlogDemo.Core.Entities;
using BlogDemo.Infrastructure.Database.EntityConfigurations;
using Microsoft.EntityFrameworkCore;

namespace BlogDemo.Infrastructure.Database
{
    public class MyContext:DbContext
    {
        public MyContext(DbContextOptions<MyContext> options):base(options)
        {
                
        }

        public DbSet<Post> Posts { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new PostConfiguration());
        }
    }
}