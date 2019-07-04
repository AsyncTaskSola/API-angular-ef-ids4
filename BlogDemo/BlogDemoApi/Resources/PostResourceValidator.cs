using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;

namespace BlogDemoApi.Resources
{
    public class PostResourceValidator:AbstractValidator<PostResource>
    {
        public PostResourceValidator()
        {
            RuleFor(x => x.Author)
                .NotNull()
                .WithName("作者")
                .WithMessage("{PropertyName}是必添的")
                .MaximumLength(50)
                .WithMessage("{PropertyName}的最大长度是{MaximumLength}");

        }
    }
}
