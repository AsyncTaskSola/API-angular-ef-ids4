using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BlogDemo.Core.Entities;
using BlogDemo.Core.interfaces;
using BlogDemo.Infrastructure.Resources;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlogDemoApi.Controllers
{
    [Route("api/postimages")]
    public class PostImageController:Controller
    {
        private readonly IPostImageRepository _postImageRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IHostingEnvironment _hostingEnvironment;

        public PostImageController(IPostImageRepository postImageRepository,
            IUnitOfWork unitOfWork,
            IMapper mapper,
            IHostingEnvironment hostingEnvironment)
        {
            _postImageRepository = postImageRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _hostingEnvironment = hostingEnvironment;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Upload(IFormFile file)
        {
            if (file == null)
            {
                return BadRequest("File is null");
            }

            if (file.Length == 0)
            {
                return BadRequest("File is empty");
            }

            if (file.Length > 10 * 1024 * 1024)
            {
                return BadRequest("File size cannot exceed 10M");
            }

            var acceptTypes = new[] { ".jpg", ".jpeg", ".png" };
            if (acceptTypes.All(t => t != Path.GetExtension(file.FileName).ToLower()))
            {
                return BadRequest("File type not valid, only jpg and png are acceptable.");
            }

            if (string.IsNullOrWhiteSpace(_hostingEnvironment.WebRootPath))
            {
                _hostingEnvironment.WebRootPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
            }

            var uploadsFolderPath = Path.Combine(_hostingEnvironment.WebRootPath, "uploads");
            if (!Directory.Exists(uploadsFolderPath))
            {
                Directory.CreateDirectory(uploadsFolderPath);
            }

            var fileName = Guid.NewGuid() + Path.GetExtension(file.FileName);
            var filePath = Path.Combine(uploadsFolderPath, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            var postImage = new PostImage
            {
                FileName = fileName
            };

            _postImageRepository.Add(postImage);

            await _unitOfWork.SaveAsync();

            var result = _mapper.Map<PostImage, PostImageResource>(postImage);

            return Ok(result);
        }
    }
}
