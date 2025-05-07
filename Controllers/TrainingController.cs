using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using VeskoAppV2.Data;
using VeskoAppV2.Models;

namespace SashoApp.Controllers
{
    [Authorize(Roles = "Trainer")]
    public class TrainingController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public TrainingController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Training
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return Challenge();
            }

            var workouts = await _context.Workouts
                .Include(w => w.Member)
                .Where(w => w.TrainerId == user.Id)
                .OrderBy(w => w.Member.Email)
                .ToListAsync();

            return View(workouts);
        }
    }
}
