using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VeskoAppV2.Data;
using VeskoAppV2.Models;
using VeskoAppV2.Models.Account;
using VeskoAppV2.Models.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace SashoApp.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ApplicationDbContext _context;

        public AdminController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _context = context;
        }

        // GET: Admin
        public async Task<IActionResult> Index()
        {
            var trainers = await _userManager.GetUsersInRoleAsync("Trainer");

            var trainerWorkouts = new List<VeskoAppV2.Models.ViewModels.AdminTrainerWorkoutsViewModel>();

            foreach (var trainer in trainers)
            {
                var workouts = await _context.Workouts
                    .Include(w => w.Member)
                    .Where(w => w.TrainerId == trainer.Id)
                    .ToListAsync();

                trainerWorkouts.Add(new VeskoAppV2.Models.ViewModels.AdminTrainerWorkoutsViewModel
                {
                    Trainer = trainer,
                    Workouts = workouts
                });
            }

            return View(trainerWorkouts);
        }

        // List users with roles
        public async Task<IActionResult> ListUsers()
        {
            var users = _userManager.Users.ToList();
            var userViewModels = new List<UserViewModel>();

            foreach (var user in users)
            {
                var roles = await _userManager.GetRolesAsync(user);
                userViewModels.Add(new UserViewModel
                {
                    Id = user.Id,
                    Email = user.Email,
                    FirstName = user.FirstName,
                    FamilyName = user.FamilyName,
                    Roles = roles
                });
            }

            return View("~/Views/Account/ListUsers.cshtml", userViewModels);
        }

        // GET: Admin/CreateWorkout
        public async Task<IActionResult> CreateWorkout()
        {
            var members = await _userManager.GetUsersInRoleAsync("Member");
            var trainers = await _userManager.GetUsersInRoleAsync("Trainer");

            var model = new CreateWorkoutViewModel
            {
                Members = members.Select(m => new SelectListItem { Value = m.Id, Text = m.Email }).ToList(),
                Trainers = trainers.Select(t => new SelectListItem { Value = t.Id, Text = t.Email }).ToList(),
                Workout = new Workouts
                {
                    StartsAtStation = DateTime.Now,
                    ArrivesAtDestination = DateTime.Now.AddHours(1)
                }
            };

            return View(model);
        }

        // POST: Admin/CreateWorkout
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateWorkout(CreateWorkoutViewModel model)
        {
            if (ModelState.IsValid)
            {
                var workout = model.Workout;
                workout.MemberId = model.SelectedMemberId;
                workout.TrainerId = model.SelectedTrainerId;

                // Load related entities to set navigation properties
                workout.Member = await _userManager.FindByIdAsync(model.SelectedMemberId);
                workout.Trainer = await _userManager.FindByIdAsync(model.SelectedTrainerId);

                _context.Workouts.Add(workout);
                await _context.SaveChangesAsync();

                return RedirectToAction("Index");
            }

            var members = await _userManager.GetUsersInRoleAsync("Member");
            var trainers = await _userManager.GetUsersInRoleAsync("Trainer");

            model.Members = members.Select(m => new SelectListItem { Value = m.Id, Text = m.Email }).ToList();
            model.Trainers = trainers.Select(t => new SelectListItem { Value = t.Id, Text = t.Email }).ToList();

            return View(model);
        }

        // GET: Admin/WorkoutCalendar
        public async Task<IActionResult> WorkoutCalendar()
        {
            var workouts = await _context.Workouts
                .Include(w => w.Trainer)
                .Include(w => w.Member)
                .ToListAsync();

            return View(workouts);
        }
        // Promote user to Trainer role
    }
    
}
