using CoreLiveCode.Dtos.User;

namespace CoreLiveCode.Dtos.Client
{
    public class ClientCreateResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string AtCreated { get; set; }
        public UserClientResponse User { get; set; }

    }
}
