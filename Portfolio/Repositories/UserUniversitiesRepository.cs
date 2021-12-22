using Microsoft.EntityFrameworkCore;
using Portfolio.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Portfolio.Repositories
{
    public class UserUniversitiesRepository : IUserUniversitiesRepository
    {
        private readonly ApplicationDbContext _context;
        public UserUniversitiesRepository(ApplicationDbContext _context)
        {
            this._context = _context;
        }
       public List<University> GetUniversities()
        {
            return  _context.Universities.ToList();
        }
        public void AddUniversity(University university)
        {
            _context.Universities.Add(university);
            _context.SaveChanges();
        }
        public void DeleteUniversity(int Id)
        {
            University university = new University()
            {
                UniversityId = Id
        };
            _context.Universities.Remove(university);
            _context.SaveChanges();
        }
    }
}
