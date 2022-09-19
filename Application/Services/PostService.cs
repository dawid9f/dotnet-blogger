using Application.Dto;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class PostService : IPostService
    {
        private readonly IPostRepository _postRepository;
        private readonly IMapper _mapper;

        public PostService(IPostRepository postRepository, IMapper mapper)
        {
            _postRepository = postRepository;
            _mapper = mapper;
        }

        public PostDto AddNewPost(CreatePostDto newPost)
        {
            if (string.IsNullOrEmpty(newPost.Title))
            {
                throw new Exception("Brak tytułu");
            }
            var post = _mapper.Map<Post>(newPost);
            _postRepository.Add(post);
            return _mapper.Map<PostDto>(post);
        }

        public void DeletePost(int id)
        {
            var post = _postRepository.GetById(id);
            _postRepository.Delete(post);
        }

        public IEnumerable<PostDto> GetAllPosts()
        {
            return _mapper.Map<IEnumerable<PostDto>>(_postRepository.GetAll());
        }

        public PostDto GetPostById(int id)
        {
            return _mapper.Map<PostDto>(_postRepository.GetById(id));
        }

        public void UpdatePost(UpdatePostDto updatePostDto)
        {
            var exPost = _postRepository.GetById(updatePostDto.Id);
            var post = _mapper.Map(updatePostDto, exPost);
            _postRepository.Update(post);
        }
    }
}
