using System;

namespace CommonServices {
    public class CollectionAttribute : Attribute {
        public string CollectionName { get; init; }

        public CollectionAttribute(string collectionName) {
            this.CollectionName = collectionName;
        }

    }
}