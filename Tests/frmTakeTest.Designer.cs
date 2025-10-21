namespace DVLD_Project
{
    partial class frmTakeTest
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
            this.btnSave = new Siticone.Desktop.UI.WinForms.SiticoneButton();
            this.label8 = new System.Windows.Forms.Label();
            this.rbFile = new System.Windows.Forms.RadioButton();
            this.rbPass = new System.Windows.Forms.RadioButton();
            this.label10 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.siticonePictureBox4 = new Siticone.Desktop.UI.WinForms.SiticonePictureBox();
            this.siticonePictureBox3 = new Siticone.Desktop.UI.WinForms.SiticonePictureBox();
            this.btnClose = new Siticone.Desktop.UI.WinForms.SiticoneButton();
            this.txtNotes = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.ctrlSecheduledTest11 = new DVLD_Project.ctrlSecheduledTest1();
            ((System.ComponentModel.ISupportInitialize)(this.siticonePictureBox4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.siticonePictureBox3)).BeginInit();
            this.SuspendLayout();
            // 
            // btnSave
            // 
            this.btnSave.Animated = true;
            this.btnSave.AnimatedGIF = true;
            this.btnSave.BorderRadius = 10;
            this.btnSave.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnSave.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnSave.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnSave.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnSave.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(150)))), ((int)(((byte)(243)))));
            this.btnSave.Font = new System.Drawing.Font("Roboto", 9F, System.Drawing.FontStyle.Bold);
            this.btnSave.ForeColor = System.Drawing.Color.White;
            this.btnSave.Image = global::DVLD_Project.Properties.Resources.icons8_save_33;
            this.btnSave.ImageAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.btnSave.ImeMode = System.Windows.Forms.ImeMode.On;
            this.btnSave.Location = new System.Drawing.Point(285, 792);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(134, 38);
            this.btnSave.TabIndex = 136;
            this.btnSave.Text = "Save";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.ForeColor = System.Drawing.Color.Red;
            this.label8.Location = new System.Drawing.Point(327, 638);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(194, 16);
            this.label8.TabIndex = 134;
            this.label8.Text = "You Cannot Change The Result";
            this.label8.Visible = false;
            // 
            // rbFile
            // 
            this.rbFile.AutoSize = true;
            this.rbFile.Location = new System.Drawing.Point(259, 635);
            this.rbFile.Name = "rbFile";
            this.rbFile.Size = new System.Drawing.Size(50, 20);
            this.rbFile.TabIndex = 132;
            this.rbFile.TabStop = true;
            this.rbFile.Text = "File";
            this.rbFile.UseVisualStyleBackColor = true;
            this.rbFile.CheckedChanged += new System.EventHandler(this.rbFile_CheckedChanged);
            // 
            // rbPass
            // 
            this.rbPass.AutoSize = true;
            this.rbPass.Location = new System.Drawing.Point(190, 635);
            this.rbPass.Name = "rbPass";
            this.rbPass.Size = new System.Drawing.Size(59, 20);
            this.rbPass.TabIndex = 133;
            this.rbPass.TabStop = true;
            this.rbPass.Text = "Pass";
            this.rbPass.UseVisualStyleBackColor = true;
            this.rbPass.CheckedChanged += new System.EventHandler(this.rbPass_CheckedChanged);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.BackColor = System.Drawing.Color.White;
            this.label10.Font = new System.Drawing.Font("Roboto", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label10.ForeColor = System.Drawing.Color.Black;
            this.label10.Location = new System.Drawing.Point(24, 674);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(70, 20);
            this.label10.TabIndex = 128;
            this.label10.Text = "Notes  ";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.White;
            this.label7.Font = new System.Drawing.Font("Roboto", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label7.ForeColor = System.Drawing.Color.Black;
            this.label7.Location = new System.Drawing.Point(24, 634);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(77, 20);
            this.label7.TabIndex = 129;
            this.label7.Text = "Result   ";
            // 
            // siticonePictureBox4
            // 
            this.siticonePictureBox4.BackColor = System.Drawing.Color.White;
            this.siticonePictureBox4.Image = global::DVLD_Project.Properties.Resources.Notes_32;
            this.siticonePictureBox4.ImageRotate = 0F;
            this.siticonePictureBox4.Location = new System.Drawing.Point(110, 668);
            this.siticonePictureBox4.Name = "siticonePictureBox4";
            this.siticonePictureBox4.Size = new System.Drawing.Size(32, 32);
            this.siticonePictureBox4.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.siticonePictureBox4.TabIndex = 130;
            this.siticonePictureBox4.TabStop = false;
            // 
            // siticonePictureBox3
            // 
            this.siticonePictureBox3.BackColor = System.Drawing.Color.White;
            this.siticonePictureBox3.Image = global::DVLD_Project.Properties.Resources.Number_32;
            this.siticonePictureBox3.ImageRotate = 0F;
            this.siticonePictureBox3.Location = new System.Drawing.Point(112, 630);
            this.siticonePictureBox3.Name = "siticonePictureBox3";
            this.siticonePictureBox3.Size = new System.Drawing.Size(32, 32);
            this.siticonePictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.siticonePictureBox3.TabIndex = 131;
            this.siticonePictureBox3.TabStop = false;
            // 
            // btnClose
            // 
            this.btnClose.Animated = true;
            this.btnClose.AnimatedGIF = true;
            this.btnClose.BorderRadius = 10;
            this.btnClose.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnClose.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnClose.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnClose.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnClose.FillColor = System.Drawing.Color.Red;
            this.btnClose.Font = new System.Drawing.Font("Roboto", 9F, System.Drawing.FontStyle.Bold);
            this.btnClose.ForeColor = System.Drawing.Color.White;
            this.btnClose.Image = global::DVLD_Project.Properties.Resources.icons8_close_20__2_;
            this.btnClose.ImageAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.btnClose.ImeMode = System.Windows.Forms.ImeMode.On;
            this.btnClose.Location = new System.Drawing.Point(423, 792);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(134, 38);
            this.btnClose.TabIndex = 136;
            this.btnClose.Text = "Close";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // txtNotes
            // 
            this.txtNotes.Location = new System.Drawing.Point(158, 674);
            this.txtNotes.Multiline = true;
            this.txtNotes.Name = "txtNotes";
            this.txtNotes.Size = new System.Drawing.Size(389, 91);
            this.txtNotes.TabIndex = 197;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.panel1.Location = new System.Drawing.Point(-11, 614);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(570, 1);
            this.panel1.TabIndex = 199;
            // 
            // ctrlSecheduledTest11
            // 
            this.ctrlSecheduledTest11.Location = new System.Drawing.Point(-1, -1);
            this.ctrlSecheduledTest11.Name = "ctrlSecheduledTest11";
            this.ctrlSecheduledTest11.Size = new System.Drawing.Size(560, 613);
            this.ctrlSecheduledTest11.TabIndex = 200;
            // 
            // frmTakeTest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(553, 837);
            this.Controls.Add(this.ctrlSecheduledTest11);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.txtNotes);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.rbFile);
            this.Controls.Add(this.rbPass);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.siticonePictureBox4);
            this.Controls.Add(this.siticonePictureBox3);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "frmTakeTest";
            this.Text = "frmTest";
            this.Load += new System.EventHandler(this.frmTest_Load);
            ((System.ComponentModel.ISupportInitialize)(this.siticonePictureBox4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.siticonePictureBox3)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private Siticone.Desktop.UI.WinForms.SiticoneButton btnSave;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.RadioButton rbFile;
        private System.Windows.Forms.RadioButton rbPass;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label7;
        private Siticone.Desktop.UI.WinForms.SiticonePictureBox siticonePictureBox4;
        private Siticone.Desktop.UI.WinForms.SiticonePictureBox siticonePictureBox3;
        private Siticone.Desktop.UI.WinForms.SiticoneButton btnClose;
        private System.Windows.Forms.TextBox txtNotes;
        private ctrlSecheduledTest1 ctrlTakeTest1;
        private System.Windows.Forms.Panel panel1;
        private ctrlSecheduledTest1 ctrlSecheduledTest11;
    }
}