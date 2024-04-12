using System.ComponentModel.DataAnnotations;

namespace School_Management_System.Models
{
    public class Students
    {
        [Key]
        public int StudentId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public DateOnly DOB { get; set; }

        public string Phone { get; set; }





    }
}
