using System.Collections.Generic;
using System.Reflection.Metadata;
using System.Threading.Tasks;
using W2.Models;

namespace W2.Repositories
{
    public interface IBlogRepository
    {
        Task<IEnumerable<BlogPost>> GetAllBlogsAsync();
        Task<BlogPost> GetBlogByIdAsync(int id);
        Task<IEnumerable<BlogPost>> GetBlogsByAuthorAsync(string authorId);
        Task AddBlogAsync(BlogPost blog);
        Task UpdateBlogAsync(BlogPost blog);
        Task DeleteBlogAsync(int id);
    }
}
