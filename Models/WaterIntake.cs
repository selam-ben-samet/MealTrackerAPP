using System;
using System.Collections.Generic;

namespace MealTrackerAPP.Models;

public partial class WaterIntake
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public string Amount { get; set; }

    public DateTime? Datetime { get; set; }

    public virtual User User { get; set; } = null!;
}
