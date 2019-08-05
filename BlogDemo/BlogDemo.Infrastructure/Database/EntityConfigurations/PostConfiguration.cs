using BlogDemo.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BlogDemo.Infrastructure.Database.EntityConfigurations
{
   public  class PostConfiguration : IEntityTypeConfiguration<Post>
    {
        /// <summary>
        /// 属性约束（麻烦）,建议在类下面的自定义参数变量上面加中括号写明就行了
        /// </summary>
        /// <param name="builder"></param>
        public void Configure(EntityTypeBuilder<Post> builder)
        {
           // builder.Property(x => x.Remark).IsRequired().HasMaxLength(200);
            builder.Property(x => x.Remark).HasMaxLength(200);//好像去掉了IsRequired()也不行，可能要重新迁移吧
        }
    }
}
