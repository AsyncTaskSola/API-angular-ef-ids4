using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BlogDemo.Core.Entities;
using BlogDemo.Infrastructure.Resources;
using PostResource = BlogDemoApi.Resources.PostResource;

namespace BlogDemoApi.Exceptions
{
    //联系上下文 这个很重要映射的时候必写
    public class MypingProfile:Profile
    {
        public MypingProfile()
        {
            CreateMap<Post, PostResource>()
                .ForMember(dest=>dest.UpDateTime,opt=>opt.MapFrom(src=>src.LastModified));//建议了一个post道postResource的映射
            CreateMap< PostResource,Post>();
            CreateMap<PostAddResource, Post>();
            CreateMap<PostUpdateResource, Post>();
        }
        
    }
}
