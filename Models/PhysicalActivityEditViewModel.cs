namespace MealTrackerAPP.Models
{
    public class PhysicalActivityEditViewModel
    {
        public int Id { get; set; }
        public string ActivityType { get; set; } = null!;
        public string Duration { get; set; } = null!;
        public DateTime? Datetime { get; set; }
    }

}
