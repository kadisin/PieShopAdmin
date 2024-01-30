using Microsoft.AspNetCore.Mvc.Rendering;
using PieShopAdmin.Models;

namespace PieShopAdmin.ViewModel
{
    public class PieAddViewModel
    {
        public IEnumerable<SelectListItem>? Categories { get; set; } = default!;
        public Pie? Pie { get; set; }
    }
}
