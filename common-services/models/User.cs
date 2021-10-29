using System;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using System.Security.Principal;
using System.Text.Json.Serialization;
using DataAdapters.DocumentDb;

namespace CommonServices.Models {

    [Collection(Collections.User)]
    public class User : BaseModel, IIdentity {

        [JsonPropertyName("salt"), JsonIgnore]
        public string? Salt { get; set; }
        [JsonPropertyName("email"), Required]
        public string? Email { get; set; }
        [JsonPropertyName("firstName"), Required]
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
        [JsonPropertyName("optInChoices"), Required]
        public CommunicationSettings? OptInChoices { get; set; }
        [JsonPropertyName("acceptsTermsAndConditions"), Required]
        public bool? AcceptsTermsAndConditions { get; set; }
        [JsonPropertyName("reputation")]
        public string? Reputation { get; set; }

        [JsonPropertyName("nextBadge")]
        public string? NextBadge { get; set; }

        [JsonPropertyName("ordersUntilNextBadge")]
        public int OrdersUntilNextBadge { get; set; }
        [JsonPropertyName("OrderCount")]
        public int OrderCount { get; set; }
        public bool IsProfileComplete {
            get {
                return this.Street is not null && this.PostalCode is not null && this.HouseNumber is not null && this.City is not null && this.CountryTwoDigitCode is not null;
            }
        }

        [JsonIgnore]
        public string? AuthenticationType => string.Empty;

        [JsonIgnore]
        public bool IsAuthenticated => true;

        [JsonIgnore]
        public string? Name => this.Email;
    }
}