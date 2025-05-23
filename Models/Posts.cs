namespace EBIN.Models
{
    public class Posts
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? PathToFile { get; set; }
        
        public int ProfilesID { get; set; }
        public Profiles? Profile { get; set; }
    }
}
