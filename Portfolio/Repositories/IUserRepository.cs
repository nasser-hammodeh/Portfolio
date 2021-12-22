using Microsoft.AspNetCore.Mvc;
using Portfolio.Data;
using Portfolio.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace Portfolio.Repositories
{
   public interface IUserRepository
    {
        public void AddUser(UserDto userDto);
        public List<User> GetUsers();
        public User GetUserById(string id);
        public void UpdateUser(UserDto UpdatedUser);
        public void DeleteUser(string Id);
        public List<UserUniversity> GetUserUniversities();
        public List<Project> GetUserProject(string id);
        public List<int> GetUserTechnicalSkillsId(string id);
        public List<int> GetUserInterpersonalSkillsId(string id);
        public List<UserUniversity> GetUserUniversitiesById(string id);
        public List<UserTechnicalSkill> GetUserTechnicalSkills();
        public List<UserInterpersonalSkill> GetUserInterpersonalSkills();
        public List<Project> GetProjects();
        public List<User> GetValidUser();
        public Project GetProject(int Id);
        

    }
}
