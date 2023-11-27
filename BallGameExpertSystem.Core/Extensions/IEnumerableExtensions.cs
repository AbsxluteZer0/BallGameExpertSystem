using System;
using System.Collections.Generic;

namespace BallGameExpertSystem.Core.Extensions
{
    public static class IEnumerableExtensions
    {
        public static Dictionary<int, T> ToDictionary<T>(this IEnumerable<T> collection)
        {
            if (collection == null)
                throw new ArgumentNullException(nameof(collection));

            Dictionary<int, T> result = new Dictionary<int, T>();
            int index = 1;

            foreach (T item in collection)
            {
                result[index++] = item;
            }

            return result;
        }


        public static void ForEach<T>(this IEnumerable<T> enumeration, Action<T> action)
        {
            foreach (T item in enumeration)
            {
                action(item);
            }
        }
    }
}
