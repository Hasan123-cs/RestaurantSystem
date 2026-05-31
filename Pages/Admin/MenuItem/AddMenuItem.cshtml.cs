using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using RestaurantSystem.Models;
using RestaurantSystem.Models.ViewModel;
using RestaurantSystem.Services;
using RestaurantSystem.Services.Admin;

namespace RestaurantSystem.Pages.Admin.MenuItem
{
    public class AddMenuItem : PageModel
    {
        public CategoryOperation c;
        public CRUD_CATEGORY x;
        
        public AddMenuItem(CategoryOperation c,CRUD_CATEGORY z)
        {
            x = z;
            this.c = c;
        }
        [BindProperty]
        public int CategoryId { get; set; }
        [BindProperty]
        public MenuItemAddViewModel MenuItem { get; set; } = new();
            public List<SelectListItem> ListOfCategory { get; set; } = new();
        public List<RestaurantSystem.Models.Category> Categories { get; set; } = new();
        public async Task OnGet()
        {
            Categories = await x.LoadAllCategory();
            ListOfCategory = Categories.Select(n=>new SelectListItem { Text= n.Name , Value = n.Id.ToString() }).ToList();
            
        }

        public async Task<IActionResult> OnPostAsync(IFormFile ImageFile)
        {
            Categories = await x.LoadAllCategory();
            ListOfCategory = Categories.Select(n => new SelectListItem { Text = n.Name, Value = n.Id.ToString() }).ToList();

            if (ImageFile == null)
            {
                ModelState.AddModelError("", "Image Field Required");
            }
            if(!ModelState.IsValid)
            {
                return Page();
            }
            if (ImageFile != null)
            {
                var fileFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images");
                if(!Directory.Exists(fileFolder))
                {
                    Directory.CreateDirectory(fileFolder);
                }
                var fullFile = Path.GetFileName(ImageFile.FileName);
                var filePath = Path.Combine(fileFolder, fullFile);
                using(var stream = new FileStream(filePath,FileMode.Create))
                {
                    await ImageFile.CopyToAsync(stream);
                }
                MenuItem.ImagePath = "/images/"+ fullFile;
            }
            else
            {
                MenuItem.ImagePath = "NO_PHOTO";
            }
            await x.AddMenuItem(MenuItem);
            return RedirectToPage("../MenuItem/Index");
        }
    }
}
