using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using VeskoAppV2.Models;

namespace VeskoAppV2.Models
{
    public class Workouts
    {
        [Key]
        public int Id { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime StartsAtStation { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime ArrivesAtDestination { get; set; }

        [ForeignKey("Trainer")]
        public string? TrainerId { get; set; }

        public ApplicationUser? Trainer { get; set; }

        [ForeignKey("Member")]
        public string? MemberId { get; set; }

        public ApplicationUser? Member { get; set; }
    }
}
