namespace ecomServer.DTOs
{
    /// <summary>
    /// Data transfer object for creating a new order item as part of an order.
    /// </summary>
    public class CreateOrderItemDto
    {
        /// <summary>
        /// Gets or sets the ID of the product being ordered.
        /// </summary>
        public int ProductId { get; set; }

        /// <summary>
        /// Gets or sets the quantity of the product to order.
        /// </summary>
        public int Quantity { get; set; }
        
        /// <summary>
        /// Gets or sets the unit price of the product at the time of ordering.
        /// </summary>
        public decimal UnitPrice { get; set; }
    }
}