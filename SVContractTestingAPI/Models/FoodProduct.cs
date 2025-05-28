using System.ComponentModel.DataAnnotations;

namespace SVContractTestingAPI.Models
{
    public class FoodProduct
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public DateTime ExpiryDate { get; set; }
    }
}
