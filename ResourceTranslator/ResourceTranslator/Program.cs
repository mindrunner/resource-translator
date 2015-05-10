// ***********************************************************************
// Assembly         : ResourceTranslatorGUI
// Author           : Lukas Elsner
// Created          : 09-30-2014
//
// Last Modified By : Lukas Elsner
// Last Modified On : 10-02-2014
// ***********************************************************************
// <copyright file="Program.cs" company="mindrunner">
//     Copyright (c) Lukas Elsner. All rights reserved.
// </copyright>
// <summary>Main entry point fur the GUI application</summary>
// ***********************************************************************
using System;
using System.Windows.Forms;

namespace ResourceTranslatorGUI
{
    /// <summary>
    /// Class Program.
    /// </summary>
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new ResourceTranslatorForm());
        }
    }
}
