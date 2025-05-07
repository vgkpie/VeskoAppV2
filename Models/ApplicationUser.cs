using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace VeskoAppV2.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string FamilyName { get; set; }

        public ICollection<Workouts>? Workouts { get; set; }
    }
}
