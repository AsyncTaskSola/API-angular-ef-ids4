using System.Collections.Generic;
using System.Threading.Tasks;
using BlogDemo.Core.Entities;
using BlogDemo.Core.interfaces;
using BlogDemo.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace BlogDemo.Infrastructure.Repositories
{
    /// <summary>
    /// 仓储模式
    /// </summary>
   public class PostRepository:IPostRepository
    {
        private readonly MyContext _myContext;

        public PostRepository(MyContext myContext)
        {
            _myContext = myContext;
        }

        public void AddPost(Post post)
        {
            _myContext.Posts.Add(post);
        }

        public async Task<IEnumerable<Post>> GetAllPostsAsync()
        {
            return await _myContext.Posts.ToListAsync();//查出所有数据
        }
    }
}