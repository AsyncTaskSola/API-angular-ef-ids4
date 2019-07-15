using System.Collections.Generic;
using System.Threading.Tasks;
using BlogDemo.Core.Entities;

namespace BlogDemo.Core.interfaces
{
    public interface IPostRepository
    {
        //  Task<IEnumerable<Post>> GetAllPostsAsync();
        Task<PaginateList<Post>> GetAllPostsAsync(PostParameters postParameters);
        Task<Post> GetPostByIdAsync(int id);
        void AddPost(Post post);
        void Delete(Post post);

        void Update(Post post);




    }
}