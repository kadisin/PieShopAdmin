using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ActionConstraints;
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
        
        //method to get category to update on edit-view
        public async Task<IActionResult> Edit(int? id)
        {
            if(id == null)
            {
                return NotFound();
            }

            var selectedCategory = await _categoryRepository.GetCategoryByIdAsync(id.Value);
            return View(selectedCategory);
        }

        //edit category
        [HttpPost]
        public async Task<IActionResult> Edit(Category category)
        {
            try
            {
                if(ModelState.IsValid)
                {
                    await _categoryRepository.UpdateCategoryAsync(category); ;
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    return BadRequest();
                }
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", $"Updating the category failed, please try again! Error: {ex.Message}");
            }

            return View(category);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var selectedCategory = await _categoryRepository.GetCategoryByIdAsync(id);
            return View(selectedCategory);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int? CategoryId)
        {
            if(CategoryId == null)
            {
                ViewData["ErrorMessage"] = "Deleting the category failed, invalid ID";
                return View();
            }

            try
            {
                await _categoryRepository.DeleteCategoryAsync(CategoryId.Value);
                TempData["CategoryDeleted"] = "Category deleted successfully";

                return RedirectToAction(nameof(Index));
            }
            catch(Exception ex)
            {
                ViewData["ErrorMessage"] = $"Deleting the category failed, please try again! Error: {ex.Message}";

            }

            var selectedCategory = await _categoryRepository.GetCategoryByIdAsync(CategoryId.Value);
            return View(selectedCategory);
        }
    }
}
