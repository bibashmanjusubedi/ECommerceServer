namespace ecomServer.DTOs
{
    public class UpdateOrderItemDto
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
    }

}
