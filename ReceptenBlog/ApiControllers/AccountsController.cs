using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ReceptenBlog.ApiModels;
using ReceptenBlog.Models;
using System.Threading.Tasks;

namespace ReceptenBlog.APIControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly SignInManager<ReceptenblogUser> _signInManager;

        public AccountsController(SignInManager<ReceptenblogUser> signInManager)
        {
            _signInManager = signInManager;
        }

        [HttpPost]
        [Route("/api/login")]
        public async Task<ActionResult<bool>> PostAccount([FromBody] LoginModel login)
        {
            var result = await _signInManager.PasswordSignInAsync(login.Name, login.Password, true, lockoutOnFailure: false);

            return result.Succeeded;
        }
    }
}

