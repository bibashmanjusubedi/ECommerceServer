namespace ecomServer.DTOs
{
    public class UpdateOrderDto
    {
        public DateTime OrderDate { get; set; }
        public int CustomerId { get; set; }

        // If you want to support updating order items as well:
        public List<UpdateOrderItemDto> OrderItems { get; set; }
    }
}