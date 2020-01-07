namespace Models
{
    public class UserRoles:BaseModel
    {
        public int UserId { get; set; }
        public Person User { get; set; }
        public int RoleId { get; set; }
        public Role Role { get; set; }
    }
}