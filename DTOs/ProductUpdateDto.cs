
using Microsoft.AspNetCore.Http;

namespace ecomServer.DTOs
{
    /// <summary>
    /// Data transfer object for updating an existing product.
    /// </summary>
    public class ProductUpdateDto
    {
        /// <summary>
        /// Gets or sets the updated name of the product.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the updated SKU (stock keeping unit) of the product.
        /// </summary>
        public string SKU { get; set; }

        /// <summary>
        /// Gets or sets the updated price of the product.
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// Gets or sets the updated category ID for the product.
        /// </summary>
        public int CategoryId { get; set; }
        
        /// <summary>
        /// Gets or sets the new image file for the product, if provided.
        /// If null, the image will not be changed.
        /// </summary>
        public IFormFile? ImageFile { get; set; } // Optional on update; null means no change to image
    }
}
