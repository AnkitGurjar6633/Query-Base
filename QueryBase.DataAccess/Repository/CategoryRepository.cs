using QueryBase.DataAccess.DatabaseContext;
using QueryBase.DataAccess.Repository.IRepository;
using QueryBase.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueryBase.DataAccess.Repository
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        private readonly ApplicationDbContext _db;
        public CategoryRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Add(Category category)
        {
            _db.Categories.Add(category);
        }

        public void Remove(Category category)
        {
            _db.Categories.Remove(category);
        }

        public void Update(Category category)
        {
            _db.Categories.Update(category);
        }
    }
}
