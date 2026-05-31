using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RestaurantSystem.Models;
using RestaurantSystem.Services;
using RestaurantSystem.Services.Admin;

namespace RestaurantSystem.Pages.Admin
{
    public class AdminDashboardModel : PageModel
    {
        public DashboardAdmin service1 { get; set; }
        public UserOperation service2 { get; set; }
        public AdminDashboardModel(DashboardAdmin d ,  UserOperation service1)
        {
            this.service1 = d;
            this.service2= service1;
        }
        public int TotalOrders { get; set; }
        public int TotalUsers { get; set; }
        public int TotalCategories { get; set; }
        public int TotalMenuItems { get; set; }
        public List<RestaurantSystem.Models.Order> RecentOrders { get; set; } = new();
        public List<User> RecentUser { get; set; } = new();
        public async Task OnGetAsync()
        {
            TotalOrders = await service1.CountOrder();
            TotalUsers = await service1.CountUser();
            TotalCategories = await service1.CountCategory();
            TotalMenuItems = await service1.CountMenuItem();
            RecentOrders = await service1.GiveLastFiveOrder(0);
            foreach(var u in RecentOrders)
            {
                var x = await service2.LoadUser(u.UserId);
                RecentUser.Add(x);
            }
        }
    }
}
