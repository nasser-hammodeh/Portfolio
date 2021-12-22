﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Portfolio.Data
{
    public class UserTechnicalSkill
    {
        public int TechnicalSkillId { get; set; }
        public string UserId { get; set; }
        public User user { get; set; }
        public TechnicalSkill technicalSkill { get; set; }
        public bool RemoveUserTechnicalSkill { get; set; }
    }
}
