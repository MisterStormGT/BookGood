using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ContosoUniversity.Models
{
    public class Book
    {
        [Display(Name = "ID Книги")]
        public int BookID { get; set; }

        [Display(Name = "Автор")]
        public int? AuthorID { get; set; }

        [Display(Name = "Раздел")]
        public int? SectionID { get; set; }

        [Display(Name = "Издательство")]
        public int? PublisherID { get; set; }

        [Display(Name = "Наименование книги")]
        public string BookName { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-dd-MM}", ApplyFormatInEditMode = true)]
        [Display(Name = "Год издания")]
        public DateTime YearOfPublishing { get; set; }

        [Display(Name = "Автор")]
        public Author Author { get; set; }

        [Display(Name = "Раздел")]
        public Section Section { get; set; }

        [Display(Name = "Издательство")]
        public Publisher Publisher { get; set; }

        
        

    }
}