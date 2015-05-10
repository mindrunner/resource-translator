// ***********************************************************************
// Assembly         : ResourceTranslatorLib
// Author           : Lukas Elsner
// Created          : 10-02-2014
//
// Last Modified By : Lukas Elsner
// Last Modified On : 10-09-2014
// ***********************************************************************
// <copyright file="TranslationItem.cs" company="mindrunner">
//     Copyright (c) Lukas Elsner. All rights reserved.
// </copyright>
// <summary>Data structure for holding all the translation items</summary>
// ***********************************************************************

namespace ResourceTranslatorLib
{
    /// <summary>
    /// Class TranslationItem.
    /// </summary>
    public class TranslationItem
    {
        /// <summary>
        /// The Translation of an item
        /// </summary>
        private string _target;

        /// <summary>
        /// Initializes a new instance of the <see cref="TranslationItem"/> class.
        /// </summary>
        public TranslationItem()
        {
            _target = "";
            Id = "";
            Source = "";
            Translator = "N/A";
        }

        /// <summary>
        /// Gets or sets the translator.
        /// </summary>
        /// <value>The translator.</value>
        public string Translator { get; set; }

        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>The identifier.</value>
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the source.
        /// </summary>
        /// <value>The source.</value>
        public string Source { get; set; }

        /// <summary>
        /// Gets or sets the target.
        /// </summary>
        /// <value>The target.</value>
        public string Target
        {
            get
            {
                return _target;
            }
            set
            {
                Translator = "User";
                _target = value;
            }
        }
    }
}
