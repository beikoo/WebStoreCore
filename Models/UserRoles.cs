namespace Models
{
    public class UserRoles
    {
        public int UserId { get; set; }
        public Person User { get; set; }
        public int RoleId { get; set; }
        public Role Role { get; set; }
    }
}