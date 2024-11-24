using ReceptenBlog.Models;
using System;
using System.ComponentModel.DataAnnotations;

namespace RecipesBlog.Models
{
    public class Recipe
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;

        [Required]
        public string Description { get; set; } = string.Empty;

        [DataType(DataType.Date)]
        public DateTime DateCreated { get; set; } = DateTime.Now;

        [Required]
        public string Ingredients { get; set; } = string.Empty;

        public string Instructions { get; set; } = string.Empty;

        [Display(Name = "Category")]
        public int CategoryId { get; set; }

        public DateTime Deleted { get; set; } = DateTime.MaxValue;

        public Category? Category { get; set; }
    }
}
