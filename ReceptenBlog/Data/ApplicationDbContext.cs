using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ReceptenBlog.Models;
using ReceptenBlog.Models;

namespace ReceptenBlog.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<ReceptenBlog.Models.Recipe> Recipe { get; set; } = default!;
        public DbSet<ReceptenBlog.Models.Category> Category { get; set; } = default!;
        public DbSet<ReceptenBlog.Models.Comment> Comment { get; set; } = default!;
    }
}
