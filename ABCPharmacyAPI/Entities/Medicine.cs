using System.ComponentModel.DataAnnotations;

namespace ABCPharmacyAPI.Entities
{
    public class Medicine
    {
        public string FullName { get; set; }
        public string Notes { get; set; }
        public DateTime ExpiryDate { get; set; }
        public int Quantity { get; set; }
        [Required(ErrorMessage = "Price is required.")]
        // This regex allows numbers with exactly or up to 2 decimal places
        [RegularExpression(@"^\d+(\.\d{1,2})?$", ErrorMessage = "Price must have up to 2 decimal places.")]
        public decimal Price { get; set; }
        public string Brand { get; set; }
    }
}
