namespace ecomServer.Models
{
    public class Order
    {
        public int OrderId {get;set;}
        public DateTime OrderDate {get;set;}
        public int CustomerId {get;set;}
        public Customer Customer {get;set;} // many-to-one relationship with Customerr
        public ICollection<OrderItem> OrderItems {get;set;} // one-to-many relationship with OrderItems

    }
}