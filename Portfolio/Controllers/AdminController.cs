using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting.Server.Features;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Portfolio.Data;
using Portfolio.Models;
using Portfolio.Repositories;

namespace Portfolio.Controllers
{
    [ApiController]
    [Route("api/[Controller]/[Action]")]

    public class AdminController : Controller
    {

        private readonly IUserRepository userRepository;
        private readonly IUserSkillsRepository userSkillsRepository;
        private readonly IUserUniversitiesRepository userUniversitiesRepository;
        private readonly IDegreeRepository degreeRepository;
        public AdminController(IUserRepository userRepository, IUserSkillsRepository userSkillsRepository, IUserUniversitiesRepository userUniversitiesRepository, IDegreeRepository degreeRepository)
        {
            this.userRepository = userRepository;
            this.userSkillsRepository = userSkillsRepository;
            this.userUniversitiesRepository = userUniversitiesRepository;
            this.degreeRepository = degreeRepository;
        }
        [Authorize]
        [HttpGet]
        public IActionResult UserInfo()
        {
            ViewBag.Users = userRepository.GetValidUser();
            ViewBag.UserUniversities = userRepository.GetUserUniversities();
            ViewBag.Universities = userUniversitiesRepository.GetUniversities();
            ViewBag.Degrees = degreeRepository.GetDegrees();
            ViewBag.UserTechnicalSkills = userRepository.GetUserTechnicalSkills();
            ViewBag.TechnicalSkills = userSkillsRepository.GetTechnicalSkills();
            ViewBag.UserInterpersonalSkills = userRepository.GetUserInterpersonalSkills();
            ViewBag.InterpersonalSkills = userSkillsRepository.GetInterpersonalSkills();
            ViewBag.UserProjects = userRepository.GetProjects();
            return View();
        }
        [Authorize]
        [HttpGet]
        public IActionResult GetSkills()
        {
            var InterpersonalSkills = userSkillsRepository.GetInterpersonalSkills();
            ViewBag.InterpersonalSkills = InterpersonalSkills;
            var TechnicalSkills = userSkillsRepository.GetTechnicalSkills();
            ViewBag.TechnicalSkills = TechnicalSkills;
            return View();
        }
        [Authorize]
        [HttpPost]
        public IActionResult AddTechnicalSkill([FromForm] SkillsDto skillsDto)
        {
            TechnicalSkill technicalSkill = new TechnicalSkill()
            {
                TechnicalSkillName = skillsDto.TechnicalSkillName
            };
            userSkillsRepository.AddTechnicalSkill(technicalSkill);
            return RedirectToAction("GetSkills");
        }
        [Authorize]
        [HttpGet("{Id}")]
        public IActionResult DeleteTechnicalSkill(int Id)
        {
            userSkillsRepository.DeleteTechnicalSkill(Id);
            return RedirectToAction("GetSkills");
        }
        [Authorize]
        [HttpPost]
        public IActionResult AddInterpersonalSkill([FromForm]SkillsDto skillsDto)
        {
            InterpersonalSkill interpersonalSkill = new InterpersonalSkill()
            {
                InterpersonalSkillName=skillsDto.InterpersonalSkillName
            };
            userSkillsRepository.AddInterpersonalSkill(interpersonalSkill);
            return RedirectToAction("GetSkills");
        }
        [Authorize]
        [HttpGet("{Id}")]
        public IActionResult DeleteInterpersonalSkill(int Id)
        {
            userSkillsRepository.DeleteInterpersonalSkill(Id);
            return RedirectToAction("GetSkills");
        }
        [Authorize]
        [HttpGet]
        public IActionResult GetUniversities()
        {
            var Universities = userUniversitiesRepository.GetUniversities();
            ViewBag.Universities = Universities;
            var Degrees = degreeRepository.GetDegrees();
            ViewBag.Degrees = Degrees;
            return View();
        }
        [Authorize]
        [HttpPost]
        public IActionResult AddUniversity([FromForm] UniversitiesDto universitiesDto)
        {
            University university = new University()
            {
                UniversityName = universitiesDto.UniversityName
            };
            userUniversitiesRepository.AddUniversity(university);
            return RedirectToAction("GetUniversities");
        }
        [Authorize]
        [HttpGet("{Id}")]
        public IActionResult DeleteUniversity(int Id)
        {
            userUniversitiesRepository.DeleteUniversity(Id);
            return RedirectToAction("GetUniversities");
        }
        [Authorize]
        [HttpPost]
        public IActionResult AddDegree([FromForm] UniversitiesDto universitiesDto)
        {
            Degree degree = new Degree()
            {
                DegreeName = universitiesDto.DegreeName
            };
            degreeRepository.AddDegree(degree);
            return RedirectToAction("GetUniversities");
        }
        [Authorize]
        [HttpGet("{Id}")]
        public IActionResult DeleteDegree(int Id)
        {
            degreeRepository.DeleteDegree(Id);
            return RedirectToAction("GetUniversities");
        }
        [HttpGet("{Id}")]
        public IActionResult Portfolio(string Id)
        {
            ViewBag.User = userRepository.GetUserById(Id);
            ViewBag.UserUniversities = userRepository.GetUserUniversitiesById(Id);
            ViewBag.Universities = userUniversitiesRepository.GetUniversities();
            ViewBag.UserTechnicalSkills = userRepository.GetUserTechnicalSkillsId(Id);
            ViewBag.TechnicalSkills = userSkillsRepository.GetTechnicalSkills();
            ViewBag.UserInterpersonalSkills = userRepository.GetUserInterpersonalSkillsId(Id);
            ViewBag.InterpersonalSkills = userSkillsRepository.GetInterpersonalSkills();
            ViewBag.Degrees = degreeRepository.GetDegrees();
            ViewBag.UserProjects = userRepository.GetUserProject(Id);

            return View();
        }
        
        public FileStreamResult GetCV(string Id)
        { var user = userRepository.GetUserById(Id);
            var file = user.CV;
            Stream stream = new MemoryStream(file);
            return new FileStreamResult(stream, "application/pdf");
        }
        public FileStreamResult GetProjectFile(int Id)
        {
            var project = userRepository.GetProject(Id);
            var file = project.ProjectPDF;
            Stream stream = new MemoryStream(file);
            return new FileStreamResult(stream, "application/pdf");
        }
    }
}
