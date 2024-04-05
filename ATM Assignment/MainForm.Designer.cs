namespace ATM
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
            this.lblTitle = new System.Windows.Forms.Label();
            this.btnOn = new System.Windows.Forms.Button();
            this.lblRaceCondition = new System.Windows.Forms.Label();
            this.lblNoATMs = new System.Windows.Forms.Label();
            this.btnOff = new System.Windows.Forms.Button();
            this.btn1 = new System.Windows.Forms.Button();
            this.btn2 = new System.Windows.Forms.Button();
            this.btn3 = new System.Windows.Forms.Button();
            this.btnStart = new System.Windows.Forms.Button();
            this.btnLogs = new System.Windows.Forms.Button();
            this.btnAboutUs = new System.Windows.Forms.Button();
            this.listLogs = new System.Windows.Forms.ListView();
            this.btnBack = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.Location = new System.Drawing.Point(268, 45);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(232, 25);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Central Bank System";
            // 
            // btnOn
            // 
            this.btnOn.Location = new System.Drawing.Point(439, 203);
            this.btnOn.Name = "btnOn";
            this.btnOn.Size = new System.Drawing.Size(109, 25);
            this.btnOn.TabIndex = 1;
            this.btnOn.Text = "On";
            this.btnOn.UseVisualStyleBackColor = true;
            this.btnOn.Click += new System.EventHandler(this.switchRaceCondition);
            // 
            // lblRaceCondition
            // 
            this.lblRaceCondition.AutoSize = true;
            this.lblRaceCondition.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRaceCondition.Location = new System.Drawing.Point(434, 175);
            this.lblRaceCondition.Name = "lblRaceCondition";
            this.lblRaceCondition.Size = new System.Drawing.Size(217, 25);
            this.lblRaceCondition.TabIndex = 2;
            this.lblRaceCondition.Text = "Race Condition: On";
            // 
            // lblNoATMs
            // 
            this.lblNoATMs.AutoSize = true;
            this.lblNoATMs.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNoATMs.Location = new System.Drawing.Point(121, 175);
            this.lblNoATMs.Name = "lblNoATMs";
            this.lblNoATMs.Size = new System.Drawing.Size(214, 25);
            this.lblNoATMs.TabIndex = 4;
            this.lblNoATMs.Text = "Number of ATMs: 1";
            // 
            // btnOff
            // 
            this.btnOff.Location = new System.Drawing.Point(554, 203);
            this.btnOff.Name = "btnOff";
            this.btnOff.Size = new System.Drawing.Size(97, 25);
            this.btnOff.TabIndex = 5;
            this.btnOff.Text = "Off";
            this.btnOff.UseVisualStyleBackColor = true;
            this.btnOff.Click += new System.EventHandler(this.switchRaceCondition);
            // 
            // btn1
            // 
            this.btn1.Location = new System.Drawing.Point(126, 203);
            this.btn1.Name = "btn1";
            this.btn1.Size = new System.Drawing.Size(96, 23);
            this.btn1.TabIndex = 6;
            this.btn1.Text = "1";
            this.btn1.UseVisualStyleBackColor = true;
            this.btn1.Click += new System.EventHandler(this.btn_Click);
            // 
            // btn2
            // 
            this.btn2.Location = new System.Drawing.Point(228, 203);
            this.btn2.Name = "btn2";
            this.btn2.Size = new System.Drawing.Size(94, 23);
            this.btn2.TabIndex = 7;
            this.btn2.Text = "2";
            this.btn2.UseVisualStyleBackColor = true;
            this.btn2.Click += new System.EventHandler(this.btn_Click);
            // 
            // btn3
            // 
            this.btn3.Location = new System.Drawing.Point(126, 232);
            this.btn3.Name = "btn3";
            this.btn3.Size = new System.Drawing.Size(196, 23);
            this.btn3.TabIndex = 8;
            this.btn3.Text = "3";
            this.btn3.UseVisualStyleBackColor = true;
            this.btn3.Click += new System.EventHandler(this.btn_Click);
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(242, 333);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(170, 70);
            this.btnStart.TabIndex = 10;
            this.btnStart.Text = "Start ATMs";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // btnLogs
            // 
            this.btnLogs.Location = new System.Drawing.Point(418, 333);
            this.btnLogs.Name = "btnLogs";
            this.btnLogs.Size = new System.Drawing.Size(92, 33);
            this.btnLogs.TabIndex = 11;
            this.btnLogs.Text = "View Logs";
            this.btnLogs.UseVisualStyleBackColor = true;
            this.btnLogs.Click += new System.EventHandler(this.btnLogs_Click);
            // 
            // btnAboutUs
            // 
            this.btnAboutUs.Location = new System.Drawing.Point(418, 370);
            this.btnAboutUs.Name = "btnAboutUs";
            this.btnAboutUs.Size = new System.Drawing.Size(92, 33);
            this.btnAboutUs.TabIndex = 12;
            this.btnAboutUs.Text = "About Us";
            this.btnAboutUs.UseVisualStyleBackColor = true;
            this.btnAboutUs.Click += new System.EventHandler(this.btnAboutUs_Click);
            // 
            // listLogs
            // 
            this.listLogs.HideSelection = false;
            this.listLogs.Location = new System.Drawing.Point(12, 88);
            this.listLogs.Name = "listLogs";
            this.listLogs.Size = new System.Drawing.Size(761, 350);
            this.listLogs.TabIndex = 13;
            this.listLogs.UseCompatibleStateImageBehavior = false;
            this.listLogs.View = System.Windows.Forms.View.List;
            this.listLogs.Visible = false;
            // 
            // btnBack
            // 
            this.btnBack.Location = new System.Drawing.Point(688, 44);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(85, 33);
            this.btnBack.TabIndex = 14;
            this.btnBack.Text = "Back";
            this.btnBack.UseVisualStyleBackColor = true;
            this.btnBack.Visible = false;
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(785, 450);
            this.Controls.Add(this.btnBack);
            this.Controls.Add(this.listLogs);
            this.Controls.Add(this.btnAboutUs);
            this.Controls.Add(this.btnLogs);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.btn3);
            this.Controls.Add(this.btn2);
            this.Controls.Add(this.btn1);
            this.Controls.Add(this.btnOff);
            this.Controls.Add(this.lblNoATMs);
            this.Controls.Add(this.lblRaceCondition);
            this.Controls.Add(this.btnOn);
            this.Controls.Add(this.lblTitle);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form1";
            this.Text = "Central Bank System";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Button btnOn;
        private System.Windows.Forms.Label lblRaceCondition;
        private System.Windows.Forms.Label lblNoATMs;
        private System.Windows.Forms.Button btnOff;
        private System.Windows.Forms.Button btn1;
        private System.Windows.Forms.Button btn2;
        private System.Windows.Forms.Button btn3;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Button btnLogs;
        private System.Windows.Forms.Button btnAboutUs;
        private System.Windows.Forms.ListView listLogs;
        private System.Windows.Forms.Button btnBack;
    }
}

