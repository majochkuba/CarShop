using CarShop.Models;
using CarShop.Models.Database;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace CarShop.Controllers
{
    [Route("[controller]")]
    public class CarsController : Controller
    {
        private DataContext _context;
        private HttpClient _httpClient;

        public CarsController(DataContext context, HttpClient httpClient)
        {
            _context = context;
            _httpClient = httpClient;
        }


        [HttpGet]
        public async Task<IActionResult> Index(string searchString)
        {
            List<Car> cars = _context.Cars.ToList();
            var response = await _httpClient.GetAsync("https://vpic.nhtsa.dot.gov/api/vehicles/GetMakesForVehicleType/car?format=json");

            var apiContent = await response.Content.ReadAsStringAsync();
            var apiVechicles = JsonConvert.DeserializeObject<VechiclesModel>(apiContent);

            if (!string.IsNullOrEmpty(searchString))
            {
                cars = cars.Where(car => car.Make.ToLower().Contains(searchString.ToLower()) || car.Model.ToLower().Contains(searchString.ToLower())).ToList();
                apiVechicles.Results = apiVechicles.Results.Where(car => car.MakeName.ToLower().Contains(searchString.ToLower())).ToList();
            }

            return View(new CarsViewModel {
                Cars = cars,
                VechiclesFromApi = apiVechicles
            });
        }

        // GET: Cars/Create (Formularz dodawania samochodu)
        [HttpGet("Create")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Cars/Create (Dodanie samochodu)
        [HttpPost("Create")]
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
