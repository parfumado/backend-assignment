using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using CommonServices;

namespace AuthService {
    public class UserCreate {
        [JsonConstructor]
        public UserCreate(string firstName, string email, string password, bool acceptsMarketingEmail, bool acceptsTermsAndConditions, string languageLocaleCode) {
            this.FirstName = firstName;
            this.Email = email;
            this.Password = password;
            this.AcceptsMarketingEmail = acceptsMarketingEmail;
            this.AcceptsTermsAndConditions = acceptsTermsAndConditions;
            this.LanguageLocaleCode = languageLocaleCode;
        }

        [JsonPropertyName("firstName"), Required]
        public string FirstName { get; set; }

        [JsonPropertyName("lastName")]
        public string? LastName { get; set; }

        [JsonPropertyName("email"), Required]
        public string Email { get; set; }

        [JsonPropertyName("password"), Required]
        public string Password { get; set; }

        [JsonPropertyName("acceptsMarketingEmail"), Required]
        public bool AcceptsMarketingEmail { get; set; }

        [JsonPropertyName("acceptsTermsAndConditions"), Required]
        public bool AcceptsTermsAndConditions { get; set; }

        [JsonPropertyName("languageLocaleCode"), Required]
        public string LanguageLocaleCode { get; set; }

    }
}