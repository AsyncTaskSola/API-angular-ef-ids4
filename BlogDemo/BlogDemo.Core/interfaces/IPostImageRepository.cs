
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using BlogDemo.Core.Entities;

namespace BlogDemo.Core.interfaces
{
    public interface IPostImageRepository
    {
        void Add(PostImage postImage);
    }
}