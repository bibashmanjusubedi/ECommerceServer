namespace ecomServer.DTOs
{
    /// <summary>
    /// Data transfer object for updating an order item within an order.
    /// </summary>
    public class UpdateOrderItemDto
    {
        /// <summary>
        /// Gets or sets the ID of the product being updated in the order item.
        /// </summary>
        public int ProductId { get; set; }

        /// <summary>
        /// Gets or sets the updated quantity of the product for the order item.
        /// </summary>
        public int Quantity { get; set; }

        /// <summary>
        /// Gets or sets the updated unit price of the product for the order item.
        /// </summary>
        public decimal UnitPrice { get; set; }
    }

}
