using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;

namespace Lab6.Models.EntityMetadata
{
    public class AcademicRecordsMetadata
    {
        [Required(ErrorMessage = "Please enter a grade")]
        [Range(0,100, ErrorMessage = "Must be between 0 to 100")]
        public int? Grade { get; set; }
    }
}
