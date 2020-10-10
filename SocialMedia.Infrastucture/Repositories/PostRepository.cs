﻿using Microsoft.EntityFrameworkCore;
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
    }
}

