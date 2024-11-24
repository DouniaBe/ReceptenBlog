using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RecipesBlog.Models;

namespace ReceptenBlog.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<RecipesBlog.Models.Recipe> Recipe { get; set; } = default!;
        public DbSet<RecipesBlog.Models.Category> Category { get; set; } = default!;
    }
}
