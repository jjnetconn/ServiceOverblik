namespace ServiceOverblik
{
    partial class MultipleInverters
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
            this.mInvertersLbx1 = new System.Windows.Forms.ListBox();
            this.mInvertersBtn1 = new System.Windows.Forms.Button();
            this.mInvertersBtn2 = new System.Windows.Forms.Button();
            this.mInverterCbx1 = new System.Windows.Forms.ComboBox();
            this.mInvertersLbl1 = new System.Windows.Forms.Label();
            this.mInvertersLbl2 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // mInvertersLbx1
            // 
            this.mInvertersLbx1.FormattingEnabled = true;
            this.mInvertersLbx1.Location = new System.Drawing.Point(41, 108);
            this.mInvertersLbx1.Name = "mInvertersLbx1";
            this.mInvertersLbx1.Size = new System.Drawing.Size(233, 121);
            this.mInvertersLbx1.TabIndex = 0;
            // 
            // mInvertersBtn1
            // 
            this.mInvertersBtn1.Location = new System.Drawing.Point(40, 58);
            this.mInvertersBtn1.Name = "mInvertersBtn1";
            this.mInvertersBtn1.Size = new System.Drawing.Size(75, 23);
            this.mInvertersBtn1.TabIndex = 1;
            this.mInvertersBtn1.Text = "Tilføj";
            this.mInvertersBtn1.UseVisualStyleBackColor = true;
            this.mInvertersBtn1.Click += new System.EventHandler(this.mInvertersBtn1_Click);
            // 
            // mInvertersBtn2
            // 
            this.mInvertersBtn2.Location = new System.Drawing.Point(41, 244);
            this.mInvertersBtn2.Name = "mInvertersBtn2";
            this.mInvertersBtn2.Size = new System.Drawing.Size(75, 23);
            this.mInvertersBtn2.TabIndex = 2;
            this.mInvertersBtn2.Text = "Gem";
            this.mInvertersBtn2.UseVisualStyleBackColor = true;
            this.mInvertersBtn2.Click += new System.EventHandler(this.mInvertersBtn2_Click);
            // 
            // mInverterCbx1
            // 
            this.mInverterCbx1.FormattingEnabled = true;
            this.mInverterCbx1.Location = new System.Drawing.Point(40, 31);
            this.mInverterCbx1.Name = "mInverterCbx1";
            this.mInverterCbx1.Size = new System.Drawing.Size(157, 21);
            this.mInverterCbx1.TabIndex = 3;
            // 
            // mInvertersLbl1
            // 
            this.mInvertersLbl1.AutoSize = true;
            this.mInvertersLbl1.Location = new System.Drawing.Point(41, 12);
            this.mInvertersLbl1.Name = "mInvertersLbl1";
            this.mInvertersLbl1.Size = new System.Drawing.Size(49, 13);
            this.mInvertersLbl1.TabIndex = 4;
            this.mInvertersLbl1.Text = "Invertere";
            // 
            // mInvertersLbl2
            // 
            this.mInvertersLbl2.AutoSize = true;
            this.mInvertersLbl2.Location = new System.Drawing.Point(40, 88);
            this.mInvertersLbl2.Name = "mInvertersLbl2";
            this.mInvertersLbl2.Size = new System.Drawing.Size(90, 13);
            this.mInvertersLbl2.TabIndex = 5;
            this.mInvertersLbl2.Text = "Tilføjede inverters";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(199, 244);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 6;
            this.button1.Text = "Luk";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // MultipleInverters
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(385, 307);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.mInvertersLbl2);
            this.Controls.Add(this.mInvertersLbl1);
            this.Controls.Add(this.mInverterCbx1);
            this.Controls.Add(this.mInvertersBtn2);
            this.Controls.Add(this.mInvertersBtn1);
            this.Controls.Add(this.mInvertersLbx1);
            this.Name = "MultipleInverters";
            this.Text = "MultipleInverters";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox mInvertersLbx1;
        private System.Windows.Forms.Button mInvertersBtn1;
        private System.Windows.Forms.Button mInvertersBtn2;
        private System.Windows.Forms.ComboBox mInverterCbx1;
        private System.Windows.Forms.Label mInvertersLbl1;
        private System.Windows.Forms.Label mInvertersLbl2;
        private System.Windows.Forms.Button button1;
    }
}