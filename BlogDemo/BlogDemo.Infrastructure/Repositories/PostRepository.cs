using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlogDemo.Core.Entities;
using BlogDemo.Core.interfaces;
using BlogDemo.Infrastructure.Database;
using BlogDemo.Infrastructure.Exceptions;
using BlogDemo.Infrastructure.Resources;
using BlogDemo.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;

namespace BlogDemo.Infrastructure.Repositories
{
    /// <summary>
    /// 仓储模式
    /// </summary>
    public class PostRepository : IPostRepository
    {
        private readonly MyContext _myContext;
        private readonly IPropertyMappingContainer _propertyMappingContainer;

        public PostRepository(MyContext myContext,
            IPropertyMappingContainer propertyMappingContainer)
        {
            _myContext = myContext;
            _propertyMappingContainer = propertyMappingContainer;
        }

        public async Task<Post> GetPostByIdAsync(int id)
        {
            return await _myContext.Posts.FindAsync(id);
        }

        #region 增，删，改
        /// <summary>
        /// 添加 更改
        /// </summary>
        /// <param name="post"></param>
        public void AddPost(Post post)
        {
            _myContext.Posts.Add(post);
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="post"></param>
        public void Delete(Post post)
        {
            _myContext.Posts.Remove(post);
        }
        /// <summary>
        /// 更改
        /// </summary>
        /// <param name="post">该实体由上下文跟踪并存在于数据库中。 一些或它的所有属性值都已被修改。</param>
        public void Update(Post post)
        {
            _myContext.Entry(post).State = EntityState.Modified;
        }
        #endregion


        #region 这种是没翻页的

        //public async Task<IEnumerable<Post>> GetAllPostsAsync()
        //{
        //    return await _myContext.Posts.ToListAsync();//查出所有数据
        //}

        #endregion

        public async Task<PaginateList<Post>> GetAllPostsAsync(PostParameters postParameters)
        {
            //先排序
            //var query = _myContext.Posts.OrderBy(x => x.Id);

            //过滤
            var query = _myContext.Posts.AsQueryable();
            if (!string.IsNullOrEmpty(postParameters.Title))
            {
                var title = postParameters.Title.ToLowerInvariant();
                query = query.Where(x => x.Title.ToLowerInvariant() == title);
            }
            //容器


            //query = query.OrderBy(x => x.Id);
            // https://localhost:5001/api/posts?pageIndex=0&pageSize=10&orderBy=id 20desc 可以用倒序了 贼难
            query = query.ApplySort(postParameters.OrderBy, _propertyMappingContainer.Resolve<PostResource, Post>());

            var count = await query.CountAsync();
            var data = await query
                .Skip(postParameters.PageIndex * postParameters.PageSize)
                .Take(postParameters.PageSize)
                .ToListAsync();
            return new PaginateList<Post>(postParameters.PageIndex, postParameters.PageSize, count, data);

        }

      
    }
}