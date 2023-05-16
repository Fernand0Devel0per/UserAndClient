using System.ComponentModel.DataAnnotations;

namespace UserAndClient.Dtos.Client
{
    public class ClientCreateRequest
    {
        [Required]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [Phone]
        public string PhoneNumber { get; set; }

        [Required]
        public Guid UserId { get; set; }
    }
}
