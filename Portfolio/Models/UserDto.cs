using Microsoft.AspNetCore.Http;
using Portfolio.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Portfolio.Models
{
    public class UserDto
    {
        public string UserId { get; set; }
        [Display(Name ="First name")]
         [Required(ErrorMessage ="*")]
        public string FirstName { get; set; }
        [Display(Name = "Second name")]
        [Required(ErrorMessage = "*")]
        public string SecondName { get; set; }
        [Display(Name = "Last name")]
        [Required(ErrorMessage = "*")]
        public string LastName { get; set; }
        [Display(Name = "Date of birth")]
        [Required(ErrorMessage = "*")]
        public DateTime DateofBirth { get; set; }
        [Display(Name = "Gender")]
        public string Gender { get; set; }
        [Display(Name = "Address")]
        [Required(ErrorMessage = "*")]
        public string Address { get; set; }
        [Display(Name = "Email")]
        [Required(ErrorMessage = "*")]
        public string PortfolioEmail { get; set; }
        [Display(Name = "Vision")]
        [Required(ErrorMessage = "*")]
        public string Vision { get; set; }
        [Display(Name = "About")]
        [Required(ErrorMessage = "*")]
        public string About { get; set; }
        [Display(Name = "Personal image")]
        public IFormFile PersonalImageFile { get; set; }
        public Byte[] PersonalImage { get; set; }
        [Display(Name = "Curriculum Vitae (CV)")]

        public IFormFile CVFile { set; get; }
        public byte[] CV { get; set; }
        
        [Display(Name = "Mobile number")]
        [Required(ErrorMessage = "*")]
        public long MobileNumber { get; set; }
        [Display(Name = "Facebook URL")]
        [Required(ErrorMessage = "*" )]
        public string FacebookURL { get; set; }
        [Display(Name = "Twitter URL")]
        public string TwitterURL { get; set; }
        [Display(Name = "LinkedIn URL")]
        public string LinkedInURL { get; set; }
        public List<int> TechnicalSkillsDto { get;set; }
        public List<int> InterpersonalSkillsDto { get; set; }
        public List<UniversityDto> UniversityDto { get; set; }
        public List<ProjectDto> ProjectDto { set; get; }
    }
}
