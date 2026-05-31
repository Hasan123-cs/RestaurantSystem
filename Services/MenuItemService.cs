using Microsoft.EntityFrameworkCore;
using RestaurantSystem.Data;
using RestaurantSystem.Models;
using RestaurantSystem.Models.ViewModel;

namespace RestaurantSystem.Services
{
    public class MenuItemService
    {
        public AppDbContext _db { get; set; }
        public MenuItemService(AppDbContext db)
        {
            _db = db;
        }
        public async Task<MenuItem?> LoadItemToOrder(int id)
        {
            return await  _db.MenuItems.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
        }
       public async Task<List<RestaurantSystem.Models.ViewModel.PopularDishViewModel>> MostPopularLove()
        {
            var popularDishes =  _db.OrderItems
       .Join(_db.Orders,
           oi => oi.OrderId,
           o => o.Id,
           (oi, o) => new { oi, o })
       .Where(x => x.o.Status == "Completed")

       .Join(_db.MenuItems,
           x => x.oi.MenuItemId,
           m => m.Id,
           (x, m) => new { x.oi, m })

       .GroupBy(x => new
       {
           x.m.Id,
           x.m.Name,
           x.m.Price,
           x.m.ImagePath,
           x.m.Description
       })
       .Select(g => new PopularDishViewModel
       {
           id = g.Key.Id,
           Name = g.Key.Name,
           Price = g.Key.Price,
           ImagePath = g.Key.ImagePath,
           Description = g.Key.Description
       })
       .OrderByDescending(x => x.id) 
       .Take(3)
       .ToListAsync();
            return await popularDishes;
        }
    }
}
