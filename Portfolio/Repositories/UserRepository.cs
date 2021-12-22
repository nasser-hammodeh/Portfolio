using Microsoft.AspNetCore.Identity.UI.V3.Pages.Account.Internal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Portfolio.Areas.Identity.Pages.Account;
using Portfolio.Data;
using Portfolio.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using LogoutModel = Portfolio.Areas.Identity.Pages.Account.LogoutModel;

namespace Portfolio.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;
       public UserRepository(ApplicationDbContext _context)
        {
            this._context = _context;
        }
        public List<User> GetUsers()
        {
            return _context.User.ToList();
        }
        public void AddUser(UserDto user)
        {
            var newUser = _context.User.Where(x => x.Id == user.UserId).SingleOrDefault();
            newUser.FirstName = user.FirstName;
            newUser.SecondName = user.SecondName;
            newUser.LastName = user.LastName;
            newUser.DateofBirth = user.DateofBirth;
            newUser.Gender = user.Gender;
            newUser.Address = user.Address;
            newUser.PortfolioEmail = user.PortfolioEmail;
            newUser.PersonalImage = user.PersonalImage;
            newUser.Vision = user.Vision;
            newUser.About = user.About;
            newUser.CV = user.CV;
            newUser.MobileNumber = user.MobileNumber;
            newUser.FacebookURL = user.FacebookURL;
            newUser.LinkedInURL = user.LinkedInURL;
            newUser.TwitterURL = user.TwitterURL;
            _context.SaveChanges();
            foreach(var technicalskill in user.TechnicalSkillsDto)
            {
                UserTechnicalSkill userTechnicalSkill = new UserTechnicalSkill();
                userTechnicalSkill.UserId = newUser.Id;
                userTechnicalSkill.TechnicalSkillId = technicalskill;
                _context.UserTechnicalSkills.Add(userTechnicalSkill);
                _context.SaveChanges();
            }
            foreach(var interpersonalSkill in user.InterpersonalSkillsDto)
            {
                UserInterpersonalSkill userInterpersonalSkill = new UserInterpersonalSkill();
                userInterpersonalSkill.UserId = newUser.Id;
                userInterpersonalSkill.InterpersonalSkillId = interpersonalSkill;
                _context.UserInterpersonalSkills.Add(userInterpersonalSkill);
                _context.SaveChanges();
            }
            foreach(var UU in user.UniversityDto)
            {
                UserUniversity userUniversity = new UserUniversity();
                userUniversity.UserId = newUser.Id;
                userUniversity.UniversityId = UU.UniversityId;
                userUniversity.DegreeId = UU.DegreeId;
                userUniversity.MajorName = UU.MajorName;
                _context.UserUniversities.Add(userUniversity);
                _context.SaveChanges();
            }
            foreach(var UP in user.ProjectDto)
            {
                Project project = new Project();
                project.UserId = newUser.Id;
                project.ProjectId = UP.ProjectId;
                project.ProjectName = UP.ProjectName;
                project.ProjectDescription = UP.ProjectDescription;
                project.ProjectImage = UP.ProjectImage;
                project.ProjectPDF = UP.ProjectPdf;
                _context.Projects.Add(project);
                _context.SaveChanges();
            }
        }
        public void UpdateUser(UserDto UpdatedUser)
        {
            var user = _context.User.Where(x => x.Id == UpdatedUser.UserId).SingleOrDefault();
            user.FirstName = UpdatedUser.FirstName;
            user.SecondName = UpdatedUser.SecondName;
            user.LastName = UpdatedUser.LastName;
            user.MobileNumber = UpdatedUser.MobileNumber;
            user.DateofBirth = UpdatedUser.DateofBirth;
            user.Gender = UpdatedUser.Gender;
            user.PortfolioEmail = UpdatedUser.PortfolioEmail;
            user.Address = UpdatedUser.Address;
            user.About = UpdatedUser.About;
            user.Vision = UpdatedUser.Vision;
            user.FacebookURL = UpdatedUser.FacebookURL;
            user.LinkedInURL = UpdatedUser.LinkedInURL;
            user.TwitterURL = UpdatedUser.TwitterURL;
            user.CV = UpdatedUser.CV;
            user.PersonalImage = UpdatedUser.PersonalImage;
            _context.SaveChanges();
            var usertechnicalskills = _context.UserTechnicalSkills.Where(x => x.UserId == user.Id).ToList();
            foreach (var skill in usertechnicalskills)
            {
                _context.Remove(skill);
                _context.SaveChanges();
            }
            var userTechnicalSkill = new UserTechnicalSkill();
            foreach (var skill in UpdatedUser.TechnicalSkillsDto)
            {
                userTechnicalSkill.UserId = UpdatedUser.UserId;
                userTechnicalSkill.TechnicalSkillId = skill;
                _context.UserTechnicalSkills.Add(userTechnicalSkill);
                _context.SaveChanges();
            }
            var userInterpersonalSkills = _context.UserInterpersonalSkills.Where(x => x.UserId == user.Id).ToList();
            foreach(var skill in userInterpersonalSkills)
            {
                _context.Remove(skill);
                _context.SaveChanges();
            }
            var userinterpersonalSkill = new UserInterpersonalSkill();
            foreach (var skill in UpdatedUser.InterpersonalSkillsDto)
            {
                userinterpersonalSkill.UserId = user.Id;
                userinterpersonalSkill.InterpersonalSkillId = skill;
                _context.UserInterpersonalSkills.Add(userinterpersonalSkill);
                _context.SaveChanges();

            }
            var userUniversity = _context.UserUniversities.Where(x => x.UserId == user.Id).ToList();
            foreach (var UU in userUniversity)
            {
                
                //userUniversity.UserId = UpdatedUser.UserId;
                //userUniversity.UniversityId = UU.UniversityId;
                //userUniversity.DegreeId = UU.DegreeId;
                //userUniversity.MajorName = UU.MajorName;
                _context.Remove(UU);
                _context.SaveChanges();
            }
            foreach(var UU in UpdatedUser.UniversityDto)
            {
                var userUniversities = new UserUniversity();
                userUniversities.UserId = user.Id;
                userUniversities.UniversityId = UU.UniversityId;
                userUniversities.DegreeId = UU.DegreeId;
                userUniversities.MajorName = UU.MajorName;
                _context.UserUniversities.Add(userUniversities);
                _context.SaveChanges();


            }
            var userProject = _context.Projects.Where(x => x.UserId == user.Id).ToList();
            foreach (var UP in userProject)
            {
                _context.Remove(UP);
                _context.SaveChanges();
            }
            foreach (var UP in UpdatedUser.ProjectDto)
            {
                var project = new Project();
                project.UserId = user.Id;
                project.ProjectName = UP.ProjectName;
                project.ProjectDescription = UP.ProjectDescription;
                project.ProjectImage = UP.ProjectImage;
                project.ProjectPDF = UP.ProjectPdf;
                _context.Add(project);
                _context.SaveChanges();
                
            }

        }
        public List<UserUniversity> GetUserUniversities()
        {
            return  _context.UserUniversities.ToList();
        }
        public User GetUserById(string id)
        {  
            return  _context.User.Where(x => x.Id == id).FirstOrDefault();
        }
        public List<Project> GetUserProject(string id)
        {
            return _context.Projects.Where(x => x.UserId == id).ToList();
        }
        public List<int> GetUserTechnicalSkillsId(string id)
        {
            return _context.UserTechnicalSkills.Where(x => x.UserId == id).Select(x=>x.TechnicalSkillId).ToList();
        }
        public List<int> GetUserInterpersonalSkillsId(string id)
        {
            return _context.UserInterpersonalSkills.Where(x => x.UserId == id).Select(x => x.InterpersonalSkillId).ToList();
        }
        public List<UserUniversity> GetUserUniversitiesById(string id)
        {
            return _context.UserUniversities.Where(x => x.UserId == id).ToList();
        }

        public void DeleteUser(string id)
        {
            List<UserTechnicalSkill> Tskills = _context.UserTechnicalSkills.Where(x => x.UserId == id).ToList();
           foreach(var ts in Tskills)
            {
                _context.Remove(ts);
            }
            List<UserInterpersonalSkill> Iskills = _context.UserInterpersonalSkills.Where(x => x.UserId == id).ToList();
            foreach(var ius in Iskills)
            {
                _context.Remove(ius);
            }
            List<UserUniversity> Uuniversity = _context.UserUniversities.Where(x => x.UserId == id).ToList();
            foreach(var uu in Uuniversity)
            {
                _context.Remove(uu);
            }
            List<Project> Uprojects = _context.Projects.Where(x => x.UserId == id).ToList();
            foreach(var up in Uprojects)
            {
                _context.Remove(up);
            }
            _context.SaveChanges();
            var user = _context.User.Where(x => x.Id == id).SingleOrDefault();
            user.FirstName = null;
            user.SecondName = null;
            user.LastName = null;
            user.MobileNumber = 0;
            user.DateofBirth = Convert.ToDateTime(null) ;
            user.Gender = null;
            user.PortfolioEmail = null;
            user.Address = null;
            user.About = null;
            user.Vision = null;
            user.FacebookURL = null;
            user.LinkedInURL = null;
            user.TwitterURL = null;
            user.CV = null;
            user.PersonalImage = null;

            _context.SaveChanges();
        }
        public string GetUserRole(string id)
        {
            return _context.UserRoles.Where(x => x.UserId == id).Select(x => x.RoleId).SingleOrDefault();
        }

        public List<UserTechnicalSkill> GetUserTechnicalSkills()
        {
            return _context.UserTechnicalSkills.ToList();
        }
        public List<UserInterpersonalSkill> GetUserInterpersonalSkills()
        {
            return _context.UserInterpersonalSkills.ToList();
        }
        public List<Project> GetProjects()
        {
            return _context.Projects.ToList();
        }
        public List<User> GetValidUser()
        {
            return _context.User.Where(x => x.FirstName.Length > 0).Where(x=>!x.UserName.Contains("Admin")).Where(x => !x.UserName.Contains("admin")).ToList();
        }
        public Project GetProject(int Id)
        {
            var project = _context.Projects.Where(x => x.ProjectId == Id).FirstOrDefault();
            return project;
        }
    }
}
