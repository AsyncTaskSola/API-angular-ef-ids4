using System.Collections.Generic;
using System.Linq;
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

        public async Task<Post> GetPostByIdAsync(int id)
        {
            return await _myContext.Posts.FindAsync(id);
        }

        public void AddPost(Post post)
        {
            _myContext.Posts.Add(post);
        }

        #region 这种是没翻页的

        //public async Task<IEnumerable<Post>> GetAllPostsAsync()
        //{
        //    return await _myContext.Posts.ToListAsync();//查出所有数据
        //}

        #endregion

        public async Task<IEnumerable<Post>> GetAllPostsAsync(PostParameters postParameters)
        {
           //先排序
            var query = _myContext.Posts.OrderBy(x => x.Id);
            var refult= await query
                .Skip(postParameters.PageIndex * postParameters.PageSize)
                .Take(postParameters.PageSize)
                .ToListAsync();
            return refult;

        }

    }
}