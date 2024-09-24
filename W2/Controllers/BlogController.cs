using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using W2.Models;
using W2.Repositories;

public class BlogController : Controller
{
    private readonly IBlogRepository _blogRepository;

    public BlogController(IBlogRepository blogRepository)
    {
        _blogRepository = blogRepository;
    }

    // GET: /Blog/
    [AllowAnonymous] // Allow anyone to view the blogs
    public async Task<IActionResult> Index()
    {
        var blogs = await _blogRepository.GetAllBlogsAsync();
        return View(blogs);
    }

    // GET: /Blog/Details/5
    [AllowAnonymous] // Allow anyone to view blog details
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
    [Authorize(Roles = "Admin")] // Only Admin can create
    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    // POST: /Blog/Create
    [Authorize(Roles = "Admin")] // Only Admin can create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(BlogPost blog)
    {
        if (ModelState.IsValid)
        {
            await _blogRepository.AddBlogAsync(blog);
            return RedirectToAction(nameof(Index));
        }
        return View(blog);
    }

    // GET: /Blog/Edit/5
    [Authorize(Roles = "Admin")] // Only Admin can edit
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
    [Authorize(Roles = "Admin")] // Only Admin can edit
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
            await _blogRepository.UpdateBlogAsync(blog);
            return RedirectToAction(nameof(Index));
        }
        return View(blog);
    }

    // GET: /Blog/Delete/5
    [Authorize(Roles = "Admin")] // Only Admin can delete
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
    [Authorize(Roles = "Admin")] // Only Admin can delete
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        await _blogRepository.DeleteBlogAsync(id);
        return RedirectToAction(nameof(Index));
    }
}
