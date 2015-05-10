namespace ResourceTranslatorGUI
{
    partial class ResourceTranslatorForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ResourceTranslatorForm));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadSourceResxToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadTmxToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveProjectTMXToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportDestinationResxToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.resxGridView = new System.Windows.Forms.DataGridView();
            this.translatorStatusStrip = new System.Windows.Forms.StatusStrip();
            this.srcLocaleLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.destLocaleBtn = new System.Windows.Forms.ToolStripSplitButton();
            this.toolStripProgressBar1 = new System.Windows.Forms.ToolStripProgressBar();
            this.statusTextLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.resxStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.tmxStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.startButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.stopButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.tmxUpdateCheckbox = new ResourceTranslatorGUI.ToolStripCheckBoxItem();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.googleCheckBox = new ResourceTranslatorGUI.ToolStripCheckBoxItem();
            this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
            this.fuzzinessTrackbar = new ResourceTranslatorGUI.ToolStripTrackBarItem();
            this.fuzzinessComboBox = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.resxGridView)).BeginInit();
            this.translatorStatusStrip.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            resources.ApplyResources(this.menuStrip1, "menuStrip1");
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.aboutToolStripMenuItem});
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newToolStripMenuItem,
            this.loadSourceResxToolStripMenuItem,
            this.loadTmxToolStripMenuItem,
            this.saveProjectTMXToolStripMenuItem,
            this.exportDestinationResxToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            resources.ApplyResources(this.fileToolStripMenuItem, "fileToolStripMenuItem");
            // 
            // newToolStripMenuItem
            // 
            this.newToolStripMenuItem.Name = "newToolStripMenuItem";
            resources.ApplyResources(this.newToolStripMenuItem, "newToolStripMenuItem");
            this.newToolStripMenuItem.Click += new System.EventHandler(this.newToolStripMenuItem_Click);
            // 
            // loadSourceResxToolStripMenuItem
            // 
            this.loadSourceResxToolStripMenuItem.Name = "loadSourceResxToolStripMenuItem";
            resources.ApplyResources(this.loadSourceResxToolStripMenuItem, "loadSourceResxToolStripMenuItem");
            this.loadSourceResxToolStripMenuItem.Click += new System.EventHandler(this.loadSourceResxToolStripMenuItem_Click);
            // 
            // loadTmxToolStripMenuItem
            // 
            this.loadTmxToolStripMenuItem.Name = "loadTmxToolStripMenuItem";
            resources.ApplyResources(this.loadTmxToolStripMenuItem, "loadTmxToolStripMenuItem");
            this.loadTmxToolStripMenuItem.Click += new System.EventHandler(this.loadTmxToolStripMenuItem_Click);
            // 
            // saveProjectTMXToolStripMenuItem
            // 
            this.saveProjectTMXToolStripMenuItem.Name = "saveProjectTMXToolStripMenuItem";
            resources.ApplyResources(this.saveProjectTMXToolStripMenuItem, "saveProjectTMXToolStripMenuItem");
            this.saveProjectTMXToolStripMenuItem.Click += new System.EventHandler(this.saveProjectTMXToolStripMenuItem_Click);
            // 
            // exportDestinationResxToolStripMenuItem
            // 
            this.exportDestinationResxToolStripMenuItem.Name = "exportDestinationResxToolStripMenuItem";
            resources.ApplyResources(this.exportDestinationResxToolStripMenuItem, "exportDestinationResxToolStripMenuItem");
            this.exportDestinationResxToolStripMenuItem.Click += new System.EventHandler(this.exportDestinationResxToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            resources.ApplyResources(this.exitToolStripMenuItem, "exitToolStripMenuItem");
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem1});
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            resources.ApplyResources(this.aboutToolStripMenuItem, "aboutToolStripMenuItem");
            // 
            // aboutToolStripMenuItem1
            // 
            this.aboutToolStripMenuItem1.Name = "aboutToolStripMenuItem1";
            resources.ApplyResources(this.aboutToolStripMenuItem1, "aboutToolStripMenuItem1");
            this.aboutToolStripMenuItem1.Click += new System.EventHandler(this.aboutToolStripMenuItem1_Click);
            // 
            // resxGridView
            // 
            this.resxGridView.AllowUserToDeleteRows = false;
            this.resxGridView.AllowUserToResizeRows = false;
            resources.ApplyResources(this.resxGridView, "resxGridView");
            this.resxGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.resxGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.resxGridView.Name = "resxGridView";
            // 
            // translatorStatusStrip
            // 
            resources.ApplyResources(this.translatorStatusStrip, "translatorStatusStrip");
            this.translatorStatusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.srcLocaleLabel,
            this.toolStripStatusLabel1,
            this.destLocaleBtn,
            this.toolStripProgressBar1,
            this.statusTextLabel,
            this.resxStatusLabel,
            this.tmxStatusLabel});
            this.translatorStatusStrip.Name = "translatorStatusStrip";
            // 
            // srcLocaleLabel
            // 
            this.srcLocaleLabel.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)(((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.srcLocaleLabel.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenInner;
            this.srcLocaleLabel.Name = "srcLocaleLabel";
            resources.ApplyResources(this.srcLocaleLabel, "srcLocaleLabel");
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((System.Windows.Forms.ToolStripStatusLabelBorderSides.Top | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.toolStripStatusLabel1.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenInner;
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            resources.ApplyResources(this.toolStripStatusLabel1, "toolStripStatusLabel1");
            // 
            // destLocaleBtn
            // 
            this.destLocaleBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            resources.ApplyResources(this.destLocaleBtn, "destLocaleBtn");
            this.destLocaleBtn.Name = "destLocaleBtn";
            this.destLocaleBtn.DropDownItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.destLocaleBtn_DropDownItemClicked);
            // 
            // toolStripProgressBar1
            // 
            this.toolStripProgressBar1.Name = "toolStripProgressBar1";
            resources.ApplyResources(this.toolStripProgressBar1, "toolStripProgressBar1");
            this.toolStripProgressBar1.Step = 1;
            this.toolStripProgressBar1.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            // 
            // statusTextLabel
            // 
            this.statusTextLabel.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.statusTextLabel.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenInner;
            this.statusTextLabel.Name = "statusTextLabel";
            resources.ApplyResources(this.statusTextLabel, "statusTextLabel");
            // 
            // resxStatusLabel
            // 
            this.resxStatusLabel.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.resxStatusLabel.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenInner;
            this.resxStatusLabel.Name = "resxStatusLabel";
            resources.ApplyResources(this.resxStatusLabel, "resxStatusLabel");
            // 
            // tmxStatusLabel
            // 
            this.tmxStatusLabel.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.tmxStatusLabel.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenInner;
            this.tmxStatusLabel.Name = "tmxStatusLabel";
            resources.ApplyResources(this.tmxStatusLabel, "tmxStatusLabel");
            // 
            // toolStrip1
            // 
            resources.ApplyResources(this.toolStrip1, "toolStrip1");
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.startButton,
            this.toolStripSeparator2,
            this.stopButton,
            this.toolStripSeparator1,
            this.toolStripSeparator3,
            this.tmxUpdateCheckbox,
            this.toolStripSeparator5,
            this.googleCheckBox,
            this.toolStripSeparator7,
            this.fuzzinessTrackbar,
            this.fuzzinessComboBox,
            this.toolStripLabel1,
            this.toolStripSeparator6});
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            // 
            // startButton
            // 
            this.startButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            resources.ApplyResources(this.startButton, "startButton");
            this.startButton.Name = "startButton";
            this.startButton.Click += new System.EventHandler(this.startButton_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            resources.ApplyResources(this.toolStripSeparator2, "toolStripSeparator2");
            // 
            // stopButton
            // 
            this.stopButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            resources.ApplyResources(this.stopButton, "stopButton");
            this.stopButton.Name = "stopButton";
            this.stopButton.Click += new System.EventHandler(this.stopButton_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            resources.ApplyResources(this.toolStripSeparator1, "toolStripSeparator1");
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            resources.ApplyResources(this.toolStripSeparator3, "toolStripSeparator3");
            // 
            // tmxUpdateCheckbox
            // 
            this.tmxUpdateCheckbox.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.tmxUpdateCheckbox.Name = "tmxUpdateCheckbox";
            resources.ApplyResources(this.tmxUpdateCheckbox, "tmxUpdateCheckbox");
            this.tmxUpdateCheckbox.Click += new System.EventHandler(this.tmxUpdateCheckbox_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            resources.ApplyResources(this.toolStripSeparator5, "toolStripSeparator5");
            // 
            // googleCheckBox
            // 
            this.googleCheckBox.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.googleCheckBox.Name = "googleCheckBox";
            resources.ApplyResources(this.googleCheckBox, "googleCheckBox");
            this.googleCheckBox.Click += new System.EventHandler(this.googleCheckBox_Click);
            // 
            // toolStripSeparator7
            // 
            this.toolStripSeparator7.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripSeparator7.Name = "toolStripSeparator7";
            resources.ApplyResources(this.toolStripSeparator7, "toolStripSeparator7");
            // 
            // fuzzinessTrackbar
            // 
            this.fuzzinessTrackbar.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            resources.ApplyResources(this.fuzzinessTrackbar, "fuzzinessTrackbar");
            this.fuzzinessTrackbar.Name = "fuzzinessTrackbar";
            // 
            // fuzzinessComboBox
            // 
            this.fuzzinessComboBox.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.fuzzinessComboBox.Items.AddRange(new object[] {
            resources.GetString("fuzzinessComboBox.Items"),
            resources.GetString("fuzzinessComboBox.Items1"),
            resources.GetString("fuzzinessComboBox.Items2")});
            this.fuzzinessComboBox.Name = "fuzzinessComboBox";
            resources.ApplyResources(this.fuzzinessComboBox, "fuzzinessComboBox");
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripLabel1.Name = "toolStripLabel1";
            resources.ApplyResources(this.toolStripLabel1, "toolStripLabel1");
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            resources.ApplyResources(this.toolStripSeparator6, "toolStripSeparator6");
            // 
            // ResourceTranslatorForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.translatorStatusStrip);
            this.Controls.Add(this.resxGridView);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "ResourceTranslatorForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ResourceTranslatorForm_FormClosing);
            this.Load += new System.EventHandler(this.ResourceTranslatorForm_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.resxGridView)).EndInit();
            this.translatorStatusStrip.ResumeLayout(false);
            this.translatorStatusStrip.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.DataGridView resxGridView;
        private System.Windows.Forms.StatusStrip translatorStatusStrip;
        private System.Windows.Forms.ToolStripProgressBar toolStripProgressBar1;
        private System.Windows.Forms.ToolStripMenuItem loadSourceResxToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exportDestinationResxToolStripMenuItem;
        private System.Windows.Forms.ToolStripStatusLabel statusTextLabel;
        private System.Windows.Forms.ToolStripMenuItem loadTmxToolStripMenuItem;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton startButton;
        private System.Windows.Forms.ToolStripButton stopButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel srcLocaleLabel;
        private System.Windows.Forms.ToolStripStatusLabel resxStatusLabel;
        private System.Windows.Forms.ToolStripStatusLabel tmxStatusLabel;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private ToolStripTrackBarItem fuzzinessTrackbar;
        private ToolStripCheckBoxItem tmxUpdateCheckbox;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private ToolStripCheckBoxItem googleCheckBox;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveProjectTMXToolStripMenuItem;
        private System.Windows.Forms.ToolStripComboBox fuzzinessComboBox;
        private System.Windows.Forms.ToolStripSplitButton destLocaleBtn;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem1;
    }
}

