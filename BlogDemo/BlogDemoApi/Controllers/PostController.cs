using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using BlogDemo.Core.Entities;
using BlogDemo.Core.interfaces;
using BlogDemo.Infrastructure.Database;
using BlogDemoApi.Resources;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace BlogDemoApi.Controllers
{
    [Route("api/posts")]
    public class PostController : Controller
    {
        private readonly IPostRepository _postRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<PostController> _logger;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        #region 之前写法，后用仓储

        //private readonly MyContext _myContext;

        //public PostController(MyContext myContext)
        //{
        //    _myContext = myContext;
        //}
        #endregion

        public PostController(IPostRepository postRepository,
            IUnitOfWork unitOfWork,
            ILogger<PostController>logger,
            IConfiguration configuration,
            IMapper mapper)
        {
            _postRepository = postRepository;
            _unitOfWork = unitOfWork;
            _logger = logger;
            _configuration = configuration;
            _mapper = mapper;
        }



        #region 这种是没翻页的

        //[HttpGet]

        //public async Task<IActionResult> Get()
        //{
        //    //var posts = await _myContext.Posts.ToListAsync();
        //    var posts = await _postRepository.GetAllPostsAsync();
        //    //映射
        //    var postresource = _mapper.Map<IEnumerable<Post>, IEnumerable<PostResource>>(posts);

        //    _logger.LogInformation(_configuration["key"]);
        //    _logger.LogInformation("Get All Post...");
        //    return Ok(postresource);
        //}

        #endregion

        [HttpGet]

        public async Task<IActionResult> Get(PostParameters postParameters)
        {
            //var posts = await _myContext.Posts.ToListAsync();
            var posts = await _postRepository.GetAllPostsAsync(postParameters);
            //映射
            var postresource = _mapper.Map<IEnumerable<Post>, IEnumerable<PostResource>>(posts);

            _logger.LogInformation(_configuration["key"]);
            _logger.LogInformation("Get All Post...");
            return Ok(postresource);
        }



        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var posts = await _postRepository.GetPostByIdAsync(id);
            if (posts == null)
            {
                return NotFound();
            }
            var postresource = _mapper.Map<Post, PostResource>(posts);
            return Ok(postresource);
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
