using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TMS.Models
{
    public class Course
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [Required]
        public string Duration { get; set; }

        public List<Batch> Batches { get; set; }
        public List<Trainee> Trainees { get; set; }
        public List<Tasks> Task { get; set; }
        public List<Progress> Progresses { get; set; }
        public List<Performance> Performances { get; set; }
    }
}
