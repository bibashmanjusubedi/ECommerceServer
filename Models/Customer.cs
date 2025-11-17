namespace ecomServer.Models
{
    /// <summary>
    /// Represents a customer in the e-commerce system.
    /// </summary>
    public class Customer
    {
        /// <summary>
        /// Gets or sets the unique identifier for the customer.
        /// </summary>
        public int CustomerId { get; set; }

        /// <summary>
        /// Gets or sets the full name of the customer.
        /// </summary>
        public string FullName { get; set; }

        /// <summary>
        /// Gets or sets the email address of the customer.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the password hash for the customer.
        /// </summary>
        public string PasswordHash { get; set; }
  
        /// <summary>
        /// Gets or sets the collection of orders associated with this customer.
        /// </summary>
        public ICollection<Order> Orders { get; set; } = new List<Order>(); // one-to-many relationship with Orders
    }
}