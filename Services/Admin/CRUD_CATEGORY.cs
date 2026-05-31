using Microsoft.EntityFrameworkCore;
using RestaurantSystem.Data;
using RestaurantSystem.Models;
using RestaurantSystem.Models.ViewModel;

namespace RestaurantSystem.Services.Admin
{
    public class CRUD_CATEGORY
    {
        public AppDbContext _db;
        public CRUD_CATEGORY(AppDbContext db)
        {
            _db= db;
        }
        public async Task AddCategoryToMenu(CategoryAddViewModel c)
        {
            var cat = new Category()
            {
                BackgroundImage = c.BackgroundImage,
                Name = c.Name,
            };
            await _db.Categories.AddAsync(cat);
            await _db.SaveChangesAsync();
        }
        public async Task AddMenuItem(MenuItemAddViewModel v)
        {
            MenuItem m = new MenuItem()
            {
                Description = v.Description,
                CategoryId = v.CategoryId,
                ImagePath = v.ImagePath,
                Name = v.Name,
                Price = v.Price,
            };
            await _db.MenuItems.AddAsync(m);
            await _db.SaveChangesAsync();
        }
        public async Task<Category> LoadCategoryById(int id)
        {
            return await _db.Categories.Include(r=>r.MenuItems).FirstOrDefaultAsync(x=>x.Id==id);
        }

        public async Task UpdateRecentCategory(Category c)
        {
            var x = await _db.Categories.FindAsync(c.Id);
            if(x is null) { return; }
            x.Name = c.Name;
            x.MenuItems= c.MenuItems;
            if(c.BackgroundImage != null)
            {

            x.BackgroundImage = c.BackgroundImage;
            }
            await _db.SaveChangesAsync();
        }
        public async Task RemoveCategoryFromService(Category data)
        { 
                var menuItems = await _db.MenuItems
                .Where(m => m.CategoryId == data.Id)
                .ToListAsync();
            _db.MenuItems.RemoveRange(menuItems);
            _db.Categories.Remove(data);
            await _db.SaveChangesAsync();
        }
        public async Task<List<Category>> LoadAllCategory()
        {
            return await _db.Categories.Include(r=>r.MenuItems).ToListAsync();
        }
        public async Task<string> GetCategoryName(int id)
        {
            var x= await _db.Categories.FindAsync(id);
            return x!=null?x.Name:"No Category For This Menu Item Available!";
        }
    }
}
