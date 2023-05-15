using System.ComponentModel.DataAnnotations;

namespace CoreLiveCode.Dtos.Client
{
    public class ClientCreateRequest
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public Guid UserId { get; set; }
    }
}
