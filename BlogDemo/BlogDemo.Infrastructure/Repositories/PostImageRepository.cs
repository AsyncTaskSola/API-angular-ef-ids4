using System;
using System.Collections.Generic;
using System.Text;
using BlogDemo.Core.Entities;
using BlogDemo.Core.interfaces;
using BlogDemo.Infrastructure.Database;

namespace BlogDemo.Infrastructure.Repositories
{
    /// <summary>
    /// 图片上传
    /// </summary>
  public  class PostImageRepository:IPostImageRepository
    {
        private readonly MyContext _myContext;

        public PostImageRepository(MyContext myContext)
        {
            _myContext = myContext;
        }
        public void Add(PostImage postImage)
        {
            _myContext.Add(postImage);
        }
    }
}
