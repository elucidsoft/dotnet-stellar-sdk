using System;
using System.Collections.Generic;
using System.Text;

namespace stellar_dotnetcore_sdk
{
    public static class EnumerableExtensions
    {
        /// <summary>
        /// Creates a string from the sequence by concatenating the result
        /// of the specified string selector function for each element.
        /// </summary>
        public static string ToConcatenatedString<T>(this IEnumerable<T> source,
            Func<T, string> stringSelector)
        {
            return source.ToConcatenatedString(stringSelector, String.Empty);
        }

        /// <summary>
        /// Creates a string from the sequence by concatenating the result
        /// of the specified string selector function for each element.
        /// </summary>
        ///<param name="separator">The string which separates each concatenated item.</param>
        public static string ToConcatenatedString<T>(this IEnumerable<T> source,
            Func<T, string> stringSelector,
            string separator)
        {
            var b = new StringBuilder();
            bool needsSeparator = false; // don't use for first item

            foreach (var item in source)
            {
                if (needsSeparator)
                    b.Append(separator);

                b.Append(stringSelector(item));
                needsSeparator = true;
            }

            return b.ToString();
        }
    }
}



