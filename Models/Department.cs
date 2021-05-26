﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ContosoUniversity.Models
{
    public class Department
    {
        public int DepartmentId { get; set; }

        [StringLength(50, MinimumLength = 1)]
        [Display(Name = "Название")]
        public string Name { get; set;}

        [DataType(DataType.Currency)]
        [Column(TypeName="money")]
        [Display(Name = "Деньги")]
        public decimal Budget { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name= "Дата начала")]
        public DateTime StartDate{ get; set; }

        public int? InstructorId { get; set; }

        [Timestamp]
        [Display(Name = "Версия столбца")]
        public byte[] RowVersion { get; set; }
        [Display(Name = "Администратор")]
        public Instructor Administrator { get; set; }
        public ICollection<Course> Courses { get; set; }
    }
}