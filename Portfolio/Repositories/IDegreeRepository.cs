using Portfolio.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Portfolio.Repositories
{
    public interface IDegreeRepository
    {
        public List<Degree> GetDegrees();
        public void AddDegree(Degree degree);
        public void DeleteDegree(int Id);
    }
}
