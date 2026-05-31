using Microsoft.EntityFrameworkCore;
using RestaurantSystem.Data;
using RestaurantSystem.Models;

namespace RestaurantSystem.Services
{
    public class CategoryOperation
    {
        public AppDbContext _db { get; set; }
        public CategoryOperation(AppDbContext db)
        {
            _db = db;
        }
        public async Task<Category> GetCategoryWithItems(int id)
        {
            return await _db.Categories
                .Include(x => x.MenuItems)
                .FirstOrDefaultAsync(x => x.Id == id);
        }
       
        public async Task<Category> LoadcategoryById(int id)
        {
            return await _db.Categories.FindAsync(id);
        }
    }
}
