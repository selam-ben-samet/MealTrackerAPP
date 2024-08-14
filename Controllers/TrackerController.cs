using MealTrackerAPP.Models;
using MealTrackerAPP.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace MealTracker.Controllers
{
    public class TrackerController : Controller
    {
        private readonly MealTrackingDbContext _context;

        public TrackerController(MealTrackingDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Tracker()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Records()
        {
            var username = HttpContext.Session.GetString("Username");

            if (username != null)
            {
                var user = _context.Users.FirstOrDefault(u => u.Username == username);

                if (user != null)
                {
                    var viewModel = new RecordsViewModel
                    {
                        Meals = _context.Meals.Where(m => m.UserId == user.Id).ToList(),
                        WaterIntakes = _context.WaterIntakes.Where(w => w.UserId == user.Id).ToList(),
                        PhysicalActivities = _context.PhysicalActivities.Where(a => a.UserId == user.Id).ToList()
                    };

                    return View(viewModel);
                }
            }

            return RedirectToAction("Tracker");
        }

        [HttpPost]
        public IActionResult SaveMeal(string mealName, string mealPortion, DateTime mealDateTime)
        {
            var username = HttpContext.Session.GetString("Username");

            if (username != null)
            {
                var user = _context.Users.FirstOrDefault(u => u.Username == username);

                if (user != null)
                {
                    var meal = new Meal
                    {
                        Name = mealName,
                        PortionSize = mealPortion,
                        Datetime = mealDateTime,
                        UserId = user.Id
                    };

                    _context.Meals.Add(meal);
                    _context.SaveChanges();
                }
            }

            return RedirectToAction("Tracker");
        }

        [HttpPost]
        public IActionResult SaveWater(string waterAmount, DateTime waterDateTime)
        {
            var username = HttpContext.Session.GetString("Username");

            if (username != null)
            {
                var user = _context.Users.FirstOrDefault(u => u.Username == username);

                if (user != null)
                {
                    var waterIntake = new WaterIntake
                    {
                        Amount = waterAmount,
                        Datetime = waterDateTime,
                        UserId = user.Id
                    };

                    _context.WaterIntakes.Add(waterIntake);
                    _context.SaveChanges();
                }
            }

            return RedirectToAction("Tracker");
        }

        [HttpPost]
        public IActionResult SaveActivity(string activityType, string activityDuration, DateTime activityDateTime)
        {
            var username = HttpContext.Session.GetString("Username");

            if (username != null)
            {
                var user = _context.Users.FirstOrDefault(u => u.Username == username);

                if (user != null)
                {
                    var activity = new PhysicalActivity
                    {
                        ActivityType = activityType,
                        Duration = activityDuration,
                        Datetime = activityDateTime,
                        UserId = user.Id
                    };

                    _context.PhysicalActivities.Add(activity);
                    _context.SaveChanges();
                }
            }

            return RedirectToAction("Tracker");
        }

        [HttpGet]
        public IActionResult EditMeal(int id)
        {
            var meal = _context.Meals.Find(id);
            if (meal == null) return NotFound();

            var viewModel = new MealEditViewModel
            {
                Id = meal.Id,
                Name = meal.Name,
                PortionSize = meal.PortionSize,
                Datetime = meal.Datetime
            };

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult EditMeal(MealEditViewModel viewModel)
        {
            if (!ModelState.IsValid) return View(viewModel);

            var meal = _context.Meals.Find(viewModel.Id);
            if (meal == null) return NotFound();

            meal.Name = viewModel.Name;
            meal.PortionSize = viewModel.PortionSize;
            meal.Datetime = viewModel.Datetime;

            _context.SaveChanges();
            return RedirectToAction("Records");
        }

        // Yemek silme metodu
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteMeal(int id)
        {
            var meal = _context.Meals.Find(id);
            if (meal == null)
            {
                return NotFound();
            }

            _context.Meals.Remove(meal);
            _context.SaveChanges();
            return RedirectToAction("Records");
        }





        [HttpGet]
        public IActionResult EditWater(int id)
        {
            var waterIntake = _context.WaterIntakes.Find(id);
            if (waterIntake == null) return NotFound();

            var viewModel = new WaterIntakeEditViewModel
            {
                Id = waterIntake.Id,
                Amount = waterIntake.Amount,
                Datetime = waterIntake.Datetime
            };

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult EditWater(WaterIntakeEditViewModel viewModel)
        {
            if (!ModelState.IsValid) return View(viewModel);

            var waterIntake = _context.WaterIntakes.Find(viewModel.Id);
            if (waterIntake == null) return NotFound();

            waterIntake.Amount = viewModel.Amount;
            waterIntake.Datetime = viewModel.Datetime;

            _context.SaveChanges();
            return RedirectToAction("Records");
        }


        // Su silme metodu
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteWater(int id)
        {
            var water = _context.WaterIntakes.Find(id);
            if (water == null)
            {
                return NotFound();
            }

            _context.WaterIntakes.Remove(water);
            _context.SaveChanges();
            return RedirectToAction("Records");
        }

        [HttpGet]
        public IActionResult EditActivity(int id)
        {
            var physicalActivity = _context.PhysicalActivities.Find(id);
            if (physicalActivity == null) return NotFound();

            var viewModel = new PhysicalActivityEditViewModel
            {
                Id = physicalActivity.Id,
                ActivityType = physicalActivity.ActivityType,
                Duration = physicalActivity.Duration,
                Datetime = physicalActivity.Datetime
            };

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult EditActivity(PhysicalActivityEditViewModel viewModel)
        {
            if (!ModelState.IsValid) return View(viewModel);

            var physicalActivity = _context.PhysicalActivities.Find(viewModel.Id);
            if (physicalActivity == null) return NotFound();

            physicalActivity.ActivityType = viewModel.ActivityType;
            physicalActivity.Duration = viewModel.Duration;
            physicalActivity.Datetime = viewModel.Datetime;

            _context.SaveChanges();
            return RedirectToAction("Records");
        }

        // Fiziksel Aktivite silme metodu
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteActivity(int id)
        {
            var activity = _context.PhysicalActivities.Find(id);
            if (activity == null)
            {
                return NotFound();
            }

            _context.PhysicalActivities.Remove(activity);
            _context.SaveChanges();
            return RedirectToAction("Records");
        }
    }
}
