using ReceptenBlog.Models;
using Microsoft.EntityFrameworkCore;
using ReceptenBlog.Data;
using Microsoft.AspNetCore.Identity;

namespace ReceptenBlog.Data
{
    public class SeedDataContext
    {
        public static void Initialize(ApplicationDbContext context)
        {
            context.Database.EnsureCreated();
            context.Database.Migrate();

            if (!context.Language.Any())
            {
                context.Language.AddRange(
                    new Language(),
                    new Language { Code = "en", IsSystemLanguage = true, Name = "English" },
                    new Language { Code = "fr", IsSystemLanguage = true, Name = "français" },
                    new Language { Code = "nl", IsSystemLanguage = true, Name = "Nederlands" }
                    );
                context.SaveChanges();
            }

            Language.Languages = context.Language.Where(l => l.IsSystemLanguage && l.Code != "? ").ToList();


            if (!context.Category.Any())
            {
                context.Category.AddRange(
                    new Category { Name = "Appetizers", Description = "Delicious starters" },
                    new Category { Name = "Main Course", Description = "Hearty meals" },
                    new Category { Name = "Desserts", Description = "Sweet treats" }
                );
                context.SaveChanges();
            }

            if (!context.Recipe.Any())
            {
                var defaultCategory = context.Category.FirstOrDefault(c => c.Name == "Main Course");
                context.Recipe.AddRange(
                    new Recipe { Name = "Spaghetti Bolognese", Description = "A classic Italian dish", Ingredients = "Spaghetti, minced beef, tomato sauce", Instructions = "Cook spaghetti, prepare sauce, combine" },
                    new Recipe { Name = "Chocolate Cake", Description = "Rich and moist cake", Ingredients = "Flour, cocoa, sugar, eggs, butter", Instructions = "Mix ingredients, bake at 180°C" }
                );
                context.SaveChanges();
            }
           

            if (!context.Roles.Any())
            {
                context.Roles.AddRange(
                    new IdentityRole { Id = "User", Name = "User", NormalizedName = "USER" },
                    new IdentityRole { Id = "UserAdmin", Name = "UserAdmin", NormalizedName = "USERADMIN" }
                    );
                context.SaveChanges();
                context.UserRoles.Add(new IdentityUserRole<string> { RoleId = "User", UserId = "?" });
              
            }
            context.SaveChanges();
        }
    }
}
