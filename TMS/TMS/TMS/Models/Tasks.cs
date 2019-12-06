using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TMS.Models
{
    public class Tasks
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [Required]
        public int Number { get; set; }

        [Required]
        [StringLength(500)]
        public string Description { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime AssignDate { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime SubmissionDate { get; set; }

        [Required]
        public int CourseId { get; set; }

        [Required]
        public int BatchId { get; set; }

        [Required]
        public int TraineeId { get; set; }

        [Required]
        public int InstructorId { get; set; }

        public Course Course { get; set; }
        public Batch Batch { get; set; }
        public Trainee Trainee { get; set; }
        public Instructor Instructor { get; set; }

        public Progress Progress { get; set; }
        public Performance Performance { get; set; }
    }
}
