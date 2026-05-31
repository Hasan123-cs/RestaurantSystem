using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RestaurantSystem.Models;
using RestaurantSystem.Services;
using RestaurantSystem.Services.Admin;

namespace RestaurantSystem.Pages.Admin.Category
{
    public class RemoveCategoryModel : PageModel
    {
        public CRUD_CATEGORY Operations { get; set; }
        public IWebHostEnvironment env { get; set; }
        [BindProperty]
        public RestaurantSystem.Models.Category Category { get; set; }
        public RemoveCategoryModel(CRUD_CATEGORY s, IWebHostEnvironment e) {  Operations = s;env = e; }
        public async  Task OnGetAsync(int id)
        {
            Category = await Operations.LoadCategoryById(id);
        }
        public async Task<IActionResult> OnPostAsync()
        {
            var data = await Operations.LoadCategoryById(Category.Id);

            if (data != null)
            {
                string imagePath = env.WebRootPath + data.BackgroundImage;

                // check if file exists
                if (System.IO.File.Exists(imagePath))
                {
                    System.IO.File.Delete(imagePath);
                }

                // delete category from db

            }
                await Operations.RemoveCategoryFromService(data);
            return RedirectToPage("../Category/Index");
        }


    }
}
