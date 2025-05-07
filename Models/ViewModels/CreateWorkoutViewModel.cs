using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using VeskoAppV2.Models;

namespace VeskoAppV2.Models.ViewModels
{
    public class CreateWorkoutViewModel
    {
        [Required]
        public Workouts Workout { get; set; } = new Workouts();

        [Required]
        [Display(Name = "Member")]
        public string SelectedMemberId { get; set; }

        [Required]
        [Display(Name = "Trainer")]
        public string SelectedTrainerId { get; set; }

        public List<SelectListItem> Members { get; set; } = new List<SelectListItem>();
        public List<SelectListItem> Trainers { get; set; } = new List<SelectListItem>();
    }
}
