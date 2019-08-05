
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using BlogDemo.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BlogDemo.Infrastructure.Database.EntityConfigurations
{
    public class PostImageConfiguration : IEntityTypeConfiguration<PostImage>
    {
        public void Configure(EntityTypeBuilder<PostImage> builder)
        {
            builder.Property(x => x.FileName).HasMaxLength(100);
        }
    }
}