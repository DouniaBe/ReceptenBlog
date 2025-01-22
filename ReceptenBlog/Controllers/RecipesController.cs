using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ReceptenBlog.Data;
using ReceptenBlog.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ReceptenBlog.Controllers
{
    [Authorize(Roles = "User")]
    public class RecipesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RecipesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Recipes
        public async Task<IActionResult> Index()
        {
            // Alleen recepten met de categorieën laden (zonder reacties)
            var recipes = await _context.Recipe.Include(r => r.Category).ToListAsync();
            return View(recipes);
        }

        // GET: Recipes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var recipe = await _context.Recipe
                .Include(r => r.Category)   // Laad de categorie van het recept
                .Include(r => r.Comments)   // Laad de reacties van het recept
                .FirstOrDefaultAsync(m => m.Id == id);

            if (recipe == null)
            {
                return NotFound();
            }

            return View(recipe); // Geef het recept terug, inclusief de reacties
        }

        // GET: Recipes/Create
        public IActionResult Create()
        {
            // Voeg de lijst van categorieën toe aan de view voor dropdown menu
            ViewData["CategoryId"] = new SelectList(_context.Set<Category>(), "Id", "Name");
            return View();
        }

        // POST: Recipes/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description,DateCreated,Ingredients,Instructions,CategoryId,Deleted,Comments")] Recipe recipe)
        {
            if (ModelState.IsValid)
            {
                recipe.DateCreated = DateTime.Now;  // Zet de datum van het recept
                _context.Add(recipe);
                await _context.SaveChangesAsync();  // Sla het recept op in de database
                return RedirectToAction(nameof(Index));  // Redirect naar de lijst van recepten
            }

            // Als de modelstate niet geldig is, geef dan de view opnieuw weer
            ViewData["CategoryId"] = new SelectList(_context.Set<Category>(), "Id", "Name", recipe.CategoryId);
            return View(recipe);
        }

        // GET: Recipes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var recipe = await _context.Recipe.FindAsync(id);
            if (recipe == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(_context.Set<Category>(), "Id", "Name", recipe.CategoryId);
            return View(recipe);
        }

        // POST: Recipes/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,DateCreated,Ingredients,Instructions,Deleted,CategoryId,Comments")] Recipe recipe)
        {
            if (id != recipe.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(recipe);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RecipeExists(recipe.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_context.Set<Category>(), "Id", "Name", recipe.CategoryId);
            return View(recipe);
        }

        // GET: Recipes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var recipe = await _context.Recipe
                .Include(r => r.Category)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (recipe == null)
            {
                return NotFound();
            }

            return View(recipe);
        }

        // POST: Recipes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var recipe = await _context.Recipe.FindAsync(id);
            _context.Recipe.Remove(recipe);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // POST: Add a new comment to a recipe
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddComment(int recipeId, string content)
        {
            if (string.IsNullOrWhiteSpace(content))
            {
                return RedirectToAction("Details", new { id = recipeId });
            }

            // Maak een nieuwe comment aan
            var comment = new Comment
            {
                RecipeId = recipeId,
                Content = content,
                DateCreated = DateTime.Now
            };

            // Voeg de reactie toe aan de database
            _context.Comment.Add(comment);
            await _context.SaveChangesAsync();

            // Redirect terug naar de detailpagina van het recept
            return RedirectToAction("Details", new { id = recipeId });
        }

        private bool RecipeExists(int id)
        {
            return _context.Recipe.Any(e => e.Id == id);
        }
    }
}
