namespace SharedModels.Models
{
    public class Role
    {
        public int RoleId { get; set; }

        public string UserRole { get; set; }

        public virtual ICollection<User> Users { get; set; } = new List<User>();
    }
}
