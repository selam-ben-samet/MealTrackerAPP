using System;
using System.Collections.Generic;

namespace MealTrackerAPP.Models;

public partial class User
{
    public int Id { get; set; }

    public string Username { get; set; } = null!;

    public string Password { get; set; } = null!;

    public virtual ICollection<Meal> Meals { get; set; } = new List<Meal>();

    public virtual ICollection<PhysicalActivity> PhysicalActivities { get; set; } = new List<PhysicalActivity>();

    public virtual ICollection<WaterIntake> WaterIntakes { get; set; } = new List<WaterIntake>();
}
