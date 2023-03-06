using AppOwnsData.Models;
using System.Collections.Generic;
using System.Linq;

namespace AppOwnsData.Services
{
    public class SellerService
    {
        private readonly AppOwnsDataContext _context;

        public SellerService(AppOwnsDataContext context)
        {
            _context = context;
        }

        public List<Seller> FindAll()
        {
            return _context.Seller.ToList();
        }

        public void Insert(Seller obj)
        {
            _context.Add(obj);
            _context.SaveChanges();
        }
    }
}
