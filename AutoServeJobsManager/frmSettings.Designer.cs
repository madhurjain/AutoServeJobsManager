namespace AutoServeJobsManager
{
    partial class frmSettings
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
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("Pending Message");
            System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("Completed Message");
            System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode("Work In Progress Message");
            System.Windows.Forms.TreeNode treeNode4 = new System.Windows.Forms.TreeNode("SMS", new System.Windows.Forms.TreeNode[] {
            treeNode1,
            treeNode2,
            treeNode3});
            System.Windows.Forms.TreeNode treeNode5 = new System.Windows.Forms.TreeNode("MySQL");
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSettings));
            this.treeViewConfig = new System.Windows.Forms.TreeView();
            this.pnlMySQL = new System.Windows.Forms.Panel();
            this.grpBoxMySQL = new System.Windows.Forms.GroupBox();
            this.btnTestMySQLConnection = new System.Windows.Forms.Button();
            this.lblDatabase = new System.Windows.Forms.Label();
            this.lblPassword = new System.Windows.Forms.Label();
            this.lblUsername = new System.Windows.Forms.Label();
            this.lblHost = new System.Windows.Forms.Label();
            this.lblMySQL = new System.Windows.Forms.Label();
            this.pnlSMS = new System.Windows.Forms.Panel();
            this.grpBoxeBayGeneral = new System.Windows.Forms.GroupBox();
            this.btnQueryDevice = new System.Windows.Forms.Button();
            this.lblDevicePort = new System.Windows.Forms.Label();
            this.lblTexting = new System.Windows.Forms.Label();
            this.grpBoxApply = new System.Windows.Forms.GroupBox();
            this.btnApply = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.pnlSMSMessage = new System.Windows.Forms.Panel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rtbCompletedMessage = new System.Windows.Forms.RichTextBox();
            this.rtbWIPMessage = new System.Windows.Forms.RichTextBox();
            this.lblMessage = new System.Windows.Forms.Label();
            this.rtbPendingMessage = new System.Windows.Forms.RichTextBox();
            this.btnInsert = new System.Windows.Forms.Button();
            this.cBoxDynamicText = new System.Windows.Forms.ComboBox();
            this.lblDynamicText = new System.Windows.Forms.Label();
            this.lblPendingMessage = new System.Windows.Forms.Label();
            this.cBoxDevicePort = new System.Windows.Forms.ComboBox();
            this.txtDatabase = new System.Windows.Forms.TextBox();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.txtUsername = new System.Windows.Forms.TextBox();
            this.txtHost = new System.Windows.Forms.TextBox();
            this.pnlMySQL.SuspendLayout();
            this.grpBoxMySQL.SuspendLayout();
            this.pnlSMS.SuspendLayout();
            this.grpBoxeBayGeneral.SuspendLayout();
            this.grpBoxApply.SuspendLayout();
            this.pnlSMSMessage.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // treeViewConfig
            // 
            this.treeViewConfig.Location = new System.Drawing.Point(12, 12);
            this.treeViewConfig.Name = "treeViewConfig";
            treeNode1.Name = "PendingNode";
            treeNode1.Text = "Pending Message";
            treeNode2.Name = "CompletedNode";
            treeNode2.Text = "Completed Message";
            treeNode3.Name = "WorkInProgressNode";
            treeNode3.Text = "Work In Progress Message";
            treeNode4.Name = "SMSNode";
            treeNode4.Text = "SMS";
            treeNode5.Name = "MySQLNode";
            treeNode5.Text = "MySQL";
            this.treeViewConfig.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode4,
            treeNode5});
            this.treeViewConfig.Size = new System.Drawing.Size(120, 288);
            this.treeViewConfig.TabIndex = 1;
            this.treeViewConfig.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeViewConfig_AfterSelect);
            // 
            // pnlMySQL
            // 
            this.pnlMySQL.Controls.Add(this.grpBoxMySQL);
            this.pnlMySQL.Controls.Add(this.lblMySQL);
            this.pnlMySQL.Location = new System.Drawing.Point(139, 14);
            this.pnlMySQL.Name = "pnlMySQL";
            this.pnlMySQL.Size = new System.Drawing.Size(408, 288);
            this.pnlMySQL.TabIndex = 5;
            this.pnlMySQL.Visible = false;
            // 
            // grpBoxMySQL
            // 
            this.grpBoxMySQL.Controls.Add(this.btnTestMySQLConnection);
            this.grpBoxMySQL.Controls.Add(this.txtDatabase);
            this.grpBoxMySQL.Controls.Add(this.lblDatabase);
            this.grpBoxMySQL.Controls.Add(this.lblPassword);
            this.grpBoxMySQL.Controls.Add(this.txtPassword);
            this.grpBoxMySQL.Controls.Add(this.txtUsername);
            this.grpBoxMySQL.Controls.Add(this.lblUsername);
            this.grpBoxMySQL.Controls.Add(this.lblHost);
            this.grpBoxMySQL.Controls.Add(this.txtHost);
            this.grpBoxMySQL.Location = new System.Drawing.Point(17, 32);
            this.grpBoxMySQL.Name = "grpBoxMySQL";
            this.grpBoxMySQL.Size = new System.Drawing.Size(376, 189);
            this.grpBoxMySQL.TabIndex = 3;
            this.grpBoxMySQL.TabStop = false;
            // 
            // btnTestMySQLConnection
            // 
            this.btnTestMySQLConnection.Location = new System.Drawing.Point(80, 139);
            this.btnTestMySQLConnection.Name = "btnTestMySQLConnection";
            this.btnTestMySQLConnection.Size = new System.Drawing.Size(140, 23);
            this.btnTestMySQLConnection.TabIndex = 4;
            this.btnTestMySQLConnection.Text = "&Test Connection";
            this.btnTestMySQLConnection.UseVisualStyleBackColor = true;
            this.btnTestMySQLConnection.Click += new System.EventHandler(this.btnTestMySQLConnection_Click);
            // 
            // lblDatabase
            // 
            this.lblDatabase.AutoSize = true;
            this.lblDatabase.Location = new System.Drawing.Point(15, 103);
            this.lblDatabase.Name = "lblDatabase";
            this.lblDatabase.Size = new System.Drawing.Size(53, 13);
            this.lblDatabase.TabIndex = 7;
            this.lblDatabase.Text = "Database";
            // 
            // lblPassword
            // 
            this.lblPassword.AutoSize = true;
            this.lblPassword.Location = new System.Drawing.Point(15, 77);
            this.lblPassword.Name = "lblPassword";
            this.lblPassword.Size = new System.Drawing.Size(53, 13);
            this.lblPassword.TabIndex = 6;
            this.lblPassword.Text = "Password";
            // 
            // lblUsername
            // 
            this.lblUsername.AutoSize = true;
            this.lblUsername.Location = new System.Drawing.Point(15, 51);
            this.lblUsername.Name = "lblUsername";
            this.lblUsername.Size = new System.Drawing.Size(55, 13);
            this.lblUsername.TabIndex = 3;
            this.lblUsername.Text = "Username";
            // 
            // lblHost
            // 
            this.lblHost.AutoSize = true;
            this.lblHost.Location = new System.Drawing.Point(15, 25);
            this.lblHost.Name = "lblHost";
            this.lblHost.Size = new System.Drawing.Size(29, 13);
            this.lblHost.TabIndex = 1;
            this.lblHost.Text = "Host";
            // 
            // lblMySQL
            // 
            this.lblMySQL.AutoSize = true;
            this.lblMySQL.BackColor = System.Drawing.Color.CornflowerBlue;
            this.lblMySQL.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMySQL.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.lblMySQL.Location = new System.Drawing.Point(17, 11);
            this.lblMySQL.MinimumSize = new System.Drawing.Size(376, 0);
            this.lblMySQL.Name = "lblMySQL";
            this.lblMySQL.Size = new System.Drawing.Size(376, 16);
            this.lblMySQL.TabIndex = 0;
            this.lblMySQL.Text = "MySQL";
            this.lblMySQL.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pnlSMS
            // 
            this.pnlSMS.Controls.Add(this.grpBoxeBayGeneral);
            this.pnlSMS.Controls.Add(this.lblTexting);
            this.pnlSMS.Location = new System.Drawing.Point(139, 14);
            this.pnlSMS.Name = "pnlSMS";
            this.pnlSMS.Size = new System.Drawing.Size(405, 288);
            this.pnlSMS.TabIndex = 5;
            // 
            // grpBoxeBayGeneral
            // 
            this.grpBoxeBayGeneral.Controls.Add(this.btnQueryDevice);
            this.grpBoxeBayGeneral.Controls.Add(this.cBoxDevicePort);
            this.grpBoxeBayGeneral.Controls.Add(this.lblDevicePort);
            this.grpBoxeBayGeneral.Location = new System.Drawing.Point(17, 31);
            this.grpBoxeBayGeneral.Name = "grpBoxeBayGeneral";
            this.grpBoxeBayGeneral.Size = new System.Drawing.Size(376, 242);
            this.grpBoxeBayGeneral.TabIndex = 3;
            this.grpBoxeBayGeneral.TabStop = false;
            // 
            // btnQueryDevice
            // 
            this.btnQueryDevice.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnQueryDevice.Location = new System.Drawing.Point(226, 18);
            this.btnQueryDevice.Name = "btnQueryDevice";
            this.btnQueryDevice.Size = new System.Drawing.Size(91, 24);
            this.btnQueryDevice.TabIndex = 11;
            this.btnQueryDevice.Text = "&Query Device";
            this.btnQueryDevice.UseVisualStyleBackColor = true;
            this.btnQueryDevice.Click += new System.EventHandler(this.btnQueryDevice_Click);
            // 
            // lblDevicePort
            // 
            this.lblDevicePort.AutoSize = true;
            this.lblDevicePort.Location = new System.Drawing.Point(11, 21);
            this.lblDevicePort.Name = "lblDevicePort";
            this.lblDevicePort.Size = new System.Drawing.Size(112, 13);
            this.lblDevicePort.TabIndex = 3;
            this.lblDevicePort.Text = "Device Connected To";
            // 
            // lblTexting
            // 
            this.lblTexting.AutoSize = true;
            this.lblTexting.BackColor = System.Drawing.Color.CornflowerBlue;
            this.lblTexting.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTexting.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.lblTexting.Location = new System.Drawing.Point(14, 12);
            this.lblTexting.MinimumSize = new System.Drawing.Size(376, 0);
            this.lblTexting.Name = "lblTexting";
            this.lblTexting.Size = new System.Drawing.Size(376, 16);
            this.lblTexting.TabIndex = 0;
            this.lblTexting.Text = "SMS - General";
            this.lblTexting.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // grpBoxApply
            // 
            this.grpBoxApply.Controls.Add(this.btnApply);
            this.grpBoxApply.Controls.Add(this.btnCancel);
            this.grpBoxApply.Controls.Add(this.btnOK);
            this.grpBoxApply.Location = new System.Drawing.Point(155, 307);
            this.grpBoxApply.Name = "grpBoxApply";
            this.grpBoxApply.Size = new System.Drawing.Size(376, 48);
            this.grpBoxApply.TabIndex = 6;
            this.grpBoxApply.TabStop = false;
            // 
            // btnApply
            // 
            this.btnApply.Enabled = false;
            this.btnApply.Location = new System.Drawing.Point(245, 15);
            this.btnApply.Name = "btnApply";
            this.btnApply.Size = new System.Drawing.Size(114, 23);
            this.btnApply.TabIndex = 2;
            this.btnApply.Text = "&Apply";
            this.btnApply.UseVisualStyleBackColor = true;
            this.btnApply.Click += new System.EventHandler(this.btnApply_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(113, 15);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(99, 23);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(18, 15);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(49, 23);
            this.btnOK.TabIndex = 0;
            this.btnOK.Text = "&Ok";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // pnlSMSMessage
            // 
            this.pnlSMSMessage.Controls.Add(this.groupBox1);
            this.pnlSMSMessage.Controls.Add(this.lblPendingMessage);
            this.pnlSMSMessage.Location = new System.Drawing.Point(139, 14);
            this.pnlSMSMessage.Name = "pnlSMSMessage";
            this.pnlSMSMessage.Size = new System.Drawing.Size(405, 288);
            this.pnlSMSMessage.TabIndex = 6;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rtbCompletedMessage);
            this.groupBox1.Controls.Add(this.rtbWIPMessage);
            this.groupBox1.Controls.Add(this.lblMessage);
            this.groupBox1.Controls.Add(this.rtbPendingMessage);
            this.groupBox1.Controls.Add(this.btnInsert);
            this.groupBox1.Controls.Add(this.cBoxDynamicText);
            this.groupBox1.Controls.Add(this.lblDynamicText);
            this.groupBox1.Location = new System.Drawing.Point(17, 31);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(376, 242);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            // 
            // rtbCompletedMessage
            // 
            this.rtbCompletedMessage.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::AutoServeJobsManager.Properties.Settings.Default, "completedMessage", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.rtbCompletedMessage.Location = new System.Drawing.Point(99, 79);
            this.rtbCompletedMessage.Name = "rtbCompletedMessage";
            this.rtbCompletedMessage.Size = new System.Drawing.Size(200, 96);
            this.rtbCompletedMessage.TabIndex = 15;
            this.rtbCompletedMessage.Text = global::AutoServeJobsManager.Properties.Settings.Default.completedMessage;
            this.rtbCompletedMessage.Visible = false;
            // 
            // rtbWIPMessage
            // 
            this.rtbWIPMessage.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::AutoServeJobsManager.Properties.Settings.Default, "workInProgressMessage", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.rtbWIPMessage.Location = new System.Drawing.Point(99, 79);
            this.rtbWIPMessage.Name = "rtbWIPMessage";
            this.rtbWIPMessage.Size = new System.Drawing.Size(200, 96);
            this.rtbWIPMessage.TabIndex = 14;
            this.rtbWIPMessage.Text = global::AutoServeJobsManager.Properties.Settings.Default.workInProgressMessage;
            this.rtbWIPMessage.Visible = false;
            // 
            // lblMessage
            // 
            this.lblMessage.AutoSize = true;
            this.lblMessage.Location = new System.Drawing.Point(31, 82);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new System.Drawing.Size(50, 13);
            this.lblMessage.TabIndex = 13;
            this.lblMessage.Text = "Message";
            // 
            // rtbPendingMessage
            // 
            this.rtbPendingMessage.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::AutoServeJobsManager.Properties.Settings.Default, "pendingMessage", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.rtbPendingMessage.Location = new System.Drawing.Point(99, 79);
            this.rtbPendingMessage.Name = "rtbPendingMessage";
            this.rtbPendingMessage.Size = new System.Drawing.Size(200, 96);
            this.rtbPendingMessage.TabIndex = 12;
            this.rtbPendingMessage.Text = global::AutoServeJobsManager.Properties.Settings.Default.pendingMessage;
            // 
            // btnInsert
            // 
            this.btnInsert.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnInsert.Location = new System.Drawing.Point(244, 26);
            this.btnInsert.Name = "btnInsert";
            this.btnInsert.Size = new System.Drawing.Size(55, 24);
            this.btnInsert.TabIndex = 11;
            this.btnInsert.Text = "&Insert";
            this.btnInsert.UseVisualStyleBackColor = true;
            this.btnInsert.Click += new System.EventHandler(this.btnInsert_Click);
            // 
            // cBoxDynamicText
            // 
            this.cBoxDynamicText.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cBoxDynamicText.FormattingEnabled = true;
            this.cBoxDynamicText.Items.AddRange(new object[] {
            "Service Tag",
            "Customer Name",
            "Contact Person",
            "Printer Model",
            "Printer Serial",
            "Problem",
            "Work In Progress",
            "Status",
            "Remarks"});
            this.cBoxDynamicText.Location = new System.Drawing.Point(147, 29);
            this.cBoxDynamicText.Name = "cBoxDynamicText";
            this.cBoxDynamicText.Size = new System.Drawing.Size(91, 21);
            this.cBoxDynamicText.TabIndex = 10;
            // 
            // lblDynamicText
            // 
            this.lblDynamicText.AutoSize = true;
            this.lblDynamicText.Location = new System.Drawing.Point(31, 32);
            this.lblDynamicText.Name = "lblDynamicText";
            this.lblDynamicText.Size = new System.Drawing.Size(101, 13);
            this.lblDynamicText.TabIndex = 3;
            this.lblDynamicText.Text = "Insert Dynamic Text";
            // 
            // lblPendingMessage
            // 
            this.lblPendingMessage.AutoSize = true;
            this.lblPendingMessage.BackColor = System.Drawing.Color.CornflowerBlue;
            this.lblPendingMessage.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPendingMessage.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.lblPendingMessage.Location = new System.Drawing.Point(14, 12);
            this.lblPendingMessage.MinimumSize = new System.Drawing.Size(376, 0);
            this.lblPendingMessage.Name = "lblPendingMessage";
            this.lblPendingMessage.Size = new System.Drawing.Size(376, 16);
            this.lblPendingMessage.TabIndex = 0;
            this.lblPendingMessage.Text = "SMS";
            this.lblPendingMessage.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cBoxDevicePort
            // 
            this.cBoxDevicePort.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::AutoServeJobsManager.Properties.Settings.Default, "portName", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.cBoxDevicePort.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cBoxDevicePort.FormattingEnabled = true;
            this.cBoxDevicePort.Items.AddRange(new object[] {
            "None"});
            this.cBoxDevicePort.Location = new System.Drawing.Point(129, 18);
            this.cBoxDevicePort.Name = "cBoxDevicePort";
            this.cBoxDevicePort.Size = new System.Drawing.Size(91, 21);
            this.cBoxDevicePort.TabIndex = 10;
            this.cBoxDevicePort.Text = global::AutoServeJobsManager.Properties.Settings.Default.portName;
            this.cBoxDevicePort.SelectedIndexChanged += new System.EventHandler(this.cBoxDevicePort_SelectedIndexChanged);
            // 
            // txtDatabase
            // 
            this.txtDatabase.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::AutoServeJobsManager.Properties.Settings.Default, "MySQLDatabase", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.txtDatabase.Location = new System.Drawing.Point(80, 100);
            this.txtDatabase.Name = "txtDatabase";
            this.txtDatabase.Size = new System.Drawing.Size(140, 20);
            this.txtDatabase.TabIndex = 3;
            this.txtDatabase.Text = global::AutoServeJobsManager.Properties.Settings.Default.MySQLDatabase;
            // 
            // txtPassword
            // 
            this.txtPassword.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::AutoServeJobsManager.Properties.Settings.Default, "MySQLPassword", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.txtPassword.Location = new System.Drawing.Point(80, 74);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.Size = new System.Drawing.Size(140, 20);
            this.txtPassword.TabIndex = 2;
            this.txtPassword.Text = global::AutoServeJobsManager.Properties.Settings.Default.MySQLPassword;
            // 
            // txtUsername
            // 
            this.txtUsername.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::AutoServeJobsManager.Properties.Settings.Default, "MySQLUsername", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.txtUsername.Location = new System.Drawing.Point(80, 48);
            this.txtUsername.Name = "txtUsername";
            this.txtUsername.Size = new System.Drawing.Size(140, 20);
            this.txtUsername.TabIndex = 1;
            this.txtUsername.Text = global::AutoServeJobsManager.Properties.Settings.Default.MySQLUsername;
            // 
            // txtHost
            // 
            this.txtHost.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::AutoServeJobsManager.Properties.Settings.Default, "MySQLHost", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.txtHost.Location = new System.Drawing.Point(80, 22);
            this.txtHost.Name = "txtHost";
            this.txtHost.Size = new System.Drawing.Size(140, 20);
            this.txtHost.TabIndex = 0;
            this.txtHost.Text = global::AutoServeJobsManager.Properties.Settings.Default.MySQLHost;
            // 
            // frmSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(554, 369);
            this.Controls.Add(this.pnlSMSMessage);
            this.Controls.Add(this.grpBoxApply);
            this.Controls.Add(this.treeViewConfig);
            this.Controls.Add(this.pnlSMS);
            this.Controls.Add(this.pnlMySQL);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmSettings";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Settings - AutoServe Jobs Manager";
            this.Load += new System.EventHandler(this.frmSettings_Load);
            this.pnlMySQL.ResumeLayout(false);
            this.pnlMySQL.PerformLayout();
            this.grpBoxMySQL.ResumeLayout(false);
            this.grpBoxMySQL.PerformLayout();
            this.pnlSMS.ResumeLayout(false);
            this.pnlSMS.PerformLayout();
            this.grpBoxeBayGeneral.ResumeLayout(false);
            this.grpBoxeBayGeneral.PerformLayout();
            this.grpBoxApply.ResumeLayout(false);
            this.pnlSMSMessage.ResumeLayout(false);
            this.pnlSMSMessage.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TreeView treeViewConfig;
        private System.Windows.Forms.Panel pnlMySQL;
        private System.Windows.Forms.GroupBox grpBoxMySQL;
        private System.Windows.Forms.Button btnTestMySQLConnection;
        private System.Windows.Forms.TextBox txtDatabase;
        private System.Windows.Forms.Label lblDatabase;
        private System.Windows.Forms.Label lblPassword;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.TextBox txtUsername;
        private System.Windows.Forms.Label lblUsername;
        private System.Windows.Forms.Label lblHost;
        private System.Windows.Forms.TextBox txtHost;
        private System.Windows.Forms.Label lblMySQL;
        private System.Windows.Forms.GroupBox grpBoxApply;
        private System.Windows.Forms.Button btnApply;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Panel pnlSMS;
        private System.Windows.Forms.GroupBox grpBoxeBayGeneral;
        private System.Windows.Forms.Label lblDevicePort;
        private System.Windows.Forms.Label lblTexting;
        private System.Windows.Forms.ComboBox cBoxDevicePort;
        private System.Windows.Forms.Button btnQueryDevice;
        private System.Windows.Forms.Panel pnlSMSMessage;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnInsert;
        private System.Windows.Forms.ComboBox cBoxDynamicText;
        private System.Windows.Forms.Label lblDynamicText;
        private System.Windows.Forms.Label lblPendingMessage;
        private System.Windows.Forms.RichTextBox rtbPendingMessage;
        private System.Windows.Forms.Label lblMessage;
        private System.Windows.Forms.RichTextBox rtbCompletedMessage;
        private System.Windows.Forms.RichTextBox rtbWIPMessage;
    }
}