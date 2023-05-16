namespace UserAndClient.Dtos.User
{
    public class UserCreateResponse
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string AtCreated { get; set; }
    }
}
