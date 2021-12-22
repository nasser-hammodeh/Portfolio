using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.UI.V3.Pages.Account.Internal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.Extensions.WebEncoders.Testing;
using Portfolio.Data;
using Portfolio.Models;
using Portfolio.Repositories;

namespace Portfolio.Controllers
{
    [ApiController]
    [Route("api/[Controller]/[Action]")]

    public class UserController : Controller
    {
        private readonly IUserRepository userRepository;
        private readonly IUserSkillsRepository userSkillsRepository;
        private readonly IUserUniversitiesRepository userUniversitiesRepository;
        private readonly IDegreeRepository degreeRepository;
        public UserController(IUserRepository userRepository, IUserSkillsRepository userSkillsRepository, IUserUniversitiesRepository userUniversitiesRepository, IDegreeRepository degreeRepository)
        {
            this.userRepository = userRepository;
            this.userSkillsRepository = userSkillsRepository;
            this.userUniversitiesRepository = userUniversitiesRepository;
            this.degreeRepository = degreeRepository;
        }
        public IActionResult Home()
        {
            return View();
        }
        [Authorize]
        [HttpGet]
        public IActionResult UserInfo()
        {
            var user = userRepository.GetUserById(User.FindFirstValue(ClaimTypes.NameIdentifier));
            if (user.FirstName ==  null )
            {
                var TechnicalSkills = userSkillsRepository.GetTechnicalSkills();
                ViewBag.technicalSkills = TechnicalSkills;
                var InterpersonalSkills = userSkillsRepository.GetInterpersonalSkills();
                ViewBag.interpersonalSkills = InterpersonalSkills;
                var Universities = userUniversitiesRepository.GetUniversities();
                ViewBag.universities = Universities;
                var Degrees = degreeRepository.GetDegrees();
                ViewBag.degrees = Degrees;
                return View();
            }
            else
            {
                return RedirectToAction("UserPortfolio", "User", new { Id = user.Id });
            }
        }
        [Authorize]
        [HttpPost]
        public IActionResult AddUser([FromForm] UserDto userDto)
        {
            userDto.UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userDto.CVFile.Length > 0)
            {
                Stream stream = userDto.CVFile.OpenReadStream();
                using (BinaryReader binaryReader = new BinaryReader(stream))
                {
                    var ByteCVFile = binaryReader.ReadBytes((int)stream.Length);
                    userDto.CV = ByteCVFile;
                }
            }
            if (userDto.PersonalImageFile.Length > 0)
            {
                Stream stream = userDto.PersonalImageFile.OpenReadStream();
                using (BinaryReader binaryReader = new BinaryReader(stream))
                {
                    var BytePersonalImage = binaryReader.ReadBytes((int)stream.Length);
                    userDto.PersonalImage = BytePersonalImage;
                }
            }
            foreach (var project in userDto.ProjectDto)
            {
                if (project.ProjectImageFile.Length > 0)
                {
                    Stream stream = project.ProjectImageFile.OpenReadStream();
                    using (BinaryReader binaryReader = new BinaryReader(stream))
                    {
                        var ByteProjectImage = binaryReader.ReadBytes((int)stream.Length);
                        project.ProjectImage = ByteProjectImage;
                    }
                }
                if (project.ProjectFile.Length > 0)
                {
                    Stream stream = project.ProjectFile.OpenReadStream();
                    using (BinaryReader binaryReader = new BinaryReader(stream))
                    {
                        var ByteProjectFile = binaryReader.ReadBytes((int)stream.Length);
                        project.ProjectPdf = ByteProjectFile;
                    }
                }
            }
            userRepository.AddUser(userDto);
            using (var message = new MailMessage())
            {
                message.To.Add(new MailAddress(userDto.PortfolioEmail));
                message.From = new MailAddress("nasser.hammodeh@HTU.EDU.JO");
                message.Subject = "PORTFOLIO builder website";
                message.Body = $"Dear {userDto.FirstName},<br/><br/>A new <span style='color:#a92330;'>portfolio</span> has been created for you at https://portfoliobuilder.azurewebsites.net .<br/>Your current username : {userDto.PortfolioEmail}<br/>Your Portfolio link : https://portfoliobuilder.azurewebsites.net/api/Admin/Portfolio/{userDto.UserId} <br/>You can update your portfolio, just visit us.<br/><h4 style='font-family:monospace;'>Best Regardes,<br>Portfolio Management : Nasser <span style='color:#a92330;'>Hammodeh.</span>";
                message.IsBodyHtml = true;
                using (var smtpClient = new SmtpClient("smtp.office365.com", 587))
                {
                    smtpClient.EnableSsl = true;
                    smtpClient.UseDefaultCredentials = false;
                    smtpClient.Credentials = new NetworkCredential("nasser.hammodeh@HTU.EDU.JO", "Nassoroutlook1");
                    smtpClient.Send(message);
                }
            }
            return RedirectToAction("UserPortfolio", "User", new { Id = userDto.UserId });
        }
        [Authorize]
        [HttpGet]
        public IActionResult Update()
        {
            string id = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = userRepository.GetUserById(id);
            //if (user.FirstName != null)
            //{
                var TechnicalSkills = userSkillsRepository.GetTechnicalSkills();
                ViewBag.technicalSkills = TechnicalSkills;
                var InterpersonalSkills = userSkillsRepository.GetInterpersonalSkills();
                ViewBag.interpersonalSkills = InterpersonalSkills;
                var Universities = userUniversitiesRepository.GetUniversities();
                ViewBag.universities = Universities;
                var Degrees = degreeRepository.GetDegrees();
                ViewBag.degrees = Degrees;
                ViewData["gender"] = user.Gender;
                UserDto userDto = new UserDto()
                {
                    FirstName = user.FirstName,
                    SecondName = user.SecondName,
                    LastName = user.LastName,
                    DateofBirth = user.DateofBirth,
                    Address = user.Address,
                    Gender = user.Gender,
                    MobileNumber = user.MobileNumber,
                    PortfolioEmail = user.PortfolioEmail,
                    About = user.About,
                    Vision = user.Vision,
                    FacebookURL = user.FacebookURL,
                    LinkedInURL = user.LinkedInURL,
                    TwitterURL = user.TwitterURL
                };
                return View(userDto);
                //var user = userRepository.GetUserById(id);
                //var userTechnicalSkills = userRepository.GetUserTechnicalSkillsId(id);
                //var userInterpersonalSkills = userRepository.GetUserInterpersonalSkillsId(id);
                //UserDto userDto = new UserDto
                //{
                //    UserId = id,
                //    FirstName = user.FirstName,
                //    SecondName = user.SecondName,
                //    LastName = user.LastName,
                //    DateofBirth = user.DateofBirth,
                //    Address = user.Address,
                //    Gender = user.Gender,
                //    MobileNumber = user.MobileNumber,
                //    PortfolioEmail = user.PortfolioEmail,
                //    About = user.About,
                //    Vision = user.Vision,
                //    FacebookURL = user.FacebookURL,
                //    LinkedInURL = user.LinkedInURL,
                //    TwitterURL = user.TwitterURL,
                //    TechnicalSkillsDto = userTechnicalSkills,
                //    InterpersonalSkillsDto = userInterpersonalSkills
                //};
                ////List<UniversityDto> universityDtos = userRepository.GetUserUniversitiesById(id);
                ////User user = userRepository.GetUserById(id);
                ////List<int> userTechnicalSkillsId = userRepository.GetUserTechnicalSkillsId(id);
                ////List<int> userInterpersonalSkillsId = userRepository.GetUserInterpersonalSkillsId(id);
                ////List<UserUniversity> userUniversities = userRepository.GetUserUniversitiesById(id);
                ////List<Project> userProject = userRepository.GetUserProject(id);
                ////UserDto userDto = new UserDto()
                ////{
                ////    UserId = id,
                ////    FirstName = user.FirstName,
                ////    SecondName = user.SecondName,
                ////    LastName = user.LastName,
                ////    DateofBirth = user.DateofBirth,
                ////    Address = user.Address,
                ////    Gender = user.Gender,
                ////    MobileNumber = user.MobileNumber,
                ////    PortfolioEmail = user.PortfolioEmail,
                ////    About = user.About,
                ////    Vision = user.Vision,
                ////    FacebookURL = user.FacebookURL,
                ////    LinkedInURL = user.LinkedInURL,
                ////    TwitterURL = user.TwitterURL,
                ////    TechnicalSkillsDto = userTechnicalSkillsId,
                ////    InterpersonalSkillsDto = userInterpersonalSkillsId
                ////};
                ////var TechnicalSkills = userSkillsRepository.GetTechnicalSkills();
                ////ViewBag.technicalSkills = TechnicalSkills;
                ////var InterpersonalSkills = userSkillsRepository.GetInterpersonalSkills();
                ////ViewBag.interpersonalSkills = InterpersonalSkills;
                ////var Universities = userUniversitiesRepository.GetUniversities();
                ////ViewBag.universities = Universities;
                ////var Degrees = degreeRepository.GetDegrees();
                ////ViewBag.degrees = Degrees;
                ////ViewData["gender"] = userDto.Gender;
                ////ProjectDto projectDto = new ProjectDto();
                ////foreach (var up in userProject)
                ////{
                ////    projectDto.UserId = id;
                ////    projectDto.ProjectId = up.ProjectId;
                ////    projectDto.ProjectName = up.ProjectName;
                ////    projectDto.ProjectDescription = up.ProjectDescription;
                //////}
                //////var universityDto = userUniversities;
                //////UniversityDto universityDto = new UniversityDto();
                //////foreach (var uu in userUniversities)
                //////{
                //////    universityDto.UserId = uu.UserId;
                //////    universityDto.UniversityId = uu.UniversityId;
                //////    universityDto.MajorName = uu.MajorName;
                //////    universityDto.DegreeId = uu.DegreeId;

                //////}
                ////List<ProjectDto> projectDtos = new List<ProjectDto>();
                ////projectDtos.Add(projectDto);
                ////userDto.ProjectDto = projectDtos;
                ////    //List<UniversityDto> universityDtos = new List<UniversityDto>();
                ////    var universityDtos = userUniversities;
                ////    //universityDtos.Add(universityDto);
                ////    //userDto.UniversityDto = universityDtos; 
                ////    userDto.UniversityDto = userUniversities;
                
            //}
            //else
            //{
            //    return RedirectToAction("UpdateError","DefaultHome");
            //}
        }
        [Authorize]
        [HttpPost]
        public IActionResult UpdateUser([FromForm] UserDto updatedUser)
        {
            updatedUser.UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (updatedUser.CVFile.Length > 0)
            {
                Stream stream = updatedUser.CVFile.OpenReadStream();
                using (BinaryReader binaryReader = new BinaryReader(stream))
                {
                    var ByteCVFile = binaryReader.ReadBytes((int)stream.Length);
                    updatedUser.CV = ByteCVFile;
                }
            }
            if (updatedUser.PersonalImageFile.Length > 0)
            {
                Stream stream = updatedUser.PersonalImageFile.OpenReadStream();
                using (BinaryReader binaryReader = new BinaryReader(stream))
                {
                    var BytePersonalImage = binaryReader.ReadBytes((int)stream.Length);
                    updatedUser.PersonalImage = BytePersonalImage;
                }
            }
            foreach (var project in updatedUser.ProjectDto)
            {
                if (project.ProjectImageFile.Length > 0)
                {
                    Stream stream = project.ProjectImageFile.OpenReadStream();
                    using (BinaryReader binaryReader = new BinaryReader(stream))
                    {
                        var ByteProjectImage = binaryReader.ReadBytes((int)stream.Length);
                        project.ProjectImage = ByteProjectImage;
                    }
                }
                if (project.ProjectFile.Length > 0)
                {
                    Stream stream = project.ProjectFile.OpenReadStream();
                    using (BinaryReader binaryReader = new BinaryReader(stream))
                    {
                        var ByteProjectFile = binaryReader.ReadBytes((int)stream.Length);
                        project.ProjectPdf = ByteProjectFile;
                    }
                }
            }
            userRepository.UpdateUser(updatedUser);
            using (var message = new MailMessage())
            {
                message.To.Add(new MailAddress(updatedUser.PortfolioEmail));
                message.From = new MailAddress("nasser.hammodeh@HTU.EDU.JO");
                message.Subject = "PORTFOLIO builder website";
                message.Body = $"Dear {updatedUser.FirstName},<br/><br/>Your <span style='color:#a92330;'>portfolio</span> has been updated successfully at https://portfoliobuilder.azurewebsites.net<br/>Your current username : {updatedUser.PortfolioEmail}<br/>Your Portfolio link : https://portfoliobuilder.azurewebsites.net/api/Admin/Portfolio/{updatedUser.UserId} <br/><h4 style='font-family:monospace;'>Best Regardes,<br>Portfolio Management : Nasser <span style='color:#a92330;'>Hammodeh</span>.";
                message.IsBodyHtml = true;
                using (var smtpClient = new SmtpClient("smtp.office365.com", 587))
                {
                    smtpClient.EnableSsl = true;
                    smtpClient.UseDefaultCredentials = false;
                    smtpClient.Credentials = new NetworkCredential("nasser.hammodeh@HTU.EDU.JO", "Nassoroutlook1");
                    smtpClient.Send(message);
                }
            }
            
            return RedirectToAction("UserPortfolio","User",new {Id=updatedUser.UserId });
        }
        [Authorize]
        public IActionResult DeleteUser()
        {
           // var user = userRepository.GetUserById(User.FindFirstValue(ClaimTypes.NameIdentifier));
            //if (user.FirstName == null)
            //{

               // return RedirectToAction("DeleteError","DefaultHome");
            //}
            //else
            //{
                string UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                userRepository.DeleteUser(UserId);
                return RedirectToAction("newhome","DefaultHome");
            //}
        }
        [HttpGet("{Id}")]
        public IActionResult UserPortfolio(string Id)

        {
             //var Id = User.FindFirstValue(ClaimTypes.NameIdentifier);
            ViewBag.User = userRepository.GetUserById(Id);
            ViewBag.UserUniversities = userRepository.GetUserUniversitiesById(Id);
            ViewBag.Universities = userUniversitiesRepository.GetUniversities();
            ViewBag.UserTechnicalSkills = userRepository.GetUserTechnicalSkillsId(Id);
            ViewBag.TechnicalSkills = userSkillsRepository.GetTechnicalSkills();
            ViewBag.UserInterpersonalSkills = userRepository.GetUserInterpersonalSkillsId(Id);
            ViewBag.InterpersonalSkills = userSkillsRepository.GetInterpersonalSkills();
            ViewBag.Degrees = degreeRepository.GetDegrees();
            ViewBag.UserProjects =userRepository.GetUserProject(Id);

         //    var user = userRepository.GetUserById(Id);
           // userRepository.SendEmail(user.UserName, user.FirstName, user.PasswordHash);

            return View();
        }
        
        public FileStreamResult GetCV(string Id)
        {
            var user = userRepository.GetUserById(Id);
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

