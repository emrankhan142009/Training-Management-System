using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TMS.Models
{
    public class Batch
    {
        public int Id { get; set; }

        [Required]
        public int Number { get; set; }

        [Required]
        public int CourseId { get; set; }

        [Required]
        public int InstructorId { get; set; }

        public Course Course { get; set; }
        public Instructor Instructor { get; set; }

        public List<Trainee> Trainees { get; set; }
        public List<Tasks> Task { get; set; }
        public List<Progress> Progresses { get; set; }
        public List<Performance> Performances { get; set; }
    }
}
