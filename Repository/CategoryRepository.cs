using Ticket.Data;
using Ticket.Models;
using Ticket.Repository.IRepository;

namespace Ticket.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationDBContext _db;
        public CategoryRepository(ApplicationDBContext db)
        {
            _db = db;
        }
        public bool CategoryExists(int categoyId)
        {
            return _db.Categories.Any(c => c.Id == categoyId);
        }

        public bool CategoryExists(string categoryName)
        {
            return _db.Categories.Any(c => c.Name.ToLower().Trim() == categoryName.ToLower().Trim());
        }

        public bool CreateCategory(Category category)
        {
            category.CreationDate = DateTime.Now;
            _db.Categories.Add(category);
            return Save();
        }

        public bool DeleteCategory(Category category)
        {
            _db.Categories.Remove(category);
            return Save();
        }

        public ICollection<Category> GetCategories()
        {
            return _db.Categories.OrderBy(c => c.Name).ToList();
        }

        public Category? GetCategory(int categoryId)
        {
            return _db.Categories.FirstOrDefault(c => c.Id == categoryId);
        }

        public bool Save()
        {
            return _db.SaveChanges() >= 0;
        }

        public bool UpdateCategory(Category category)
        {
            category.CreationDate = DateTime.Now;
            _db.Categories.Update(category);
            return Save();
        }
    }
}
