namespace Client
{
    partial class ConnectInputForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ConnectInputForm));
            this.label1 = new System.Windows.Forms.Label();
            this.ipBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.portBox = new System.Windows.Forms.TextBox();
            this.connectBtn = new System.Windows.Forms.Button();
            this.exitBtn = new System.Windows.Forms.Button();
            this.loadGif = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.loadGif)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(109, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(52, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "IP Adress";
            // 
            // ipBox
            // 
            this.ipBox.Location = new System.Drawing.Point(12, 45);
            this.ipBox.Name = "ipBox";
            this.ipBox.Size = new System.Drawing.Size(260, 20);
            this.ipBox.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(121, 87);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(26, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Port";
            // 
            // portBox
            // 
            this.portBox.Location = new System.Drawing.Point(12, 112);
            this.portBox.Name = "portBox";
            this.portBox.Size = new System.Drawing.Size(260, 20);
            this.portBox.TabIndex = 3;
            // 
            // connectBtn
            // 
            this.connectBtn.Location = new System.Drawing.Point(163, 178);
            this.connectBtn.Name = "connectBtn";
            this.connectBtn.Size = new System.Drawing.Size(75, 23);
            this.connectBtn.TabIndex = 4;
            this.connectBtn.Text = "Connect";
            this.connectBtn.UseVisualStyleBackColor = true;
            this.connectBtn.Click += new System.EventHandler(this.connectBtn_Click);
            // 
            // exitBtn
            // 
            this.exitBtn.Location = new System.Drawing.Point(54, 178);
            this.exitBtn.Name = "exitBtn";
            this.exitBtn.Size = new System.Drawing.Size(75, 23);
            this.exitBtn.TabIndex = 5;
            this.exitBtn.Text = "Exit";
            this.exitBtn.UseVisualStyleBackColor = true;
            this.exitBtn.Click += new System.EventHandler(this.exitBtn_Click);
            // 
            // loadGif
            // 
            this.loadGif.Image = ((System.Drawing.Image)(resources.GetObject("loadGif.Image")));
            this.loadGif.Location = new System.Drawing.Point(38, 45);
            this.loadGif.Name = "loadGif";
            this.loadGif.Size = new System.Drawing.Size(200, 171);
            this.loadGif.TabIndex = 6;
            this.loadGif.TabStop = false;
            // 
            // ConnectInputForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 229);
            this.Controls.Add(this.loadGif);
            this.Controls.Add(this.exitBtn);
            this.Controls.Add(this.connectBtn);
            this.Controls.Add(this.portBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.ipBox);
            this.Controls.Add(this.label1);
            this.Name = "ConnectInputForm";
            this.Text = "ConnectInput";
            ((System.ComponentModel.ISupportInitialize)(this.loadGif)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox ipBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox portBox;
        private System.Windows.Forms.Button connectBtn;
        private System.Windows.Forms.Button exitBtn;
        private System.Windows.Forms.PictureBox loadGif;
    }
}