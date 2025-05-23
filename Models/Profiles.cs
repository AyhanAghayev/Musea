namespace EBIN.Models
{
    public class Profiles
    {
        public int Id { get; set; }
        public string? UserName { get; set; }
        public string? Password { get; set; }
        public string? ProfilePicturePath { get;set; }

        public ICollection<Posts> Post { get; set; } = new List<Posts>();

        // Many-to-many relationship
        public ICollection<ProfileFollowers> Followers { get; set; } = new List<ProfileFollowers>();
        public ICollection<ProfileFollowers> Following { get; set; } = new List<ProfileFollowers>();
    }
}