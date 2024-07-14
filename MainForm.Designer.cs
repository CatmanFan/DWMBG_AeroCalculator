
namespace AeroIntensityCalculator
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
            this.panel2 = new System.Windows.Forms.Panel();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.openAppMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.openDwmBgMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.exitAppMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.menu_1 = new System.Windows.Forms.ToolStripMenuItem();
            this.menu_write_dwmbg = new System.Windows.Forms.ToolStripMenuItem();
            this.menu_write_openglass = new System.Windows.Forms.ToolStripMenuItem();
            this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.whenWritingToConfigToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menu_sib_change = new System.Windows.Forms.ToolStripMenuItem();
            this.menu_autorestart_dwm = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.menu_restart_dwmbg = new System.Windows.Forms.ToolStripMenuItem();
            this.menu_restart_openglass = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.menu_refresh_dwm = new System.Windows.Forms.ToolStripMenuItem();
            this.menu_kill_dwm = new System.Windows.Forms.ToolStripMenuItem();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.menu_save_calculation = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).BeginInit();
            this.contextMenuStrip.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // trackBar1
            // 
            this.trackBar1.Location = new System.Drawing.Point(12, 37);
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
            this.label1.Font = new System.Drawing.Font("Tahoma", 8F);
            this.label1.Location = new System.Drawing.Point(9, 78);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(98, 39);
            this.label1.TabIndex = 1;
            this.label1.Text = "Colour balance:\r\nAfterglow balance:\r\nBlur balance:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.label2.Location = new System.Drawing.Point(128, 78);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "label2";
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.SystemColors.ControlLight;
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 161);
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
            this.openAppMenuItem,
            this.toolStripSeparator2,
            this.openDwmBgMenuItem,
            this.toolStripSeparator1,
            this.exitAppMenuItem});
            this.contextMenuStrip.Name = "contextMenuStrip";
            this.contextMenuStrip.Size = new System.Drawing.Size(235, 82);
            // 
            // openAppMenuItem
            // 
            this.openAppMenuItem.Name = "openAppMenuItem";
            this.openAppMenuItem.Size = new System.Drawing.Size(234, 22);
            this.openAppMenuItem.Text = "Open Aero intensity calculator";
            this.openAppMenuItem.Click += new System.EventHandler(this.openAppMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(231, 6);
            // 
            // openDwmBgMenuItem
            // 
            this.openDwmBgMenuItem.Name = "openDwmBgMenuItem";
            this.openDwmBgMenuItem.Size = new System.Drawing.Size(234, 22);
            this.openDwmBgMenuItem.Text = "Open DWMBlurGlass";
            this.openDwmBgMenuItem.Click += new System.EventHandler(this.openDwmBgMenuItem_Click);
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
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menu_1,
            this.optionsToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(484, 24);
            this.menuStrip1.TabIndex = 7;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // menu_1
            // 
            this.menu_1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menu_save_calculation,
            this.toolStripSeparator5,
            this.menu_write_dwmbg,
            this.menu_write_openglass});
            this.menu_1.Name = "menu_1";
            this.menu_1.Size = new System.Drawing.Size(37, 20);
            this.menu_1.Text = "File";
            // 
            // menu_write_dwmbg
            // 
            this.menu_write_dwmbg.Name = "menu_write_dwmbg";
            this.menu_write_dwmbg.Size = new System.Drawing.Size(197, 22);
            this.menu_write_dwmbg.Text = "Write to DWMBlurGlass";
            this.menu_write_dwmbg.Click += new System.EventHandler(this.menu_write_dwmbg_Click);
            // 
            // menu_write_openglass
            // 
            this.menu_write_openglass.Name = "menu_write_openglass";
            this.menu_write_openglass.Size = new System.Drawing.Size(197, 22);
            this.menu_write_openglass.Text = "Write to OpenGlass";
            this.menu_write_openglass.Click += new System.EventHandler(this.menu_write_openglass_Click);
            // 
            // optionsToolStripMenuItem
            // 
            this.optionsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.whenWritingToConfigToolStripMenuItem,
            this.toolStripSeparator4,
            this.menu_restart_dwmbg,
            this.menu_restart_openglass,
            this.toolStripSeparator3,
            this.menu_refresh_dwm,
            this.menu_kill_dwm});
            this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
            this.optionsToolStripMenuItem.Size = new System.Drawing.Size(61, 20);
            this.optionsToolStripMenuItem.Text = "Options";
            // 
            // whenWritingToConfigToolStripMenuItem
            // 
            this.whenWritingToConfigToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menu_sib_change,
            this.menu_autorestart_dwm});
            this.whenWritingToConfigToolStripMenuItem.Name = "whenWritingToConfigToolStripMenuItem";
            this.whenWritingToConfigToolStripMenuItem.Size = new System.Drawing.Size(205, 22);
            this.whenWritingToConfigToolStripMenuItem.Text = "When writing to config...";
            // 
            // menu_sib_change
            // 
            this.menu_sib_change.CheckOnClick = true;
            this.menu_sib_change.Name = "menu_sib_change";
            this.menu_sib_change.Size = new System.Drawing.Size(270, 22);
            this.menu_sib_change.Text = "Change StartIsBack++ colour opacity";
            this.menu_sib_change.Click += new System.EventHandler(this.menu_sib_change_Click);
            // 
            // menu_autorestart_dwm
            // 
            this.menu_autorestart_dwm.CheckOnClick = true;
            this.menu_autorestart_dwm.Name = "menu_autorestart_dwm";
            this.menu_autorestart_dwm.Size = new System.Drawing.Size(270, 22);
            this.menu_autorestart_dwm.Text = "Auto-restart DWM";
            this.menu_autorestart_dwm.Click += new System.EventHandler(this.menu_autorestart_dwm_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(202, 6);
            // 
            // menu_restart_dwmbg
            // 
            this.menu_restart_dwmbg.Name = "menu_restart_dwmbg";
            this.menu_restart_dwmbg.Size = new System.Drawing.Size(205, 22);
            this.menu_restart_dwmbg.Text = "Restart DWMBlurGlass";
            this.menu_restart_dwmbg.Click += new System.EventHandler(this.menu_restart_dwmbg_Click);
            // 
            // menu_restart_openglass
            // 
            this.menu_restart_openglass.Name = "menu_restart_openglass";
            this.menu_restart_openglass.Size = new System.Drawing.Size(205, 22);
            this.menu_restart_openglass.Text = "Restart OpenGlass";
            this.menu_restart_openglass.Click += new System.EventHandler(this.menu_restart_openglass_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(202, 6);
            // 
            // menu_refresh_dwm
            // 
            this.menu_refresh_dwm.Name = "menu_refresh_dwm";
            this.menu_refresh_dwm.Size = new System.Drawing.Size(205, 22);
            this.menu_refresh_dwm.Text = "Refresh DWM";
            this.menu_refresh_dwm.Click += new System.EventHandler(this.menu_refresh_dwm_Click);
            // 
            // menu_kill_dwm
            // 
            this.menu_kill_dwm.Name = "menu_kill_dwm";
            this.menu_kill_dwm.Size = new System.Drawing.Size(205, 22);
            this.menu_kill_dwm.Text = "Kill DWM";
            this.menu_kill_dwm.Click += new System.EventHandler(this.menu_kill_dwm_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(23, 127);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(216, 23);
            this.button1.TabIndex = 8;
            this.button1.Text = "Write to DWMBlurGlass";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.menu_write_dwmbg_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(245, 127);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(216, 23);
            this.button2.TabIndex = 9;
            this.button2.Text = "Write to OpenGlass";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.menu_write_openglass_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(194, 6);
            // 
            // menu_save_calculation
            // 
            this.menu_save_calculation.Name = "menu_save_calculation";
            this.menu_save_calculation.Size = new System.Drawing.Size(197, 22);
            this.menu_save_calculation.Text = "Save calculation";
            this.menu_save_calculation.Click += new System.EventHandler(this.menu_save_calculation_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(484, 162);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.trackBar1);
            this.Font = new System.Drawing.Font("Tahoma", 8F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Aero Intensity Calculator";
            this.Resize += new System.EventHandler(this.MainForm_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).EndInit();
            this.contextMenuStrip.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TrackBar trackBar1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.NotifyIcon notifyIcon;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem openDwmBgMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openAppMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitAppMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem menu_1;
        private System.Windows.Forms.ToolStripMenuItem menu_write_dwmbg;
        private System.Windows.Forms.ToolStripMenuItem menu_write_openglass;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem optionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem menu_restart_dwmbg;
        private System.Windows.Forms.ToolStripMenuItem menu_restart_openglass;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripMenuItem menu_refresh_dwm;
        private System.Windows.Forms.ToolStripMenuItem menu_kill_dwm;
        private System.Windows.Forms.ToolStripMenuItem whenWritingToConfigToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem menu_sib_change;
        private System.Windows.Forms.ToolStripMenuItem menu_autorestart_dwm;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.ToolStripMenuItem menu_save_calculation;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
    }
}

