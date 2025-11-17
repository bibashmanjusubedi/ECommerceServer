namespace ecomServer.Models
{
    /// <summary>
    /// Represents a product in the e-commerce system.
    /// </summary>
    public class Product
    {
        /// <summary>
        /// Gets or sets the unique identifier for the product.
        /// </summary>
        public int ProductId { get; set; }


        /// <summary>
        /// Gets or sets the name of the product.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the Stock Keeping Unit (SKU) of the product.
        /// </summary>
        public string SKU { get; set; }

        /// <summary>
        /// Gets or sets the price of the product.
        /// </summary>
        public decimal Price { get; set; }


        /// <summary>
        /// Gets or sets the category ID to which this product belongs.
        /// </summary>
        public int CategoryId { get; set; }

        /// <summary>
        /// Gets or sets the category associated with this product.
        /// </summary>
        public Category Category { get; set; } // Many-to-One Relationship with Category

        /// <summary>
        /// Gets or sets the inventory entry related to this product.
        /// </summary>
        public Inventory Inventory { get; set; } // One=to-One Relationship with Inventory

        /// <summary>
        /// Gets or sets the order items associated with this product.
        /// </summary>
        public ICollection<OrderItem> OrderItems { get; set; } // One-to-many Relationship with OrderItems
        
        /// <summary>
        /// Gets or sets the image data for the product.
        /// </summary>
        public byte[] ImageData { get; set; } // for Image data

        /// <summary>
        /// Gets or sets the MIME type of the product image.
        /// </summary>
        public string ImageMimeType { get; set; } // Stoe Image string
    }
}