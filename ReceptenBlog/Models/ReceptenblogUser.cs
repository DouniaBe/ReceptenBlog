using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace ReceptenBlog.Models
{
    public class ReceptenblogUser : IdentityUser 
    {
   
        [Required]
        public string FirstName { get; set; }

        [Required] 
        public string LastName { get; set; }
    }
}
