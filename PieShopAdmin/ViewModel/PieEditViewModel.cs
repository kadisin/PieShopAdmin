using Microsoft.AspNetCore.Mvc.Rendering;
using PieShopAdmin.Models;

namespace PieShopAdmin.ViewModel
{
    public class PieEditViewModel
    {
        public IEnumerable<SelectListItem>? Categories { get; set; } = default!;
        public Pie? Pie;
    }
}
