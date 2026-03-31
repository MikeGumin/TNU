namespace TNU.Models
{
    public class Worker
    {
        public string FullName { get; set; }
        Role Role { get; set; }
    }

    public enum Role 
    {
        Defolt,
    }
}
