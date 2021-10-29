using System;
using System.Text.Json.Serialization;

namespace DataAdapters
{
    public abstract class BaseDataModel
    {
        protected BaseDataModel() {
            this.Identity = Guid.NewGuid();
        }

        protected BaseDataModel(Guid guid) {
            this.Identity = guid;
        }

        [JsonConstructor]
        public BaseDataModel(string identity, string? externalIdentity, DateTime? dateCreated, DateTime? dateUpdated) {
            this.Identity = Guid.Parse(identity);
            this.ExternalIdentity = externalIdentity;
            this.DateCreated = dateCreated;
            this.DateUpdated = dateUpdated;
        }

        [JsonPropertyName("identity"), JsonInclude]
        public Guid Identity { get; init; }

        [JsonPropertyName("externalIdentity"), JsonInclude]
        public virtual string? ExternalIdentity { get; init; }

        [JsonPropertyName("dateCreated"), JsonInclude]
        public DateTime? DateCreated { get; set; }

        [JsonPropertyName("dateUpdated"), JsonInclude]
        public DateTime? DateUpdated { get; set; }
    }
}