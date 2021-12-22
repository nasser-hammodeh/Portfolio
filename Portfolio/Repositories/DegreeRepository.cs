using Portfolio.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Portfolio.Repositories
{
    public class DegreeRepository : IDegreeRepository
    {
        private readonly ApplicationDbContext _context;
        public DegreeRepository(ApplicationDbContext _context)
        {
            this._context = _context;
        }
        public List<Degree> GetDegrees()
        {
            return _context.Degrees.ToList();
        }
        public void AddDegree(Degree degree)
        {
            _context.Degrees.Add(degree);
            _context.SaveChanges();
        }
        public void DeleteDegree(int Id)
        {
            Degree degree = new Degree()
            {
                DegreeId = Id
            };
            _context.Degrees.Remove(degree);
            _context.SaveChanges();
        }
    }
}
