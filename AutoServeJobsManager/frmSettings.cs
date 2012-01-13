using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AutoServeJobsManager.Properties;

namespace AutoServeJobsManager
{
    public partial class frmSettings : Form
    {
        public EventHandler txtChanged;
        public bool btxtChanged = false;

        public frmSettings()
        {
            InitializeComponent();
            txtChanged = new EventHandler(this.Text_Changed);
            addEventHandlers_TextBoxes(this);
            cBoxDevicePort.Items.AddRange(Texting.GetPorts());
            if(Settings.Default.portName.Equals("None"))
            {
                btnQueryDevice.Enabled = false;
            }
        }

        private void addEventHandlers_TextBoxes(Control c)
        {
            if (c is Panel || c is GroupBox || c is Form)
            {
                foreach (Control x in c.Controls)
                    addEventHandlers_TextBoxes(x);
            }
            else if (c is TextBox || c is ComboBox || c is RichTextBox)
            {
                c.TextChanged += txtChanged;
            }
        }

        private void Text_Changed(object sender, EventArgs e)
        {
            btxtChanged = true;
            btnApply.Enabled = true;
        }

        void treeViewConfig_AfterSelect(object sender, System.Windows.Forms.TreeViewEventArgs e)
        {
            switch (e.Node.Name)
            {
                case "SMSNode":
                    pnlSMSMessage.Visible = false;
                    pnlMySQL.Visible = false;
                    pnlSMS.Visible = true;
                    break;
                case "PendingNode":
                    pnlMySQL.Visible = false;
                    pnlSMS.Visible = false;
                    pnlSMSMessage.Visible = true;
                    rtbPendingMessage.Visible = true;
                    rtbCompletedMessage.Visible = false;
                    rtbWIPMessage.Visible = false;
                    break;
                case "CompletedNode":
                    pnlMySQL.Visible = false;
                    pnlSMS.Visible = false;
                    pnlSMSMessage.Visible = true;
                    rtbPendingMessage.Visible = false;
                    rtbCompletedMessage.Visible = true;
                    rtbWIPMessage.Visible = false;
                    break;
                case "WorkInProgressNode":
                    pnlMySQL.Visible = false;
                    pnlSMS.Visible = false;
                    pnlSMSMessage.Visible = true;
                    rtbPendingMessage.Visible = false;
                    rtbCompletedMessage.Visible = false;
                    rtbWIPMessage.Visible = true;
                    break;
                case "MySQLNode":
                    pnlSMS.Visible = false;
                    pnlSMSMessage.Visible = false;
                    pnlMySQL.Visible = true;
                    break;
                default:
                    hideAllPanels();
                    break;
            }
        }

        private void hideAllPanels()
        {
            foreach (Control c in this.Controls)
                if (c is Panel)
                    c.Visible = false;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (btxtChanged)
                Settings.Default.Save();
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Settings.Default.Reload();
            this.Close();
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            Settings.Default.Save();
            btnApply.Enabled = false;
        }

        private void btnTestMySQLConnection_Click(object sender, EventArgs e)
        {
            dbWork.testConnection(txtHost.Text, txtUsername.Text,
                                        txtPassword.Text, txtDatabase.Text);
        }

        private void btnQueryDevice_Click(object sender, EventArgs e)
        {
            Texting.QueryDevice(cBoxDevicePort.SelectedItem.ToString());
        }

        private void frmSettings_Load(object sender, EventArgs e)
        {
            
        }

        private void cBoxDevicePort_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cBoxDevicePort.SelectedItem.ToString().Equals("None"))
            {
                btnQueryDevice.Enabled = false;
               // rtbOnPendingMsg.Enabled = false;
               // rtbOnCompleteMsg.Enabled = false;
            }
            else
            {
                btnQueryDevice.Enabled = true;
               // rtbOnPendingMsg.Enabled = true;
               // rtbOnCompleteMsg.Enabled = true;
            }

        }

        private void btnInsert_Click(object sender, EventArgs e)
        {
            string dynamicText = cBoxDynamicText.SelectedItem.ToString().Replace(" ","");
            dynamicText = "<" + dynamicText + ">";
            if(rtbCompletedMessage.Visible)
                rtbCompletedMessage.AppendText(dynamicText);
            else if(rtbPendingMessage.Visible)
                rtbPendingMessage.AppendText(dynamicText);
            else if(rtbWIPMessage.Visible)
                rtbWIPMessage.AppendText(dynamicText);
        }

        

    }
}
