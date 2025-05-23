namespace EBIN.Models;

public class ProfileFollowers
{
    public int FollowerId { get; set; }
    public Profiles Follower { get; set; }
    
    public int FollowingId { get; set; }
    public Profiles Following { get; set; }
}