using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ContosoUniversity.Models
{
    public class Author
    {
        [Display(Name = "ID Автора")]
        public int AuthorID { get; set; }

        [Display(Name = "Фамилия")]
        [StringLength(100, MinimumLength = 1)]
        public string Surname { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 1)]
        [Display(Name = "Имя")]
        public string Name { get; set; }

        [StringLength(100, MinimumLength = 1)]
        [Display(Name = "Отчество")]
        public string MiddleName { get; set; }
    }
}