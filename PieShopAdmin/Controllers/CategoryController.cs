using Microsoft.AspNetCore.Mvc;
using PieShopAdmin.Models;
using PieShopAdmin.Models.Repositories;
using PieShopAdmin.ViewModel;
using System.Security.Cryptography;

namespace PieShopAdmin.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryController(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<IActionResult> Index()
        {
            CategoryListViewModel model = new()
            {
                Categories = (await _categoryRepository.GetAllCategoriesAsync()).ToList()
            };
            return View(model);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if(id == null)
            {
                return NotFound();
            }

            var selectedCategory = await _categoryRepository.GetCategoryByIdAsync(id.Value);
            return View(selectedCategory);
        }

        public IActionResult Add()
        {
            return View();
        }

        //we only need that properties that are in bind section to create new category
        [HttpPost]
        public async Task<IActionResult> Add([Bind("Name,Description,DateAdded")] Category category)
        {
            try
            {
                if(ModelState.IsValid)
                {
                    await _categoryRepository.AddCategoryAsync(category);
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"Adding the category failed, please try again! Error: {ex.Message}");

            }
            return View(category);
        }
    }
}
