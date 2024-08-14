using System;
using System.Collections.Generic;

namespace MealTrackerAPP.Models;

public partial class PhysicalActivity
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public string ActivityType { get; set; } = null!;

    public string Duration { get; set; }

    public DateTime? Datetime { get; set; }

    public virtual User User { get; set; } = null!;
}
