using System;
using System.Threading.Tasks;
using BlogDemo.Core.Entities;
using BlogDemo.Core.interfaces;
using BlogDemo.Infrastructure.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlogDemoApi.Controllers
{
    [Route("api/posts")]
    public class PostController : Controller
    {
        private readonly IPostRepository _postRepository;
        private readonly IUnitOfWork _unitOfWork;

        #region 之前写法，后用仓储

        //private readonly MyContext _myContext;

        //public PostController(MyContext myContext)
        //{
        //    _myContext = myContext;
        //}
        #endregion

        public PostController(IPostRepository postRepository,IUnitOfWork unitOfWork)
        {
            _postRepository = postRepository;
            _unitOfWork = unitOfWork;
        }


        [HttpGet]
        public async Task<IActionResult> Get()
        {
            //var posts = await _myContext.Posts.ToListAsync();
            var posts = await _postRepository.GetAllPostsAsync();
            return Ok(posts);
        }

        [HttpPost]
        public async Task<IActionResult> Post()
        {
            var newPost = new Post
            {
                Author = "admin",
                Boby = "32123123123",
                Title = "Title A",
                LastModified = DateTime.Now
            };
            _postRepository.AddPost(newPost);
            await _unitOfWork.SaveAsync();
            return Ok();
        }
    }
}
