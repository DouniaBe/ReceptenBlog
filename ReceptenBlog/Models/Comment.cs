using ReceptenBlog.Models;
using System.ComponentModel.DataAnnotations;

namespace ReceptenBlog.Models
{
    public class Comment
    {
        public int Id { get; set; }

        [Required]
        public string Content { get; set; } = string.Empty;

        [DataType(DataType.Date)]
        public DateTime DateCreated { get; set; } = DateTime.Now;

        // Foreign key for Recipe
        public int RecipeId { get; set; }
        public Recipe Recipe { get; set; }
    }
}
