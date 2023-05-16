using System.ComponentModel.DataAnnotations;

namespace UserAndClient.Dtos.User
{
    public class UserUpdateRequest
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Name { get; set; }
    }
}
