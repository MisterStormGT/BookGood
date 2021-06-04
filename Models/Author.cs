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
        [RegularExpression(@"(([A-Za-zА-яёЁ])+(\s?)([А-яёЁ])*(\s?))*", ErrorMessage = "Введите Наименование правильно, цифры запрещены")]
        public string Surname { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 1)]
        [Display(Name = "Имя")]
        [RegularExpression(@"(([A-Za-zА-яёЁ])+(\s?)([А-яёЁ])*(\s?))*", ErrorMessage = "Введите Наименование правильно, цифры запрещены")]
        public string Name { get; set; }

        [StringLength(100, MinimumLength = 1)]
        [Display(Name = "Отчество")]
        [RegularExpression(@"(([A-Za-zА-яёЁ])+(\s?)([А-яёЁ])*(\s?))*", ErrorMessage = "Введите Наименование правильно, цифры запрещены")]
        public string MiddleName { get; set; }
    }
}