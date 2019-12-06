using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TMS.Models
{
    public class Salary
    {
        public int Id { get; set; }

        [Required]
        public double BasicSalary { get; set; }

        [Required]
        public double Bonus { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }

        [Required]
        public int InstructorId { get; set; }

        public Instructor Instructor { get; set; }
    }
}
