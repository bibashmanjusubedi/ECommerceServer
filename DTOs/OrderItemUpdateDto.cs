namespace ecomServer.DTOs
{
    /// <summary>
    /// Data transfer object for updating an existing order item.
    /// </summary>
    public class OrderItemUpdateDto
    {
        /// <summary>
        /// Gets or sets the unique identifier for the order item.
        /// </summary>
        public int OrderItemId { get; set; }

        /// <summary>
        /// Gets or sets the ID of the order this item belongs to.
        /// </summary>
        public int OrderId { get; set; }

        /// <summary>
        /// Gets or sets the ID of the product being ordered.
        /// </summary>
        public int ProductId { get; set; }

        /// <summary>
        /// Gets or sets the quantity of the product to update.
        /// </summary>
        public int Quantity { get; set; }

        /// <summary>
        /// Gets or sets the unit price of the product at the time of update.
        /// </summary>
        public decimal UnitPrice { get; set; }
    }

}