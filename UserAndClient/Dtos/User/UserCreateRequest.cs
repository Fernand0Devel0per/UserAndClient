﻿using System.ComponentModel.DataAnnotations;

namespace UserAndClient.Dtos.User
{
    public class UserCreateRequest
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        
        [Required]
        public string Name { get; set; }
    }
}
