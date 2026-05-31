using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using RestaurantSystem.Models.ViewModel;
using RestaurantSystem.Services;
using RestaurantSystem.Services.Admin;

namespace RestaurantSystem.Pages.Admin.MenuItem
{
    public class EditMenuItemModel : PageModel
    {
        public CRUD_CATEGORY c { get; set; }
        public CRUD_MENU m { get; set; }
        public List<RestaurantSystem.Models.Category> categorys { get; set; }

        public EditMenuItemModel(CRUD_MENU m, CRUD_CATEGORY c)
        {
            this.m = m;
            this.c = c;
        }

        [BindProperty]
        public RestaurantSystem.Models.MenuItem MenuItem { get; set; } = new();
        public RestaurantSystem.Models.MenuItem OldMenuItem { get; set; } = new();
        public List<SelectListItem> categ { get; set; }
        public async Task<IActionResult> OnGetAsync(int id)
        {
            MenuItem = await m.LoadItemById(id);
            if(MenuItem == null)
            {
                return NotFound("zabeta aapage error");
            }
            categorys = await c.LoadAllCategory();
            if(categorys is null)
            {
                return NotFound("zabeta aapage error");

            }
            categ = new List<SelectListItem>();
            foreach (var category in categorys) { 
            if(category.Id == MenuItem.CategoryId)
                {
                    categ.Add(new SelectListItem() { Text = category.Name, Value = category.Id.ToString() ,Selected=true });

                }
                else
                {

                categ.Add(new SelectListItem() { Text = category.Name ,Value=category.Id.ToString()});
                }
            
            }
            return Page();
        }
        public async Task<IActionResult> OnPostAsync(IFormFile newImage)
        {
            OldMenuItem = await m.LoadItemById(MenuItem.Id);
            if (newImage != null)
            {
                string imagesFolder = Path.Combine(
                    Directory.GetCurrentDirectory(),
                    "wwwroot/images"
                );

                if (!Directory.Exists(imagesFolder))
                {
                    Directory.CreateDirectory(imagesFolder);
                }

                string fileName = Path.GetFileName(newImage.FileName);

                string filePath = Path.Combine(imagesFolder, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await newImage.CopyToAsync(stream);
                }
            MenuItem.ImagePath = "/images/" + fileName;
            }
            else
            {
                MenuItem.ImagePath = OldMenuItem.ImagePath;
            }
            await m.UpdateMenuItem(MenuItem);
            return RedirectToPage("../MenuItem/Index");
        }
    }
}
