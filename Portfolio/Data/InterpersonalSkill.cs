using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Portfolio.Data
{
    public class InterpersonalSkill
    {
        public int InterpersonalSkillId { get; set; }
        public string InterpersonalSkillName { get; set; }
        public ICollection<UserInterpersonalSkill> userInterpersonalSkills { get; set; }
    }
}
