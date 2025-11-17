namespace ecomServer.Models
{
    /// <summary>
    /// Represents a category in the e-commerce system.
    /// </summary>
    public class Category
    {
        /// <summary>
        /// Gets or sets the unique identifier for the category.
        /// </summary>
        public int CategoryId { get; set; }

        /// <summary>
        /// Gets or sets the name of the category.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the collection of products that belong to this category.
        /// </summary>
        public ICollection<Product> Products { get; set; } = new List<Product>();// One-to-many Relationship with Product
    }
}