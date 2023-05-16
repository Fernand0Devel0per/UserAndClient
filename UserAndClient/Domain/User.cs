namespace UserAndClient.Domain
{
    public class User
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public DateTime AtCreated { get; set; }
    }
}
