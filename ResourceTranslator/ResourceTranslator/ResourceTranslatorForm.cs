// ***********************************************************************
// Assembly         : ResourceTranslatorGUI
// Author           : Lukas Elsner
// Created          : 09-30-2014
//
// Last Modified By : Lukas Elsner
// Last Modified On : 10-09-2014
// ***********************************************************************
// <copyright file="ResourceTranslatorForm.cs" company="mindrunner">
//     Copyright (c) Lukas Elsner. All rights reserved.
// </copyright>
// <summary>Main Form fur the Translator GUI</summary>
// ***********************************************************************
using System.Diagnostics;
using System.Globalization;
using System.Text.RegularExpressions;
using ResourceTranslatorGUI.Properties;
using ResourceTranslatorLib;
using System;
using System.ComponentModel;

using System.Windows.Forms;

namespace ResourceTranslatorGUI
{
    /// <summary>
    /// Class ResourceTranslatorForm.
    /// </summary>
    public partial class ResourceTranslatorForm : Form
    {
        /// <summary>
        /// The controller
        /// </summary>
        private TranslatorController _controller;
        /// <summary>
        /// The background worker
        /// </summary>
        private BackgroundWorker _backgroundWorker;
        /// <summary>
        /// The binding source for the <see cref="DataGrid"/>
        /// </summary>
        private BindingSource _bindingSource;
        /// <summary>
        /// The resx filename
        /// </summary>
        private String _resxFilename;

        /// <summary>
        /// Initializes a new instance of the <see cref="ResourceTranslatorForm"/> class.
        /// </summary>
        public ResourceTranslatorForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Handles the Load event of the Form.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void ResourceTranslatorForm_Load(object sender, EventArgs e)
        {
            Init();
            var column = new DataGridViewTextBoxColumn { DataPropertyName = "ID", Name = "ID", ReadOnly = true };
            resxGridView.Columns.Add(column);

            column = new DataGridViewTextBoxColumn { DataPropertyName = "Source", Name = "Source", ReadOnly = true };
            resxGridView.Columns.Add(column);

            column = new DataGridViewTextBoxColumn { DataPropertyName = "Target", Name = "Target" };
            resxGridView.Columns.Add(column);

            column = new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Translator",
                Name = "Translator",
                ReadOnly = true
            };
            resxGridView.Columns.Add(column);
            resxGridView.CellEndEdit += ResxGridViewOnCellEndEdit;
        }

        /// <summary>
        /// OnChange event of the Fuzzinesses ComboBox.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="eventArgs">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void FuzzinessComboBoxOnSelectedIndexChanged(object sender, EventArgs eventArgs)
        {

            if (fuzzinessComboBox.SelectedItem.ToString() == "None")
            {
                fuzzinessTrackbar.Enabled = false;
                _controller.SessionModel.Fuzziness = 0;
            }

            if (fuzzinessComboBox.SelectedItem.ToString() == "Auto")
            {
                fuzzinessTrackbar.Enabled = false;
                _controller.SessionModel.Fuzziness = -1;
            }

            if (fuzzinessComboBox.SelectedItem.ToString() == "Manual")
            {
                fuzzinessTrackbar.Enabled = true;
                var tb = (fuzzinessTrackbar.Control as TrackBar);
                Debug.Assert(tb != null, "tb != null");
                _controller.SessionModel.Fuzziness = 1.0 - tb.Value / 100.0;
            }
        }

        /// <summary>
        /// Update the Grid view after the User has edited it.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="dataGridViewCellEventArgs">The <see cref="DataGridViewCellEventArgs"/> instance containing the event data.</param>
        private void ResxGridViewOnCellEndEdit(object sender, DataGridViewCellEventArgs dataGridViewCellEventArgs)
        {
            resxGridView.Refresh();
        }

        /// <summary>
        /// Onchange event for the Fuzziness TrackBar
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="eventArgs">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void TbOnValueChanged(object sender, EventArgs eventArgs)
        {
            var tb = sender as TrackBar;
            Debug.Assert(tb != null, "tb != null");
            _controller.SessionModel.Fuzziness = 1.0 - tb.Value / 100.0;
        }

        /// <summary>
        /// Handles the FormClosing event of the ResourceTranslatorForm control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="FormClosingEventArgs"/> instance containing the event data.</param>
        private void ResourceTranslatorForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            const string messageBoxText = "Do you really want to quit? All unsaved data will be lost!";
            const string caption = "Translator";
            const MessageBoxButtons button = MessageBoxButtons.YesNo;
            const MessageBoxIcon icon = MessageBoxIcon.Warning;
            e.Cancel = MessageBox.Show(messageBoxText, caption, button, icon) == DialogResult.No;
        }

        /// <summary>
        /// Handles the Click event of the loadSourceResxToolStripMenuItem control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void loadSourceResxToolStripMenuItem_Click(object sender, EventArgs e)
        {
            /* Create an instance of the open file dialog box. */
            var openFileDialog = new OpenFileDialog
            {
                Filter = Resources.ResourceTranslatorForm_loadSourceResxToolStripMenuItem_Click_Text_Files___resx____resx_All_Files__________,
                FilterIndex = 1,
                Multiselect = false
            };

            /* Set filter options and filter index. */

            if (openFileDialog.ShowDialog() != DialogResult.OK) return;
            resxStatusLabel.Text = _controller.InitResx(openFileDialog.FileName);
            _resxFilename = openFileDialog.SafeFileName;
            /* detect language from filename or set default en-US */
            Match match = Regex.Match(openFileDialog.FileName, @"\.[a-z]{2}(-[A-Z]{2})?\.", RegexOptions.None);
            String srcLocale = match.ToString().Replace(".", "");
            if (String.IsNullOrEmpty(srcLocale))
            {
                srcLocale = "en-US";
                Debug.Assert(_resxFilename != null, "_resxFilename != null");
                _resxFilename = _resxFilename.Replace(".resx", ".en-US.resx");
            }
            srcLocaleLabel.Text = srcLocale;
            startButton.Enabled = _controller.IsReadyToTranslate();
            _bindingSource.DataSource = _controller.SessionModel.TranslationItems;
            loadSourceResxToolStripMenuItem.Enabled = false;
        }

        /// <summary>
        /// Handles the Click event of the loadTmxToolStripMenuItem control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void loadTmxToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Create an instance of the open file dialog box.
            var openFileDialog = new OpenFileDialog
            {
                Filter =
                    Resources
                        .ResourceTranslatorForm_loadTmxToolStripMenuItem_Click_Text_Files___tmx____tmx_All_Files__________,
                FilterIndex = 1,
                Multiselect = false
            };
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                tmxStatusLabel.Text = _controller.InitTmx(openFileDialog.FileName);
                destLocaleBtn.DropDownItems.Clear();
                foreach (var destinationCulture in _controller.GetDestinationCultures())
                {
                    destLocaleBtn.DropDownItems.Add(destinationCulture.ToString());
                }
                destLocaleBtn.Text = _controller.SessionModel.DestinationLanguage.ToString();
                startButton.Enabled = _controller.IsReadyToTranslate();
                loadTmxToolStripMenuItem.Enabled = false;
            }
        }

        /// <summary>
        /// Handles the Click event of the exportDestinationResxToolStripMenuItem control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void exportDestinationResxToolStripMenuItem_Click(object sender, EventArgs e)
        {
            /* replace source language in resx file to destination language, to assist the user with file name selection */
            Match match = Regex.Match(_resxFilename, @"\.[a-z]{2}(-[A-Z]{2})?\.", RegexOptions.None);
            String srcLocale = match.ToString().Replace(".", "");
            var saveFileDialog = new SaveFileDialog
            {
                FileName = _resxFilename.Replace(srcLocale, _controller.SessionModel.DestinationLanguage.ToString())
            };

            if (saveFileDialog.ShowDialog() != DialogResult.OK) return;
            statusTextLabel.Text = Resources.ResourceTranslatorForm_exportDestinationResxToolStripMenuItem_Click_Exporting___;
            _backgroundWorker = new BackgroundWorker();
            resxGridView.Columns[2].ReadOnly = true;
            resxGridView.Enabled = false;
            startButton.Enabled = false;
            stopButton.Enabled = false;
            googleCheckBox.Control.Enabled = false;
            tmxUpdateCheckbox.Control.Enabled = false;
            var tb = fuzzinessTrackbar.Control as TrackBar;
            Debug.Assert(tb != null, "tb != null");
            tb.Enabled = false;
            Debug.Assert(fuzzinessComboBox.ComboBox != null, "fuzzinessComboBox.ComboBox != null");
            fuzzinessComboBox.ComboBox.Enabled = false;
            _controller.ExportFileName = saveFileDialog.FileName;
            _backgroundWorker.WorkerSupportsCancellation = false;
            _backgroundWorker.WorkerReportsProgress = true;
            _backgroundWorker.ProgressChanged += bw_ProgressChangedExporting;
            _backgroundWorker.RunWorkerCompleted += bw_RunWorkerCompleted;
            _backgroundWorker.DoWork += _controller.ExportResX;
            _backgroundWorker.RunWorkerAsync();
        }

        /// <summary>
        /// Handles the RunWorkerCompleted event of the background worker control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RunWorkerCompletedEventArgs"/> instance containing the event data.</param>
        private void bw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled)
                statusTextLabel.Text = Resources.ResourceTranslatorForm_bw_RunWorkerCompleted_Canceled_;
            else if (e.Error != null)
                statusTextLabel.Text = (Resources.ResourceTranslatorForm_bw_RunWorkerCompleted_Error__ + e.Error.Message);
            else
            {
                exportDestinationResxToolStripMenuItem.Enabled = true;
                saveProjectTMXToolStripMenuItem.Enabled = true;
                statusTextLabel.Text = Resources.ResourceTranslatorForm_bw_RunWorkerCompleted_Done_;
            }

            resxGridView.Columns[2].ReadOnly = false;
            resxGridView.Enabled = true;
            startButton.Enabled = true;
            stopButton.Enabled = false;

            if (fuzzinessComboBox.SelectedItem.ToString() == "None")
            {
                fuzzinessTrackbar.Enabled = false;
            }

            if (fuzzinessComboBox.SelectedItem.ToString() == "Auto")
            {
                fuzzinessTrackbar.Enabled = false;
            }

            if (fuzzinessComboBox.SelectedItem.ToString() == "Manual")
            {
                fuzzinessTrackbar.Enabled = true;
            }

            Debug.Assert(fuzzinessComboBox.ComboBox != null, "fuzzinessComboBox.ComboBox != null");
            fuzzinessComboBox.ComboBox.Enabled = true;
            googleCheckBox.Control.Enabled = true;
            tmxUpdateCheckbox.Control.Enabled = true;
        }

        /// <summary>
        /// Handles the ProgressChanged event of the background worker control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="ProgressChangedEventArgs"/> instance containing the event data.</param>
        private void bw_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            toolStripProgressBar1.Value = e.ProgressPercentage;
            resxGridView.Refresh();
            resxGridView.AutoResizeColumns();
        }

        /// <summary>
        /// Handles the ProgressChangedExporting event of the bw control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="ProgressChangedEventArgs"/> instance containing the event data.</param>
        private void bw_ProgressChangedExporting(object sender, ProgressChangedEventArgs e)
        {
            toolStripProgressBar1.Value = e.ProgressPercentage;
        }

        /// <summary>
        /// Handles the Click event of the startButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void startButton_Click(object sender, EventArgs e)
        {
            _backgroundWorker = new BackgroundWorker();
            resxGridView.Columns[2].ReadOnly = true;
            resxGridView.Enabled = false;
            startButton.Enabled = false;
            stopButton.Enabled = true;
            googleCheckBox.Control.Enabled = false;
            tmxUpdateCheckbox.Control.Enabled = false;
            var tb = fuzzinessTrackbar.Control as TrackBar;
            Debug.Assert(tb != null, "tb != null");
            tb.Enabled = false;
            Debug.Assert(fuzzinessComboBox.ComboBox != null, "fuzzinessComboBox.ComboBox != null");
            fuzzinessComboBox.ComboBox.Enabled = false;
            statusTextLabel.Text = Resources.ResourceTranslatorForm_startButton_Click_Translating___;
            _backgroundWorker.WorkerSupportsCancellation = true;
            _backgroundWorker.WorkerReportsProgress = true;
            _backgroundWorker.ProgressChanged += bw_ProgressChanged;
            _backgroundWorker.RunWorkerCompleted += bw_RunWorkerCompleted;
            _backgroundWorker.DoWork += _controller.PerformTranslations;
            _backgroundWorker.RunWorkerAsync();
        }

        /// <summary>
        /// Handles the Click event of the stopButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void stopButton_Click(object sender, EventArgs e)
        {
            statusTextLabel.Text = Resources.ResourceTranslatorForm_stopButton_Click_Stopping___;
            _backgroundWorker.CancelAsync();
        }

        /// <summary>
        /// Handles the Click event of the googleCheckBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void googleCheckBox_Click(object sender, EventArgs e)
        {
            _controller.SessionModel.UseExternalTranslator = ((CheckBox)googleCheckBox.Control).Checked;

        }

        /// <summary>
        /// Handles the Click event of the tmxUpdateCheckbox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void tmxUpdateCheckbox_Click(object sender, EventArgs e)
        {
            _controller.SessionModel.UpdateTmxFile = ((CheckBox)tmxUpdateCheckbox.Control).Checked;
        }

        /// <summary>
        /// Handles the Click event of the exitToolStripMenuItem control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        /// <summary>
        /// Handles the Click event of the newToolStripMenuItem control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            const string messageBoxText = "Do you really want to reset the session? All unsaved data will be lost!";
            const string caption = "Translator";
            const MessageBoxButtons button = MessageBoxButtons.YesNo;
            const MessageBoxIcon icon = MessageBoxIcon.Warning;
            if (MessageBox.Show(messageBoxText, caption, button, icon) == DialogResult.Yes)
            {
                Init();
            }
        }

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        private void Init()
        {

            _controller = new TranslatorController();
            _backgroundWorker = new BackgroundWorker();
            _bindingSource = new BindingSource();

            stopButton.Enabled = false;
            startButton.Enabled = false;
            Controls.Add(resxGridView);

            loadSourceResxToolStripMenuItem.Enabled = true;
            loadTmxToolStripMenuItem.Enabled = true;
            exportDestinationResxToolStripMenuItem.Enabled = false;
            saveProjectTMXToolStripMenuItem.Enabled = false;

            ((CheckBox)googleCheckBox.Control).Checked = true;
            ((CheckBox)tmxUpdateCheckbox.Control).Checked = true;

            _controller.SessionModel.UseExternalTranslator = ((CheckBox)googleCheckBox.Control).Checked;
            _controller.SessionModel.UpdateTmxFile = ((CheckBox)tmxUpdateCheckbox.Control).Checked;

            var tb = fuzzinessTrackbar.Control as TrackBar;
            Debug.Assert(tb != null, "tb != null");
            tb.Maximum = 100;
            tb.Minimum = 0;
            tb.Value = 0;
            tb.ValueChanged += TbOnValueChanged;

            Debug.Assert(fuzzinessComboBox.ComboBox != null, "fuzzinessComboBox.ComboBox != null");
            fuzzinessComboBox.ComboBox.SelectedIndex = 0;
            fuzzinessComboBox.ComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            fuzzinessComboBox.SelectedIndexChanged += FuzzinessComboBoxOnSelectedIndexChanged;

            if (fuzzinessComboBox.SelectedItem.ToString() == "None")
            {
                fuzzinessTrackbar.Enabled = false;
                _controller.SessionModel.Fuzziness = 0;
            }

            if (fuzzinessComboBox.SelectedItem.ToString() == "Auto")
            {
                fuzzinessTrackbar.Enabled = false;
                _controller.SessionModel.Fuzziness = -1;
            }

            if (fuzzinessComboBox.SelectedItem.ToString() == "Manual")
            {
                fuzzinessTrackbar.Enabled = true;
                _controller.SessionModel.Fuzziness = 1.0 - tb.Value / 100.0;
            }

            statusTextLabel.Text = Resources.ResourceTranslatorForm_Init_Idle;
            resxStatusLabel.Text = Resources.ResourceTranslatorForm_Init_No_RESX_loaded;
            tmxStatusLabel.Text = Resources.ResourceTranslatorForm_Init_No_TMX_loaded;
            srcLocaleLabel.Text = Resources.ResourceTranslatorForm_Init______;
            destLocaleBtn.Text = Resources.ResourceTranslatorForm_Init______;
            destLocaleBtn.DropDownItems.Clear();

            // Initialize the DataGridView.
            resxGridView.AutoGenerateColumns = false;
            resxGridView.AutoSize = true;
            resxGridView.DataSource = _bindingSource;
        }

        /// <summary>
        /// Handles the Click event of the saveProjectTMXToolStripMenuItem control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void saveProjectTMXToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var saveFileDialog = new SaveFileDialog();
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                _controller.SaveProjectSpecificTmx(saveFileDialog.FileName);
            }
        }

        /// <summary>
        /// Handles the DropDownItemClicked event of the destLocaleBtn control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="ToolStripItemClickedEventArgs"/> instance containing the event data.</param>
        private void destLocaleBtn_DropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            destLocaleBtn.Text = e.ClickedItem.Text;
            _controller.SessionModel.DestinationLanguage = new CultureInfo(destLocaleBtn.Text);
        }

        /// <summary>
        /// Shows the About dialog
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void aboutToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            AboutTranslatorBox aboutTranslatorBox = new AboutTranslatorBox();
            aboutTranslatorBox.Show();
        }
    }
}
