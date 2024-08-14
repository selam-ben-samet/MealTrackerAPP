using MealTrackerAPP.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace MealTracker.Controllers
{
    public class AccountController : Controller
    {
        private readonly ILogger<AccountController> _logger;
        private readonly MealTrackingDbContext _context;

        public AccountController(ILogger<AccountController> logger, MealTrackingDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _context.Users
                    .FirstOrDefaultAsync(u => u.Username == model.Username);

                if (user != null && user.Password == model.Password)
                {
                    // Kullanıcı başarılı bir şekilde giriş yaptı
                    // Kullanıcının oturumunu başlat
                    HttpContext.Session.SetString("Username", user.Username);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Kullanıcı adı veya şifre yanlış.");
                }
            }

            return View(model);
        }

        public IActionResult Logout()
        {
            // Oturumu sonlandır
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }

        public IActionResult AccountDetails()
        {
            // Kullanıcı hesabı sayfası
            if (HttpContext.Session.GetString("Username") != null)
            {
                var username = HttpContext.Session.GetString("Username");
                var user = _context.Users.FirstOrDefault(u => u.Username == username);
                return View(user);
            }
            return RedirectToAction("Login", "Account");
        }
        [HttpPost]
        public IActionResult DeleteAccount()
        {
            var username = HttpContext.Session.GetString("Username");
            if (string.IsNullOrEmpty(username))
            {
                return RedirectToAction("Login", "Account");
            }

            var user = _context.Users
                                .Include(u => u.Meals)
                                .Include(u => u.WaterIntakes)
                                .Include(u => u.PhysicalActivities)
                                .FirstOrDefault(u => u.Username == username);

            if (user != null)
            {
                // Kullanıcıya ait tüm kayıtları sil
                _context.Meals.RemoveRange(user.Meals);
                _context.WaterIntakes.RemoveRange(user.WaterIntakes);
                _context.PhysicalActivities.RemoveRange(user.PhysicalActivities);

                // Kullanıcıyı sil
                _context.Users.Remove(user);
                _context.SaveChanges(); // Veritabanındaki değişiklikleri kaydet

                HttpContext.Session.Clear(); // Kullanıcının oturumunu sonlandır
            }

            return RedirectToAction("Index", "Home");
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new User
                {
                    Username = model.Username,
                    Password = model.Password
                };

                _context.Users.Add(user);
                await _context.SaveChangesAsync();

                return RedirectToAction("Login", "Account");
            }

            return View(model);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
