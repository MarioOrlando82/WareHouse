using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WareHouse_MarioOrlando.Data;
using WareHouse_MarioOrlando.Models;
using WareHouse_MarioOrlando.Models.Domain;

namespace WareHouse_MarioOrlando.Controllers
{
    public class CategoryController : Controller
    {
        private readonly MVCDbContext mvcDbContext;

        public CategoryController(MVCDbContext mvcDbContext)
        {
            this.mvcDbContext = mvcDbContext;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var categories = await mvcDbContext.Categories.ToListAsync();
            return View(categories);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddCategoryViewModel addCategoryRequest)
        {
            var category = new Category()
            {
                Id = Guid.NewGuid(),
                Name = addCategoryRequest.Name
            };

            await mvcDbContext.Categories.AddAsync(category);
            await mvcDbContext.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> View(Guid Id)
        {
            var category = await mvcDbContext.Categories.FirstOrDefaultAsync(x => x.Id == Id);

            if (category != null)
            {
                var viewModel = new UpdateCategoryViewModel()
                {
                    Id = category.Id,
                    Name = category.Name
                };
                return await Task.Run(() => View("View", viewModel));
            }

            return RedirectToAction("Index");
        }

        
        [HttpPost]
        public async Task<IActionResult> View(UpdateCategoryViewModel model)
        {
            var category = await mvcDbContext.Categories.FindAsync(model.Id);
            if (category != null)
            {
                category.Name = model.Name;
                await mvcDbContext.SaveChangesAsync();

                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }

        /*
         
         */
        [HttpPost]
        public async Task<IActionResult> Delete(UpdateCategoryViewModel model)
        {
            var category = await mvcDbContext.Categories.FindAsync(model.Id);
            if (category != null)
            {
                mvcDbContext.Categories.Remove(category);
                await mvcDbContext.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");

        }
    }
}
