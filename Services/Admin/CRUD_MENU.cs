using Microsoft.EntityFrameworkCore;
using RestaurantSystem.Data;
using RestaurantSystem.Models;

namespace RestaurantSystem.Services.Admin
{
    public class CRUD_MENU
    {
        public AppDbContext _db { get; set; }
        public CRUD_MENU(AppDbContext db)
        {
            _db = db;
        }
        public async Task<MenuItem> LoadItemById(int id)
        {
            return await _db.MenuItems.FindAsync(id);
        }
        public async Task UpdateMenuItem(MenuItem item)
        {
            var x = await _db.MenuItems.FindAsync(item.Id);
           if(x is null) { return; }
            x.Description=item.Description;
            x.Name=item.Name;
            x.Price=item.Price;
            x.ImagePath=item.ImagePath;
            x.CategoryId=item.CategoryId;
            await _db.SaveChangesAsync();
        }
        public async Task RemoveMenuItem(int m)
        {
            var x = await _db.MenuItems.FindAsync(m);
            if(x is null) { return; }
             _db.MenuItems.Remove(x);
            await _db.SaveChangesAsync();
        }
        public async Task<List<MenuItem>> LoadAllMenuItem()
        {
            return await _db.MenuItems.ToListAsync();

        }
    }
}
