namespace ecomServer.DTOs
{
    public class CreateOrderDto
    {
        public DateTime OrderDate { get; set; }
        public int CustomerId { get; set; }
        public List<CreateOrderItemDto> OrderItems { get; set; }
    }

}