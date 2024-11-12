using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using CarShop.Models.Database;

namespace CarShop.Controllers
{
    [Route("[controller]")]
    public class OrdersController : Controller
    {
        private DataContext _context;

        public OrdersController(DataContext context)
        {
            _context = context;
        }

        // GET: Orders/Index (Zarządzaj Zamówieniami)
        [HttpGet]
        public IActionResult Index()
        {
            if(HttpContext.Session.GetString("role") == "Admin")
            {
                List<Order> orders = _context.Orders.Include(o => o.Car).Include(o => o.User).ToList();
            return View(orders);
            }
            else
            {
                string username = HttpContext.Session.GetString("user");
                List<Order> orders = _context.Orders.Include(o => o.Car).Include(o => o.User).Where(x => x.User.UserName == username).ToList();
                return View(orders);
            }
        }

        // GET: Orders/Create (Formularz składania zamówienia)
        [HttpGet("Create")]
        public IActionResult Create()
        {
            ViewBag.Cars = new SelectList(_context.Cars, "CarId", "Model");
            if(HttpContext.Session.GetString("role") == "Admin")
            {
                ViewBag.Users = new SelectList(_context.Users, "UserId", "UserName");
            }
            else
            {
                string username = HttpContext.Session.GetString("user");
                ViewBag.Users = new SelectList(_context.Users.Where(x => x.UserName == username).ToList(), "UserId", "UserName");
            }
            return View();
        }

        // POST: Orders/Create (Złóż zamówienie)
        [HttpPost("Create")]
        public IActionResult Create(Order order)
        {
            if (ModelState.IsValid)
            {
                _context.Add(order);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            else
            {
                ViewBag.Cars = new SelectList(_context.Cars, "CarId", "Model");
                if(HttpContext.Session.GetString("role") == "Admin")
                {
                    ViewBag.Users = new SelectList(_context.Users, "UserId", "UserName");
                }
                else
                {
                    string username = HttpContext.Session.GetString("user");
                    ViewBag.Users = new SelectList(_context.Users.Where(x => x.UserName == username).ToList(), "UserId", "UserName");
                }
                return View(order);
            }
        }

        // GET: Orders/Edit/5 (Edytuj zamówienie)
        [HttpGet("Edit/{id}")]
        public IActionResult Edit(int? id)
        {
            ViewBag.Cars = new SelectList(_context.Cars, "CarId", "Model");
            if(HttpContext.Session.GetString("role") == "Admin")
            {
                ViewBag.Users = new SelectList(_context.Users, "UserId", "UserName");
            }
            else
            {
                string username = HttpContext.Session.GetString("user");
                ViewBag.Users = new SelectList(_context.Users.Where(x => x.UserName == username).ToList(), "UserId", "UserName");
            }

            Order order = _context.Orders.Find(id);
            if (order == null)
            {
                return NotFound();
            }
            
            return View(order);
        }

        // POST: Orders/Edit/5
        [HttpPost("Edit/{id}")]
        public IActionResult Edit(int id, Order order)
        {
            if (ModelState.IsValid)
            {
                Order orderUpdate = _context.Orders.Find(id);
                orderUpdate.UserId = order.UserId;
                orderUpdate.CarId = order.CarId;
                orderUpdate.OrderDate = order.OrderDate;

                _context.Update(orderUpdate);
                _context.SaveChanges();

                return RedirectToAction(nameof(Index));
            }
            ViewBag.Cars = new SelectList(_context.Cars, "CarId", "Name", order.CarId);
            ViewBag.Users = new SelectList(_context.Users, "UserId", "UserName", order.UserId);
            
            return View(order);
        }

        // POST: Orders/Delete/5
        [HttpPost("Delete")]
        public IActionResult Delete(int id)
        {
            _context.Orders.Where(x => x.OrderId == id).ExecuteDelete();

            return RedirectToAction("Index");
        }


        // POST: Orders/Pay
        [HttpPost("Pay")]
        public IActionResult Pay(int id)
        {
            Payment payment = new Payment {
                OrderId = id,
                PaymentDate = DateTime.Now 
            };

            _context.Add(payment);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

    }
}
