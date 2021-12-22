using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Portfolio.Models
{
    public class UniversitiesDto
    {
        public int UniversityId { get; set; }
        public string UniversityName { get; set; }
        public int DegreeId { get; set; }
        public string DegreeName { get; set; }
    }
}
