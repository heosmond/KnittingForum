using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using KnittingForum.Data;
using KnittingForum.Models;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Azure;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace KnittingForum.Controllers
{
    [Authorize]
    public class CommentsController : Controller
    {
        private readonly KnittingForumContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public CommentsController(KnittingForumContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // Deleted the following:
        // GET: Comments
        // GET: Comments/Details/5
        // GET: Comments/Edit/5
        // POST: Comments/Edit/5
        // GET: Comments/Delete/5
        // POST: Comments/Delete/5


        // ORIGINAL CREATE FUNCTION
        //public IActionResult Create()
        //{
        //    ViewData["DiscussionId"] = new SelectList(_context.Discussion, "DiscussionId", "DiscussionId");
        //    return View();
        //}

        // take in ID from URL
        // GET: Comments/Create/[id]
        public IActionResult Create(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ViewData["DiscussionId"] = id;

            return View();
        }

        // POST: Comments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CommentId,Content,CreateDate,DiscussionId")] Comment comment)
        {
            comment.ApplicationUserId = _userManager.GetUserId(User);

            if (ModelState.IsValid)
            {
                _context.Add(comment);
                await _context.SaveChangesAsync();

                return RedirectToAction("GetDiscussion", "Home", new { id = comment.DiscussionId });
            }

            ViewData["DiscussionId"] = comment.DiscussionId;

            return View(comment);
        }

       private bool CommentExists(int id)
        {
            return _context.Comment.Any(e => e.CommentId == id);
        } 
    }
}
