using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RestaurantSystem.Models;
using RestaurantSystem.Services;

namespace RestaurantSystem.Pages.Admin.Orders
{
    public class DetailsModel : PageModel
    {
        public OrderOperation o;
        public DetailsModel(OrderOperation o)
        {
            this.o = o;
         
        }

        public RestaurantSystem.Models.Order Order { get; set; } = new();

        public List<OrderItem> Items { get; set; } = new();
        public async Task OnGet(int Orderid)
        {
            Items =  await o.LoadDetailsOrder(Orderid);
            Order = await o.LoadOrder(Orderid);
            if(Order is null) { return; }
        }
    }
}
