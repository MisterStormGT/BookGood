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
        [Display(Name = "Наименование раздела")]
        [StringLength(100, MinimumLength = 1)]
        public string SectionName { get; set; }

    }
}
