using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PieShopAdmin.Models;
using PieShopAdmin.Models.Repositories;
using PieShopAdmin.Models.Utilities;
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
            try
            {
                var allCategories = await _categoryRepository.GetAllCategoriesAsync();
                IEnumerable<SelectListItem> selectListItems = new SelectList(allCategories,
                    "CategoryId", "Name", null);
                PieAddViewModel model = new() { Categories = selectListItems };
                return View(model);
            }
            catch (Exception ex)
            {
                ViewData["ErrorMessage"] = $"There was an error: {ex.Message}";
            }
            return View(new PieAddViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Add(PieAddViewModel model)
        {
            if (ModelState.IsValid)
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

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var allCategories = await _categoryRepository.GetAllCategoriesAsync();
            IEnumerable<SelectListItem> selectListItems = new SelectList(allCategories,
                "CategoryId", "Name", null);

            var selectedPie = await _pieRepository.GetPieByIdAsync(id.Value);

            PieEditViewModel pieEditViewModel = new()
            {
                Categories = selectListItems,
                Pie = selectedPie
            };
            return View(pieEditViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(PieEditViewModel pieEditViewModel)
        {
            try
            {
                if(ModelState.IsValid)
                {
                    await _pieRepository.UpdatePieAsync(pieEditViewModel.Pie);
                    return RedirectToAction(nameof(Index));
                }  
                else
                {
                    return BadRequest();
                }
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", $"Updating the Pie failed, please try again! Error: {ex.Message}");
            }

            var allCategories = await _categoryRepository.GetAllCategoriesAsync();
            IEnumerable<SelectListItem> selectListItems = new SelectList(allCategories,
                "CategoryId", "Name", null);
            pieEditViewModel.Categories = selectListItems;
            return View(pieEditViewModel);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var pieToDelete = await _pieRepository.GetPieByIdAsync(id);
            return View(pieToDelete);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int? pieId)
        {
            if(pieId == null)
            {
                ViewData["ErrorMessage"] = "Deleting the pie failed, invalid id";
                return View();
            }

            try
            {
                await _pieRepository.DeletePieAsync(pieId.Value);
                TempData["PieDeleted"] = "Pie deleted successfully";

                return RedirectToAction(nameof(Index));
            }
            catch(Exception ex)
            {
                ViewData["ErrorMessage"] = $"Deleting the pie failed! Error: {ex.Message}";
            }

            var selectedPie = await _pieRepository.GetPieByIdAsync(pieId.Value);
            return View(selectedPie);
        }

        public async Task<IActionResult> IndexPaging(int? pageNumber)
        {
            var pies = await _pieRepository.GetPiesPagedAsync(pageNumber, Consts.PageSize);
            pageNumber ??= 1;

            var count = await _pieRepository.GetAllPiesCountAsync();
            return View(new PagedList<Pie>(pies.ToList(), count, pageNumber.Value, Consts.PageSize));
        }

        public async Task<IActionResult> IndexPagingSorting(string sortBy, int? pageNumber)
        {
            ViewData["CurrentSort"] = sortBy;
            ViewData["IdSortParam"] = String.IsNullOrEmpty(sortBy) || sortBy == Consts.IdDesc ? Consts.Id : Consts.IdDesc;
            ViewData["NameSortParam"] = sortBy == Consts.Name ? Consts.NameDesc : Consts.Name;
            ViewData["PriceSortParam"] = sortBy == Consts.Price ? Consts.PriceDesc : Consts.Price;

            pageNumber ??= 1;
            var pies = await _pieRepository.GetPiesSortedAndPagedAsync(sortBy, pageNumber, Consts.PageSize);

            var count = await _pieRepository.GetAllPiesCountAsync();
            return View(new PagedList<Pie>(pies.ToList(), count, pageNumber.Value, Consts.PageSize));

        }

        public async Task<IActionResult> Search(string? searchQuery, int? searchCategory)
        {
            var allCategories = await _categoryRepository.GetAllCategoriesAsync();
            IEnumerable<SelectListItem> selectListItems = new SelectList(allCategories,
                "CategoryId", "Name", null);
            if(searchQuery != null)
            {
                var pies = await _pieRepository.SearchPies(searchQuery, searchCategory);
                return View(new PieSearchViewModel()
                {
                    Pies = pies,
                    SearchCategory = searchCategory,
                    Categories = selectListItems,
                    SearchQuery = searchQuery
                });
            }
            return View(new PieSearchViewModel()
            {
                Pies = new List<Pie>(),
                SearchCategory = null,
                Categories = selectListItems,
                SearchQuery = string.Empty
            });
        }
  }
}
