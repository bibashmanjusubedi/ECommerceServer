namespace ecomServer.Models
{
    /// <summary>
    /// Represents the association between a user and a role in the e-commerce system.
    /// </summary>
    public class UserRole
    {
        /// <summary>
        /// Gets or sets the unique identifier for the user-role association.
        /// </summary>
        public int UserRoleId { get; set; }

        /// <summary>
        /// Gets or sets the ID of the user in this association.
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// Gets or sets the user in this association. (Many-to-one relationship)
        /// </summary>
        public User User { get; set; } // many to one relationship with User

        /// <summary>
        /// Gets or sets the ID of the role in this association.
        /// </summary>
        public int RoleId { get; set; }

        /// <summary>
        /// Gets or sets the role in this association. (Many-to-one relationship)
        /// </summary>
        public Role Role { get; set; } // many to one relationship with Role
    }
}