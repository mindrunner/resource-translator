// ***********************************************************************
// Assembly         : ResourceTranslatorLib
// Author           : Lukas Elsner
// Created          : 10-02-2014
//
// Last Modified By : Lukas Elsner
// Last Modified On : 10-09-2014
// ***********************************************************************
// <copyright file="Tools.cs" company="mindrunner">
//     Copyright (c) Lukas Elsner. All rights reserved.
// </copyright>
// <summary>Helper class to calculate the Levenshtein distance between strings to do fuzzy searches.</summary>
// ***********************************************************************
using System;

namespace ResourceTranslatorLib
{
    /// <summary>
    /// Class Tools.
    /// </summary>
    class Tools
    {
        /// <summary>
        /// Levenshteins distance.
        /// </summary>
        /// <param name="src">String 1</param>
        /// <param name="dest">String 2</param>
        /// <returns>Levenshtein distance between the two strings</returns>
        public static int LevenshteinDistance(string src, string dest)
        {
            var d = new int[src.Length + 1, dest.Length + 1];
            int i, j;
            var str1 = src.ToCharArray();
            var str2 = dest.ToCharArray();

            for (i = 0; i <= str1.Length; i++)
            {
                d[i, 0] = i;
            }
            for (j = 0; j <= str2.Length; j++)
            {
                d[0, j] = j;
            }
            for (i = 1; i <= str1.Length; i++)
            {
                for (j = 1; j <= str2.Length; j++)
                {
                    var cost = str1[i - 1] == str2[j - 1] ? 0 : 1;
                    d[i, j] =
                        Math.Min(
                            d[i - 1, j] + 1,              // Deletion
                            Math.Min(
                                d[i, j - 1] + 1,          // Insertion
                                d[i - 1, j - 1] + cost)); // Substitution
                    if ((i > 1) && (j > 1) && (str1[i - 1] ==
                        str2[j - 2]) && (str1[i - 2] == str2[j - 1]))
                    {
                        d[i, j] = Math.Min(d[i, j], d[i - 2, j - 2] + cost);
                    }
                }
            }
            return d[str1.Length, str2.Length];
        }
    }
}
