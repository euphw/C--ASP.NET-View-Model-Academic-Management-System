using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Lab6.Models.EntityMetadata;
using Microsoft.AspNetCore.Mvc;

namespace Lab6.Models.DataAccess
{
    public partial class AcademicRecord
    {
        public string CourseCode { get; set; } = null!;
        public string StudentId { get; set; } = null!;
        public int? Grade { get; set; }

        public virtual Course CourseCodeNavigation { get; set; } = null!;
        public virtual Student Student { get; set; } = null!;


    }
}
