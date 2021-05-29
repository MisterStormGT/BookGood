using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ContosoUniversity.Models
{
    public class Publisher
    {
        [Display(Name = "ID издательства")]
        public int PublisherID { get; set; }


        [Display(Name = "Город издательства")]
        [StringLength(100, MinimumLength = 1)]
        public string PublishingCity { get; set; }

        [Required]
        [Display(Name = "Наименование издательства")]
        [StringLength(100, MinimumLength = 1)]
        public string PublisherName { get; set; }

    }
}