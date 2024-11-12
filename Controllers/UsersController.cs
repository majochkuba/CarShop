using System.Security.Cryptography;
using CarShop.Models;
using CarShop.Models.Database;
using Microsoft.AspNetCore.Mvc;

namespace CarShop.Controllers
{
    [Route("[controller]")]
    public class UsersController : Controller
    {
        private readonly DataContext _context;

        public UsersController(DataContext context)
        {
            _context = context;
        }

        // GET: Users (Panel użytkownika)
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("Logout")]
        public IActionResult Logout()
        {
            HttpContext.Session.Remove("user");
            HttpContext.Session.Remove("role");
            return RedirectToAction("Index", "Home");
        }


        // GET: Users/Login
        [HttpGet("Login")]
        public IActionResult Login()
        {
            return View();
        }

        // POST: Users/Register
        [HttpPost("Register")]
        public IActionResult Register(LoginData model)
        {
            if (ModelState.IsValid)
            {
                bool isUserAlreadyTaken = _context.Users.Any(x => x.UserName == model.UserName);
                if(isUserAlreadyTaken)
                {
                    return Login();
                }
                else
                {
                    HMACSHA256 hmac = new HMACSHA256(System.Text.Encoding.UTF8.GetBytes(""));

                    byte[] hash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(model.Password));
                    User user = new User {
                        UserName = model.UserName,
                        PasswordHash = hash
                    };

                    _context.Users.Add(user);
                    _context.SaveChanges();

                    //sesjsa dla zalogowanego usera
                    HttpContext.Session.SetString("user", user.UserName);
                    HttpContext.Session.SetString("role", "Uzytkownik");

                    return RedirectToAction("Index", "Home");
                }
            }

            return View(model);
        }

        // POST: Users/Login
        [HttpPost("Login")]
        public IActionResult Login(LoginData model)
        {
            if (ModelState.IsValid)
            {
                if(model.UserName == "admin" && model.Password == "admin")
                {
                    HttpContext.Session.SetString("user", model.UserName);
                    HttpContext.Session.SetString("role", "Admin");
                    return RedirectToAction("Index", "Home");
                }

                User user = _context.Users.FirstOrDefault(x => x.UserName == model.UserName);

                if (user == null)
                {
                    return View(model);
                }

                HMACSHA256 hmac = new HMACSHA256(System.Text.Encoding.UTF8.GetBytes(""));
                byte[] hash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(model.Password));

                if(hash.SequenceEqual(user.PasswordHash))
                {
                    HttpContext.Session.SetString("user", model.UserName);
                    HttpContext.Session.SetString("role", "Uzytkownik");
                    return RedirectToAction("Index", "Home");
                }
            }

            return View(model);
        }
    }
}
