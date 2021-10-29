using System;
using System.Text.Json.Serialization;

namespace AuthService {
    public class UserSession {
        [JsonIgnore]
        public string? SessionToken { get; set; }
        [JsonPropertyName("csrfToken")]
        public string? CsrFToken { get; set; }
        [JsonPropertyName("user")]
        public User? User { get; set; }
        [JsonPropertyName("expiresIn")]
        public TimeSpan ExpiresIn { get; set; }
    }
}