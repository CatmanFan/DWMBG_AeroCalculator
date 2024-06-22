
namespace DWMBG_AeroCalculator
{
    partial class MainForm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.trackBar1 = new System.Windows.Forms.TrackBar();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.WriteToConfig = new System.Windows.Forms.Button();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.panel1 = new System.Windows.Forms.Panel();
            this.RestartDWMBG = new System.Windows.Forms.Button();
            this.RefreshSIB = new System.Windows.Forms.CheckBox();
            this.RefreshDWM = new System.Windows.Forms.Button();
            this.KillDWM = new System.Windows.Forms.Button();
            this.WarningIcon = new System.Windows.Forms.PictureBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.openDwmBgMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openAppMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.separator = new System.Windows.Forms.ToolStripSeparator();
            this.restartAsConsoleApplicationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.exitAppMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.WarningIcon)).BeginInit();
            this.contextMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // trackBar1
            // 
            this.trackBar1.Location = new System.Drawing.Point(12, 10);
            this.trackBar1.Maximum = 100;
            this.trackBar1.Name = "trackBar1";
            this.trackBar1.Size = new System.Drawing.Size(460, 45);
            this.trackBar1.TabIndex = 0;
            this.trackBar1.TickFrequency = 5;
            this.trackBar1.Scroll += new System.EventHandler(this.trackBar1_Scroll);
            this.trackBar1.ValueChanged += new System.EventHandler(this.trackBar1_Scroll);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label1.Location = new System.Drawing.Point(9, 51);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(110, 42);
            this.label1.TabIndex = 1;
            this.label1.Text = "Colour balance:\r\nAfterglow balance:\r\nBlur balance:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(128, 51);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(43, 14);
            this.label2.TabIndex = 2;
            this.label2.Text = "label2";
            // 
            // WriteToConfig
            // 
            this.WriteToConfig.Location = new System.Drawing.Point(9, 11);
            this.WriteToConfig.Name = "WriteToConfig";
            this.WriteToConfig.Size = new System.Drawing.Size(122, 48);
            this.WriteToConfig.TabIndex = 3;
            this.WriteToConfig.Text = "Write to config";
            this.WriteToConfig.UseVisualStyleBackColor = true;
            this.WriteToConfig.Click += new System.EventHandler(this.WriteToConfig_Click);
            // 
            // openFileDialog
            // 
            this.openFileDialog.Filter = "INI files|*.ini|All files|*.*";
            this.openFileDialog.SupportMultiDottedExtensions = true;
            this.openFileDialog.Title = "Select a valid DWMBlurGlass configuration file...";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.Control;
            this.panel1.Controls.Add(this.RestartDWMBG);
            this.panel1.Controls.Add(this.RefreshSIB);
            this.panel1.Controls.Add(this.RefreshDWM);
            this.panel1.Controls.Add(this.KillDWM);
            this.panel1.Controls.Add(this.WarningIcon);
            this.panel1.Controls.Add(this.WriteToConfig);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 107);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(484, 90);
            this.panel1.TabIndex = 5;
            // 
            // RestartDWMBG
            // 
            this.RestartDWMBG.Location = new System.Drawing.Point(353, 58);
            this.RestartDWMBG.Name = "RestartDWMBG";
            this.RestartDWMBG.Size = new System.Drawing.Size(122, 23);
            this.RestartDWMBG.TabIndex = 12;
            this.RestartDWMBG.Text = "Restart DWMBG";
            this.toolTip.SetToolTip(this.RestartDWMBG, "Restarts DWMBlurGlass as well as any Windhawk mod enabled which modifies\r\nthe win" +
        "dow titlebar button sizes. This can be useful if DWM had crashed\r\nunexpectedly.");
            this.RestartDWMBG.UseVisualStyleBackColor = true;
            this.RestartDWMBG.Click += new System.EventHandler(this.RestartDWMBG_Click);
            // 
            // RefreshSIB
            // 
            this.RefreshSIB.AutoSize = true;
            this.RefreshSIB.Location = new System.Drawing.Point(9, 63);
            this.RefreshSIB.Name = "RefreshSIB";
            this.RefreshSIB.Size = new System.Drawing.Size(190, 17);
            this.RefreshSIB.TabIndex = 11;
            this.RefreshSIB.Text = "Also change StartIsBack++ colour";
            this.RefreshSIB.UseVisualStyleBackColor = true;
            this.RefreshSIB.CheckedChanged += new System.EventHandler(this.RefreshSIB_CheckedChanged);
            // 
            // RefreshDWM
            // 
            this.RefreshDWM.Location = new System.Drawing.Point(353, 33);
            this.RefreshDWM.Name = "RefreshDWM";
            this.RefreshDWM.Size = new System.Drawing.Size(122, 23);
            this.RefreshDWM.TabIndex = 10;
            this.RefreshDWM.Text = "Refresh DWM";
            this.RefreshDWM.UseVisualStyleBackColor = true;
            this.RefreshDWM.Click += new System.EventHandler(this.RefreshDWM_Click);
            // 
            // KillDWM
            // 
            this.KillDWM.Location = new System.Drawing.Point(353, 8);
            this.KillDWM.Name = "KillDWM";
            this.KillDWM.Size = new System.Drawing.Size(122, 23);
            this.KillDWM.TabIndex = 9;
            this.KillDWM.Text = "Kill DWM";
            this.KillDWM.UseVisualStyleBackColor = true;
            this.KillDWM.Click += new System.EventHandler(this.KillDWM_Click);
            // 
            // WarningIcon
            // 
            this.WarningIcon.Location = new System.Drawing.Point(329, 9);
            this.WarningIcon.Name = "WarningIcon";
            this.WarningIcon.Size = new System.Drawing.Size(20, 20);
            this.WarningIcon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.WarningIcon.TabIndex = 7;
            this.WarningIcon.TabStop = false;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.SystemColors.ControlLight;
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 106);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(484, 1);
            this.panel2.TabIndex = 6;
            // 
            // toolTip
            // 
            this.toolTip.IsBalloon = true;
            this.toolTip.ToolTipTitle = "Information";
            // 
            // notifyIcon
            // 
            this.notifyIcon.ContextMenuStrip = this.contextMenuStrip;
            this.notifyIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon.Icon")));
            this.notifyIcon.Text = "Aero intensity calculator for DWMBlurGlass";
            this.notifyIcon.Visible = true;
            this.notifyIcon.MouseClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon_MouseClick);
            // 
            // contextMenuStrip
            // 
            this.contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openDwmBgMenuItem,
            this.openAppMenuItem,
            this.separator,
            this.restartAsConsoleApplicationToolStripMenuItem,
            this.toolStripSeparator1,
            this.exitAppMenuItem});
            this.contextMenuStrip.Name = "contextMenuStrip";
            this.contextMenuStrip.Size = new System.Drawing.Size(235, 104);
            // 
            // openDwmBgMenuItem
            // 
            this.openDwmBgMenuItem.Name = "openDwmBgMenuItem";
            this.openDwmBgMenuItem.Size = new System.Drawing.Size(234, 22);
            this.openDwmBgMenuItem.Text = "Open DWMBlurGlass";
            this.openDwmBgMenuItem.Click += new System.EventHandler(this.openDwmBgMenuItem_Click);
            // 
            // openAppMenuItem
            // 
            this.openAppMenuItem.Name = "openAppMenuItem";
            this.openAppMenuItem.Size = new System.Drawing.Size(234, 22);
            this.openAppMenuItem.Text = "Open Aero intensity calculator";
            this.openAppMenuItem.Click += new System.EventHandler(this.openAppMenuItem_Click);
            // 
            // separator
            // 
            this.separator.Name = "separator";
            this.separator.Size = new System.Drawing.Size(231, 6);
            // 
            // restartAsConsoleApplicationToolStripMenuItem
            // 
            this.restartAsConsoleApplicationToolStripMenuItem.Name = "restartAsConsoleApplicationToolStripMenuItem";
            this.restartAsConsoleApplicationToolStripMenuItem.Size = new System.Drawing.Size(234, 22);
            this.restartAsConsoleApplicationToolStripMenuItem.Text = "Restart as console application";
            this.restartAsConsoleApplicationToolStripMenuItem.Click += new System.EventHandler(this.restartAsConsoleApplicationToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(231, 6);
            // 
            // exitAppMenuItem
            // 
            this.exitAppMenuItem.Name = "exitAppMenuItem";
            this.exitAppMenuItem.Size = new System.Drawing.Size(234, 22);
            this.exitAppMenuItem.Text = "Exit";
            this.exitAppMenuItem.Click += new System.EventHandler(this.exitAppMenuItem_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(484, 197);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.trackBar1);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Tahoma", 8F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Aero intensity calculator for DWMBlurGlass";
            this.Resize += new System.EventHandler(this.MainForm_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.WarningIcon)).EndInit();
            this.contextMenuStrip.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TrackBar trackBar1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button WriteToConfig;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox WarningIcon;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.NotifyIcon notifyIcon;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem openDwmBgMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openAppMenuItem;
        private System.Windows.Forms.ToolStripSeparator separator;
        private System.Windows.Forms.ToolStripMenuItem exitAppMenuItem;
        private System.Windows.Forms.Button KillDWM;
        private System.Windows.Forms.Button RefreshDWM;
        private System.Windows.Forms.CheckBox RefreshSIB;
        private System.Windows.Forms.Button RestartDWMBG;
        private System.Windows.Forms.ToolStripMenuItem restartAsConsoleApplicationToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
    }
}

