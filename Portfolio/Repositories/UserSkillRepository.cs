using Microsoft.EntityFrameworkCore;
using Portfolio.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace Portfolio.Repositories
{
    public class UserSkillRepository : IUserSkillsRepository
    {
        private readonly ApplicationDbContext _context;
        public UserSkillRepository(ApplicationDbContext _context)
        {
            this._context = _context;
        }
        public List<TechnicalSkill> GetTechnicalSkills()
        {
            return _context.TechnicalSkills.ToList();
        }
        public List<InterpersonalSkill> GetInterpersonalSkills()
        {
            return _context.InterpersonalSkills.ToList();
        }
        public void AddTechnicalSkill(TechnicalSkill technicalSkill)
        {
            _context.TechnicalSkills.Add(technicalSkill);
            _context.SaveChanges();
        }
        public void DeleteTechnicalSkill(int Id)
        {
            var TechnicalSkill = _context.TechnicalSkills.Where(x => x.TechnicalSkillId == Id).SingleOrDefault();
            _context.TechnicalSkills.Remove(TechnicalSkill);
            _context.SaveChanges();
        }
        public void AddInterpersonalSkill(InterpersonalSkill interpersonalSkill)
        {
            _context.InterpersonalSkills.Add(interpersonalSkill);
            _context.SaveChanges();
        }
        public void DeleteInterpersonalSkill(int Id)
        {
            InterpersonalSkill interpersonalSkill = new InterpersonalSkill()
            {
               InterpersonalSkillId = Id
            };
            _context.InterpersonalSkills.Remove(interpersonalSkill);
            _context.SaveChanges();
        }
    }
}
