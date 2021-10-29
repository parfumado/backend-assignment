using System;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Text.Json.Serialization;
using CommonServices;
using Utility;
using Utility.Runtime;

namespace AuthService {
    public class User : BaseModel {
        private string? _newPassword;

        public User() : base(Guid.NewGuid()) {

        }

        public User(Guid? identity) : base(identity ?? Guid.NewGuid()) {

        }

        public User(CommonServices.Models.User commonUser) : this(commonUser.Identity) {
            ObjectUtils.CopyProperties<CommonServices.Models.User, User>(commonUser, true, this);
        }

        public User(UpdateUserRequest userRequest, Guid? fromExisting = null) : this(fromExisting ?? Guid.NewGuid()) {
            ObjectUtils.CopyProperties<UpdateUserRequest, User>(userRequest, true, this);
        }

        [JsonPropertyName("password")]
        public string Password {
            set {
                if (!string.IsNullOrEmpty(value)) {
                    this._newPassword = value;
                    this.PasswordHash = null;
                }
            }
        }

        [JsonPropertyName("username")]
        public string? Username { get; set; }
        [JsonPropertyName("registrationSource")]
        public RegistrationSources? RegistrationSource { get; set; }
        [JsonPropertyName("passwordHash")]
        public string? PasswordHash { get; set; }
        [JsonPropertyName("salt")]
        public string? Salt { get; set; }
        [JsonPropertyName("email")]
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
        [JsonPropertyName("subscriptionId")]
        public string? SubscriptionId { get; set; }
        [JsonPropertyName("reputation")]
        public MembershipReputation Reputation {
            get {
                return MembershipReputation.None.GetEnumTierForValue(this.OrderCount);
            }
        }

        [JsonPropertyName("nextBadge")]
        public MembershipReputation NextBadge {
            get {
                return this.Reputation.GetNextTier();
            }
        }

        [JsonPropertyName("ordersUntilNextBadge")]
        public int OrdersUntilNextBadge {
            get {
                return this.Reputation.GetUnitsToReachNextTier(this.OrderCount);
            }
        }
        [JsonPropertyName("OrderCount")]
        public int OrderCount { get; set; }

        [JsonIgnore]
        public CultureInfo? CultureSettings {
            get {
                if (LanguageLocaleCode is not null)
                    return new CultureInfo(LanguageLocaleCode);
                else
                    return null;
            }
        }

        [JsonPropertyName("optInChoices")]
        public CommunicationSettings? OptInChoices { get; set; }
        [JsonPropertyName("acceptsTermsAndConditions"), Required]
        public bool? AcceptsTermsAndConditions { get; set; }

        [JsonIgnore]
        public bool PasswordChanged {
            get {
                return !string.IsNullOrEmpty(this._newPassword);
            }
        }

        public void UpdatePassword(string? pepper) {
            if (!PasswordChanged || string.IsNullOrEmpty(pepper)) return;

            var cred = StringHasher.HashPassword(this._newPassword!, pepper);
            this.PasswordHash = cred.hash;
            this.Salt = cred.salt;
        }
    }
}
