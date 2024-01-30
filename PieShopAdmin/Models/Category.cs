using System.ComponentModel.DataAnnotations;

namespace PieShopAdmin.Models
{
    public class Category
    {
        public int CategoryId { get; set; }

        [StringLength(50, ErrorMessage = "The name should be no longer than 50 characters.")]
        [Display(Name = "Name")]
        public string Name { get; set; } = string.Empty;

        [Display(Name = "Description")]
        [StringLength(1000, ErrorMessage = "The description should be no longer than 1000 characters.")]
        public string? Description { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Date added")]
        public DateTime? DateAdded { get; set; }

        public ICollection<Pie>? Pies { get; set; }

    }
}
