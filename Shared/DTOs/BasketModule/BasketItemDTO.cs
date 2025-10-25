using System.ComponentModel.DataAnnotations;

namespace Shared.DTOs.BasketModule
{
    public record BasketItemDTO
    {
        public int Id { get; init; }
        public string ProductName { get; init; } = string.Empty;
        [Range(1, double.MaxValue)]
        public decimal Price { get; init; }
        public string PictureUrl { get; init; } = string.Empty;
        [Range(1, 99)]
        public int Quantity { get; init; }
    }
}