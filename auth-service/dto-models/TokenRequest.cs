using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using CommonServices;

namespace AuthService {
    public class TokenRequest {

        [JsonPropertyName("token"), Required]
        public string? token { get; set; }

    }
}