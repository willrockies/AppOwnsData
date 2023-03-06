using AppOwnsData.Models;
using System.Collections.Generic;
using System.Linq;

namespace AppOwnsData.Services
{
    public class DepartmentService
    {
        private readonly AppOwnsDataContext _context;

        public DepartmentService(AppOwnsDataContext context)
        {
            _context = context;
        }

        public List<Department> FindAll()
        {
            return _context.Department.OrderBy(x => x.Name).ToList();
        }
    }
}
