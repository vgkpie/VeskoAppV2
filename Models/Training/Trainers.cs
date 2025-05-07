using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace VeskoAppV2.Models
{
    public class Trainers
{
    [Key]
    public int Id { get; set; }

    public string TrainerName { get; set; }

    public ICollection<Workouts> Workouts { get; set; }
}
}