using System.ComponentModel.DataAnnotations;

namespace CoreLiveCode.Dtos.User
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
