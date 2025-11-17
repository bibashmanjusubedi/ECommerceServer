namespace ecomServer.Models
{
    /// <summary>
    /// Represents an order placed by a customer in the e-commerce system.
    /// </summary>
    public class Order
    {
        /// <summary>
        /// Gets or sets the unique identifier for the order.
        /// </summary>
        public int OrderId {get;set;}

        /// <summary>
        /// Gets or sets the date and time when the order was placed.
        /// </summary>
        public DateTime OrderDate {get;set;}

        /// <summary>
        /// Gets or sets the ID of the customer who placed the order.
        /// </summary>
        public int CustomerId {get;set;}

        /// <summary>
        /// Gets or sets the customer who placed the order. (Many-to-one relationship)
        /// </summary>
        public Customer Customer {get;set;} // many-to-one relationship with Customerr

        /// <summary>
        /// Gets or sets the collection of order items associated with this order. (One-to-many relationship)
        /// </summary>
        public ICollection<OrderItem> OrderItems {get;set;} // one-to-many relationship with OrderItems

    }
}