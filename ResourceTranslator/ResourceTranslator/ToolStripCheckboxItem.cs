// ***********************************************************************
// Assembly         : ResourceTranslatorGUI
// Author           : Lukas Elsner
// Created          : 10-03-2014
//
// Last Modified By : Lukas Elsner
// Last Modified On : 10-09-2014
// ***********************************************************************
// <copyright file="ToolStripCheckboxItem.cs" company="mindrunner">
//     Copyright (c) Lukas Elsner. All rights reserved.
// </copyright>
// <summary>Container class for adding a <see cref="CheckBox" /> to the <see cref="ToolStrip" /></summary>
// ***********************************************************************
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace ResourceTranslatorGUI
{
    /// <summary>
    /// Class ToolStripCheckBoxItem.
    /// </summary>
    [ToolStripItemDesignerAvailability
    (ToolStripItemDesignerAvailability.ToolStrip |
    ToolStripItemDesignerAvailability.StatusStrip)]

    public class ToolStripCheckBoxItem : ToolStripControlHost
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="ToolStripCheckBoxItem"/> class.
        /// </summary>
        public ToolStripCheckBoxItem()
            : base(new CheckBox())
        {
        }

    }
}
