using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ContosoUniversity.Models
{
    public class Section
    {
        [Display(Name = "ID раздела")]
        public int SectionID { get; set; }

        [Required]
        [Display(Name = "Наименование")]
        [StringLength(100, MinimumLength = 1)]
        [RegularExpression(@"(([A-zА-яёЁ])+(\s?)([A-zА-яёЁ])*(\s?))*", ErrorMessage = "Введите Наименование правильно, цифры запрещены")]
        public string SectionName { get; set; }

    }
}
