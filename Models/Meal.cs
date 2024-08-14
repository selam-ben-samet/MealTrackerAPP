using System;
using System.Collections.Generic;

namespace MealTrackerAPP.Models;

public partial class Meal
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public string Name { get; set; } = null!;

    public string PortionSize { get; set; }

    public DateTime? Datetime { get; set; }

    public virtual User User { get; set; } = null!;
}
