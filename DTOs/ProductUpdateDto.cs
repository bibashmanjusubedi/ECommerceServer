
using Microsoft.AspNetCore.Http;

namespace ecomServer.DTOs
{
    public class ProductUpdateDto
    {
        public string Name { get; set; }
        public string SKU { get; set; }
        public decimal Price { get; set; }
        public int CategoryId { get; set; }
        public IFormFile? ImageFile { get; set; } // Optional on update; null means no change to image
    }
}
