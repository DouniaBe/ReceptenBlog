using ReceptenBlog.Data;
using ReceptenBlog.Models;

namespace ReceptenBlog.Services
{
    public interface IMyUser
    {
        public ReceptenblogUser User { get; }
    }

    public class MyUser : IMyUser
    {
        ApplicationDbContext _context;
        IHttpContextAccessor _httpContext;

        public ReceptenblogUser User { get { return GetUser(); } }


        public MyUser(ApplicationDbContext context, IHttpContextAccessor httpContext)
        {
            _context = context;
            _httpContext = httpContext;
        }

        public ReceptenblogUser GetUser()
        {
            string name = _httpContext.HttpContext.User.Identity.Name;
            if (name == null || name == "")
                return Globals.DefaultUser;
            else
                return _context.Users.FirstOrDefault(u => u.UserName == name);
        }
    }
}
