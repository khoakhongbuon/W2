using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using W2.Data;
using W2.Models;
using W2.Repositories;

namespace W2.Repositories
{
    public class BlogRepository : IBlogRepository
    {
        private readonly ApplicationDbContext _context;

        public BlogRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<BlogPost>> GetAllBlogsAsync()
        {
            return await _context.Blogs.Include(b => b.Author).ToListAsync();
        }

        public async Task<BlogPost> GetBlogByIdAsync(int id)
        {
            return await _context.Blogs.Include(b => b.Author).FirstOrDefaultAsync(b => b.Id == id);
        }

        public async Task<IEnumerable<BlogPost>> GetBlogsByAuthorAsync(string authorId)
        {
            return await _context.Blogs.Include(b => b.Author).Where(b => b.AuthorId == authorId).ToListAsync();
        }

        public async Task AddBlogAsync(BlogPost blog)
        {
            _context.Blogs.Add(blog);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateBlogAsync(BlogPost blog)
        {
            _context.Blogs.Update(blog);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteBlogAsync(int id)
        {
            var blog = await _context.Blogs.FindAsync(id);
            if (blog != null)
            {
                _context.Blogs.Remove(blog);
                await _context.SaveChangesAsync();
            }
        }
    }
}
