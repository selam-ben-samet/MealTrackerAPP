using MealTrackerAPP.Models;

namespace MealTrackerAPP.ViewModels
{
    public class RecordsViewModel
    {
        public List<Meal> Meals { get; set; }
        public List<WaterIntake> WaterIntakes { get; set; }
        public List<PhysicalActivity> PhysicalActivities { get; set; }
    }
}
