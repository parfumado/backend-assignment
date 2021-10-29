using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Text.Json.Serialization;
using CommonServices;
using Utility;
using Utility.Runtime;

namespace AuthService {
    public class UpdateUserRequest {

        [JsonPropertyName("password")]
        public string? Password { get; set; }

        [JsonPropertyName("username")]
        public string? Username { get; set; }

        [JsonPropertyName("email")]
        public string? Email { get; set; }
        [JsonPropertyName("firstName")]
        public string? FirstName { get; set; }
        [JsonPropertyName("lastName")]
        public string? LastName { get; set; }
        [JsonPropertyName("phoneNumber")]
        public string? PhoneNumber { get; set; }
        [JsonPropertyName("postalCode")]
        public string? PostalCode { get; set; }
        [JsonPropertyName("houseNumber")]
        public string? HouseNumber { get; set; }
        [JsonPropertyName("addition")]
        public string? Addition { get; set; }
        [JsonPropertyName("street")]
        public string? Street { get; set; }
        [JsonPropertyName("city")]
        public string? City { get; set; }
        [JsonPropertyName("countryTwoDigitCode")]
        public string? CountryTwoDigitCode { get; set; }
        [JsonPropertyName("languageLocaleCode")]
        public string? LanguageLocaleCode { get; set; }

        [JsonPropertyName("optInChoices")]
        public CommunicationSettings? OptInChoices { get; set; }

        [JsonPropertyName("acceptsTermsAndConditions")]
        public bool? AcceptsTermsAndConditions { get; set; }

        [JsonPropertyName("dateCreated")]
        public DateTime? DateCreated { get; set; }

        [JsonPropertyName("dateUpdated")]
        public DateTime? DateUpdated { get; set; }
    }
}
