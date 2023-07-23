namespace DoallVietnam
{
    partial class Form_PlaceParkingLot
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
            this.label2 = new System.Windows.Forms.Label();
            this.OkButton = new System.Windows.Forms.Button();
            this.CancelBtn = new System.Windows.Forms.Button();
            this.cbFamily = new System.Windows.Forms.ComboBox();
            this.cbBlockCad = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(19, 88);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(139, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Family Generic Model Name";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(19, 195);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(87, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Block Cad Name";
            // 
            // OkButton
            // 
            this.OkButton.Location = new System.Drawing.Point(411, 284);
            this.OkButton.Name = "OkButton";
            this.OkButton.Size = new System.Drawing.Size(75, 23);
            this.OkButton.TabIndex = 1;
            this.OkButton.Text = "OK";
            this.OkButton.UseVisualStyleBackColor = true;
            this.OkButton.Click += new System.EventHandler(this.OkButton_Click);
            // 
            // CancelBtn
            // 
            this.CancelBtn.Location = new System.Drawing.Point(544, 284);
            this.CancelBtn.Name = "CancelBtn";
            this.CancelBtn.Size = new System.Drawing.Size(75, 23);
            this.CancelBtn.TabIndex = 1;
            this.CancelBtn.Text = "Cancel";
            this.CancelBtn.UseVisualStyleBackColor = true;
            this.CancelBtn.Click += new System.EventHandler(this.CancelButton_Click);
            // 
            // cbFamily
            // 
            this.cbFamily.FormattingEnabled = true;
            this.cbFamily.Location = new System.Drawing.Point(176, 85);
            this.cbFamily.Name = "cbFamily";
            this.cbFamily.Size = new System.Drawing.Size(488, 21);
            this.cbFamily.TabIndex = 2;
            this.cbFamily.SelectedValueChanged += new System.EventHandler(this.cbFamily_SelectedValueChanged);
            // 
            // cbBlockCad
            // 
            this.cbBlockCad.FormattingEnabled = true;
            this.cbBlockCad.Location = new System.Drawing.Point(176, 191);
            this.cbBlockCad.Name = "cbBlockCad";
            this.cbBlockCad.Size = new System.Drawing.Size(488, 21);
            this.cbBlockCad.TabIndex = 2;
            this.cbBlockCad.SelectedValueChanged += new System.EventHandler(this.cbBlockCad_SelectedValueChanged);
            // 
            // Form_PlaceParkingLot
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(688, 352);
            this.Controls.Add(this.cbBlockCad);
            this.Controls.Add(this.cbFamily);
            this.Controls.Add(this.CancelBtn);
            this.Controls.Add(this.OkButton);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "Form_PlaceParkingLot";
            this.Text = "Parking Place";
            this.Load += new System.EventHandler(this.Form_PlaceParkingLot_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button OkButton;
        private System.Windows.Forms.Button CancelBtn;
        private System.Windows.Forms.ComboBox cbFamily;
        private System.Windows.Forms.ComboBox cbBlockCad;
    }
}