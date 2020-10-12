using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SocialMedia.Core.DTOs;
using SocialMedia.Core.Entities;
using SocialMedia.Core.Interfaces;

namespace SocialMedia.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {

        private readonly IMapper _mapper;
        private readonly IPostService _postService;
        public PostController(IMapper map,IPostService service)
        {
            _postService = service;
            _mapper = map;
        }
        [HttpGet]
        public async Task<IActionResult> GetPosts()
        {
            var posts = await _postService.GetPosts();
            var postsDTO = _mapper.Map<IEnumerable<PostDTO>>(posts);
            return Ok(postsDTO);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult>GetPost(int id)
        {
            var post = await _postService.GetPost(id);
            var postsDTO = _mapper.Map<PostDTO>(post);

            return Ok(postsDTO);
        }
        [HttpPost]
        public async Task<IActionResult> Post(PostDTO postDTO)
        {
            var post = _mapper.Map<Post>(postDTO);
           await _postService.InsertPost(post);
            return Ok(postDTO);
        } 
        [HttpPut]
        public async Task<IActionResult>Put (PostDTO postDTO)
        {
            var post = _mapper.Map<Post>(postDTO);
            return Ok(await _postService.UpdatePost(post));
        }
        [HttpDelete("{idPost}")]
        public async Task<IActionResult> Delete(int idPost)
        {
            return Ok(await _postService.DeletePost(idPost));
        }




    }
}