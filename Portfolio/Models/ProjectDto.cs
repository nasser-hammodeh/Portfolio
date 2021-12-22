using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Portfolio.Models
{
    public class ProjectDto
    {public string UserId { set; get; }
        public int ProjectId { get; set; }
        [Display(Name ="Project name")]
        public string ProjectName { get; set; }
        [Display(Name ="Project description")]
        public string ProjectDescription { get; set; }
        [Display(Name = "Project image")]
        public IFormFile ProjectImageFile { set; get; }
        public byte[] ProjectImage { get; set; }
        [Display(Name = "Project file")]
        public IFormFile ProjectFile { set; get; }
        public byte[] ProjectPdf { get; set; }
        public string ProjectImgSrc { get; set; }
    }
}
