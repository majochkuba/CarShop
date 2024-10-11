using CarShop.Models.Database;
using Microsoft.AspNetCore.Mvc;

namespace CarShop.Controllers
{
    public class CarsController : Controller
    {
        private DataContext _context;

        public CarsController(DataContext context)
        {
            _context = context;
        }

        public IActionResult Index(string searchString)
        {
            List<Car> cars = _context.Cars.ToList();

            if (!string.IsNullOrEmpty(searchString))
            {
                cars = cars.Where(car => car.Make.Contains(searchString) || car.Model.Contains(searchString)).ToList();
            }

            return View(cars);
        }

        // GET: Cars/Create (Formularz dodawania samochodu)
        public IActionResult Create()
        {
            return View();
        }

        // POST: Cars/Create (Dodanie samochodu)
        [HttpPost]
        public IActionResult Create(Car car)
        {
            if (ModelState.IsValid)
            {
                _context.Add(car);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index)); // Po dodaniu samochodu wracamy do listy samochodów
            }
            return View(car);
        }
    }
}
