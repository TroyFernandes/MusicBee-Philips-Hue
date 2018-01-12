namespace MusicBeePlugin.SettingsComponent
{
    partial class SettingsSelector
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
            this.listLightsButton = new System.Windows.Forms.Button();
            this.hueLightsListBox = new System.Windows.Forms.ListBox();
            this.addButton = new System.Windows.Forms.Button();
            this.addListBox = new System.Windows.Forms.ListBox();
            this.saveSettingsButton = new System.Windows.Forms.Button();
            this.deleteButton = new System.Windows.Forms.Button();
            this.outputLabel = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.colorPaletteRadio = new System.Windows.Forms.RadioButton();
            this.averageColorRadio = new System.Windows.Forms.RadioButton();
            this.averageColorTooltip = new System.Windows.Forms.ToolTip(this.components);
            this.colorPaletteTooltip = new System.Windows.Forms.ToolTip(this.components);
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.registerButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.qualitySettingsCombobox = new System.Windows.Forms.ComboBox();
            this.refreshButton = new System.Windows.Forms.Button();
            this.brightnessTrackbar = new System.Windows.Forms.TrackBar();
            this.brightnessLabel = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.brightnessTrackbar)).BeginInit();
            this.SuspendLayout();
            // 
            // listLightsButton
            // 
            this.listLightsButton.Location = new System.Drawing.Point(12, 12);
            this.listLightsButton.Name = "listLightsButton";
            this.listLightsButton.Size = new System.Drawing.Size(75, 23);
            this.listLightsButton.TabIndex = 0;
            this.listLightsButton.Text = "List Lights";
            this.listLightsButton.UseVisualStyleBackColor = true;
            this.listLightsButton.Click += new System.EventHandler(this.listLightsButton_Click);
            // 
            // hueLightsListBox
            // 
            this.hueLightsListBox.FormattingEnabled = true;
            this.hueLightsListBox.Location = new System.Drawing.Point(93, 12);
            this.hueLightsListBox.Name = "hueLightsListBox";
            this.hueLightsListBox.Size = new System.Drawing.Size(102, 212);
            this.hueLightsListBox.TabIndex = 1;
            // 
            // addButton
            // 
            this.addButton.Location = new System.Drawing.Point(201, 12);
            this.addButton.Name = "addButton";
            this.addButton.Size = new System.Drawing.Size(75, 23);
            this.addButton.TabIndex = 2;
            this.addButton.Text = "Add";
            this.addButton.UseVisualStyleBackColor = true;
            this.addButton.Click += new System.EventHandler(this.addButton_Click);
            // 
            // addListBox
            // 
            this.addListBox.FormattingEnabled = true;
            this.addListBox.Location = new System.Drawing.Point(282, 12);
            this.addListBox.Name = "addListBox";
            this.addListBox.Size = new System.Drawing.Size(102, 212);
            this.addListBox.TabIndex = 3;
            // 
            // saveSettingsButton
            // 
            this.saveSettingsButton.Location = new System.Drawing.Point(390, 12);
            this.saveSettingsButton.Name = "saveSettingsButton";
            this.saveSettingsButton.Size = new System.Drawing.Size(75, 23);
            this.saveSettingsButton.TabIndex = 4;
            this.saveSettingsButton.Text = "Save";
            this.saveSettingsButton.UseVisualStyleBackColor = true;
            this.saveSettingsButton.Click += new System.EventHandler(this.saveSettingsButton_Click);
            // 
            // deleteButton
            // 
            this.deleteButton.Location = new System.Drawing.Point(390, 41);
            this.deleteButton.Name = "deleteButton";
            this.deleteButton.Size = new System.Drawing.Size(75, 23);
            this.deleteButton.TabIndex = 5;
            this.deleteButton.Text = "Delete";
            this.deleteButton.UseVisualStyleBackColor = true;
            this.deleteButton.Click += new System.EventHandler(this.deleteButton_Click);
            // 
            // outputLabel
            // 
            this.outputLabel.AutoSize = true;
            this.outputLabel.Location = new System.Drawing.Point(279, 227);
            this.outputLabel.Name = "outputLabel";
            this.outputLabel.Size = new System.Drawing.Size(0, 13);
            this.outputLabel.TabIndex = 6;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.brightnessLabel);
            this.groupBox1.Controls.Add(this.brightnessTrackbar);
            this.groupBox1.Controls.Add(this.colorPaletteRadio);
            this.groupBox1.Controls.Add(this.averageColorRadio);
            this.groupBox1.Location = new System.Drawing.Point(12, 230);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(110, 153);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Light Options";
            // 
            // colorPaletteRadio
            // 
            this.colorPaletteRadio.AutoSize = true;
            this.colorPaletteRadio.Location = new System.Drawing.Point(6, 42);
            this.colorPaletteRadio.Name = "colorPaletteRadio";
            this.colorPaletteRadio.Size = new System.Drawing.Size(85, 17);
            this.colorPaletteRadio.TabIndex = 9;
            this.colorPaletteRadio.Text = "Color Palette";
            this.averageColorTooltip.SetToolTip(this.colorPaletteRadio, "Cycles through colors based on the 8 most dominant colors in the album art");
            this.colorPaletteRadio.UseVisualStyleBackColor = true;
            // 
            // averageColorRadio
            // 
            this.averageColorRadio.AutoSize = true;
            this.averageColorRadio.Checked = true;
            this.averageColorRadio.Location = new System.Drawing.Point(6, 19);
            this.averageColorRadio.Name = "averageColorRadio";
            this.averageColorRadio.Size = new System.Drawing.Size(92, 17);
            this.averageColorRadio.TabIndex = 8;
            this.averageColorRadio.TabStop = true;
            this.averageColorRadio.Text = "Average Color";
            this.averageColorTooltip.SetToolTip(this.averageColorRadio, "Displays a single color based on the average color of the album art");
            this.averageColorRadio.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.registerButton);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.qualitySettingsCombobox);
            this.groupBox2.Location = new System.Drawing.Point(128, 230);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(151, 153);
            this.groupBox2.TabIndex = 10;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Plugin Settings";
            this.groupBox2.Enter += new System.EventHandler(this.groupBox2_Enter);
            // 
            // registerButton
            // 
            this.registerButton.Location = new System.Drawing.Point(12, 60);
            this.registerButton.Name = "registerButton";
            this.registerButton.Size = new System.Drawing.Size(89, 23);
            this.registerButton.TabIndex = 2;
            this.registerButton.Text = "Register Plugin";
            this.registerButton.UseVisualStyleBackColor = true;
            this.registerButton.Click += new System.EventHandler(this.registerButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(80, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Quality Settings";
            // 
            // qualitySettingsCombobox
            // 
            this.qualitySettingsCombobox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.qualitySettingsCombobox.FormattingEnabled = true;
            this.qualitySettingsCombobox.Location = new System.Drawing.Point(12, 33);
            this.qualitySettingsCombobox.Name = "qualitySettingsCombobox";
            this.qualitySettingsCombobox.Size = new System.Drawing.Size(121, 21);
            this.qualitySettingsCombobox.TabIndex = 0;
            // 
            // refreshButton
            // 
            this.refreshButton.Location = new System.Drawing.Point(390, 70);
            this.refreshButton.Name = "refreshButton";
            this.refreshButton.Size = new System.Drawing.Size(75, 23);
            this.refreshButton.TabIndex = 11;
            this.refreshButton.Text = "Refresh";
            this.refreshButton.UseVisualStyleBackColor = true;
            this.refreshButton.Click += new System.EventHandler(this.refreshButton_Click);
            // 
            // brightnessTrackbar
            // 
            this.brightnessTrackbar.Location = new System.Drawing.Point(3, 81);
            this.brightnessTrackbar.Name = "brightnessTrackbar";
            this.brightnessTrackbar.Size = new System.Drawing.Size(104, 45);
            this.brightnessTrackbar.TabIndex = 3;
            this.brightnessTrackbar.Scroll += new System.EventHandler(this.brightnessTrackbar_Scroll);
            // 
            // brightnessLabel
            // 
            this.brightnessLabel.AutoSize = true;
            this.brightnessLabel.Location = new System.Drawing.Point(6, 65);
            this.brightnessLabel.Name = "brightnessLabel";
            this.brightnessLabel.Size = new System.Drawing.Size(56, 13);
            this.brightnessLabel.TabIndex = 10;
            this.brightnessLabel.Text = "Brightness";
            // 
            // SettingsSelector
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(476, 395);
            this.Controls.Add(this.refreshButton);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.outputLabel);
            this.Controls.Add(this.deleteButton);
            this.Controls.Add(this.saveSettingsButton);
            this.Controls.Add(this.addListBox);
            this.Controls.Add(this.addButton);
            this.Controls.Add(this.hueLightsListBox);
            this.Controls.Add(this.listLightsButton);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SettingsSelector";
            this.Text = "Hue Artwork Settings";
            this.Load += new System.EventHandler(this.SettingsSelector_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.brightnessTrackbar)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button listLightsButton;
        private System.Windows.Forms.ListBox hueLightsListBox;
        private System.Windows.Forms.Button addButton;
        private System.Windows.Forms.ListBox addListBox;
        private System.Windows.Forms.Button saveSettingsButton;
        private System.Windows.Forms.Button deleteButton;
        private System.Windows.Forms.Label outputLabel;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton colorPaletteRadio;
        private System.Windows.Forms.ToolTip averageColorTooltip;
        private System.Windows.Forms.RadioButton averageColorRadio;
        private System.Windows.Forms.ToolTip colorPaletteTooltip;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox qualitySettingsCombobox;
        private System.Windows.Forms.Button registerButton;
        private System.Windows.Forms.Button refreshButton;
        private System.Windows.Forms.Label brightnessLabel;
        private System.Windows.Forms.TrackBar brightnessTrackbar;
    }
}