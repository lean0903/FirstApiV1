using Microsoft.EntityFrameworkCore;
using SocialMedia.Core.Entities;
using SocialMedia.Core.Interfaces;
using SocialMedia.Infrastucture.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialMedia.Infrastucture.Repositories
{
    public class PostRepository : IPostRepository
    {
        private readonly SocialMediaContext _context;
        public PostRepository(SocialMediaContext context)
        {
        _context=context;
        }
        public async Task<IEnumerable<Post>> GetPosts()
        {
            var posts =await _context.Posts.ToListAsync();
            return posts;              
        }
        public async  Task<Post> GetPost(int id)
        {
           
            return await _context.Posts.FirstOrDefaultAsync(x => x.PostId == id); 
        }

        public async Task InsertPost(Post post)

        {
            _context.Posts.Add(post);
            await _context.SaveChangesAsync();

        }

        public async Task<bool> UpdatePost(Post post)
        {
            var currentPost= await this.GetPost(post.PostId);
            currentPost.Image = post.Image;
            currentPost.Description = post.Description;
            currentPost.Date = post.Date;
            var row = await _context.SaveChangesAsync();//devuelve las cantidad de rows modificadas
            return row > 0;
        }
           

        
        public async Task<bool> DeletePost(int postId)
        {
            var currentPost = await this.GetPost(postId);
            _context.Remove(currentPost);
            return await _context.SaveChangesAsync() > 0;//devuelve las cantidad de rows elimininadas
        }
    }
}

