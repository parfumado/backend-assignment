using System;
using System.ComponentModel;
using System.Linq;

namespace AuthService {
    [DefaultValue(4)]//Increments
    public enum MembershipReputation {
        None = 0, Silver = 4, Gold = 8, Platinum = 12
    }
}