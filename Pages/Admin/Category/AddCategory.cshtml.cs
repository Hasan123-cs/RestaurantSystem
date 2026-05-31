using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RestaurantSystem.Models.ViewModel;
using RestaurantSystem.Services.Admin;

namespace RestaurantSystem.Pages.Admin.Category
{
    public class AddCategoryModel : PageModel
    {
        public CRUD_CATEGORY c { get; set; }
        public AddCategoryModel(CRUD_CATEGORY c)
        {
            this.c = c;
        }
        [BindProperty]
        public CategoryAddViewModel Category { get; set; } = new();
        public void OnGet()
        {

        }
        public async Task<IActionResult> OnPostAsync(IFormFile CategoryImage)
        {
            if(CategoryImage is null)
            {
                ModelState.AddModelError("", "photo field is required");
            }
            if (!ModelState.IsValid)
            {
                return Page();
            }
            if(CategoryImage !=null)
            {
                var fileFolder = Path.Combine( Directory.GetCurrentDirectory(),"wwwroot/images");
                if(!Directory.Exists(fileFolder))
                {
                    Directory.CreateDirectory(fileFolder);
                }
                string fileName = Path.GetFileName(CategoryImage.FileName);
                string filePath = Path.Combine(fileFolder, fileName);


                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await CategoryImage.CopyToAsync(stream);

                }
                Category.BackgroundImage = "/images/" + fileName;
            }
            await c.AddCategoryToMenu(Category);
            return RedirectToPage("../Category/Index");
        }

    }
}
