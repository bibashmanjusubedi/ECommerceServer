namespace ecomServer.DTOs
{
    /// <summary>
    /// Data transfer object for creating a new order.
    /// </summary>
    public class CreateOrderDto
    {
        /// <summary>
        /// Gets or sets the date and time when the order is placed.
        /// </summary>
        public DateTime OrderDate { get; set; }

        /// <summary>
        /// Gets or sets the ID of the customer placing the order.
        /// </summary>
        public int CustomerId { get; set; }

        /// <summary>
        /// Gets or sets the list of order items for the order.
        /// </summary>
        public List<CreateOrderItemDto> OrderItems { get; set; }
    }

}