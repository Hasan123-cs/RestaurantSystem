using Microsoft.EntityFrameworkCore;
using RestaurantSystem.Data;
using RestaurantSystem.Models;

namespace RestaurantSystem.Services
{
    public class OrderOperation
    {
        public AppDbContext _db { get; set; }
        public OrderOperation(AppDbContext db)
        {
            _db = db;
        }
        public async Task<bool> AddToCart(int quantity, int menuItemId, int? userId)
        {
            // Get item from DB
            var item = await _db.MenuItems.FindAsync(menuItemId);
            
            if (item == null )
                return false;
            if(userId is null)
            {
                return false;
            }

            // Check if item already exists in cart
            var existingCartItem = await _db.CartItems
                .FirstOrDefaultAsync(x =>
                    x.MenuItemId == menuItemId &&
                    x.UserId == userId);

            // If exists -> increase quantity
            if (existingCartItem != null)
            {
                existingCartItem.Quantity += quantity;
            }
            else
            {
                // Create new cart item
                var cartItem = new CartItem()
                {
                    MenuItemId = menuItemId,
                    Quantity = quantity,
                    UserId = (int)userId,
                    MenuItem = item
                };

                await _db.CartItems.AddAsync(cartItem);
            }

            await _db.SaveChangesAsync();
            return true;
        }
        public async Task<List<CartItem>> LoadCartForCartPage(int? userId)
        {
            return await _db.CartItems.Include(c => c.MenuItem).Where(x=>x.UserId== userId).ToListAsync();
        }
        public async Task RemoveFromCart(int id)
        {
            var z = await _db.CartItems.FindAsync(id);

            if(z is null)
            {
                return;
            }
             _db.CartItems.Remove(z);
         await   _db.SaveChangesAsync();
        }
        public async Task UpdateQuantityInCart(int cartItemId, int quantity)
        {
            var z = await _db.CartItems.FindAsync(cartItemId);
            if(z is null)
            {
                return;
            }
            z.Quantity = quantity;
            await _db.SaveChangesAsync();
        }
        public async Task updateAddNote(string note,int id)
        {
            var z= await _db.CartItems.FindAsync(id);
            if(z is null)
            {
                return;
            }
            z.Note = note;
            await _db.SaveChangesAsync();
        }
        public async Task<decimal> getTotalPrice(int? id)
        {
            if(id is null)
            {
                return -1;
            }
            var total = await _db.CartItems
                .Include(x => x.MenuItem)
                .Where(x => x.UserId == id)
                .SumAsync(x => x.Quantity * x.MenuItem.Price);

            return total;
        }
        public async Task<List<CartItem>> GetAllItemFromCard(int? id)
        {
            if( id is null) { return null; }
            return  _db.CartItems.Include(r => r.MenuItem).Where(x => x.UserId == id).ToList();
        }
        public async Task<decimal> GetTotalOrderPayByUserID(int? userId)
        {
            return await _db.CartItems
                .Where(r => r.UserId == userId)
                .SumAsync(x => x.MenuItem.Price * x.Quantity);

        }
        public async Task MakeTheAcceptForOrder(int userId)
        {
            // GET USER CART
            var cartItems = await _db.CartItems
                .Include(x => x.MenuItem)
                .Where(x => x.UserId == userId)
                .ToListAsync();
            
            // CALCULATE TOTAL
            decimal total = cartItems.Sum(x =>
                x.MenuItem.Price * x.Quantity);

            // CREATE ORDER
            Order order = new Order()
            {
                UserId = userId,
                TotalPrice = total,
                Status = "Pending",
                OrderDate = DateTime.Now
            };

            // SAVE ORDER
            _db.Orders.Add(order);

            await _db.SaveChangesAsync();

            // CREATE ORDER ITEMS
            foreach (var item in cartItems)
            {
                OrderItem orderItem = new OrderItem()
                {
                    OrderId = order.Id,
                    MenuItemId = item.MenuItemId,
                    Quantity = item.Quantity,
                    
                };
                if(item.Note != null)
                {
                    orderItem.Note = item.Note.Trim();
                }
                _db.OrderItems.Add(orderItem);
            }

            // SAVE ORDER ITEMS
            await _db.SaveChangesAsync();

            // CLEAR CART
            _db.CartItems.RemoveRange(cartItems);

            await _db.SaveChangesAsync();
        }
        public async Task<List<OrderItem>> LoadDetailsOrder(int orderID)
        {
            return await _db.OrderItems.Include(r => r.MenuItem).Where(x => x.OrderId == orderID).ToListAsync();

        }
        public async Task<Order> LoadOrder(int Orderid)
        {
            return await _db.Orders.FindAsync(Orderid);
        }
    }
}
