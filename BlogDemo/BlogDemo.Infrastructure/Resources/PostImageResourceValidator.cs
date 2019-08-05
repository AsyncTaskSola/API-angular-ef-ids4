
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using FluentValidation;

namespace BlogDemo.Infrastructure.Resources
{
  public  class PostImageResourceValidator:AbstractValidator<PostImageResource>
    {
        public PostImageResourceValidator()
        {
            RuleFor(x=>x.FileName)
                .NotNull()
                .WithName("文件名")
                .WithMessage("required|{FileName}是必填的")
                .MaximumLength(100)
                .WithMessage("maxlength|{FileName}的最大长度是{FileName}");
        }
    }
}