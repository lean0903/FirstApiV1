﻿using SocialMedia.Core.Entities;
using SocialMedia.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialMedia.Infrastucture.Repositories
{
    public class PostRepository : IPostRepository
    {
        public async Task<IEnumerable<Post>> GetPosts()
        {
            var posts = Enumerable.Range(1, 10).Select(x => new Post
            {
                PostId = x,
                Descripcion = $"Description Mongo {x}",
                Date = DateTime.Now,
                Image = $"https://www.misapi.com/{x}",
                UserId=x*2
            });
            await Task.Delay(10);
            return posts;
                
        }
    }
}