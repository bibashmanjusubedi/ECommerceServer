namespace ecomServer.DTOs
{
    public class ProductCreateDto
    {
        public string Name { get; set; }
        public string SKU { get; set; }
        public decimal Price { get; set; }
        public int CategoryId { get; set; }
        public IFormFile ImageFile { get; set; } // For file upload - will contain the image file
    }
}
