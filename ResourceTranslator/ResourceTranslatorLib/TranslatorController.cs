// ***********************************************************************
// Assembly         : ResourceTranslatorLib
// Author           : Lukas Elsner
// Created          : 10-02-2014
//
// Last Modified By : Lukas Elsner
// Last Modified On : 10-09-2014
// ***********************************************************************
// <copyright file="TranslatorController.cs" company="mindrunner">
//     Copyright (c) Lukas Elsner. All rights reserved.
// </copyright>
// <summary>Controller Class for doing all translation operations.</summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Xml.Linq;


namespace ResourceTranslatorLib
{
    /// <summary>
    /// Class TranslatorController.
    /// </summary>
    public class TranslatorController
    {
        /// <summary>
        /// The Sessionmodel
        /// </summary>
        private readonly TranslatorSessionModel _session = new TranslatorSessionModel();
        /// <summary>
        /// The Google Translator instance.
        /// </summary>
        private readonly GoogleTranslator _googleTranslator = new GoogleTranslator();
        /// <summary>
        /// The TMX query manager
        /// </summary>
        private TmxQueryManager _tmxQueryManager;
        /// <summary>
        /// The current step
        /// </summary>
        private int _currentStep;
        /// <summary>
        /// The maximum step
        /// </summary>
        private int _maxStep = 100;
        /// <summary>
        /// The TMX filename
        /// </summary>
        private String _tmxFilename;

        /// <summary>
        /// Initializes a new instance of the <see cref="TranslatorController"/> class.
        /// </summary>
        public TranslatorController()
        {
            _session.Fuzziness = 1.0;
        }

        /// <summary>
        /// Initializes the TMX.
        /// </summary>
        /// <param name="filename">The filename.</param>
        /// <returns>A status string</returns>
        public String InitTmx(String filename)
        {
            _session.TmxRootNode = XElement.Load(filename);
            _tmxFilename = filename;
            _tmxQueryManager = new TmxQueryManager(_session.TmxRootNode, _session);
            _currentStep = 0;
            if (_session.SourceLanguageResx == null || _session.SourceLanguageResx.Equals(_session.SourceLanguageTmx))
            {
                int cultureCount = _tmxQueryManager.Cultures().Count();

                if (cultureCount > 1)
                {
                    return String.Format("TMX ok! Please choose language. ({0} available)", cultureCount);
                }
                return "TMX ok!";
            }
            return String.Format("Warning: TMX language {0} does not match RESX language {1}",
                _session.SourceLanguageTmx, _session.SourceLanguageResx);
        }

        /// <summary>
        /// Gets the destination cultures.
        /// </summary>
        /// <returns>All possible destination cultures</returns>
        public IEnumerable<CultureInfo> GetDestinationCultures()
        {
            return _tmxQueryManager.Cultures();
        }

        /// <summary>
        /// Initializes the RESX.
        /// </summary>
        /// <param name="filename">The filename.</param>
        /// <returns>A status string</returns>
        public String InitResx(String filename)
        {
            _session.TranslationItems = ResXManager.Import(filename);
            _currentStep = 0;
            Match match = Regex.Match(filename, @"\.[a-z]{2}(-[A-Z]{2})?\.", RegexOptions.None);
            String srcLocale = match.ToString().Replace(".", "");
            /* assume en-US as default */
            if (String.IsNullOrEmpty(srcLocale))
            {
                srcLocale = "en-US";
            }
            _session.SourceLanguageResx = new CultureInfo(srcLocale);
            if (_session.SourceLanguageTmx == null || _session.SourceLanguageResx.Equals(_session.SourceLanguageTmx))
            {
                return "RESX ok!";
            }
            return String.Format("Warning: TMX language {0} does not match RESX language {1}",
                _session.SourceLanguageTmx, _session.SourceLanguageResx);
        }

        /// <summary>
        /// Determines whether controller is ready to translate.
        /// </summary>
        /// <returns><c>true</c> if controller is ready to translate; otherwise, <c>false</c>.</returns>
        public bool IsReadyToTranslate()
        {
            return _session.TmxRootNode != null && _session.TranslationItems != null;
        }

        /// <summary>
        /// Performs the translations. This method should be used with a <see cref="BackgroundWorker"/>
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="DoWorkEventArgs"/> instance containing the event data.</param>
        public void PerformTranslations(object sender, DoWorkEventArgs e)
        {
            var worker = sender as BackgroundWorker;
            Debug.Assert(worker != null, "worker != null");
            worker.ReportProgress(0);
            _maxStep = _session.TranslationItems.Count();
            foreach (TranslationItem translationItem in _session.TranslationItems)
            {
                if (worker.CancellationPending)
                {
                    e.Cancel = true;
                    break;
                }
                ++_currentStep;
                translationItem.Target = _tmxQueryManager.Search(translationItem.Source, _session.Fuzziness);
                if (String.IsNullOrEmpty(translationItem.Target))
                {
                    /* user google translate if user wants so and we haven't found a translation so far */
                    if (_session.UseExternalTranslator)
                    {
                        translationItem.Target = _googleTranslator.Translate(_session.SourceLanguageResx,
                            _session.DestinationLanguage, translationItem.Source);
                        translationItem.Translator = "Google";
                    }
                }
                else
                {
                    Double fuzziValue = _tmxQueryManager.AutoFuzzyValue;
                    translationItem.Translator = fuzziValue > 0.0 ? String.Format("TMX ({0})", fuzziValue) : "TMX";
                }
                worker.ReportProgress(Progress);
            }
        }

        /// <summary>
        /// Gets the current translation progress.
        /// </summary>
        /// <value>The progress.</value>
        public int Progress
        {
            get
            {
                var val = (_currentStep / (double)_maxStep * 100.0);
                if (val > 100) val = 100;
                return (int)val;
            }

        }

        /// <summary>
        /// Gets the session model.
        /// </summary>
        /// <value>The session model.</value>
        public TranslatorSessionModel SessionModel
        {
            get { return _session; }
        }

        /// <summary>
        /// Gets or sets the name of the export file.
        /// </summary>
        /// <value>The name of the export file.</value>
        public String ExportFileName { get; set; }

        /// <summary>
        /// Exports the resx file. Should be used with a <see cref="BackgroundWorker"/>
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="DoWorkEventArgs"/> instance containing the event data.</param>
        public void ExportResX(object sender, DoWorkEventArgs e)
        {
            var worker = sender as BackgroundWorker;
            Debug.Assert(worker != null, "worker != null");
            worker.ReportProgress(0);
            _maxStep = _session.TranslationItems.Count();
            _currentStep = 0;
            ResXManager.Export(_session.TranslationItems, ExportFileName);
            /* update current TMX file if user wants so */
            if (_session.UpdateTmxFile)
            {
                foreach (TranslationItem translationItem in _session.TranslationItems)
                {
                    ++_currentStep;
                    _tmxQueryManager.Update(translationItem.Source, _session.Fuzziness, translationItem.Target);
                    worker.ReportProgress(Progress);
                }
                _tmxQueryManager.Save(_tmxFilename);
            }
        }

        /// <summary>
        /// Saves the project specific TMX.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        public void SaveProjectSpecificTmx(String fileName)
        {
            var tmxExporter = new TmxExporter(SessionModel);
            tmxExporter.Export(fileName);
        }
    }
}
