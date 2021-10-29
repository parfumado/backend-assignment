using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace AuthService.ViewModels {
    public class PasswordResetRequest {
        [JsonConstructor]
        public PasswordResetRequest(string email) {
            this.Email = email;
        }

        [Required, JsonPropertyName("email")]
        public string Email { get; set; }
        [JsonPropertyName("authPath")]
        public string? AuthPath { get; set; }
        [JsonPropertyName("preferredLanguageCode")]
        public string? PreferredLanguageCode { get; set; }
    }
}