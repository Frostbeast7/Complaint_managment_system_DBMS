using System.ComponentModel.DataAnnotations;

namespace Sem_project.Models
{
    public class Complaint
    {
        public int Complaint_ID { get; set; }

        [Required]
        [StringLength(1000)]
        public string Description { get; set; } = string.Empty;

        [DataType(DataType.Date)]
        public DateTime Date { get; set; } = DateTime.Today;

        [StringLength(100)]
        public string Status { get; set; } = string.Empty;

        [Required]
        public int Student_ID { get; set; }

        [Required]
        public int Category_ID { get; set; }

        public string StudentName { get; set; } = string.Empty;
        public string CategoryName { get; set; } = string.Empty;
    }
}
