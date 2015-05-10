// ***********************************************************************
// Assembly         : ResourceTranslatorLib
// Author           : Lukas Elsner
// Created          : 10-02-2014
//
// Last Modified By : Lukas Elsner
// Last Modified On : 10-03-2014
// ***********************************************************************
// <copyright file="TranslatorSessionModel.cs" company="mindrunner">
//     Copyright (c) Lukas Elsner. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Xml.Linq;

namespace ResourceTranslatorLib
{
    /// <summary>
    /// Class TranslatorSessionModel.
    /// </summary>
    public class TranslatorSessionModel
    {
        /// <summary>
        /// Gets or sets the TMX root node.
        /// </summary>
        /// <value>The TMX root node.</value>
        public XElement TmxRootNode { get; set; }

        /// <summary>
        /// Gets or sets the translation items.
        /// </summary>
        /// <value>The translation items.</value>
        public IEnumerable<TranslationItem> TranslationItems { get; set; }

        /// <summary>
        /// Gets or sets the use external translator.
        /// </summary>
        /// <value>The use external translator.</value>
        public Boolean UseExternalTranslator { get; set; }

        /// <summary>
        /// Gets or sets the if the TMX file should be updated automatically.
        /// </summary>
        /// <value>true, if the TMX file should be updates, false otherwise</value>
        public Boolean UpdateTmxFile { get; set; }

        /// <summary>
        /// Gets or sets the fuzziness.
        /// </summary>
        /// <value>The fuzziness value between 0.0 (off) and 1.0, or -1.0 for auto mode</value>
        public Double Fuzziness { get; set; }

        /// <summary>
        /// Gets or sets the destination language.
        /// </summary>
        /// <value>The destination language.</value>
        public CultureInfo DestinationLanguage { get; set; }

        /// <summary>
        /// Gets or sets the source language of the TMX file.
        /// </summary>
        /// <value>The source language of the TMX file.</value>
        public CultureInfo SourceLanguageTmx { get; set; }

        /// <summary>
        /// Gets or sets the source language of the RESX file.
        /// </summary>
        /// <value>The source language of the RESX file .</value>
        public CultureInfo SourceLanguageResx { get; set; }
    }
}
