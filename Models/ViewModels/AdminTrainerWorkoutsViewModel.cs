using System.Collections.Generic;
using VeskoAppV2.Models;

namespace VeskoAppV2.Models.ViewModels
{
    public class AdminTrainerWorkoutsViewModel
    {
        public ApplicationUser Trainer { get; set; }
        public List<Workouts> Workouts { get; set; }
    }
}
