using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TMS.Models
{
    public class Trainee
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [Required]
        [StringLength(150)]
        public string Address { get; set; }

        [Required]
        [Phone]
        public string Contact { get; set; }

        [Required]
        [EmailAddress]
        public String Email { get; set; }

        [Required]
        public int CourseId { get; set; }

        [Required]
        public int BatchId { get; set; }

        public Course Course { get; set; }
        public Batch Batch { get; set; }

        public List<Tasks> Tasks { get; set; }
        public List<Progress> Progresses { get; set; }
        public List<Performance> Performances { get; set; }
    }
}
