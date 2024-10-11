using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using CarShop.Models.Database;

namespace CarShop.Controllers
{
    public class OrdersController : Controller
    {
        private DataContext _context;

        public OrdersController(DataContext context)
        {
            _context = context;
        }

        // GET: Orders/Index (Zarządzaj Zamówieniami)
        public IActionResult Index()
        {
            List<Order> orders = _context.Orders.Include(o => o.Car).Include(o => o.User).ToList();
            return View(orders);
        }

        // GET: Orders/Create (Formularz składania zamówienia)
        public IActionResult Create()
        {
            ViewBag.Cars = new SelectList(_context.Cars, "CarId", "Model");
            ViewBag.Users = new SelectList(_context.Users, "UserId", "UserName");
            return View();
        }

        // POST: Orders/Create (Złóż zamówienie)
        [HttpPost]
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
                ViewBag.Cars = new SelectList(_context.Cars, "CarId", "Name", order.CarId);
                ViewBag.Users = new SelectList(_context.Users, "UserId", "UserName", order.UserId);
                return View(order);
            }
        }

        // GET: Orders/Edit/5 (Edytuj zamówienie)
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Order order = _context.Orders.Find(id);
            if (order == null)
            {
                return NotFound();
            }
            ViewBag.Cars = new SelectList(_context.Cars, "CarId", "Name", order.CarId);
            ViewBag.Users = new SelectList(_context.Users, "UserId", "UserName", order.UserId);
            return View(order);
        }

        // POST: Orders/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Order order)
        {
            if (id != order.OrderId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {

                    _context.Update(order);
                    _context.SaveChanges();

                return RedirectToAction(nameof(Index));
            }
            ViewBag.Cars = new SelectList(_context.Cars, "CarId", "Name", order.CarId);
            ViewBag.Users = new SelectList(_context.Users, "UserId", "UserName", order.UserId);
            return View(order);
        }
    }
}
