namespace DoallVietnam
{
    partial class Form_CreatWallPit
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
            this.cbCreatWallWallType = new System.Windows.Forms.ComboBox();
            this.tbCreatWallUnconnectHeight = new System.Windows.Forms.TextBox();
            this.btCreatWallOk = new System.Windows.Forms.Button();
            this.btCreatWallCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(113, 64);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Wall Type";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(113, 154);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(100, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Unconected Height";
            // 
            // cbCreatWallWallType
            // 
            this.cbCreatWallWallType.FormattingEnabled = true;
            this.cbCreatWallWallType.Location = new System.Drawing.Point(297, 56);
            this.cbCreatWallWallType.Name = "cbCreatWallWallType";
            this.cbCreatWallWallType.Size = new System.Drawing.Size(387, 21);
            this.cbCreatWallWallType.TabIndex = 2;
            this.cbCreatWallWallType.SelectedIndexChanged += new System.EventHandler(this.cbCreatWallWallType_SelectedIndexChanged);
            this.cbCreatWallWallType.SelectedValueChanged += new System.EventHandler(this.cbCreatWallWallType_SelectedValueChanged);
            // 
            // tbCreatWallUnconnectHeight
            // 
            this.tbCreatWallUnconnectHeight.Location = new System.Drawing.Point(297, 146);
            this.tbCreatWallUnconnectHeight.Name = "tbCreatWallUnconnectHeight";
            this.tbCreatWallUnconnectHeight.Size = new System.Drawing.Size(387, 20);
            this.tbCreatWallUnconnectHeight.TabIndex = 3;
            this.tbCreatWallUnconnectHeight.Text = "3500";
            this.tbCreatWallUnconnectHeight.TextChanged += new System.EventHandler(this.tbCreatWallUnconnectHeight_TextChanged);
            // 
            // btCreatWallOk
            // 
            this.btCreatWallOk.Location = new System.Drawing.Point(557, 333);
            this.btCreatWallOk.Name = "btCreatWallOk";
            this.btCreatWallOk.Size = new System.Drawing.Size(75, 23);
            this.btCreatWallOk.TabIndex = 4;
            this.btCreatWallOk.Text = "OK";
            this.btCreatWallOk.UseVisualStyleBackColor = true;
            this.btCreatWallOk.Click += new System.EventHandler(this.btCreatWallOk_Click);
            // 
            // btCreatWallCancel
            // 
            this.btCreatWallCancel.Location = new System.Drawing.Point(701, 333);
            this.btCreatWallCancel.Name = "btCreatWallCancel";
            this.btCreatWallCancel.Size = new System.Drawing.Size(75, 23);
            this.btCreatWallCancel.TabIndex = 4;
            this.btCreatWallCancel.Text = "Cancel";
            this.btCreatWallCancel.UseVisualStyleBackColor = true;
            this.btCreatWallCancel.Click += new System.EventHandler(this.btCreatWallCancel_Click);
            // 
            // Form_CreatWallPit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(859, 401);
            this.Controls.Add(this.btCreatWallCancel);
            this.Controls.Add(this.btCreatWallOk);
            this.Controls.Add(this.tbCreatWallUnconnectHeight);
            this.Controls.Add(this.cbCreatWallWallType);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "Form_CreatWallPit";
            this.Text = "Form_CreatWallPit";
            this.Load += new System.EventHandler(this.Form_CreatWallPit_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cbCreatWallWallType;
        private System.Windows.Forms.TextBox tbCreatWallUnconnectHeight;
        private System.Windows.Forms.Button btCreatWallOk;
        private System.Windows.Forms.Button btCreatWallCancel;
    }
}