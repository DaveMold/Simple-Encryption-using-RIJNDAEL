namespace WindowsFormsApplication1
{
    partial class Form1
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
            this.rtb_output = new System.Windows.Forms.RichTextBox();
            this.rtb_input = new System.Windows.Forms.RichTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.bt_encrypt = new System.Windows.Forms.Button();
            this.bt_Decrypt = new System.Windows.Forms.Button();
            this.bt_resetTxtb = new System.Windows.Forms.Button();
            this.txtB_key = new System.Windows.Forms.TextBox();
            this.lb_Key = new System.Windows.Forms.Label();
            this.radbtn_basicEncrypt = new System.Windows.Forms.RadioButton();
            this.radbtn_AesEncryt = new System.Windows.Forms.RadioButton();
            this.encryt_type = new System.Windows.Forms.Label();
            this.numUpDown_KeySize = new System.Windows.Forms.NumericUpDown();
            this.lb_KeySize = new System.Windows.Forms.Label();
            this.lb_DataType = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.radbn_fileData = new System.Windows.Forms.RadioButton();
            this.radbn_txtData = new System.Windows.Forms.RadioButton();
            this.bn_BrousePath = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.chkbx_CopyFile = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.numUpDown_KeySize)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // rtb_output
            // 
            this.rtb_output.BackColor = System.Drawing.Color.CornflowerBlue;
            this.rtb_output.Font = new System.Drawing.Font("Times New Roman", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rtb_output.Location = new System.Drawing.Point(12, 240);
            this.rtb_output.Name = "rtb_output";
            this.rtb_output.ReadOnly = true;
            this.rtb_output.Size = new System.Drawing.Size(410, 68);
            this.rtb_output.TabIndex = 1;
            this.rtb_output.Text = "";
            this.rtb_output.TextChanged += new System.EventHandler(this.richTextBox1_TextChanged);
            // 
            // rtb_input
            // 
            this.rtb_input.BackColor = System.Drawing.Color.Lime;
            this.rtb_input.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rtb_input.Location = new System.Drawing.Point(12, 29);
            this.rtb_input.Name = "rtb_input";
            this.rtb_input.Size = new System.Drawing.Size(410, 88);
            this.rtb_input.TabIndex = 0;
            this.rtb_input.Text = "";
            this.rtb_input.TextChanged += new System.EventHandler(this.richTextBox2_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(234, 17);
            this.label1.TabIndex = 2;
            this.label1.Text = "Text for Encryption/Decryption ";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(12, 220);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(90, 17);
            this.label2.TabIndex = 3;
            this.label2.Text = "Result Text";
            // 
            // bt_encrypt
            // 
            this.bt_encrypt.AccessibleName = "bt_encrypt";
            this.bt_encrypt.Location = new System.Drawing.Point(357, 118);
            this.bt_encrypt.Name = "bt_encrypt";
            this.bt_encrypt.Size = new System.Drawing.Size(66, 31);
            this.bt_encrypt.TabIndex = 4;
            this.bt_encrypt.Text = "Encrypt";
            this.bt_encrypt.UseVisualStyleBackColor = true;
            this.bt_encrypt.Click += new System.EventHandler(this.button1_Click);
            // 
            // bt_Decrypt
            // 
            this.bt_Decrypt.Location = new System.Drawing.Point(292, 118);
            this.bt_Decrypt.Name = "bt_Decrypt";
            this.bt_Decrypt.Size = new System.Drawing.Size(67, 31);
            this.bt_Decrypt.TabIndex = 5;
            this.bt_Decrypt.Text = "Decrypt";
            this.bt_Decrypt.UseVisualStyleBackColor = true;
            this.bt_Decrypt.Click += new System.EventHandler(this.button2_Click);
            // 
            // bt_resetTxtb
            // 
            this.bt_resetTxtb.Location = new System.Drawing.Point(10, 118);
            this.bt_resetTxtb.Name = "bt_resetTxtb";
            this.bt_resetTxtb.Size = new System.Drawing.Size(84, 31);
            this.bt_resetTxtb.TabIndex = 6;
            this.bt_resetTxtb.Text = "Clear Input";
            this.bt_resetTxtb.UseVisualStyleBackColor = true;
            this.bt_resetTxtb.Click += new System.EventHandler(this.bt_resetTxtb_Click);
            // 
            // txtB_key
            // 
            this.txtB_key.BackColor = System.Drawing.Color.GreenYellow;
            this.txtB_key.Location = new System.Drawing.Point(148, 151);
            this.txtB_key.MaxLength = 16;
            this.txtB_key.Name = "txtB_key";
            this.txtB_key.ReadOnly = true;
            this.txtB_key.Size = new System.Drawing.Size(124, 22);
            this.txtB_key.TabIndex = 7;
            this.txtB_key.Text = "No key is required";
            this.txtB_key.TextChanged += new System.EventHandler(this.txtB_key_TextChanged);
            // 
            // lb_Key
            // 
            this.lb_Key.AutoSize = true;
            this.lb_Key.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lb_Key.Location = new System.Drawing.Point(97, 152);
            this.lb_Key.Name = "lb_Key";
            this.lb_Key.Size = new System.Drawing.Size(45, 17);
            this.lb_Key.TabIndex = 8;
            this.lb_Key.Text = "Key :";
            // 
            // radbtn_basicEncrypt
            // 
            this.radbtn_basicEncrypt.AutoSize = true;
            this.radbtn_basicEncrypt.Checked = true;
            this.radbtn_basicEncrypt.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radbtn_basicEncrypt.Location = new System.Drawing.Point(338, 182);
            this.radbtn_basicEncrypt.Name = "radbtn_basicEncrypt";
            this.radbtn_basicEncrypt.Size = new System.Drawing.Size(68, 21);
            this.radbtn_basicEncrypt.TabIndex = 9;
            this.radbtn_basicEncrypt.TabStop = true;
            this.radbtn_basicEncrypt.Text = "Basic";
            this.radbtn_basicEncrypt.UseVisualStyleBackColor = true;
            this.radbtn_basicEncrypt.CheckedChanged += new System.EventHandler(this.radbtn_basicEncrypt_CheckedChanged);
            // 
            // radbtn_AesEncryt
            // 
            this.radbtn_AesEncryt.AutoSize = true;
            this.radbtn_AesEncryt.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radbtn_AesEncryt.Location = new System.Drawing.Point(272, 182);
            this.radbtn_AesEncryt.Name = "radbtn_AesEncryt";
            this.radbtn_AesEncryt.Size = new System.Drawing.Size(59, 21);
            this.radbtn_AesEncryt.TabIndex = 10;
            this.radbtn_AesEncryt.Text = "AES";
            this.radbtn_AesEncryt.UseVisualStyleBackColor = true;
            this.radbtn_AesEncryt.CheckedChanged += new System.EventHandler(this.radbtn_AesEncryt_CheckedChanged);
            // 
            // encryt_type
            // 
            this.encryt_type.AutoSize = true;
            this.encryt_type.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.encryt_type.Location = new System.Drawing.Point(130, 184);
            this.encryt_type.Name = "encryt_type";
            this.encryt_type.Size = new System.Drawing.Size(136, 17);
            this.encryt_type.TabIndex = 11;
            this.encryt_type.Text = "Encryption type  :";
            // 
            // numUpDown_KeySize
            // 
            this.numUpDown_KeySize.BackColor = System.Drawing.Color.GreenYellow;
            this.numUpDown_KeySize.Cursor = System.Windows.Forms.Cursors.Default;
            this.numUpDown_KeySize.Increment = new decimal(new int[] {
            16,
            0,
            0,
            0});
            this.numUpDown_KeySize.Location = new System.Drawing.Point(365, 152);
            this.numUpDown_KeySize.Maximum = new decimal(new int[] {
            32,
            0,
            0,
            0});
            this.numUpDown_KeySize.Minimum = new decimal(new int[] {
            16,
            0,
            0,
            0});
            this.numUpDown_KeySize.Name = "numUpDown_KeySize";
            this.numUpDown_KeySize.ReadOnly = true;
            this.numUpDown_KeySize.Size = new System.Drawing.Size(57, 22);
            this.numUpDown_KeySize.TabIndex = 12;
            this.numUpDown_KeySize.Value = new decimal(new int[] {
            16,
            0,
            0,
            0});
            this.numUpDown_KeySize.ValueChanged += new System.EventHandler(this.numUpDown_KeySize_ValueChanged);
            // 
            // lb_KeySize
            // 
            this.lb_KeySize.AutoSize = true;
            this.lb_KeySize.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lb_KeySize.Location = new System.Drawing.Point(278, 154);
            this.lb_KeySize.Name = "lb_KeySize";
            this.lb_KeySize.Size = new System.Drawing.Size(81, 17);
            this.lb_KeySize.TabIndex = 13;
            this.lb_KeySize.Text = "Key Size :";
            this.lb_KeySize.Click += new System.EventHandler(this.label3_Click);
            // 
            // lb_DataType
            // 
            this.lb_DataType.AutoSize = true;
            this.lb_DataType.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lb_DataType.Location = new System.Drawing.Point(156, 211);
            this.lb_DataType.Name = "lb_DataType";
            this.lb_DataType.Size = new System.Drawing.Size(110, 17);
            this.lb_DataType.TabIndex = 14;
            this.lb_DataType.Text = "Type of data :";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.radbn_fileData);
            this.panel1.Controls.Add(this.radbn_txtData);
            this.panel1.Location = new System.Drawing.Point(269, 206);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(154, 22);
            this.panel1.TabIndex = 15;
            // 
            // radbn_fileData
            // 
            this.radbn_fileData.AutoSize = true;
            this.radbn_fileData.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radbn_fileData.Location = new System.Drawing.Point(69, 3);
            this.radbn_fileData.Name = "radbn_fileData";
            this.radbn_fileData.Size = new System.Drawing.Size(55, 21);
            this.radbn_fileData.TabIndex = 1;
            this.radbn_fileData.TabStop = true;
            this.radbn_fileData.Text = "File";
            this.radbn_fileData.UseVisualStyleBackColor = true;
            this.radbn_fileData.CheckedChanged += new System.EventHandler(this.radbn_fileData_CheckedChanged);
            // 
            // radbn_txtData
            // 
            this.radbn_txtData.AutoSize = true;
            this.radbn_txtData.Checked = true;
            this.radbn_txtData.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radbn_txtData.Location = new System.Drawing.Point(3, 3);
            this.radbn_txtData.Name = "radbn_txtData";
            this.radbn_txtData.Size = new System.Drawing.Size(60, 21);
            this.radbn_txtData.TabIndex = 0;
            this.radbn_txtData.TabStop = true;
            this.radbn_txtData.Text = "Text";
            this.radbn_txtData.UseVisualStyleBackColor = true;
            this.radbn_txtData.CheckedChanged += new System.EventHandler(this.radbn_txtData_CheckedChanged);
            // 
            // bn_BrousePath
            // 
            this.bn_BrousePath.Enabled = false;
            this.bn_BrousePath.Location = new System.Drawing.Point(92, 118);
            this.bn_BrousePath.Name = "bn_BrousePath";
            this.bn_BrousePath.Size = new System.Drawing.Size(75, 31);
            this.bn_BrousePath.TabIndex = 16;
            this.bn_BrousePath.Text = "Browse";
            this.bn_BrousePath.UseVisualStyleBackColor = true;
            this.bn_BrousePath.Click += new System.EventHandler(this.bn_BrousePath_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            this.openFileDialog1.FileOk += new System.ComponentModel.CancelEventHandler(this.openFileDialog1_FileOk);
            // 
            // chkbx_CopyFile
            // 
            this.chkbx_CopyFile.AutoSize = true;
            this.chkbx_CopyFile.Checked = true;
            this.chkbx_CopyFile.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkbx_CopyFile.Enabled = false;
            this.chkbx_CopyFile.Location = new System.Drawing.Point(181, 123);
            this.chkbx_CopyFile.Name = "chkbx_CopyFile";
            this.chkbx_CopyFile.Size = new System.Drawing.Size(105, 21);
            this.chkbx_CopyFile.TabIndex = 17;
            this.chkbx_CopyFile.Text = "BackUp File";
            this.chkbx_CopyFile.UseVisualStyleBackColor = true;
            this.chkbx_CopyFile.CheckedChanged += new System.EventHandler(this.chkbx_CopyFile_CheckedChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.MediumPurple;
            this.ClientSize = new System.Drawing.Size(434, 320);
            this.Controls.Add(this.chkbx_CopyFile);
            this.Controls.Add(this.bn_BrousePath);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.lb_DataType);
            this.Controls.Add(this.lb_KeySize);
            this.Controls.Add(this.numUpDown_KeySize);
            this.Controls.Add(this.encryt_type);
            this.Controls.Add(this.radbtn_AesEncryt);
            this.Controls.Add(this.radbtn_basicEncrypt);
            this.Controls.Add(this.lb_Key);
            this.Controls.Add(this.txtB_key);
            this.Controls.Add(this.bt_resetTxtb);
            this.Controls.Add(this.bt_Decrypt);
            this.Controls.Add(this.bt_encrypt);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.rtb_input);
            this.Controls.Add(this.rtb_output);
            this.ForeColor = System.Drawing.SystemColors.Desktop;
            this.Name = "Form1";
            this.Opacity = 0.9D;
            this.Text = "Text Encrypter";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.numUpDown_KeySize)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox rtb_output;
        private System.Windows.Forms.RichTextBox rtb_input;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button bt_encrypt;
        private System.Windows.Forms.Button bt_Decrypt;
        private System.Windows.Forms.Button bt_resetTxtb;
        private System.Windows.Forms.TextBox txtB_key;
        private System.Windows.Forms.Label lb_Key;
        private System.Windows.Forms.RadioButton radbtn_basicEncrypt;
        private System.Windows.Forms.RadioButton radbtn_AesEncryt;
        private System.Windows.Forms.Label encryt_type;
        private System.Windows.Forms.NumericUpDown numUpDown_KeySize;
        private System.Windows.Forms.Label lb_KeySize;
        private System.Windows.Forms.Label lb_DataType;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.RadioButton radbn_fileData;
        private System.Windows.Forms.RadioButton radbn_txtData;
        private System.Windows.Forms.Button bn_BrousePath;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.CheckBox chkbx_CopyFile;
    }
}

