using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TempleTours.Models
{
    public class Signup
    {
        [Key]
        public int Id { get; set; }

        [Required (ErrorMessage = "Group Names must be shorter than 50 characters")]
        [MaxLength(50)]
        public string GroupName { get; set; }

        [Required]
        [Range(1, 15, ErrorMessage = ("Group Sizes must be between 1 and 15 persons"))]
        public int GroupSize { get; set; }

        [Required(ErrorMessage = "Enter a valid Email")]
        [MaxLength(50)]
        public string Email { get; set; }

        [MaxLength(10)]
        public string Phone { get; set; }

        public int AppointmentId { get; set; }

        public Appointment Appointment { get; set; }
    }
}
