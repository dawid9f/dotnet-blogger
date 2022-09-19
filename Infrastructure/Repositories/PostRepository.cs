using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class PostRepository : IPostRepository
    {
        private readonly BloggerContext _context;
        public PostRepository(BloggerContext bloggerContext)
        {
            _context = bloggerContext;
        }

        public Post Add(Post newPost)
        {
            _context.Posts.Add(newPost);
            _context.SaveChanges();
            return newPost;
        }

        public void Delete(Post deletePost)
        {
            _context.Posts.Remove(deletePost);
            _context.SaveChanges();
        }

        public IEnumerable<Post> GetAll()
        {
            return _context.Posts;
        }

        public Post GetById(int id)
        {
            return _context.Posts.FirstOrDefault(p => p.Id == id);
        }

        public void Update(Post updatePost)
        {
            _context.Posts.Update(updatePost);
            _context.SaveChanges();
        }
    }
}
