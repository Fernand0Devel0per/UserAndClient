namespace UserAndClient.Domain
{
    public class Client
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime AtCreated { get; set; }
        public Guid UserId { get; set; }
    }
}
