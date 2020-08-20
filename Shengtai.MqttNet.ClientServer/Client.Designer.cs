namespace Shengtai.MqttNet.ClientServer
{
    partial class frmClient
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmClient));
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnDisConnect = new System.Windows.Forms.Button();
            this.tbxPort = new System.Windows.Forms.TextBox();
            this.btnConnect = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.tbxServer = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnSubscribe = new System.Windows.Forms.Button();
            this.cbxSubMqttQuality = new System.Windows.Forms.ComboBox();
            this.tbxSubscribe = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.cbxPubMqttQuality = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.btnSend = new System.Windows.Forms.Button();
            this.tbxPayload = new System.Windows.Forms.TextBox();
            this.tbxTopic = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.lbxMessage = new System.Windows.Forms.ListBox();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnDisConnect);
            this.panel1.Controls.Add(this.tbxPort);
            this.panel1.Controls.Add(this.btnConnect);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.tbxServer);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(800, 46);
            this.panel1.TabIndex = 0;
            // 
            // btnDisConnect
            // 
            this.btnDisConnect.Location = new System.Drawing.Point(360, 11);
            this.btnDisConnect.Name = "btnDisConnect";
            this.btnDisConnect.Size = new System.Drawing.Size(83, 23);
            this.btnDisConnect.TabIndex = 6;
            this.btnDisConnect.Text = "DisConnect";
            this.btnDisConnect.UseVisualStyleBackColor = true;
            this.btnDisConnect.Click += new System.EventHandler(this.btnDisConnect_Click);
            // 
            // tbxPort
            // 
            this.tbxPort.Location = new System.Drawing.Point(222, 12);
            this.tbxPort.Name = "tbxPort";
            this.tbxPort.Size = new System.Drawing.Size(51, 23);
            this.tbxPort.TabIndex = 4;
            this.tbxPort.Text = "1883";
            // 
            // btnConnect
            // 
            this.btnConnect.Location = new System.Drawing.Point(279, 11);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(75, 23);
            this.btnConnect.TabIndex = 5;
            this.btnConnect.Text = "Connect";
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(42, 15);
            this.label1.TabIndex = 1;
            this.label1.Text = "Server";
            // 
            // tbxServer
            // 
            this.tbxServer.Location = new System.Drawing.Point(64, 12);
            this.tbxServer.Name = "tbxServer";
            this.tbxServer.Size = new System.Drawing.Size(116, 23);
            this.tbxServer.TabIndex = 2;
            this.tbxServer.Text = "127.0.0.1";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(186, 15);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(30, 15);
            this.label2.TabIndex = 3;
            this.label2.Text = "Port";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.btnSubscribe);
            this.panel2.Controls.Add(this.cbxSubMqttQuality);
            this.panel2.Controls.Add(this.tbxSubscribe);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 46);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(800, 50);
            this.panel2.TabIndex = 1;
            // 
            // btnSubscribe
            // 
            this.btnSubscribe.Location = new System.Drawing.Point(713, 11);
            this.btnSubscribe.Name = "btnSubscribe";
            this.btnSubscribe.Size = new System.Drawing.Size(75, 23);
            this.btnSubscribe.TabIndex = 6;
            this.btnSubscribe.Text = "Subscribe";
            this.btnSubscribe.UseVisualStyleBackColor = true;
            this.btnSubscribe.Click += new System.EventHandler(this.btnSubscribe_Click);
            // 
            // cbxSubMqttQuality
            // 
            this.cbxSubMqttQuality.FormattingEnabled = true;
            this.cbxSubMqttQuality.Items.AddRange(new object[] {
            "At most once",
            "At least once",
            "Exactly once"});
            this.cbxSubMqttQuality.Location = new System.Drawing.Point(586, 11);
            this.cbxSubMqttQuality.Name = "cbxSubMqttQuality";
            this.cbxSubMqttQuality.Size = new System.Drawing.Size(121, 23);
            this.cbxSubMqttQuality.TabIndex = 5;
            // 
            // tbxSubscribe
            // 
            this.tbxSubscribe.Location = new System.Drawing.Point(64, 12);
            this.tbxSubscribe.Name = "tbxSubscribe";
            this.tbxSubscribe.Size = new System.Drawing.Size(478, 23);
            this.tbxSubscribe.TabIndex = 3;
            this.tbxSubscribe.Text = "fan/speed";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(548, 14);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(32, 15);
            this.label4.TabIndex = 4;
            this.label4.Text = "QoS";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(16, 15);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(42, 15);
            this.label3.TabIndex = 2;
            this.label3.Text = "TOPIC";
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.cbxPubMqttQuality);
            this.panel3.Controls.Add(this.label7);
            this.panel3.Controls.Add(this.btnSend);
            this.panel3.Controls.Add(this.tbxPayload);
            this.panel3.Controls.Add(this.tbxTopic);
            this.panel3.Controls.Add(this.label6);
            this.panel3.Controls.Add(this.label5);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel3.Location = new System.Drawing.Point(0, 361);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(800, 89);
            this.panel3.TabIndex = 2;
            // 
            // cbxPubMqttQuality
            // 
            this.cbxPubMqttQuality.FormattingEnabled = true;
            this.cbxPubMqttQuality.Items.AddRange(new object[] {
            "At most once",
            "At least once",
            "Exactly once"});
            this.cbxPubMqttQuality.Location = new System.Drawing.Point(667, 12);
            this.cbxPubMqttQuality.Name = "cbxPubMqttQuality";
            this.cbxPubMqttQuality.Size = new System.Drawing.Size(121, 23);
            this.cbxPubMqttQuality.TabIndex = 6;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(629, 15);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(32, 15);
            this.label7.TabIndex = 5;
            this.label7.Text = "QoS";
            // 
            // btnSend
            // 
            this.btnSend.Location = new System.Drawing.Point(713, 52);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(75, 23);
            this.btnSend.TabIndex = 5;
            this.btnSend.Text = "Send";
            this.btnSend.UseVisualStyleBackColor = true;
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
            // 
            // tbxPayload
            // 
            this.tbxPayload.Location = new System.Drawing.Point(80, 53);
            this.tbxPayload.Name = "tbxPayload";
            this.tbxPayload.Size = new System.Drawing.Size(627, 23);
            this.tbxPayload.TabIndex = 4;
            this.tbxPayload.Text = "23";
            // 
            // tbxTopic
            // 
            this.tbxTopic.Location = new System.Drawing.Point(64, 12);
            this.tbxTopic.Name = "tbxTopic";
            this.tbxTopic.Size = new System.Drawing.Size(559, 23);
            this.tbxTopic.TabIndex = 4;
            this.tbxTopic.Text = "fan/temperature";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(16, 15);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(42, 15);
            this.label6.TabIndex = 3;
            this.label6.Text = "TOPIC";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(16, 56);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(58, 15);
            this.label5.TabIndex = 3;
            this.label5.Text = "Message";
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.lbxMessage);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(0, 96);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(800, 265);
            this.panel4.TabIndex = 3;
            // 
            // lbxMessage
            // 
            this.lbxMessage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbxMessage.FormattingEnabled = true;
            this.lbxMessage.ItemHeight = 15;
            this.lbxMessage.Location = new System.Drawing.Point(0, 0);
            this.lbxMessage.Name = "lbxMessage";
            this.lbxMessage.Size = new System.Drawing.Size(800, 265);
            this.lbxMessage.TabIndex = 0;
            // 
            // frmClient
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmClient";
            this.Text = "Client";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmClient_FormClosed);
            this.Load += new System.EventHandler(this.frmClient_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbxServer;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbxPort;
        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.Button btnDisConnect;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbxSubscribe;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnSubscribe;
        private System.Windows.Forms.ComboBox cbxSubMqttQuality;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox tbxPayload;
        private System.Windows.Forms.Button btnSend;
        private System.Windows.Forms.ComboBox cbxPubMqttQuality;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox tbxTopic;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.ListBox lbxMessage;
    }
}