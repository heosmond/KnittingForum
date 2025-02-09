using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using KnittingForum.Models;

namespace KnittingForum.Data
{
    public class KnittingForumContext : DbContext
    {
        public KnittingForumContext (DbContextOptions<KnittingForumContext> options)
            : base(options)
        {
        }

        public DbSet<KnittingForum.Models.Discussion> Discussion { get; set; } = default!;
        public DbSet<KnittingForum.Models.Comment> Comment { get; set; } = default!;
    }
}
