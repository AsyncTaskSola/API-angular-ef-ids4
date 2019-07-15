using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BlogDemo.Core.Entities;
using BlogDemo.Core.interfaces;
using BlogDemo.Infrastructure.Exceptions;
using BlogDemo.Infrastructure.Resources;
using BlogDemo.Infrastructure.Services;
using BlogDemoApi.Helps;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using PostResource = BlogDemoApi.Resources.PostResource;

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
        private readonly IUrlHelper _urlHelper;
        private readonly ITypeHelperService _typeHelperService;
        private readonly IPropertyMappingContainer _propertyMappingContainer;


        #region 之前写法，后用仓储

        //private readonly MyContext _myContext;

        //public PostController(MyContext myContext)
        //{
        //    _myContext = myContext;
        //}
        #endregion

        public PostController(IPostRepository postRepository,
            IUnitOfWork unitOfWork,
            ILogger<PostController> logger,
            IConfiguration configuration,
            IMapper mapper,
            IUrlHelper urlHelper,
            ITypeHelperService typeHelperService,
            IPropertyMappingContainer propertyMappingContainer)
        {
            _postRepository = postRepository;
            _unitOfWork = unitOfWork;
            _logger = logger;
            _configuration = configuration;
            _mapper = mapper;
            _urlHelper = urlHelper;
            _typeHelperService = typeHelperService;
            _propertyMappingContainer = propertyMappingContainer;

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

        [HttpGet(Name = "GetPosts")]
        //https://localhost:5001/api/posts?pageIndex=3&pageSize=3

        public async Task<IActionResult> Get(PostParameters postParameters,
        [FromHeader(Name = "Accept")]string mediaType)
        {
            //排序字段验证 https://localhost:5001/api/posts?pageIndex=0&pageSize=10&orderBy=id,title&fields=id,count
            if (!_propertyMappingContainer.ValidateMappingExistsFor<BlogDemo.Infrastructure.Resources.PostResource, Post>(postParameters.OrderBy))
            {
                return BadRequest("Can't finds fields for sorting.");
            }
            //写入字段的验证
            if (!_typeHelperService.TypeHasProperties<PostResource>(postParameters.Fields))
            {
                return BadRequest("Fields not  exits");
            }

            //var posts = await _myContext.Posts.ToListAsync();
            var posts = await _postRepository.GetAllPostsAsync(postParameters);
            //映射
            var postresource = _mapper.Map<IEnumerable<Post>, IEnumerable<PostResource>>(posts);
            //https://localhost:5001/api/posts?pageIndex=0&pageSize=10&orderBy=id&filed=id,title

            if (mediaType == "application/vnd.cgzl.hateoas+json")
            {
                var shapedPostResources = postresource.ToDynamicIEnumerable(postParameters.Fields);

                var shapedWithLinks = shapedPostResources.Select(x =>
                {
                    var dict = x as IDictionary<string, object>;
                    var postLinks = CreateLinksForPost((int)dict["Id"], postParameters.Fields);
                    dict.Add("links", postLinks);
                    return dict;
                });


                var links = CreateLinksForPosts(postParameters, posts.HasPrevious, posts.HasNext);
                var resultlink = new
                {

                    value = shapedWithLinks,
                    links

                };

                #region  去掉放到了result里面

                //var previousPageLink = posts.HasPrevious ?
                //    CreatePostUri(postParameters,
                //        PaginationResourceUriType.PreviousPage) : null;
                //var nextPageLink = posts.HasNext ?
                //    CreatePostUri(postParameters,
                //        PaginationResourceUriType.NextPage) : null


                #endregion

                var meta = new
                {
                    posts.PageSize,
                    posts.PageIndex,
                    posts.TotalItemsCount,
                    posts.PageCount,
                    posts.Count
                };
                Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(meta, new JsonSerializerSettings
                {
                    //改成属性名小写
                    ContractResolver = new CamelCasePropertyNamesContractResolver()
                }));

                _logger.LogInformation(_configuration["key"]);
                _logger.LogInformation("Get All Post...");
                //return Ok(postresource);
                // return Ok(shapedPostResources);
                return Ok(resultlink);
            }

            else
            {

                var previousPageLink = posts.HasPrevious ?
                    CreatePostUri(postParameters,
                        PaginationResourceUriType.PreviousPage) : null;
                var nextPageLink = posts.HasNext
                    ? CreatePostUri(postParameters,
                        PaginationResourceUriType.NextPage)
                    : null;
                var meta = new
                {
                    posts.PageSize,
                    posts.PageIndex,
                    posts.TotalItemsCount,
                    posts.PageCount,
                    previousPageLink,
                    nextPageLink,
                    posts.Count

                };

                //这部分相当于把要的参数加入到标头了，显示相关的信息 posts.TotalItemsCount总数，posts.Count当前页面数 重要

                Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(meta, new JsonSerializerSettings
                {
                    //改成属性名小写
                    ContractResolver = new CamelCasePropertyNamesContractResolver()
                }));
                return Ok(postresource.ToDynamicIEnumerable (postParameters.Fields));
            }
        }
        /// <summary>
        /// 单个子段查询
        /// </summary>
        /// <param name="id"></param>
        /// <param name="fields"></param>
        /// <returns></returns>

        [HttpGet("{id}", Name = "GetPost")]
        public async Task<IActionResult> Get(int id, string fields = null)
        {
            if (!_typeHelperService.TypeHasProperties<PostResource>(fields))
            {
                return BadRequest("Fields not  exits");
            }

            var posts = await _postRepository.GetPostByIdAsync(id);
            if (posts == null)
            {
                return NotFound();
            }
            //https://localhost:5001/api/posts/1?felds=id
            var postresource = _mapper.Map<Post, PostResource>(posts);

            var shapePostResource = postresource.ToDynamic(fields);
            var links = CreateLinksForPost(id, fields);
            var result = shapePostResource as IDictionary<string, object>;
            result.Add("Links", links);

            //return Ok(shapePostResource);
            return Ok(result);
        }

        #region 之前写的post
        //[HttpPost]
        //public async Task<IActionResult> Post()
        //{
        //    var newPost = new Post
        //    {
        //        Author = "admin",
        //        Boby = "32123123123",
        //        Title = "Title A",
        //        LastModified = DateTime.Now
        //    };
        //    _postRepository.AddPost(newPost);
        //    await _unitOfWork.SaveAsync();
        //    return Ok();
        //}

        #endregion

        //第十讲 7.11post添加  7.12model问题 7.15为了angular 的改动
        [HttpPost(Name = "GreatePost")]
        public async Task<IActionResult> Post([FromBody] PostAddResource postAddResource)
        {
            if (postAddResource == null)
            {
                return BadRequest();
            }
            if(!ModelState.IsValid)
            {
                //return UnprocessableEntity(ModelState);
                return new MyUnprocessableEntityObjectResult(ModelState);
            }

            var newPost = _mapper.Map<PostAddResource, Post>(postAddResource);
            newPost.Author = "admin";
            newPost.LastModified=DateTime.Now;
            //这里是个坑，因为之前迁移表的时候PostConfiguration类中设置了IsRequired()的子段导致了这个值现在不能为空
            newPost.Remark = "这里是个坑，因为之前迁移表的时候PostConfiguration类中设置了IsRequired()的子段导致了这个值现在不能为空";
                _postRepository.AddPost(newPost);
            if (!await _unitOfWork.SaveAsync())
            {
                throw new Exception("save failed");
            }
            var resultReosurece= _mapper.Map<Post, PostResource>(newPost);
            var links = CreateLinksForPost(newPost.Id);
            var linkedPostResource = resultReosurece.ToDynamic() as IDictionary<string, object>;
            linkedPostResource.Add("Link",links);
            
           // return Ok(resultPost);
            return CreatedAtRoute("GetPost", new {id = linkedPostResource["id"]}, linkedPostResource);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id">删除id</param>
        /// <returns></returns>

        [HttpDelete("{id}", Name = "DeletePost")]
        public async Task<IActionResult> DeletePost(int id)
        {
            var post = await _postRepository.GetPostByIdAsync(id);
            if(post==null)
            {
                return NotFound();
            }
            _postRepository.Delete(post);
            if(!await _unitOfWork.SaveAsync())
            {
                throw new Exception($"Deleting post {id} failed when saving");
            }
            return NoContent();
        }
        /// <summary>
        /// 全体更新put 少用 这里的name没用
        /// </summary>
        /// <param name="id"></param>
        /// <param name="postUpdate"></param>
        /// <returns></returns>
        [HttpPut("{id}", Name = "UpdatePost")]
        public async Task<IActionResult> UpdatePost(int id,[FromBody]PostUpdateResource postUpdate)
        {
            if(postUpdate==null)
            {
                return BadRequest();
            }
            if(!ModelState.IsValid)
            {
                return new MyUnprocessableEntityObjectResult(ModelState);
            }
            var post = await _postRepository.GetPostByIdAsync(id);
            if(post==null)
            {
                return NotFound();
            }
            post.LastModified = DateTime.Now;
            _mapper.Map(postUpdate, post);
            if (!await _unitOfWork.SaveAsync())
            {
                throw new Exception($"Deleting post {id} failed when saving");
            }
            return NoContent();
        }
        [HttpPatch("{id}",Name = "PartiallyUpdate")]
        public async Task<IActionResult> PartiallyUpdateCityForCountry(int id,[FromBody]JsonPatchDocument<PostUpdateResource> patchDoc)
        {
            if(patchDoc==null)
            {
                return BadRequest();
            }
            var post = await _postRepository.GetPostByIdAsync(id);
            if(post==null)
            {
                return NotFound();
            }
            var postTopatch = _mapper.Map<PostUpdateResource>(post);
            patchDoc.ApplyTo(postTopatch, ModelState);
            TryValidateModel(postTopatch);//手动验证
            if (!ModelState.IsValid)
            {
                return new MyUnprocessableEntityObjectResult(ModelState);
            }
            _mapper.Map(postTopatch, post); 
            post.LastModified = DateTime.Now;
            _postRepository.Update(post);
            if (!await _unitOfWork.SaveAsync())
            {
                throw new Exception($"Deleting post {id} failed when saving");
            }
            return NoContent();

        }

        /// <summary>
        /// 用在翻页的
        /// </summary>
        /// <param name="parameters"></param>
        /// <param name="uriType"></param>
        /// <returns></returns>
        private string CreatePostUri(PostParameters parameters, PaginationResourceUriType uriType)
        {
            switch (uriType)
            {
                case PaginationResourceUriType.PreviousPage:
                    var previousParameters = new
                    {
                        pageIndex = parameters.PageIndex - 1,
                        pageSize = parameters.PageSize,
                        orderBy = parameters.OrderBy,
                        fields = parameters.Fields,
                        title = parameters.Title
                    };
                    return _urlHelper.Link("GetPosts", previousParameters);
                case PaginationResourceUriType.NextPage:
                    var nextParameters = new
                    {
                        pageIndex = parameters.PageIndex + 1,
                        pageSize = parameters.PageSize,
                        orderBy = parameters.OrderBy,
                        fields = parameters.Fields,
                        title = parameters.Title
                    };
                    return _urlHelper.Link("GetPosts", nextParameters);
                default:
                    var currentParameters = new
                    {
                        pageIndex = parameters.PageIndex,
                        pageSize = parameters.PageSize,
                        orderBy = parameters.OrderBy,
                        fields = parameters.Fields,
                        title = parameters.Title
                    };
                    return _urlHelper.Link("GetPosts", currentParameters);
            }
        }
        /// <summary>
        /// HATEOAS Uri 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="fields"></param>
        /// <returns></returns>
        private IEnumerable<LinkResource> CreateLinksForPost(int id, string fields = null)
        {
            var links = new List<LinkResource>();

            if (string.IsNullOrWhiteSpace(fields))
            {
                links.Add(
                    new LinkResource(
                        _urlHelper.Link("GetPost", new { id }), "self", "GET"));
            }
            else
            {
                links.Add(
                    new LinkResource(
                        _urlHelper.Link("GetPost", new { id, fields }), "self", "GET"));
            }

            links.Add(
                new LinkResource(
                    _urlHelper.Link("DeletePost", new { id }), "delete_post", "DELETE"));

            return links;
        }

        /// <summary>
        /// 针对翻页
        /// </summary>
        /// <param name="postResourceParameters"></param>
        /// <param name="hasPrevious"></param>
        /// <param name="hasNext"></param>
        /// <returns></returns>
        private IEnumerable<LinkResource> CreateLinksForPosts(PostParameters postResourceParameters,
            bool hasPrevious, bool hasNext)
        {
            var links = new List<LinkResource>
            {
                new LinkResource(
                    CreatePostUri(postResourceParameters, PaginationResourceUriType.CurrentPage),
                    "self", "GET")
            };

            if (hasPrevious)
            {
                links.Add(
                    new LinkResource(
                        CreatePostUri(postResourceParameters, PaginationResourceUriType.PreviousPage),
                        "previous_page", "GET"));
            }

            if (hasNext)
            {
                links.Add(
                    new LinkResource(
                        CreatePostUri(postResourceParameters, PaginationResourceUriType.NextPage),
                        "next_page", "GET"));
            }

            return links;
        }
    }
}
