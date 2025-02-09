using System.Diagnostics;
using KnittingForum.Models;
using Microsoft.AspNetCore.Mvc;
using KnittingForum.Data;
using Microsoft.EntityFrameworkCore;

namespace KnittingForum.Controllers
{
    public class HomeController : Controller
    {
        private readonly KnittingForumContext _context;

        public HomeController(KnittingForumContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            // get a list of all discussions, including the comments associated with each
            var discussions = await _context.Discussion
                .Include(d => d.Comments)
                .ToListAsync();

            // pass photos object into View
            return View(discussions);
        }

        // pass in key, get discussion from that
        public async Task<IActionResult> GetDiscussion(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var discussion = await _context.Discussion
                .Include(d => d.Comments)
                .FirstOrDefaultAsync(d => d.DiscussionId == id);

            return View(discussion);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
