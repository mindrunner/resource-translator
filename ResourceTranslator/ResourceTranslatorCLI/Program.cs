// ***********************************************************************
// Assembly         : ResourceTranslatorCLI
// Author           : Lukas Elsner
// Created          : 10-02-2014
//
// Last Modified By : Lukas Elsner
// Last Modified On : 10-09-2014
// ***********************************************************************
// <copyright file="Program.cs" company="mindrunner">
//     Copyright (c) Lukas Elsner. All rights reserved.
// </copyright>
// <summary>Command line application for the Resource Translator</summary>
// ***********************************************************************
using System;
using System.ComponentModel;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Threading;
using ResourceTranslatorLib;

namespace ResourceTranslatorCLI
{
    /// <summary>
    /// Class Program.
    /// </summary>
    internal class Program
    {
        /// <summary>
        /// The input resx file name
        /// </summary>
        private static String _resxFileName;
        /// <summary>
        /// The input TMX filename
        /// </summary>
        private static String _tmxFilename;
        /// <summary>
        /// The fuzziness factor for fuzzy searching
        /// </summary>
        private static Double _fuzziness;
        /// <summary>
        /// The controller
        /// </summary>
        private static TranslatorController _controller;
        /// <summary>
        /// Will be set to true if everything finished, so that the application can cleanly exit.
        /// </summary>
        private static Boolean _isFinished;
        /// <summary>
        /// The filename for the project specific TMX export
        /// </summary>
        private static String _projectExportFilename;

        /// <summary>
        /// Defines the entry point of the application.
        /// </summary>
        /// <param name="args">The arguments.</param>
        private static void Main(string[] args)
        {
            String exportFilename = null;
            Boolean updateTmxFile = false;
            Boolean useExternalTranslator = false;
            CultureInfo destinationLocale = null;
            /* initialize options */
            for (int i = 0; i < args.Length; ++i)
            {
                string s = args[i];
                if (s.StartsWith("-r"))
                {
                    _resxFileName = args[i + 1];
                }
                if (s.StartsWith("-l"))
                {
                    destinationLocale = new CultureInfo(args[i + 1]);
                }
                if (s.StartsWith("-t"))
                {
                    _tmxFilename = args[i + 1];
                }
                if (s.StartsWith("-e"))
                {
                    exportFilename = args[i + 1];
                }
                if (s.StartsWith("-p"))
                {
                    _projectExportFilename = args[i + 1];
                }
                if (s.StartsWith("-f"))
                {
                    _fuzziness = Double.Parse(args[i + 1]);
                }
                if (s.StartsWith("-g"))
                {
                    useExternalTranslator = true;
                }
                if (s.StartsWith("-u"))
                {
                    updateTmxFile = true;
                }
                if (s.StartsWith("-h"))
                {
                    Usage();
                    return;
                }
                Console.WriteLine(s);
            }

            if (String.IsNullOrEmpty(exportFilename))
            {
                Match match = Regex.Match(_resxFileName, @"\.[a-z]{2}(-[A-Z]{2})?\.", RegexOptions.None);
                String srcLocale = match.ToString().Replace(".", "");
                if (!String.IsNullOrEmpty(srcLocale) && null != destinationLocale)
                {
                    exportFilename = _resxFileName.Replace(srcLocale, destinationLocale.ToString());
                }
            }

            /* check if all needed parameters are set */
            if (String.IsNullOrEmpty(exportFilename) || String.IsNullOrEmpty(_resxFileName) ||
                String.IsNullOrEmpty(_tmxFilename))
            {
                Usage();
                return;
            }

            /* init controller */
            _controller = new TranslatorController { ExportFileName = exportFilename };
            _controller.SessionModel.Fuzziness = _fuzziness;
            _controller.SessionModel.UpdateTmxFile = updateTmxFile;
            _controller.SessionModel.UseExternalTranslator = useExternalTranslator;


            try
            {
                Console.WriteLine(_controller.InitResx(_resxFileName));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return;
            }

            try
            {
                Console.WriteLine(_controller.InitTmx(_tmxFilename));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return;
            }
            if (destinationLocale != null)
            {
                _controller.SessionModel.DestinationLanguage = destinationLocale;
            }
            if (_controller.IsReadyToTranslate())
            {
                /* we need to spawn a background worker here. 
                 * This is done, because the GUI has to do the 
                 * stuff in background to stay responsive and 
                 * both GUI and CLI sharing the same controller methods. 
                 */
                Console.WriteLine("Translating...");
                var backgroundWorker = new BackgroundWorker
                {
                    WorkerSupportsCancellation = true,
                    WorkerReportsProgress = true
                };
                backgroundWorker.ProgressChanged += bw_ProgressChanged;
                backgroundWorker.RunWorkerCompleted += bw_RunWorkerCompleted;
                backgroundWorker.DoWork += _controller.PerformTranslations;
                backgroundWorker.RunWorkerAsync();
                /* wait until all other tasks are finished */
                while (!_isFinished)
                {
                    Thread.Sleep(1000);
                }
            }
            else
            {
                Console.WriteLine("Not ready to translate");
            }
            Console.WriteLine("Application will exit now!");
        }

        /// <summary>
        /// Help output
        /// </summary>
        private static void Usage()
        {
            Console.WriteLine("Resource Translator CLI");
            Console.WriteLine("Parameters:");
            Console.WriteLine("-r <Filename>: Input RESX file");
            Console.WriteLine("-t <Filename>: TMX file with translations");
            Console.WriteLine("Optional Parameters:");
            Console.WriteLine("-e <Filename>: Output RESX file");
            Console.WriteLine("-f <Fuzziness between 0.0 and 1.0, or -1 for automode>: Use fuzzy string matching");
            Console.WriteLine("-p <Filename>: Export project specific TMX file");
            Console.WriteLine("-g: Use Google Translate");
            Console.WriteLine("-l: Destination locale");
            Console.WriteLine("-u: Update TMX file after translating");
            Console.WriteLine("-h: Show this help");
        }

        /// <summary>
        /// Handles the RunWorkerCompleted event of the translation background worker.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RunWorkerCompletedEventArgs"/> instance containing the event data.</param>
        private static void bw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            Console.WriteLine("Translation finished");
            Console.WriteLine("Exporting...");
            /* spawn another background worker for saving the new resx file */
            var backgroundWorker = new BackgroundWorker
            {
                WorkerSupportsCancellation = true,
                WorkerReportsProgress = true
            };
            backgroundWorker.ProgressChanged += bw_ProgressChanged;
            backgroundWorker.RunWorkerCompleted += bw_RunWorkerCompleted_Export;
            backgroundWorker.DoWork += _controller.ExportResX;
            backgroundWorker.RunWorkerAsync();
        }

        /// <summary>
        /// Handles the RunWorkerCompleted event of the export background worker.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RunWorkerCompletedEventArgs"/> instance containing the event data.</param>
        private static void bw_RunWorkerCompleted_Export(object sender, RunWorkerCompletedEventArgs e)
        {
            Console.WriteLine("Export completed.");
            /* save project specific TMX file, if the user wants so */
            if (_projectExportFilename != null)
            {
                Console.WriteLine("Creating project specific TMX");
                _controller.SaveProjectSpecificTmx(_projectExportFilename);
            }
            _isFinished = true;
        }

        /// <summary>
        /// Handles the ProgressChanged event of the bw control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="ProgressChangedEventArgs"/> instance containing the event data.</param>
        private static void bw_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            Console.WriteLine("{0}%", e.ProgressPercentage);
        }
    }
}