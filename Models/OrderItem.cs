namespace ecomServer.Models
{
    /// <summary>
    /// Represents an item in an order in the e-commerce system.
    /// </summary>
    public class OrderItem
    {
        /// <summary>
        /// Gets or sets the unique identifier for the order item.
        /// </summary>
        public int OrderItemId { get; set; }

        /// <summary>
        /// Gets or sets the ID of the order that this item belongs to.
        /// </summary>
        public int OrderId { get; set; }

        /// <summary>
        /// Gets or sets the order that this item belongs to. (Many-to-one relationship)
        /// </summary>
        public Order Order { get; set; } // many-to-one relationship with Order

        /// <summary>
        /// Gets or sets the ID of the product for this order item.
        /// </summary>
        public int ProductId { get; set; }

        /// <summary>
        /// Gets or sets the product for this order item. (Many-to-one relationship)
        /// </summary>
        public Product Product { get; set; } // many to one relationship with Product

        /// <summary>
        /// Gets or sets the quantity of the product ordered.
        /// </summary>
        public int Quantity { get; set; }

        /// <summary>
        /// Gets or sets the unit price of the product at the time of ordering.
        /// </summary>
        public decimal UnitPrice { get; set; }
    }
}