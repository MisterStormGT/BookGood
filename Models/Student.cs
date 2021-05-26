using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ContosoUniversity.Models
{
    public class Student
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50, MinimumLength=1)]
        [Display(Name="Фамилия")]
        public string LastName { get; set; }

        [StringLength(50, MinimumLength = 1)]
        [Column("FirstName")]
        [Display(Name="Имя")]
        public string FirstMidName { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString ="{0:yyyy-MM-dd}",ApplyFormatInEditMode = true)]
        [Display(Name="Дата поступления")]
        public DateTime EnrollmentDate { get; set; }
        [Display(Name = "Полное имя")]
        public string FullName
        {
            get
            {
                return LastName + ", " + FirstMidName;
            }
        }

        public ICollection<Enrollment> Enrollments{ get; set; }
    }
}
