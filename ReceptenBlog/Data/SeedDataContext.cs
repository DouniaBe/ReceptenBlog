using ReceptenBlog.Models;
using Microsoft.EntityFrameworkCore;
using ReceptenBlog.Data;

namespace RecipesBlog.Data
{
    public class SeedDataContext
    {
        public static void Initialize(ApplicationDbContext context)
        {
            context.Database.EnsureCreated();
            context.Database.Migrate();

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
        }
    }
}
