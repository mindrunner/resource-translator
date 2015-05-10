// ***********************************************************************
// Assembly         : ResourceTranslatorGUI
// Author           : Lukas Elsner
// Created          : 10-02-2014
//
// Last Modified By : Lukas Elsner
// Last Modified On : 10-09-2014
// ***********************************************************************
// <copyright file="ToolStripTrackBarItem.cs" company="mindrunner">
//     Copyright (c) Lukas Elsner. All rights reserved.
// </copyright>
// <summary>Container class for adding a <see cref="TrackBar" /> to the <see cref="ToolStrip" /></summary>
// ***********************************************************************
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace ResourceTranslatorGUI
{
    /// <summary>
    /// Class ToolStripTrackBarItem.
    /// </summary>
    [ToolStripItemDesignerAvailability
    (ToolStripItemDesignerAvailability.ToolStrip |
    ToolStripItemDesignerAvailability.StatusStrip)]

    public class ToolStripTrackBarItem : ToolStripControlHost
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="ToolStripTrackBarItem"/> class.
        /// </summary>
        public ToolStripTrackBarItem()
            : base(new TrackBar())
        {
        }

    }
}
