using Portfolio.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Portfolio.Repositories
{
   public interface IUserUniversitiesRepository
    {
        public List<University> GetUniversities();
        public void AddUniversity(University university);
        public void DeleteUniversity(int Id);


    }
}
