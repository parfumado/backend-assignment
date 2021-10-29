using System;
using System.Collections;
using System.ComponentModel;
using System.Linq;

namespace Utility.Runtime {
    public static class EnumExtensions {
        public static T GetEnumTierForValue<T>(this T enumInstance, int value)
            where T : struct, Enum {
            int tierIncrement = enumInstance.GetIncrementValue<T>();
            int maxEnumValue = enumInstance.GetHighestTier();
            int numberOfIncrements = value / tierIncrement; 
            int startPosition = tierIncrement / 2;

            return (T)Enum.ToObject(typeof(T), (Math.Min(startPosition += (numberOfIncrements * tierIncrement), maxEnumValue)) / tierIncrement * tierIncrement);
        }

        public static int GetHighestTier<T>(this T tier)
            where T : struct, Enum {
            return (int)(object)Enum.GetValues<T>().Max();
        }

        public static T GetNextTier<T>(this T tier)
            where T : struct, Enum {
            return (T)(object)Math.Min((int)(object)tier! + tier.GetIncrementValue(), GetHighestTier<T>(tier));
        }

        public static int GetUnitsToReachNextTier<T>(this T tier, int? currentUnits) where T : struct, Enum {
            return Math.Max(0, (int)(object)tier.GetNextTier() - currentUnits ?? (int)(object)tier);
        }

        private static int GetIncrementValue<T>(this T tier) where T : struct, Enum {
            DefaultValueAttribute increment = (DefaultValueAttribute)typeof(T).GetCustomAttributes(typeof(DefaultValueAttribute), true).Single();
            return (int)increment.Value!;
        }
    }
}