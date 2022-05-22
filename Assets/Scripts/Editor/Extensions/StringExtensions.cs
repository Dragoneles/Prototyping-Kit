// Author:  Joseph Crump
// Date:    05/22/22

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JC.Prototyping.Editor
{
    /// <summary>
    /// Extension methods for strings.
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// Changes the first character of a string to lowercase.
        /// </summary>
        public static string ToLowerFirstChar(this string input)
        {
            if (string.IsNullOrEmpty(input))
                return input;

            return char.ToLower(input[0]) + input[1..];
        }
    }
}
