namespace DoallVietnam
{
    partial class Form_CreatRoom
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form_CreatRoom));
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.btnCreat = new System.Windows.Forms.Button();
            this.btCancel = new System.Windows.Forms.Button();
            this.tbHighRoom = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cbUseFloorsModel = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.CbFloorType = new System.Windows.Forms.ComboBox();
            this.CbWallType = new System.Windows.Forms.ComboBox();
            this.cbCeilingType = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(470, 25);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(191, 202);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // btnCreat
            // 
            this.btnCreat.Location = new System.Drawing.Point(472, 255);
            this.btnCreat.Name = "btnCreat";
            this.btnCreat.Size = new System.Drawing.Size(75, 23);
            this.btnCreat.TabIndex = 1;
            this.btnCreat.Text = "Creat";
            this.btnCreat.UseVisualStyleBackColor = true;
            this.btnCreat.Click += new System.EventHandler(this.btnCreat_Click);
            // 
            // btCancel
            // 
            this.btCancel.Location = new System.Drawing.Point(584, 255);
            this.btCancel.Name = "btCancel";
            this.btCancel.Size = new System.Drawing.Size(75, 23);
            this.btCancel.TabIndex = 1;
            this.btCancel.Text = "Cancel";
            this.btCancel.UseVisualStyleBackColor = true;
            // 
            // tbHighRoom
            // 
            this.tbHighRoom.Location = new System.Drawing.Point(396, 135);
            this.tbHighRoom.Name = "tbHighRoom";
            this.tbHighRoom.Size = new System.Drawing.Size(68, 20);
            this.tbHighRoom.TabIndex = 2;
            this.tbHighRoom.Text = "2700";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(367, 138);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(24, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "H =";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 47);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(57, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Floor Type";
            // 
            // cbUseFloorsModel
            // 
            this.cbUseFloorsModel.AutoSize = true;
            this.cbUseFloorsModel.Location = new System.Drawing.Point(360, 71);
            this.cbUseFloorsModel.Name = "cbUseFloorsModel";
            this.cbUseFloorsModel.Size = new System.Drawing.Size(108, 17);
            this.cbUseFloorsModel.TabIndex = 4;
            this.cbUseFloorsModel.Text = "Use Floors Model";
            this.cbUseFloorsModel.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(15, 133);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(55, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "Wall Type";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(13, 225);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(61, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "Ceiling type";
            // 
            // CbFloorType
            // 
            this.CbFloorType.FormattingEnabled = true;
            this.CbFloorType.Location = new System.Drawing.Point(16, 69);
            this.CbFloorType.Name = "CbFloorType";
            this.CbFloorType.Size = new System.Drawing.Size(323, 21);
            this.CbFloorType.TabIndex = 5;
            // 
            // CbWallType
            // 
            this.CbWallType.FormattingEnabled = true;
            this.CbWallType.Location = new System.Drawing.Point(16, 155);
            this.CbWallType.Name = "CbWallType";
            this.CbWallType.Size = new System.Drawing.Size(323, 21);
            this.CbWallType.TabIndex = 5;
            // 
            // cbCeilingType
            // 
            this.cbCeilingType.FormattingEnabled = true;
            this.cbCeilingType.Location = new System.Drawing.Point(16, 246);
            this.cbCeilingType.Name = "cbCeilingType";
            this.cbCeilingType.Size = new System.Drawing.Size(323, 21);
            this.cbCeilingType.TabIndex = 5;
            // 
            // Form_CreatRoom
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(677, 307);
            this.Controls.Add(this.cbCeilingType);
            this.Controls.Add(this.CbWallType);
            this.Controls.Add(this.CbFloorType);
            this.Controls.Add(this.cbUseFloorsModel);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tbHighRoom);
            this.Controls.Add(this.btCancel);
            this.Controls.Add(this.btnCreat);
            this.Controls.Add(this.pictureBox1);
            this.Name = "Form_CreatRoom";
            this.Text = "Form_CreatRoom";
            this.Load += new System.EventHandler(this.Form_CreatRoom_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button btnCreat;
        private System.Windows.Forms.Button btCancel;
        private System.Windows.Forms.TextBox tbHighRoom;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox cbUseFloorsModel;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox CbFloorType;
        private System.Windows.Forms.ComboBox CbWallType;
        private System.Windows.Forms.ComboBox cbCeilingType;
    }
}