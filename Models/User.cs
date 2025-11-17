namespace ecomServer.Models
{
    /// <summary>
    /// Represents a user in the e-commerce system.
    /// </summary>
    public class User
    {
        /// <summary>
        /// Gets or sets the unique identifier for the user.
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// Gets or sets the username of the user.
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Gets or sets the password hash for the user.
        /// </summary>
        public string PasswordHash { get; set; }

        /// <summary>
        /// Gets or sets the email address of the user.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the collection of user-role associations for this user (one-to-many relationship).
        /// </summary>
        public ICollection<UserRole> UserRoles { get; set; } // one to many relationship with UserRole

    }
}