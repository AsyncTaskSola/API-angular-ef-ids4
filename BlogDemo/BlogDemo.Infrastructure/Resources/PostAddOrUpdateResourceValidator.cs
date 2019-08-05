
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using FluentValidation;

namespace BlogDemo.Infrastructure.Resources
{
    public class PostAddOrUpdateResourceValidator<T> : AbstractValidator<T> where T : PostAddOrUpdateResource
    {
        public PostAddOrUpdateResourceValidator()
        {
            //2019.8.2已经重新迁移填好这个坑了
            //RuleFor(x => x.Remark)
            //    .NotNull()
            //    .WithName("信息")
            //    .WithMessage("required|{PropertyName}是必填的  这里是个坑，因为之前迁移表的时候PostConfiguration类中设置了IsRequired()的子段导致了这个值现在不能为空");

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
                .MinimumLength(10)
                .WithMessage("minlength|{PropertyName}的最小长度是{MinLength}");
        }
    }
}