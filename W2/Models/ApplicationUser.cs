using Microsoft.AspNetCore.Identity;
using System.Reflection.Metadata;

namespace W2.Models
{
    public class ApplicationUser : IdentityUser
    {
        public virtual ICollection<BlogPost> Blogs { get; set; }
    }
}
