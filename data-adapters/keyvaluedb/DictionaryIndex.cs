using System;

namespace DataAdapters.KeyValueDb {
    public enum DictionaryIndex {
        [DefaultExpiration("3d")] Sessions = 0,
        [DefaultExpiration("2h")] ResetTokens = 1
    }
}