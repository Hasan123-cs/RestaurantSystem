using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RestaurantSystem.Models;
using RestaurantSystem.Models.ViewModel;
using RestaurantSystem.Services.Admin;

namespace RestaurantSystem.Pages.Admin.Category
{
    public class EditCategoryModel : PageModel
    {
        public CRUD_CATEGORY c;
        public EditCategoryModel(CRUD_CATEGORY z) {  c = z; }
        [BindProperty]
        public RestaurantSystem.Models.Category Category { get; set; }
        public RestaurantSystem.Models.Category OldCategory { get; set; }

        public async Task<IActionResult> OnGet(int id)

        {
            Category = await c.LoadCategoryById(id);
            

            if (Category == null)
            {
                return RedirectToPage("/Admin/Categories/Index");
            }

            return Page();
        }
          public async Task<IActionResult> OnPost(IFormFile newImage)
        {
            OldCategory = await c.LoadCategoryById(Category.Id);
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
                Category.BackgroundImage = "/images/" + fileName;

            }
            else
            {
               Category.BackgroundImage =OldCategory.BackgroundImage;
            }
           
            await c.UpdateRecentCategory(Category);

            return RedirectToPage("Index");
        }

    }
}
