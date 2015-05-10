// ***********************************************************************
// Assembly         : ResourceTranslatorLib
// Author           : Lukas Elsner
// Created          : 10-02-2014
//
// Last Modified By : Lukas Elsner
// Last Modified On : 10-09-2014
// ***********************************************************************
// <copyright file="TmxQueryManager.cs" company="mindrunner">
//     Copyright (c) Lukas Elsner. All rights reserved.
// </copyright>
// <summary>Class for querying the TMX file with help of LINQ</summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Xml.Linq;


namespace ResourceTranslatorLib
{
    /// <summary>
    /// Class TmxQueryManager.
    /// </summary>
    public class TmxQueryManager
    {
        /// <summary>
        /// The xml Namespace
        /// </summary>
        private readonly XNamespace _xmlns = "http://www.w3.org/XML/1998/namespace";
        /// <summary>
        /// The root element of the TMX file
        /// </summary>
        private readonly XElement _rootElement;
        /// <summary>
        /// The source language string
        /// </summary>
        private readonly String _srcLangString;
        /// <summary>
        /// The destination language strings supported by the TMX file
        /// </summary>
        private readonly List<String> _destLangStrings = new List<string>();
        /// <summary>
        /// The tu elements of the TMX file
        /// </summary>
        private readonly IEnumerable<XElement> _tuElements;
        /// <summary>
        /// The Sessionmodel
        /// </summary>
        private readonly TranslatorSessionModel _model;

        /// <summary>
        /// Initializes a new instance of the <see cref="TmxQueryManager"/> class.
        /// </summary>
        /// <param name="rootElement">The root element.</param>
        /// <param name="sessionModel">The session model.</param>
        public TmxQueryManager(XElement rootElement, TranslatorSessionModel sessionModel)
        {
            _model = sessionModel;
            _rootElement = rootElement;
            /* extract the source language */
            _srcLangString = (from el in _rootElement.Elements("header") select el.Attribute("srclang").Value).FirstOrDefault();
            /* extract all possible destination languages */
            foreach (var destLang in (from tr in _rootElement.Elements("body").Elements("tu").Elements("tuv").Where(e => (string)e.Attribute(_xmlns + "lang") != _srcLangString) select tr.Attribute(_xmlns + "lang")))
            {
                if (!_destLangStrings.Contains(destLang.Value))
                    _destLangStrings.Add(destLang.Value);
            }
            Debug.Assert(_srcLangString != null, "_srcLangString != null");
            sessionModel.SourceLanguageTmx = new CultureInfo(_srcLangString);
            sessionModel.DestinationLanguage = new CultureInfo(_destLangStrings[0]);
            /* cache all tu-elements */
            _tuElements = _rootElement.Elements("body").Elements("tu");
        }

        /// <summary>
        /// Cache for the fuzziness value when using auto mode. This is needed only for displaying with which particular value the
        /// translation string has been found.
        /// </summary>
        /// <value>The automatic fuzzy value.</value>
        public Double AutoFuzzyValue { get; private set; }

        /// <summary>
        /// Search for translations without fuzziness.
        /// </summary>
        /// <param name="searchString">The search string.</param>
        /// <returns>Translations, if some are found, null otherwise</returns>
        private IEnumerable<XElement> GetTranslationElement(String searchString)
        {
            try
            {
                var tra = from tr in _tuElements.AsParallel()
                          let tuvElements = tr.Elements("tuv")
                          let word =
                              tuvElements.Where(e => (string)e.Attribute(_xmlns + "lang") == _srcLangString)
                                  .Select(f => f.Element("seg").Value)
                                  .FirstOrDefault()
                          where word.Equals(searchString)
                          select tuvElements.Where(e => (string)e.Attribute(_xmlns + "lang") == _model.DestinationLanguage.ToString());
                return tra.FirstOrDefault();
            }
            catch
            {
                return null;
            }
        }


        /// <summary>
        /// Search for translations with fuzziness.
        /// </summary>
        /// <param name="searchString">The search string.</param>
        /// <param name="fuzziness">The fuzziness.</param>
        /// <returns>Translations, if some are found, null otherwise</returns>
        private IEnumerable<XElement> GetTranslationElement(String searchString, Double fuzziness)
        {
            try
            {
                var tra = from tr in _tuElements.AsParallel()
                          let tuvElements = tr.Elements("tuv")
                          let word =
                              tuvElements.Where(e => (string)e.Attribute(_xmlns + "lang") == _srcLangString)
                                  .Select(f => f.Element("seg").Value)
                                  .FirstOrDefault()
                          let levenshteinDistance = Tools.LevenshteinDistance(word, searchString)
                          let length = Math.Max(searchString.Length, word.Length)
                          let score = 1.0 - (double)levenshteinDistance / length
                          where score > fuzziness
                          select tuvElements.Where(e => (string)e.Attribute(_xmlns + "lang") == _model.DestinationLanguage.ToString());
                return tra.FirstOrDefault();
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Searches the specified search string.
        /// </summary>
        /// <param name="searchString">The search string.</param>
        /// <param name="fuzziness">The fuzziness.</param>
        /// <returns>A translation, if one exists, null otherwise</returns>
        public String Search(String searchString, Double fuzziness)
        {
            AutoFuzzyValue = -1;
            var element = GetTranslationElement(searchString);

            if (element == null && fuzziness < -0.0001)
            {
                /* use auto mode */
                /* 
                 * start with high fuzzy value and decrement as long as no translation could be found or a fuzzy threshould is reached
                 * it pretty much looks like that fuzziness below 0.4 does not make any sense, so we stop there
                 */
                fuzziness = 1.1;
                while (element == null && fuzziness > 0.5)
                {
                    fuzziness -= 0.1;
                    element = GetTranslationElement(searchString, fuzziness);
                }
                if (element != null)
                {
                    AutoFuzzyValue = fuzziness;
                }
            }
            else if (element == null && fuzziness > 0.0001)
            {
                /* use manual mode */
                element = GetTranslationElement(searchString, fuzziness);
                AutoFuzzyValue = fuzziness;
            }

            if (element != null)
            {
                try
                {
                    return element.Select(f => f.Element("seg")).FirstOrDefault().Value;
                }
                catch (Exception)
                {
                    return null;
                }
            }
            return null;
        }


        /// <summary>
        /// Updates a translation value in our TMX, or add a new entry if not existing already
        /// </summary>
        /// <param name="searchString">The search string.</param>
        /// <param name="fuzziness">The fuzziness.</param>
        /// <param name="replacementString">The replacement string.</param>
        /// <returns>True, if update suceeds, false otherwise</returns>
        public Boolean Update(String searchString, Double fuzziness, String replacementString)
        {
            try
            {
                var element = GetTranslationElement(searchString);

                if (element == null && fuzziness > 0.0001)
                {
                    element = GetTranslationElement(searchString, fuzziness);
                }

                if (element != null)
                {
                   /* Update element */
                    element.Select(f => f.Element("seg")).FirstOrDefault().SetValue(replacementString);
                    return true;
                }
                /* Add new element */
                var tElements = new List<XElement>();
                var srcLangElement = new XElement("tuv");
                var destLangElement = new XElement("tuv");

                srcLangElement.SetAttributeValue(_xmlns + "lang", _srcLangString);
                destLangElement.SetAttributeValue(_xmlns + "lang", _model.DestinationLanguage.ToString());

                srcLangElement.Add(new XElement("seg", searchString));
                destLangElement.Add(new XElement("seg", replacementString));

                tElements.Add(srcLangElement);
                tElements.Add(destLangElement);
                _rootElement.Elements("body").FirstOrDefault().Add(new XElement("tu", tElements));
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// Saves the TMX file.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        public void Save(String fileName)
        {
            _rootElement.Save(fileName);
        }


        /// <summary>
        /// Returns possible destination Cultures
        /// </summary>
        /// <returns>The possible destination cultures</returns>
        public IEnumerable<CultureInfo> Cultures()
        {
            return _destLangStrings.Select(destLangString => new CultureInfo(destLangString)).ToList();
        }
    }
}
