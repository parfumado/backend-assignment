using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace CommonServices {
    public class CommunicationSettings {
        bool _marketingOptIn;

        [JsonConstructor]
        public CommunicationSettings(bool marketingOptIn) {
            this.MarketingOptIn = marketingOptIn;
        }

        public CommunicationSettings(bool marketingOptIn, DateTime? dateChanged) {
            this._marketingOptIn = marketingOptIn;
            this.DateChanged = dateChanged;
        }

        [JsonPropertyName("marketingOptIn"), Required]
        public bool MarketingOptIn {
            get {
                return this._marketingOptIn;
            }
            set {
                this._marketingOptIn = value;
                this.DateChanged = DateTime.UtcNow;
            }
        }

        [JsonPropertyName("dateChanged"), Required]
        public DateTime? DateChanged { get; set; }
    }
}
