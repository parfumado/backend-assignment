using System;
using System.Text.Json.Serialization;

namespace CommonServices.Models {
    public class UserSession {

        [JsonPropertyName("csrfToken")]
        public string? CsrFToken { get; set; }
        [JsonPropertyName("user")]
        public User? User { get; set; }
    }
}
