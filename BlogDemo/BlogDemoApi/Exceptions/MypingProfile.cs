using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BlogDemo.Core.Entities;
using BlogDemoApi.Resources;

namespace BlogDemoApi.Exceptions
{
    //联系上下文
    public class MypingProfile:Profile
    {
        public MypingProfile()
        {
            CreateMap<Post, PostResource>()
                .ForMember(dest=>dest.UpDateTime,opt=>opt.MapFrom(src=>src.LastModified));//建议了一个post道postResource的映射
            CreateMap< PostResource,Post>();
        }
    }
}
