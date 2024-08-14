namespace MealTrackerAPP.Models
{
    public class MealEditViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string PortionSize { get; set; }
        public DateTime? Datetime { get; set; }
    }

}
