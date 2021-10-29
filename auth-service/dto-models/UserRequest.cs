using System;
using System.ComponentModel.DataAnnotations;

namespace AuthService {
    public class UserRequest {
        [Required]
        public string? username { get; set; }

        [Required]
        public string? password { get; set; }
    }
}
