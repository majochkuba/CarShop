using Microsoft.AspNetCore.Mvc;

namespace CarShop.Controllers
{
    public class UsersController : Controller
    {
        private readonly DataContext _context;

        public UsersController(DataContext context)
        {
            _context = context;
        }

        // GET: Users/Profile (Panel użytkownika)
        public IActionResult Profile(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = _context.Users.Find(id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }
    }
}
