namespace ecomServer.DTOs
{
    /// <summary>
    /// Data transfer object for creating a new product.
    /// </summary>
    public class ProductCreateDto
    {
        /// <summary>
        /// Gets or sets the name of the product to create.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the SKU (stock keeping unit) of the product.
        /// </summary>
        public string SKU { get; set; }

        /// <summary>
        /// Gets or sets the price of the product.
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// Gets or sets the category ID for the product.
        /// </summary>
        public int CategoryId { get; set; }

        /// <summary>
        /// Gets or sets the image file uploaded for the product.
        /// </summary>
        public IFormFile ImageFile { get; set; } // For file upload - will contain the image file
    }
}
