// ***********************************************************************
// Assembly         : ResourceTranslatorLib
// Author           : Lukas Elsner
// Created          : 10-02-2014
//
// Last Modified By : Lukas Elsner
// Last Modified On : 10-09-2014
// ***********************************************************************
// <copyright file="ResXManager.cs" company="mindrunner">
//     Copyright (c) Lukas Elsner. All rights reserved.
// </copyright>
// <summary>Class for importing and Exporting Resx files.</summary>
// ***********************************************************************
using System;
using System.Collections;
using System.Collections.Generic;
using System.Resources;


namespace ResourceTranslatorLib
{
    /// <summary>
    /// Class ResXManager.
    /// </summary>
    public class ResXManager
    {
        /// <summary>
        /// Imports the specified resx file to a list of <see cref="TranslationItem"/>
        /// </summary>
        /// <param name="filename">The filename.</param>
        /// <returns>The list of <see cref="TranslationItem"/></returns>
        public static IEnumerable<TranslationItem> Import(String filename)
        {
            var translationItems = new List<TranslationItem>();
            var resxReader = new ResXResourceReader(filename);
            foreach (DictionaryEntry entry in resxReader)
            {
                var translationItem = new TranslationItem {Id = entry.Key.ToString()};
                if (entry.Value != null)
                {
                    translationItem.Source = entry.Value.ToString();
                }
                translationItems.Add(translationItem);
            }
            return translationItems;
        }

        /// <summary>
        /// Exports the specified translation items to a resx file.
        /// </summary>
        /// <param name="translationItems">The translation items.</param>
        /// <param name="filename">The filename.</param>
        public static void Export(IEnumerable<TranslationItem> translationItems, string filename)
        {
            var resxWriter = new ResXResourceWriter(filename);
            foreach (TranslationItem item in translationItems)
                resxWriter.AddResource(item.Id, item.Target);
            resxWriter.Close();
        }
    }
}
