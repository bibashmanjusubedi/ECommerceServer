namespace ecomServer.DTOs
{
    /// <summary>
    /// Data transfer object for updating an existing order.
    /// </summary>
    public class UpdateOrderDto
    {
        /// <summary>
        /// Gets or sets the new date and time for the order.
        /// </summary>
        public DateTime OrderDate { get; set; }

        /// <summary>
        /// Gets or sets the ID of the customer associated with the order.
        /// </summary>
        public int CustomerId { get; set; }

        /// <summary>
        /// Gets or sets the list of updated order items for the order.
        /// </summary>
        // If you want to support updating order items as well:
        public List<UpdateOrderItemDto> OrderItems { get; set; }
    }
}