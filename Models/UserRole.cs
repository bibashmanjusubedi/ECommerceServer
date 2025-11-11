namespace ecomServer.Models
{
    public class UserRole
    {
        public int UserRoleId { get;set;}
        public int UserId {get;set;}
        public User User {get;set;} // many to one relationship with User
        public int RoleId {get;set;}
        public Role Role{get;set;} // many to one relationship with Role
    }
}