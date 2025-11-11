namespace ecomServer.Models
{
    public class OrderItem
    {
        public int OrderItemId { get; set; }
        public int OrderId { get; set; } 
        public Order Order { get; set; } // many-to-one relationship with Order
        public int ProductId { get; set; }
        public Product Product { get; set; } // many to one relationship with Product
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
    }
}