using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlogDemo.Infrastructure.Resources;
using FluentValidation;

namespace BlogDemoApi.Resources
{
    public class PostAddResourceValidator:AbstractValidator<PostAddResource>
    {
        public PostAddResourceValidator()
        {
            RuleFor(x => x.Remark)
               .NotNull()
               .WithName("简述")
               .WithMessage("required|{{PropertyName}是必填的")
               .WithMessage("minlength|{PropertyName}这里是个坑，因为之前迁移表的时候PostConfiguration类中设置了IsRequired()的子段导致了这个值现在不能为空");

            RuleFor(x => x.Title)
                .NotNull()
                .WithName("标题")
                .WithMessage("required|{PropertyName}是必填的")
                .MaximumLength(50)
                .WithMessage("maxlength|{PropertyName}的最大长度是{MaxLength}");

            RuleFor(x => x.Boby)
               .NotNull()
               .WithName("正文")
               .WithMessage("required|{{PropertyName}是必填的")
               .MinimumLength(100)
               .WithMessage("minlength|{PropertyName}的最小长度是{MinLength}");



        }
    }
}
