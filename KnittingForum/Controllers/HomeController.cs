using System.Diagnostics;
using KnittingForum.Models;
using Microsoft.AspNetCore.Mvc;
using KnittingForum.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace KnittingForum.Controllers
{
    public class HomeController : Controller
    {
        private readonly KnittingForumContext _context;

        public HomeController(KnittingForumContext context)
        {
            _context = context;
        }

        // Home
        public async Task<IActionResult> Index()
        {
            // get a list of all discussions, including the comments associated with each
            var discussions = await _context.Discussion
                .Include(d => d.ApplicationUser)
                .Include(d => d.Comments)
                .ToListAsync();

            // pass photos object into View
            return View(discussions);
        }

        // Home/GetDiscussion/[id]
        public async Task<IActionResult> GetDiscussion(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var discussion = await _context.Discussion
                .Include(d => d.ApplicationUser)
                .Include(d => d.Comments)
                    .ThenInclude(c => c.ApplicationUser)
                .FirstOrDefaultAsync(d => d.DiscussionId == id);

            return View(discussion);
        }

        // Home/Profile/[id]
        public async Task<IActionResult> Profile(string id)
        {
            if (id.IsNullOrEmpty())
            {
                return NotFound();
            }

            var discussions = await _context.Discussion
                .Include(d => d.ApplicationUser)
                .Where(d => d.ApplicationUserId == id)
                .ToListAsync();

            var user = await _context.Users
                .Where( u => u.Id == id)
                .FirstOrDefaultAsync();

            if (user == null)
            {
                return NotFound();
            }

            return View(new ProfileViewModel(user, discussions));
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
