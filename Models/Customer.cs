namespace ecomServer.Models
{
    public class Customer
    {
        public int CustomerId {get;set;}
        public string FullName {get;set;}
        public string Email {get;set;}
        public string PasswordHash { get; set; }
        public ICollection<Order> Orders {get;set;} =new List<Order>(); // one-to-many relationship with Orders
    }
}