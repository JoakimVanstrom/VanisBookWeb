using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VanisBook.DataAccess.Data;
using VanisBook.DataAccess.Repository.IRepository;
using VanisBook.Models;

namespace VanisBook.DataAccess.Repository
{
    public class CompanyRepository : Repository<Company>, ICompanyRepository
    {
        private ApplicationDbContext _db;
        
        public CompanyRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Company obj) 
        {
            _db.Companies.Update(obj);
        }
    
    }
}
