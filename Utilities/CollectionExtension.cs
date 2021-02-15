using System;
using System.Collections.Generic;

namespace GuaranteedRateTests.Utilities
{
    public static class CollectionExtension
    {

        #region Private Fields

        private static Random rng = new Random();

        #endregion Private Fields

        #region Public Methods

        public static T RandomElement<T>(this IList<T> list)
        {
            return list[rng.Next(list.Count)];
        }

        public static T RandomElement<T>(this T[] array)
        {
            return array[rng.Next(array.Length)];
        }

        #endregion Public Methods

    }
}