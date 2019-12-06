using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TMS.Models
{
    public class Progress
    {
        public int Id { get; set; }

        [Required]
        [Range(0, 100.00)]
        public double Completed { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }

        [Required]
        public int CourseId { get; set; }

        [Required]
        public int BatchId { get; set; }

        [Required]
        public int TraineeId { get; set; }

        [Required]
        public int TaskId { get; set; }

        [Required]
        public int InstructorId { get; set; }

        public Course Course { get; set; }
        public Batch Batch { get; set; }
        public Trainee Trainee { get; set; }
        public Tasks Task { get; set; }
        public Instructor Instructor { get; set; }
        public Performance Performance { get; set; }
    }
}
