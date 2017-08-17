namespace ABI
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
            this.button_Exit = new System.Windows.Forms.Button();
            this.pictureBox_VMS = new System.Windows.Forms.PictureBox();
            this.pictureBox_Analysis = new System.Windows.Forms.PictureBox();
            this.pictureBox_Event = new System.Windows.Forms.PictureBox();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.analyticReportsUrlToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_VMS)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Analysis)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Event)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // button_Exit
            // 
            this.button_Exit.Font = new System.Drawing.Font("Microsoft Sans Serif", 4.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_Exit.Location = new System.Drawing.Point(639, 723);
            this.button_Exit.Margin = new System.Windows.Forms.Padding(2);
            this.button_Exit.Name = "button_Exit";
            this.button_Exit.Size = new System.Drawing.Size(41, 21);
            this.button_Exit.TabIndex = 0;
            this.button_Exit.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.button_Exit.UseVisualStyleBackColor = true;
            this.button_Exit.Click += new System.EventHandler(this.button_Exit_Click);
            this.button_Exit.MouseEnter += new System.EventHandler(this.button_Exit_MouseEnter);
            this.button_Exit.MouseLeave += new System.EventHandler(this.button_Exit_MouseLeave);
            // 
            // pictureBox_VMS
            // 
            this.pictureBox_VMS.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox_VMS.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.pictureBox_VMS.Image = global::ABI.Properties.Resources.Video_btn_up;
            this.pictureBox_VMS.Location = new System.Drawing.Point(86, 262);
            this.pictureBox_VMS.Margin = new System.Windows.Forms.Padding(2);
            this.pictureBox_VMS.Name = "pictureBox_VMS";
            this.pictureBox_VMS.Size = new System.Drawing.Size(385, 92);
            this.pictureBox_VMS.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox_VMS.TabIndex = 4;
            this.pictureBox_VMS.TabStop = false;
            this.pictureBox_VMS.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pictureBox_VMS_MouseClick);
            this.pictureBox_VMS.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBox_VMS_MouseDown);
            this.pictureBox_VMS.MouseEnter += new System.EventHandler(this.pictureBox_VMS_MouseEnter);
            this.pictureBox_VMS.MouseLeave += new System.EventHandler(this.pictureBox_VMS_MouseLeave);
            // 
            // pictureBox_Analysis
            // 
            this.pictureBox_Analysis.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox_Analysis.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.pictureBox_Analysis.Image = global::ABI.Properties.Resources.Analytic_btn_up;
            this.pictureBox_Analysis.Location = new System.Drawing.Point(86, 373);
            this.pictureBox_Analysis.Margin = new System.Windows.Forms.Padding(2);
            this.pictureBox_Analysis.Name = "pictureBox_Analysis";
            this.pictureBox_Analysis.Size = new System.Drawing.Size(385, 92);
            this.pictureBox_Analysis.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox_Analysis.TabIndex = 5;
            this.pictureBox_Analysis.TabStop = false;
            this.pictureBox_Analysis.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pictureBox_Analysis_MouseClick);
            this.pictureBox_Analysis.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBox_Analysis_MouseDown);
            this.pictureBox_Analysis.MouseEnter += new System.EventHandler(this.pictureBox_Analysis_MouseEnter);
            this.pictureBox_Analysis.MouseLeave += new System.EventHandler(this.pictureBox_Analysis_MouseLeave);
            // 
            // pictureBox_Event
            // 
            this.pictureBox_Event.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox_Event.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.pictureBox_Event.Image = global::ABI.Properties.Resources.Incident_btn_up;
            this.pictureBox_Event.Location = new System.Drawing.Point(86, 483);
            this.pictureBox_Event.Margin = new System.Windows.Forms.Padding(2);
            this.pictureBox_Event.Name = "pictureBox_Event";
            this.pictureBox_Event.Size = new System.Drawing.Size(385, 92);
            this.pictureBox_Event.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox_Event.TabIndex = 6;
            this.pictureBox_Event.TabStop = false;
            this.pictureBox_Event.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pictureBox_Event_MouseClick);
            this.pictureBox_Event.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBox_Event_MouseDown);
            this.pictureBox_Event.MouseEnter += new System.EventHandler(this.pictureBox_Event_MouseEnter);
            this.pictureBox_Event.MouseLeave += new System.EventHandler(this.pictureBox_Event_MouseLeave);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.analyticReportsUrlToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(273, 30);
            // 
            // analyticReportsUrlToolStripMenuItem
            // 
            this.analyticReportsUrlToolStripMenuItem.Name = "analyticReportsUrlToolStripMenuItem";
            this.analyticReportsUrlToolStripMenuItem.Size = new System.Drawing.Size(272, 26);
            this.analyticReportsUrlToolStripMenuItem.Text = "Analytic Reports URL Setup  ";
            this.analyticReportsUrlToolStripMenuItem.Click += new System.EventHandler(this.analyticReportsUrlToolStripMenuItem_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(120F, 120F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackgroundImage = global::ABI.Properties.Resources.bg;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(688, 750);
            this.ContextMenuStrip = this.contextMenuStrip1;
            this.Controls.Add(this.button_Exit);
            this.Controls.Add(this.pictureBox_VMS);
            this.Controls.Add(this.pictureBox_Event);
            this.Controls.Add(this.pictureBox_Analysis);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MainForm";
            this.Text = "MainForm";
            this.Load += new System.EventHandler(this.MainForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_VMS)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Analysis)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Event)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button_Exit;
        private System.Windows.Forms.PictureBox pictureBox_VMS;
        private System.Windows.Forms.PictureBox pictureBox_Analysis;
        private System.Windows.Forms.PictureBox pictureBox_Event;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem analyticReportsUrlToolStripMenuItem;
    }
}