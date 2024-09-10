using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using W2.Models;
using W2.Repositories;

namespace W2.Controllers
{
    [Authorize(Roles = "Admin")]
    public class BlogController : Controller
    {
        private readonly IBlogRepository _blogRepository;
        private readonly UserManager<ApplicationUser> _userManager;

        public BlogController(IBlogRepository blogRepository, UserManager<ApplicationUser> userManager)
        {
            _blogRepository = blogRepository;
            _userManager = userManager;
        }

        // GET: /Blog/
        public async Task<IActionResult> Index()
        {
            var blogs = await _blogRepository.GetAllBlogsAsync();
            return View(blogs);
        }

        // GET: /Blog/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var blog = await _blogRepository.GetBlogByIdAsync(id);
            if (blog == null)
            {
                return NotFound();
            }
            return View(blog);
        }

        // GET: /Blog/Create
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // POST: /Blog/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BlogPost blog)
        {
            if (ModelState.IsValid)
            {
                blog.AuthorId = _userManager.GetUserId(User);
                await _blogRepository.AddBlogAsync(blog);
                return RedirectToAction(nameof(Index));
            }
            return View(blog);
        }

        // GET: /Blog/Edit/5
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var blog = await _blogRepository.GetBlogByIdAsync(id);
            if (blog == null)
            {
                return NotFound();
            }
            return View(blog);
        }

        // POST: /Blog/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, BlogPost blog)
        {
            if (id != blog.Id)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _blogRepository.UpdateBlogAsync(blog);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await BlogExists(blog.Id))
                    {
                        return NotFound();
                    }
                    throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(blog);
        }

        // GET: /Blog/Delete/5
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var blog = await _blogRepository.GetBlogByIdAsync(id);
            if (blog == null)
            {
                return NotFound();
            }
            return View(blog);
        }

        // POST: /Blog/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _blogRepository.DeleteBlogAsync(id);
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> BlogExists(int id)
        {
            return await _blogRepository.GetBlogByIdAsync(id) != null;
        }
    }
}
