using System;
using System.Text.Json.Serialization;
using DataAdapters;

namespace CommonServices {
    public abstract class BaseModel : BaseDataModel {
        protected BaseModel() : this(Guid.NewGuid()) {

        }
        protected BaseModel(Guid identity) : base(identity) {

        }

        [JsonConstructor]
        protected BaseModel(string identity, string? externalIdentity, DateTime? dateCreated, DateTime? dateUpdated)
                    : base(identity, externalIdentity, dateCreated, dateUpdated) {
        }

    }
}