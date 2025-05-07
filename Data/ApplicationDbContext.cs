using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using VeskoAppV2.Models;

namespace VeskoAppV2.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Trainers> Trainers { get; set; }
        public DbSet<Workouts> Workouts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
{
    base.OnModelCreating(modelBuilder);

    // One Trainer has many Workouts
    modelBuilder.Entity<Workouts>()
        .HasOne(w => w.Trainer)
        .WithMany(t => t.Workouts)
        .HasForeignKey(w => w.TrainerId)
        .OnDelete(DeleteBehavior.Cascade);
}
    }   
}