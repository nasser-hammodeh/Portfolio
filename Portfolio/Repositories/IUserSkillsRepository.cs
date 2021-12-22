using Portfolio.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Portfolio.Repositories
{
   public interface IUserSkillsRepository
    {
        public List<TechnicalSkill> GetTechnicalSkills();
        public List<InterpersonalSkill>  GetInterpersonalSkills();
        public void AddTechnicalSkill(TechnicalSkill technicalSkill);
        public void DeleteTechnicalSkill(int Id);
        public void AddInterpersonalSkill(InterpersonalSkill interpersonalSkill);
        public void DeleteInterpersonalSkill(int Id);

    }
}
