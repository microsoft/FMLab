namespace ConnectorServiceApplication
{
    partial class mainForm
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtBoxTenant = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtBoxSecret = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtBoxClientId = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnEnqueue = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.richTextEnqueue = new System.Windows.Forms.RichTextBox();
            this.txtBoxInActivity = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.btnDequeue = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.richTxtDequeue = new System.Windows.Forms.RichTextBox();
            this.txtBoxOutActivity = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtBoxBaseUri = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.richTxtLog = new System.Windows.Forms.RichTextBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtBoxTenant);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.txtBoxSecret);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txtBoxClientId);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.ForeColor = System.Drawing.Color.White;
            this.groupBox1.Location = new System.Drawing.Point(12, 18);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(912, 81);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Azure Client Registration Details ";
            // 
            // txtBoxTenant
            // 
            this.txtBoxTenant.Location = new System.Drawing.Point(96, 48);
            this.txtBoxTenant.Name = "txtBoxTenant";
            this.txtBoxTenant.ReadOnly = true;
            this.txtBoxTenant.Size = new System.Drawing.Size(277, 20);
            this.txtBoxTenant.TabIndex = 5;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(10, 48);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(71, 15);
            this.label8.TabIndex = 4;
            this.label8.Text = "AAD Tenant";
            // 
            // txtBoxSecret
            // 
            this.txtBoxSecret.Location = new System.Drawing.Point(622, 19);
            this.txtBoxSecret.Name = "txtBoxSecret";
            this.txtBoxSecret.ReadOnly = true;
            this.txtBoxSecret.Size = new System.Drawing.Size(277, 20);
            this.txtBoxSecret.TabIndex = 3;
            this.txtBoxSecret.Text = "Client Secret key";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(519, 22);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(97, 15);
            this.label2.TabIndex = 2;
            this.label2.Text = "Client Secret key";
            // 
            // txtBoxClientId
            // 
            this.txtBoxClientId.Location = new System.Drawing.Point(235, 19);
            this.txtBoxClientId.Name = "txtBoxClientId";
            this.txtBoxClientId.ReadOnly = true;
            this.txtBoxClientId.Size = new System.Drawing.Size(277, 20);
            this.txtBoxClientId.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(219, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "Registered Client Id (from Azure Portal)";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnEnqueue);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.richTextEnqueue);
            this.groupBox2.Controls.Add(this.txtBoxInActivity);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.ForeColor = System.Drawing.Color.White;
            this.groupBox2.Location = new System.Drawing.Point(12, 134);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(912, 275);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Enqueue a message";
            // 
            // btnEnqueue
            // 
            this.btnEnqueue.ForeColor = System.Drawing.Color.Black;
            this.btnEnqueue.Location = new System.Drawing.Point(712, 28);
            this.btnEnqueue.Name = "btnEnqueue";
            this.btnEnqueue.Size = new System.Drawing.Size(150, 23);
            this.btnEnqueue.TabIndex = 6;
            this.btnEnqueue.Text = "Enqueue Message";
            this.btnEnqueue.UseVisualStyleBackColor = true;
            this.btnEnqueue.Click += new System.EventHandler(this.btnEnqueue_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(10, 68);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(58, 15);
            this.label4.TabIndex = 5;
            this.label4.Text = "Message";
            // 
            // richTextEnqueue
            // 
            this.richTextEnqueue.BackColor = System.Drawing.SystemColors.Info;
            this.richTextEnqueue.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.richTextEnqueue.Location = new System.Drawing.Point(83, 65);
            this.richTextEnqueue.Name = "richTextEnqueue";
            this.richTextEnqueue.Size = new System.Drawing.Size(816, 199);
            this.richTextEnqueue.TabIndex = 4;
            this.richTextEnqueue.Text = "";
            // 
            // txtBoxInActivity
            // 
            this.txtBoxInActivity.Location = new System.Drawing.Point(83, 30);
            this.txtBoxInActivity.Name = "txtBoxInActivity";
            this.txtBoxInActivity.Size = new System.Drawing.Size(277, 20);
            this.txtBoxInActivity.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(10, 33);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(55, 15);
            this.label3.TabIndex = 2;
            this.label3.Text = "Activity Id";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.btnDequeue);
            this.groupBox3.Controls.Add(this.label5);
            this.groupBox3.Controls.Add(this.richTxtDequeue);
            this.groupBox3.Controls.Add(this.txtBoxOutActivity);
            this.groupBox3.Controls.Add(this.label6);
            this.groupBox3.ForeColor = System.Drawing.Color.White;
            this.groupBox3.Location = new System.Drawing.Point(12, 415);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(912, 275);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Dequeue message";
            // 
            // btnDequeue
            // 
            this.btnDequeue.ForeColor = System.Drawing.Color.Black;
            this.btnDequeue.Location = new System.Drawing.Point(728, 28);
            this.btnDequeue.Name = "btnDequeue";
            this.btnDequeue.Size = new System.Drawing.Size(150, 23);
            this.btnDequeue.TabIndex = 6;
            this.btnDequeue.Text = "Dequeue Message";
            this.btnDequeue.UseVisualStyleBackColor = true;
            this.btnDequeue.Click += new System.EventHandler(this.btnDequeue_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(10, 68);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(58, 15);
            this.label5.TabIndex = 5;
            this.label5.Text = "Message";
            // 
            // richTxtDequeue
            // 
            this.richTxtDequeue.BackColor = System.Drawing.SystemColors.Info;
            this.richTxtDequeue.Location = new System.Drawing.Point(83, 65);
            this.richTxtDequeue.Name = "richTxtDequeue";
            this.richTxtDequeue.Size = new System.Drawing.Size(816, 199);
            this.richTxtDequeue.TabIndex = 4;
            this.richTxtDequeue.Text = "";
            // 
            // txtBoxOutActivity
            // 
            this.txtBoxOutActivity.Location = new System.Drawing.Point(83, 30);
            this.txtBoxOutActivity.Name = "txtBoxOutActivity";
            this.txtBoxOutActivity.Size = new System.Drawing.Size(277, 20);
            this.txtBoxOutActivity.TabIndex = 3;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(10, 33);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(55, 15);
            this.label6.TabIndex = 2;
            this.label6.Text = "Activity Id";
            // 
            // txtBoxBaseUri
            // 
            this.txtBoxBaseUri.Location = new System.Drawing.Point(200, 108);
            this.txtBoxBaseUri.Name = "txtBoxBaseUri";
            this.txtBoxBaseUri.Size = new System.Drawing.Size(479, 20);
            this.txtBoxBaseUri.TabIndex = 5;
            this.txtBoxBaseUri.Text = "Dynamics base URL for your org";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(11, 108);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(183, 15);
            this.label7.TabIndex = 4;
            this.label7.Text = "Dynamics base URL for your org";
            // 
            // richTxtLog
            // 
            this.richTxtLog.BackColor = System.Drawing.SystemColors.ControlLight;
            this.richTxtLog.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.richTxtLog.Location = new System.Drawing.Point(930, 60);
            this.richTxtLog.Name = "richTxtLog";
            this.richTxtLog.Size = new System.Drawing.Size(528, 619);
            this.richTxtLog.TabIndex = 6;
            this.richTxtLog.Text = "";
            // 
            // mainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(1470, 701);
            this.Controls.Add(this.richTxtLog);
            this.Controls.Add(this.txtBoxBaseUri);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.ForeColor = System.Drawing.Color.White;
            this.Name = "mainForm";
            this.Text = "Connector Service - Client Application";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtBoxSecret;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtBoxClientId;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox txtBoxInActivity;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.RichTextBox richTextEnqueue;
        private System.Windows.Forms.Button btnEnqueue;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button btnDequeue;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.RichTextBox richTxtDequeue;
        private System.Windows.Forms.TextBox txtBoxOutActivity;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtBoxBaseUri;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.RichTextBox richTxtLog;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtBoxTenant;
    }
}

