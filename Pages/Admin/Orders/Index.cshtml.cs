using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RestaurantSystem.Models;
using RestaurantSystem.Services;
using RestaurantSystem.Services.Admin;

namespace RestaurantSystem.Pages.Admin.Orders
{
    public class IndexModel : PageModel
    {
        public DashboardAdmin service1 { get; set; }
        public UserOperation service2 { get; set; }
        public IndexModel(DashboardAdmin d, UserOperation service1)
        {
            this.service1 = d;
            this.service2 = service1;
        }
        public List<RestaurantSystem.Models.Order> RecentOrders { get; set; } = new();
        public List<User> RecentUser { get; set; } = new();
        public async  Task  OnGetAsync()
        {
            RecentOrders = await service1.GiveLastFiveOrder(1);
            foreach (var u in RecentOrders)
            {
                var x = await service2.LoadUser(u.UserId);
                RecentUser.Add(x);
            }
        }
        public async Task<IActionResult> OnPostStartAsync(int id, string returnUrl)
        {

           await service1.UpdatePendingOrderToProcessing(id);
            return Redirect("/Admin/Orders" + returnUrl);

        }
        public async Task<IActionResult> OnPostCancelAsync(int id, string returnUrl)
        {

            await service1.CancelOrder(id);
            return Redirect("/Admin/Orders" + returnUrl);

        }
        public async Task<IActionResult> OnPostCompleteAsync(int id, string returnUrl)
        {
            await service1.UpdateProccesingOrderToComplete(id);
            return Redirect("/Admin/Orders" + returnUrl);

        }
    }
}
