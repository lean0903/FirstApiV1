using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SocialMedia.Apiv2.Responses;
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
        public  IActionResult GetPosts()
        {
            var posts =  _postService.GetPosts();
            var postsDTO = _mapper.Map<IEnumerable<PostDTO>>(posts);
            var response = new ApiResponse<IEnumerable<PostDTO>>(postsDTO);
            return Ok(response);
        }
        [HttpGet("{Id}")]
        public async Task<IActionResult>GetPost(int id)
        {
            var post = await _postService.GetPost(id);
            var postsDTO = _mapper.Map<PostDTO>(post);
           var response = new ApiResponse<PostDTO>(postsDTO);
            return Ok(response);
        }
        [HttpPost]
        public async Task<IActionResult> Post(PostDTO postDTO)
        {

           var post = _mapper.Map<Post>(postDTO);
            await _postService.InsertPost(post);
            postDTO = _mapper.Map<PostDTO>(post);//se le asigna el id
            var response = new ApiResponse<PostDTO>(postDTO);
            return Ok(response);
        } 
        [HttpPut]
        public async Task<IActionResult>Put (PostDTO postDTO)
        {
            
            var post = _mapper.Map<Post>(postDTO);
            var result = await _postService.UpdatePost(post);
            var response = new ApiResponse<bool>(result);
            return Ok();
        }
        [HttpDelete("{idPost}")]
        public async Task<IActionResult> Delete(int idPost)
        {
            var result = await _postService.DeletePost(idPost);
            var response = new ApiResponse<bool>(result);
            return Ok(response); 

        }




    }
}