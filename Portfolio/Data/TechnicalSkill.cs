using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Portfolio.Data
{
    public class TechnicalSkill
    {
        public int TechnicalSkillId { get; set; }
        public string TechnicalSkillName { get; set; }
        public ICollection<UserTechnicalSkill> userTechnicalSkills { get; set; }
    }
}
