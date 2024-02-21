using System.ComponentModel.DataAnnotations;

namespace SupermarketWebAPI.Resources
{
    public record SaveCategoryResource
    {
        [Required]
        [MaxLength(30)]
        public string? Name { get; init; }
    }
}
