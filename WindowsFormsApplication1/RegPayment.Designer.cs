namespace ServiceOverblik
{
    partial class RegPayment
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RegPayment));
            this.button1 = new System.Windows.Forms.Button();
            this.regPaymentContract = new System.Windows.Forms.TextBox();
            this.regPaymentName = new System.Windows.Forms.TextBox();
            this.button2 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.regPaymentIsPaid = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(50, 173);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "Søg";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // regPaymentContract
            // 
            this.regPaymentContract.Location = new System.Drawing.Point(50, 55);
            this.regPaymentContract.Name = "regPaymentContract";
            this.regPaymentContract.Size = new System.Drawing.Size(100, 20);
            this.regPaymentContract.TabIndex = 1;
            // 
            // regPaymentName
            // 
            this.regPaymentName.Location = new System.Drawing.Point(50, 97);
            this.regPaymentName.Name = "regPaymentName";
            this.regPaymentName.ReadOnly = true;
            this.regPaymentName.Size = new System.Drawing.Size(168, 20);
            this.regPaymentName.TabIndex = 2;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(143, 173);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 3;
            this.button2.Text = "Betalt";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(50, 39);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(61, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Service nr.:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(50, 82);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Kunde:";
            // 
            // regPaymentIsPaid
            // 
            this.regPaymentIsPaid.AutoSize = true;
            this.regPaymentIsPaid.Location = new System.Drawing.Point(50, 136);
            this.regPaymentIsPaid.Name = "regPaymentIsPaid";
            this.regPaymentIsPaid.Size = new System.Drawing.Size(53, 17);
            this.regPaymentIsPaid.TabIndex = 6;
            this.regPaymentIsPaid.Text = "Betalt";
            this.regPaymentIsPaid.UseVisualStyleBackColor = true;
            // 
            // RegPayment
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(282, 259);
            this.Controls.Add(this.regPaymentIsPaid);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.regPaymentName);
            this.Controls.Add(this.regPaymentContract);
            this.Controls.Add(this.button1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "RegPayment";
            this.Text = "Registrer betaling";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox regPaymentContract;
        private System.Windows.Forms.TextBox regPaymentName;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox regPaymentIsPaid;
    }
}