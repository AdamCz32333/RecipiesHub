using System.ComponentModel.DataAnnotations;

namespace RecipesHub.Models
{
    public class Recipe
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        [StringLength(500)]
        public string Description { get; set; }

        public string? Author { get; set; }


        public string? ImageUrl { get; set; }

    }
}
