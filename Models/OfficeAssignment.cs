using System.ComponentModel.DataAnnotations;

namespace ContosoUniversity.Models
{
    public class OfficeAssignment
    {
        [Key]
        public int InstructorId { get; set; }

        [StringLength(50)]
        [Display(Name="Местонахождение офиса")]
        public string Location { get; set; }

        public Instructor Instructor { get; set; }
    }
}