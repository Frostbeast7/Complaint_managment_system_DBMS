using System.ComponentModel.DataAnnotations;

namespace Sem_project.Models
{
    public class Student
    {
        public int Student_ID { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;

        [EmailAddress]
        [StringLength(255)]
        public string Email { get; set; } = string.Empty;

        [StringLength(20)]
        public string Phone { get; set; } = string.Empty;

        [Range(1, int.MaxValue, ErrorMessage = "Please enter a valid department ID.")]
        public int Dept_ID { get; set; }
    }
}
