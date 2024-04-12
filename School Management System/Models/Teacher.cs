using System.ComponentModel.DataAnnotations;

namespace School_Management_System.Models
{
    public class Teacher
    {
        [Key]
        public int TeacherId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Subject { get; set; }

        public string Email { get; set; }

    }
}
