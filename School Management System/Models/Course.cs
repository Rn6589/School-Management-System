using System.ComponentModel.DataAnnotations;

namespace School_Management_System.Models
{
    public class Course
    {
        [Key]
        public int CourseId { get; set; }

        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        // Additional properties as needed, such as start date, end date, teacher ID, etc.
    }
}
