namespace AutoServeJobsManager
{
    partial class frmCustomers
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
            this.lstCustomers = new System.Windows.Forms.ListBox();
            this.pnlEditCustomers = new System.Windows.Forms.Panel();
            this.gBoxButtons = new System.Windows.Forms.GroupBox();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.gBoxEditCustomer = new System.Windows.Forms.GroupBox();
            this.lblCustomerId = new System.Windows.Forms.Label();
            this.txtId = new System.Windows.Forms.TextBox();
            this.txtMobileNo = new System.Windows.Forms.TextBox();
            this.txtPhone = new System.Windows.Forms.TextBox();
            this.txtAddress = new System.Windows.Forms.TextBox();
            this.txtContactPerson = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.pnlEditCustomers.SuspendLayout();
            this.gBoxButtons.SuspendLayout();
            this.gBoxEditCustomer.SuspendLayout();
            this.SuspendLayout();
            // 
            // lstCustomers
            // 
            this.lstCustomers.FormattingEnabled = true;
            this.lstCustomers.Location = new System.Drawing.Point(11, 15);
            this.lstCustomers.Name = "lstCustomers";
            this.lstCustomers.Size = new System.Drawing.Size(135, 251);
            this.lstCustomers.TabIndex = 0;
            // 
            // pnlEditCustomers
            // 
            this.pnlEditCustomers.BackColor = System.Drawing.SystemColors.Control;
            this.pnlEditCustomers.Controls.Add(this.gBoxButtons);
            this.pnlEditCustomers.Controls.Add(this.gBoxEditCustomer);
            this.pnlEditCustomers.Controls.Add(this.lstCustomers);
            this.pnlEditCustomers.Location = new System.Drawing.Point(7, 12);
            this.pnlEditCustomers.Name = "pnlEditCustomers";
            this.pnlEditCustomers.Size = new System.Drawing.Size(515, 338);
            this.pnlEditCustomers.TabIndex = 8;
            // 
            // gBoxButtons
            // 
            this.gBoxButtons.Controls.Add(this.btnUpdate);
            this.gBoxButtons.Controls.Add(this.btnDelete);
            this.gBoxButtons.Location = new System.Drawing.Point(163, 269);
            this.gBoxButtons.Name = "gBoxButtons";
            this.gBoxButtons.Size = new System.Drawing.Size(333, 53);
            this.gBoxButtons.TabIndex = 20;
            this.gBoxButtons.TabStop = false;
            // 
            // btnUpdate
            // 
            this.btnUpdate.Location = new System.Drawing.Point(109, 19);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(60, 23);
            this.btnUpdate.TabIndex = 6;
            this.btnUpdate.Text = "&Update";
            this.btnUpdate.UseVisualStyleBackColor = true;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(202, 19);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(60, 23);
            this.btnDelete.TabIndex = 7;
            this.btnDelete.Text = "&Delete";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // gBoxEditCustomer
            // 
            this.gBoxEditCustomer.Controls.Add(this.lblCustomerId);
            this.gBoxEditCustomer.Controls.Add(this.txtId);
            this.gBoxEditCustomer.Controls.Add(this.txtMobileNo);
            this.gBoxEditCustomer.Controls.Add(this.txtPhone);
            this.gBoxEditCustomer.Controls.Add(this.txtAddress);
            this.gBoxEditCustomer.Controls.Add(this.txtContactPerson);
            this.gBoxEditCustomer.Controls.Add(this.label5);
            this.gBoxEditCustomer.Controls.Add(this.label4);
            this.gBoxEditCustomer.Controls.Add(this.txtName);
            this.gBoxEditCustomer.Controls.Add(this.label3);
            this.gBoxEditCustomer.Controls.Add(this.label2);
            this.gBoxEditCustomer.Controls.Add(this.label1);
            this.gBoxEditCustomer.Location = new System.Drawing.Point(163, 10);
            this.gBoxEditCustomer.Name = "gBoxEditCustomer";
            this.gBoxEditCustomer.Size = new System.Drawing.Size(333, 256);
            this.gBoxEditCustomer.TabIndex = 19;
            this.gBoxEditCustomer.TabStop = false;
            // 
            // lblCustomerId
            // 
            this.lblCustomerId.AutoSize = true;
            this.lblCustomerId.Location = new System.Drawing.Point(15, 23);
            this.lblCustomerId.Name = "lblCustomerId";
            this.lblCustomerId.Size = new System.Drawing.Size(63, 13);
            this.lblCustomerId.TabIndex = 18;
            this.lblCustomerId.Text = "Customer Id";
            // 
            // txtId
            // 
            this.txtId.Location = new System.Drawing.Point(109, 20);
            this.txtId.Name = "txtId";
            this.txtId.ReadOnly = true;
            this.txtId.Size = new System.Drawing.Size(68, 20);
            this.txtId.TabIndex = 10;
            // 
            // txtMobileNo
            // 
            this.txtMobileNo.Location = new System.Drawing.Point(109, 208);
            this.txtMobileNo.Name = "txtMobileNo";
            this.txtMobileNo.Size = new System.Drawing.Size(211, 20);
            this.txtMobileNo.TabIndex = 5;
            // 
            // txtPhone
            // 
            this.txtPhone.Location = new System.Drawing.Point(109, 181);
            this.txtPhone.Name = "txtPhone";
            this.txtPhone.Size = new System.Drawing.Size(211, 20);
            this.txtPhone.TabIndex = 4;
            // 
            // txtAddress
            // 
            this.txtAddress.Location = new System.Drawing.Point(109, 98);
            this.txtAddress.Multiline = true;
            this.txtAddress.Name = "txtAddress";
            this.txtAddress.Size = new System.Drawing.Size(211, 77);
            this.txtAddress.TabIndex = 3;
            // 
            // txtContactPerson
            // 
            this.txtContactPerson.Location = new System.Drawing.Point(109, 72);
            this.txtContactPerson.Name = "txtContactPerson";
            this.txtContactPerson.Size = new System.Drawing.Size(211, 20);
            this.txtContactPerson.TabIndex = 2;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(15, 208);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(55, 13);
            this.label5.TabIndex = 6;
            this.label5.Text = "Mobile No";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(15, 184);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(38, 13);
            this.label4.TabIndex = 5;
            this.label4.Text = "Phone";
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(109, 46);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(211, 20);
            this.txtName.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(15, 98);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(45, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "Address";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(15, 75);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(80, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Contact Person";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 49);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Name";
            // 
            // frmCustomers
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(532, 362);
            this.Controls.Add(this.pnlEditCustomers);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmCustomers";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Edit Customers - Autoserve";
            this.Load += new System.EventHandler(this.frmCustomers_Load);
            this.pnlEditCustomers.ResumeLayout(false);
            this.gBoxButtons.ResumeLayout(false);
            this.gBoxEditCustomer.ResumeLayout(false);
            this.gBoxEditCustomer.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox lstCustomers;
        private System.Windows.Forms.Panel pnlEditCustomers;
        private System.Windows.Forms.TextBox txtMobileNo;
        private System.Windows.Forms.TextBox txtPhone;
        private System.Windows.Forms.TextBox txtAddress;
        private System.Windows.Forms.TextBox txtContactPerson;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.TextBox txtId;
        private System.Windows.Forms.Label lblCustomerId;
        private System.Windows.Forms.GroupBox gBoxButtons;
        private System.Windows.Forms.GroupBox gBoxEditCustomer;
    }
}