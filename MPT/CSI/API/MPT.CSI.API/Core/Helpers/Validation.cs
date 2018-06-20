using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPT.CSI.API.Core.Helpers
{
    internal static class Validation
    {
        public static bool Bracket<T>(ref T value,
            T limitLower,
            T limitUpper,
            bool assignLimits = true) where T : IComparable
        {
            bool successTop = BracketTop(ref value, limitUpper, assignLimits);
            bool successBottom = BracketBottom(ref value, limitLower, assignLimits);
            return (successTop && successBottom);
        }

        public static bool BracketTop<T>(ref T value,
            T limit,
            bool assignLimit = true) where T : IComparable
        {
            if (value.CompareTo(limit) <= 0) return false;
            if (assignLimit)
                value = limit;
            return true;
        }

        public static bool BracketBottom<T>(ref T value,
            T limit,
            bool assignLimit = true) where T : IComparable
        {
            if (value.CompareTo(limit) >= 0) return false;
            if (assignLimit)
                value = limit;
            return true;
        }

        public static bool IsMonotonic<T>(ref T[] values,
            bool reorder = false,
            bool removeDuplicates = false,
            bool ignoreDuplicates = false) where T : IComparable
        {
            bool ignoreAdjacentDuplicates = (ignoreDuplicates || removeDuplicates);
            if (isMonoticAsIs(values, ignoreAdjacentDuplicates)) return true;
            if (!reorder) return false;

            // Reorder list
            Array.Sort(values);
            if (!isIncreasing(values)) Array.Reverse(values);
            if (!removeDuplicates) return true;

            // Remove duplicates
            T[] uniqueValues = new T[values.Length];
            T lastValue = values[0];
            uniqueValues[0] = lastValue;
            for (int i = 1; i < values.Length; i++)
            {
                if (values[i].CompareTo(lastValue) == 0)
                    continue;

                lastValue = values[i];
                uniqueValues[i] = lastValue;
            }
            values = uniqueValues;
            return true;
        }


        private static bool isMonoticAsIs<T>(T[] values,
            bool ignoreAdjacentDuplicates = true) where T : IComparable
        {
            T lastValue = values[0];
            bool valuesAreIncreasing = isIncreasing(values);
            for (int i = 1; i < values.Length; i++)
            {
                if (((values[i].CompareTo(lastValue) == 0) && !ignoreAdjacentDuplicates) ||
                     (valuesAreIncreasing && values[i].CompareTo(lastValue) < 0) ||
                     (!valuesAreIncreasing && values[i].CompareTo(lastValue) > 0))
                    return false;

                lastValue = values[i];
            }

            return true;
        }


        private static bool isIncreasing<T>(T[] values) where T : IComparable
        {
            T firstvalue = values[0];
            return (values[1].CompareTo(firstvalue) > 0);
        }
    }
}
