using Microsoft.EntityFrameworkCore;
using RestaurantSystem.Data;
using RestaurantSystem.Models;

namespace RestaurantSystem.Services.Admin
{
    public class DashboardAdmin
    {
        public AppDbContext _db;
        public DashboardAdmin(AppDbContext db)
        {
            _db = db;
        }
        public async Task<int> CountUser()
        {
            return await _db.Users.CountAsync();
        
        }
        public async Task<int> CountOrder()
        {
            return await _db.Orders.CountAsync();

        }
        public async Task<int> CountCategory()
        {
            return await _db.Categories.CountAsync();

        }
        public async Task<int> CountMenuItem()
        {
            return await _db.MenuItems.CountAsync();

        }
        public async Task<int> Count()
        {
            return await _db.Users.CountAsync();

        }
        public async Task<List<Order>> GiveLastFiveOrder(int x)
        {
            if(x == 0)
            {
                return await _db.Orders
                              .OrderByDescending(o => o.Id)
                              .Take(5)
                              .ToListAsync();
            }
            else
            {
                return await _db.Orders
                                  .OrderByDescending(o => o.Id)

                                  .ToListAsync();
            }
            
        }
        public async Task UpdatePendingOrderToProcessing(int id )
        {
            var order = await _db.Orders.FindAsync(id);
            if(order is null)
            {
                return; 
            }

            if (order.Status == "Pending")
            {
                order.Status = "Processing";
                await _db.SaveChangesAsync();
            }
        }
        public async Task CancelOrder(int id)
        {
            var order = await _db.Orders.FindAsync(id);
            if(order is null )
            {
                return;
            }
            if (order.Status != "Completed")
            {
                order.Status = "Cancelled";
                await _db.SaveChangesAsync();
            }
        }
        public async Task UpdateProccesingOrderToComplete(int id)
        {
            var order = await _db.Orders.FindAsync(id);
            if(order is null) { return; }
            if (order.Status == "Processing")
            {
                order.Status = "Completed";
                await _db.SaveChangesAsync();
            }
           
        }
    }
}
