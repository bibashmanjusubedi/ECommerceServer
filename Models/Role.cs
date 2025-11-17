namespace ecomServer.Models
{
    /// <summary>
    /// Represents a role that can be assigned to users in the e-commerce system.
    /// </summary>
    public class Role
    {
        /// <summary>
        /// Gets or sets the unique identifier for the role.
        /// </summary>
        public int RoleId { get; set; }

        /// <summary>
        /// Gets or sets the name of the role.
        /// </summary>
        public string RoleName { get; set; }

        /// <summary>
        /// Gets or sets the collection of user-role associations for this role (one-to-many relationship).
        /// </summary>
        public ICollection<UserRole> UserRoles { get; set; } // one to many relationship with UserRole

    }

}