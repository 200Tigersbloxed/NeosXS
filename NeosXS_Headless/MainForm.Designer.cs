namespace NeosXS_Headless
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
            this.label1 = new System.Windows.Forms.Label();
            this.StartSocketButton = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.StatusLabel = new System.Windows.Forms.Label();
            this.StopSocketButton = new System.Windows.Forms.Button();
            this.HostTextBox = new System.Windows.Forms.TextBox();
            this.PortTextBox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.RestartSocketButton = new System.Windows.Forms.Button();
            this.TestXSButton = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.XSOPortText = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(216, 23);
            this.label1.TabIndex = 0;
            this.label1.Text = "NeosXS_Headless";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // StartSocketButton
            // 
            this.StartSocketButton.Location = new System.Drawing.Point(15, 257);
            this.StartSocketButton.Name = "StartSocketButton";
            this.StartSocketButton.Size = new System.Drawing.Size(213, 23);
            this.StartSocketButton.TabIndex = 1;
            this.StartSocketButton.Text = "START WEBSOCKET";
            this.StartSocketButton.UseVisualStyleBackColor = true;
            this.StartSocketButton.Click += new System.EventHandler(this.StartSocketButton_Click);
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(12, 68);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(104, 23);
            this.label2.TabIndex = 2;
            this.label2.Text = "Websocket Status:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // StatusLabel
            // 
            this.StatusLabel.Location = new System.Drawing.Point(122, 68);
            this.StatusLabel.Name = "StatusLabel";
            this.StatusLabel.Size = new System.Drawing.Size(104, 23);
            this.StatusLabel.TabIndex = 3;
            this.StatusLabel.Text = "DISCONNECTED";
            this.StatusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // StopSocketButton
            // 
            this.StopSocketButton.Location = new System.Drawing.Point(15, 286);
            this.StopSocketButton.Name = "StopSocketButton";
            this.StopSocketButton.Size = new System.Drawing.Size(213, 23);
            this.StopSocketButton.TabIndex = 4;
            this.StopSocketButton.Text = "STOP WEBSOCKET";
            this.StopSocketButton.UseVisualStyleBackColor = true;
            this.StopSocketButton.Click += new System.EventHandler(this.StopSocketButton_Click);
            // 
            // HostTextBox
            // 
            this.HostTextBox.Location = new System.Drawing.Point(15, 135);
            this.HostTextBox.Name = "HostTextBox";
            this.HostTextBox.Size = new System.Drawing.Size(213, 20);
            this.HostTextBox.TabIndex = 5;
            // 
            // PortTextBox
            // 
            this.PortTextBox.Location = new System.Drawing.Point(15, 183);
            this.PortTextBox.Name = "PortTextBox";
            this.PortTextBox.Size = new System.Drawing.Size(213, 20);
            this.PortTextBox.TabIndex = 6;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(12, 109);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(214, 23);
            this.label4.TabIndex = 7;
            this.label4.Text = "Host:";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(12, 157);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(214, 23);
            this.label5.TabIndex = 8;
            this.label5.Text = "Port:";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // RestartSocketButton
            // 
            this.RestartSocketButton.Location = new System.Drawing.Point(15, 315);
            this.RestartSocketButton.Name = "RestartSocketButton";
            this.RestartSocketButton.Size = new System.Drawing.Size(213, 23);
            this.RestartSocketButton.TabIndex = 9;
            this.RestartSocketButton.Text = "RESTART WEBSOCKET";
            this.RestartSocketButton.UseVisualStyleBackColor = true;
            this.RestartSocketButton.Click += new System.EventHandler(this.RestartSocketButton_Click);
            // 
            // TestXSButton
            // 
            this.TestXSButton.Location = new System.Drawing.Point(15, 344);
            this.TestXSButton.Name = "TestXSButton";
            this.TestXSButton.Size = new System.Drawing.Size(213, 23);
            this.TestXSButton.TabIndex = 10;
            this.TestXSButton.Text = "TEST XS NOTIFICATIONS";
            this.TestXSButton.UseVisualStyleBackColor = true;
            this.TestXSButton.Click += new System.EventHandler(this.TestXSButton_Click);
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(12, 206);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(214, 23);
            this.label3.TabIndex = 12;
            this.label3.Text = "XSOverlay Port:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // XSOPortText
            // 
            this.XSOPortText.Location = new System.Drawing.Point(15, 232);
            this.XSOPortText.Name = "XSOPortText";
            this.XSOPortText.Size = new System.Drawing.Size(213, 20);
            this.XSOPortText.TabIndex = 11;
            this.XSOPortText.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.XSOPortText_KeyPress);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(240, 380);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.XSOPortText);
            this.Controls.Add(this.TestXSButton);
            this.Controls.Add(this.RestartSocketButton);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.PortTextBox);
            this.Controls.Add(this.HostTextBox);
            this.Controls.Add(this.StopSocketButton);
            this.Controls.Add(this.StatusLabel);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.StartSocketButton);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "MainForm";
            this.Text = "NeosXS_Headless";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button StartSocketButton;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label StatusLabel;
        private System.Windows.Forms.Button StopSocketButton;
        private System.Windows.Forms.TextBox HostTextBox;
        private System.Windows.Forms.TextBox PortTextBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button RestartSocketButton;
        private System.Windows.Forms.Button TestXSButton;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox XSOPortText;
    }
}

