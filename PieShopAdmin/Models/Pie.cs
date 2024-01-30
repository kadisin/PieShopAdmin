using System.ComponentModel.DataAnnotations;

namespace PieShopAdmin.Models
{
    public class Pie
    {
        public int PieId { get; set; }

        [Display(Name = "Name")]
        [Required]
        public string Name { get; set; } = string.Empty;

        [StringLength(100)]
        [Display(Name = "Short Description")]
        public string? ShortDescription { get; set; }
        
        [StringLength(1000)]
        [Display(Name = "Long Description")]
        public string? LongDescription { get; set; }
        
        [StringLength(1000)]
        [Display(Name = "Allergy Information")]
        public string? AllergyInformation { get; set; }

        [Display(Name = "Price")]
        public decimal Price { get; set; }

        [Display(Name = "Image URL")]
        public string? ImageUrl { get; set; }

        [Display(Name = "Image thumbnail URL")]
        public string? ImageThumbnailUrl { get; set; }

        [Display(Name = "Is pie of the week?")]
        public bool IsPieOfTheWeek { get; set; }

        [Display(Name= "In stock?")]
        public bool InStock { get; set; }

        [Display(Name = "Category ID")]
        public int CategoryId { get; set; }

        [Display(Name = "Category")]
        public Category? Category { get; set; }

        [Display(Name = "Ingredients")]
        public ICollection<Ingredient>? Ingredients { get; set; }
    }
}
