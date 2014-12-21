namespace WinFormData
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
            this.components = new System.ComponentModel.Container();
            this.OrderGridView = new System.Windows.Forms.DataGridView();
            this.PostionGridView = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.startButton = new System.Windows.Forms.Button();
            this.stopButton = new System.Windows.Forms.Button();
            this.statusTextBox = new System.Windows.Forms.RichTextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.label42 = new System.Windows.Forms.Label();
            this.traderId = new System.Windows.Forms.TextBox();
            this.eSignalOutputPath = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.univShareSize = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.apiSymbol = new System.Windows.Forms.TextBox();
            this.apiBuy = new System.Windows.Forms.Button();
            this.apiSell = new System.Windows.Forms.Button();
            this.apiSize = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.pproPath = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.OrderGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PostionGridView)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // OrderGridView
            // 
            this.OrderGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.OrderGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.OrderGridView.Location = new System.Drawing.Point(12, 34);
            this.OrderGridView.Name = "OrderGridView";
            this.OrderGridView.RowHeadersVisible = false;
            this.OrderGridView.Size = new System.Drawing.Size(370, 307);
            this.OrderGridView.TabIndex = 0;
            // 
            // PostionGridView
            // 
            this.PostionGridView.AllowUserToAddRows = false;
            this.PostionGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.PostionGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.PostionGridView.Location = new System.Drawing.Point(438, 34);
            this.PostionGridView.MultiSelect = false;
            this.PostionGridView.Name = "PostionGridView";
            this.PostionGridView.ReadOnly = true;
            this.PostionGridView.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.PostionGridView.RowHeadersVisible = false;
            this.PostionGridView.Size = new System.Drawing.Size(370, 307);
            this.PostionGridView.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(33, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Order";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(435, 18);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(44, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Position";
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 5000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // startButton
            // 
            this.startButton.Location = new System.Drawing.Point(654, 525);
            this.startButton.Name = "startButton";
            this.startButton.Size = new System.Drawing.Size(75, 23);
            this.startButton.TabIndex = 4;
            this.startButton.Text = "Start";
            this.startButton.UseVisualStyleBackColor = true;
            // 
            // stopButton
            // 
            this.stopButton.Location = new System.Drawing.Point(733, 525);
            this.stopButton.Name = "stopButton";
            this.stopButton.Size = new System.Drawing.Size(75, 23);
            this.stopButton.TabIndex = 5;
            this.stopButton.Text = "Stop";
            this.stopButton.UseVisualStyleBackColor = true;
            // 
            // statusTextBox
            // 
            this.statusTextBox.Location = new System.Drawing.Point(438, 374);
            this.statusTextBox.Name = "statusTextBox";
            this.statusTextBox.Size = new System.Drawing.Size(370, 145);
            this.statusTextBox.TabIndex = 6;
            this.statusTextBox.Text = "";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(435, 354);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(37, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Status";
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(33, 357);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(49, 13);
            this.label21.TabIndex = 8;
            this.label21.Text = "TraderID";
            // 
            // label42
            // 
            this.label42.AutoSize = true;
            this.label42.Location = new System.Drawing.Point(11, 384);
            this.label42.Name = "label42";
            this.label42.Size = new System.Drawing.Size(71, 13);
            this.label42.TabIndex = 9;
            this.label42.Text = "E-Signal Path";
            // 
            // traderId
            // 
            this.traderId.Location = new System.Drawing.Point(88, 354);
            this.traderId.Name = "traderId";
            this.traderId.Size = new System.Drawing.Size(294, 20);
            this.traderId.TabIndex = 10;
            this.traderId.Text = "ANDRECAR";
            // 
            // eSignalOutputPath
            // 
            this.eSignalOutputPath.Location = new System.Drawing.Point(88, 381);
            this.eSignalOutputPath.Name = "eSignalOutputPath";
            this.eSignalOutputPath.Size = new System.Drawing.Size(294, 20);
            this.eSignalOutputPath.TabIndex = 11;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(24, 436);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(58, 13);
            this.label4.TabIndex = 12;
            this.label4.Text = "Share Size";
            // 
            // univShareSize
            // 
            this.univShareSize.Location = new System.Drawing.Point(88, 433);
            this.univShareSize.Name = "univShareSize";
            this.univShareSize.Size = new System.Drawing.Size(294, 20);
            this.univShareSize.TabIndex = 13;
            this.univShareSize.Text = "100";
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.groupBox1.Controls.Add(this.apiSize);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.apiSell);
            this.groupBox1.Controls.Add(this.apiBuy);
            this.groupBox1.Controls.Add(this.apiSymbol);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Location = new System.Drawing.Point(12, 468);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(370, 80);
            this.groupBox1.TabIndex = 14;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Fake API Execution Test";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(14, 22);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(41, 13);
            this.label5.TabIndex = 15;
            this.label5.Text = "Symbol";
            // 
            // apiSymbol
            // 
            this.apiSymbol.Location = new System.Drawing.Point(61, 19);
            this.apiSymbol.Name = "apiSymbol";
            this.apiSymbol.Size = new System.Drawing.Size(111, 20);
            this.apiSymbol.TabIndex = 15;
            // 
            // apiBuy
            // 
            this.apiBuy.Location = new System.Drawing.Point(195, 45);
            this.apiBuy.Name = "apiBuy";
            this.apiBuy.Size = new System.Drawing.Size(75, 23);
            this.apiBuy.TabIndex = 16;
            this.apiBuy.Text = "Market Buy";
            this.apiBuy.UseVisualStyleBackColor = true;
            // 
            // apiSell
            // 
            this.apiSell.Location = new System.Drawing.Point(276, 45);
            this.apiSell.Name = "apiSell";
            this.apiSell.Size = new System.Drawing.Size(75, 23);
            this.apiSell.TabIndex = 17;
            this.apiSell.Text = "Market Sell";
            this.apiSell.UseVisualStyleBackColor = true;
            // 
            // apiSize
            // 
            this.apiSize.Location = new System.Drawing.Point(255, 19);
            this.apiSize.Name = "apiSize";
            this.apiSize.Size = new System.Drawing.Size(82, 20);
            this.apiSize.TabIndex = 19;
            this.apiSize.Text = "100";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(222, 22);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(27, 13);
            this.label6.TabIndex = 18;
            this.label6.Text = "Size";
            // 
            // pproPath
            // 
            this.pproPath.Location = new System.Drawing.Point(88, 407);
            this.pproPath.Name = "pproPath";
            this.pproPath.Size = new System.Drawing.Size(294, 20);
            this.pproPath.TabIndex = 16;
            this.pproPath.Text = "C:\\TMS\\";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(24, 410);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(54, 13);
            this.label7.TabIndex = 15;
            this.label7.Text = "Ppro Path";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(820, 566);
            this.Controls.Add(this.pproPath);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.univShareSize);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.eSignalOutputPath);
            this.Controls.Add(this.traderId);
            this.Controls.Add(this.label42);
            this.Controls.Add(this.label21);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.statusTextBox);
            this.Controls.Add(this.stopButton);
            this.Controls.Add(this.startButton);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.PostionGridView);
            this.Controls.Add(this.OrderGridView);
            this.Name = "Form1";
            this.Text = "Esignal Trader";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.OrderGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PostionGridView)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView OrderGridView;
        private System.Windows.Forms.DataGridView PostionGridView;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Button startButton;
        private System.Windows.Forms.Button stopButton;
        private System.Windows.Forms.RichTextBox statusTextBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Label label42;
        private System.Windows.Forms.TextBox traderId;
        private System.Windows.Forms.TextBox eSignalOutputPath;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox univShareSize;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox apiSize;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button apiSell;
        private System.Windows.Forms.Button apiBuy;
        private System.Windows.Forms.TextBox apiSymbol;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox pproPath;
        private System.Windows.Forms.Label label7;
    }
}

