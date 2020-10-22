using SocialMedia.Core.Entities;
using SocialMedia.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.Core.Services
{
    public class PostService : IPostService
    {
        //private readonly IPostRepository _postRepository; se cambia ea uno generico
        //private readonly IUserRepository _userRepository;

        //private readonly IRepository<Post> _postRepository;se cambia por un UNitOfWork (engloba todos los repositorios)
        //private readonly IRepository<User> _userRepository;

        private readonly IUnitOfWork _unitOfWork;

        public PostService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> DeletePost(int postId)
        {
            await _unitOfWork.PostRepository.Delete(postId);
            return true;
        }

        public async Task<Post> GetPost(int id)
        {
            return await _unitOfWork.PostRepository.GetById(id);
        }

        public  IEnumerable<Post> GetPosts()
        {
            return  _unitOfWork.PostRepository.GetAll();

        }

        public async Task InsertPost(Post post)
         {
            var user = await _unitOfWork.PostRepository.GetById(post.UserId);
            if (user == null)
            {
                throw new Exception("el usuario no es valido");
            }
            if(post.Description.Contains("sexo"))
            {
                throw new Exception("the description contain sexo");
            }

            await _unitOfWork.PostRepository.Add(post);
        }

        public async Task <bool> UpdatePost(Post post)

        {
             _unitOfWork.PostRepository.Update(post);
             await _unitOfWork.saveChangesAsync();
             return true;

        }
    }
}
