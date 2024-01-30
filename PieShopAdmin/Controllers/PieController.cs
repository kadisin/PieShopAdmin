using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PieShopAdmin.Models;
using PieShopAdmin.Models.Repositories;
using PieShopAdmin.ViewModel;

namespace PieShopAdmin.Controllers
{
    public class PieController : Controller
    {

        private readonly IPieRepository _pieRepository;
        private readonly ICategoryRepository _categoryRepository;

        public PieController(IPieRepository pieRepository, ICategoryRepository categoryRepository)
        {
            _pieRepository = pieRepository;
            _categoryRepository = categoryRepository;
        }

        public async Task<IActionResult> Index()
        {
            var pies = await _pieRepository.GetAllPiesAsync();
            return View(pies);
        }

        public async Task<IActionResult> Details(int id)
        {
            var pie = await _pieRepository.GetPieByIdAsync(id);
            return View(pie);
        }

        //fill data to dropdown to select category related to pie etc. - pie model is empty
        public async Task<IActionResult> Add()
        {
            var allCategories = await _categoryRepository.GetAllCategoriesAsync();
            IEnumerable<SelectListItem> selectListItems = new SelectList(allCategories,
                "CategoryId", "Name", null);
            PieAddViewModel model = new() { Categories = selectListItems };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Add(PieAddViewModel model)
        {
            if(ModelState.IsValid)
            {
                Pie pie = new()
                {
                    CategoryId = model.Pie.CategoryId,
                    ShortDescription = model.Pie.ShortDescription,
                    LongDescription = model.Pie.LongDescription,
                    Price = model.Pie.Price,
                    AllergyInformation = model.Pie.AllergyInformation,
                    ImageThumbnailUrl = model.Pie.ImageThumbnailUrl,
                    ImageUrl = model.Pie.ImageUrl,
                    InStock = model.Pie.InStock,
                    IsPieOfTheWeek = model.Pie.IsPieOfTheWeek,
                    Name = model.Pie.Name
                };

                await _pieRepository.AddPieAsync(pie);
                return RedirectToAction(nameof(Index));
            }

            var allCategories = await _categoryRepository.GetAllCategoriesAsync();
            IEnumerable<SelectListItem> selectListItems = new SelectList(allCategories,
                "CategoryId", "Name", null);
            model.Categories = selectListItems;
            return View(model);
            
        }

    }
}
