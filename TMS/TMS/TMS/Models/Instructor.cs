using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TMS.Models
{
    public class Instructor
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
        public string Email { get; set; }

        public List<Batch> Batches { get; set; }
        public List<Tasks> Tasks { get; set; }
        public List<Progress> Progresses { get; set; }
        public List<Performance> Performances { get; set; }
        public List<Salary> Salaries { get; set; }
    }
}
