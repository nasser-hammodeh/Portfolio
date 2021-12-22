using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Portfolio.Models
{
    public class UniversityDto
    {
        public string UserId { get; set; }
        public int UniversityId { get; set; }
        public int DegreeId { get; set; }
        [Display(Name ="Major")]
        public string MajorName { get; set; }
    }
}
