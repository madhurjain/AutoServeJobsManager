using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Text;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using AutoServeJobsManager.Properties;
using System.Drawing;
using System.Drawing.Printing;

namespace AutoServeJobsManager
{
    public partial class mainForm : Form
    {
        private bool flagNewRecord;
        private dbWork s = new dbWork();
        public mainForm()
        {
            InitializeComponent();
            this.lvJobs.ListViewItemSorter = lvwColumnSorter;
        }

        void mainForm_Load(object sender, System.EventArgs e)
        {
            pictureBox_Logo.Location = new Point(50, 10);
            pictureBox_LogoBack.Controls.Add(pictureBox_Logo);
            if (s.InitConnection())
            {
                cmbStatusFilter.SelectedIndex = 0;
                PopulateCustomerNames();
                dtpFilterTo.Value = DateTime.Today.AddDays(7);

                lvJobs.Items.AddRange(s.PopulateListView("Pending","",""));
                lvwColumnSorter.SortColumn = 7;
                lvwColumnSorter.Order = SortOrder.Ascending;

                this.lvJobs.Sort();
                printToolStripButton.Enabled = false;
                disableForm();
                if (Settings.Default.portName.Contains("COM"))
                    initMessageSending();
            }
            else
            {
                MessageBox.Show("Error Setting up Connection with the Database\nPlease check settings.","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
                disableForSettings();
            }
            
        }

        private void mainForm_KeyUp(object sender, KeyEventArgs e)
        {            
            if (e.Modifiers == Keys.Control && Convert.ToChar(e.KeyValue) == 'S')
            {
                if (saveToolStripButton.Enabled)
                    saveToolStripButton_Click(null, null);
            }
            else if (e.Modifiers == Keys.Control && Convert.ToChar(e.KeyValue) == 'N')
            {
                newToolStripButton_Click(null, null);
            }
        }

        void mainForm_FormClosing(object sender, System.Windows.Forms.FormClosingEventArgs e)
        {
            s.DestroyConnection();
            Console.WriteLine("Destroying Connection");
        }

        internal void PopulateCustomerNames()
        {
            txtCustName.AutoCompleteCustomSource = s.PopulateCustomerNames();
        }

        private void newToolStripButton_Click(object sender, EventArgs e)
        {           
            //Generate new ServiceTag
            enableForNew();
            txtCustName.Focus();
            flagNewRecord = true;
        }


        private void GetInvoice(String ServiceTag)
        {
            // GetInvoice as instance of Invoice Class from dbWork
            // assign each field to corresponding TextBox/ComboBox/DateTimePicker            
            try
            {
                Invoice invoice = s.GetInvoice(ServiceTag);
                enableForUpdate();
                txtSearchServiceTag.Text = "Search";
                txtServiceTag.Text = invoice.ServiceTag;
                dtpArrivalDate.Value = invoice.ArrivalDate;

                txtCustName.Text = invoice.CustomerName;
                txtContactPerson.Text = invoice.ContactPerson;
                txtAddress.Text = invoice.Address;
                txtPhoneNo.Text = invoice.Phone;
                txtMobileNo.Text = invoice.MobileNo;

                txtPrinterModel.Text = invoice.PrinterModel;
                txtProblem.Text = invoice.Problem;
                cmbStatus.SelectedItem = invoice.Status;

                if (invoice.DeliveryDate != new DateTime(1, 1, 1))
                {
                    if (invoice.DeliveryDate.Date == invoice.ArrivalDate.Date)
                    {
                        //Show time
                        dtpDeliverydate.CustomFormat = "HH:mm";
                        dtpDeliverydate.ShowUpDown = true;
                        lblDeliveryDateTime.Text = "Delivery Time";
                        cmbDayHour.SelectedIndex = 1;
                    }
                    else
                    {
                        dtpDeliverydate.CustomFormat = "dd/MM/yyyy";
                        lblDeliveryDateTime.Text = "Delivery Date";
                        dtpDeliverydate.ShowUpDown = false;
                        cmbDayHour.SelectedIndex = 0;
                    }

                    dtpDeliverydate.Value = invoice.DeliveryDate;
                }

                txtWorkInProgress.Text = invoice.WorkInProgress;
                txtSpareParts.Text = invoice.SpareParts;
                txtRemarks.Text = invoice.Remarks;

            }
            catch (ServiceTagNotFoundException ex)
            {
                Console.WriteLine("Service Tag (" + ex.ServiceTag + ") Not Found In the Records!");
            }
            catch (Exception ae)
            {
                MessageBox.Show(ae.Message);
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            flagNewRecord = false;
            //search record
            string searchString = txtSearchServiceTag.Text.Trim();
            if (searchString.Length == 0 || searchString == "Search")
            {
                ShowMessage("Please specify a search string");
                return;
            }
            bool found  = false;
            bool isInt = false;
            int searchInt;
            isInt = int.TryParse(searchString, out searchInt);

            if (isInt)
            {
                if(s.ServiceTagExists(searchString))
                {
                    found = true;
                    GetInvoice(searchString);
                }
            }
            else {
                int id = s.CustomerNameExists(searchString);
                if (id > 0)
                {
                    found = true;
                    lvJobs.Items.Clear();
                    lvJobs.Items.AddRange(s.GetCustomerListView(id.ToString()));
                }
            }

            if (!found)
            { 
                if(s.SerialExists(searchString))
                {
                    found = true;
                    lvJobs.Items.Clear();
                    lvJobs.Items.AddRange(s.GetSerialListView(searchString));
                }
            }

            // GetInvoice(txtSearchServiceTag.Text);

            if (!found)
                ShowMessage("No matching records found!");
        }      

        private void saveToolStripButton_Click(object sender, EventArgs e)
        {
            string customerName = txtCustName.Text.Trim();
            if (customerName.Length == 0)
            {
                MessageBox.Show("Please Enter Customer Name", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (flagNewRecord)
            {
                // Add new job record
                int custID = s.GetCustomerId(customerName);
                if (custID == -1)   // Add customer
                {
                    custID = s.CreateCustomer(customerName, txtContactPerson.Text,
                        txtAddress.Text, txtPhoneNo.Text, txtMobileNo.Text);
                    PopulateCustomerNames();

                }

                DateTime dueDate = new DateTime();
                if (cmbDayHour.SelectedIndex == 0)
                {
                    dueDate = dtpDeliverydate.Value;
                }
                else if (cmbDayHour.SelectedIndex == 1)
                {
                    dueDate = dtpArrivalDate.Value;
                    dueDate = dueDate.AddHours(dtpDeliverydate.Value.Hour).AddMinutes(dtpDeliverydate.Value.Minute);
                }

                int serviceTag = s.AddJob(custID.ToString(), dtpArrivalDate.Value.ToString("yyyy/MM/dd"),
                            txtPrinterModel.Text, txtPrinterSerial.Text, txtProblem.Text, cmbStatus.SelectedItem.ToString(),
                            dueDate.ToString("yyyy/MM/dd HH:mm"), txtWorkInProgress.Text, txtSpareParts.Text, txtRemarks.Text);

                txtServiceTag.Text = serviceTag.ToString();
                ShowMessage("Job Successfully Added with Service Tag : " + serviceTag);
            }
            else
            {
                // Update existing job record
                s.UpdateJob(txtServiceTag.Text, txtProblem.Text, txtWorkInProgress.Text, dtpDeliverydate.Value.ToString("yyyy/MM/dd HH:mm"),
                    cmbStatus.SelectedItem.ToString(), txtSpareParts.Text, txtRemarks.Text);
                ShowMessage("Job Successfully Updated");
            }
            lvJobs.Items.Clear();
            lvJobs.Items.AddRange(s.PopulateListView());
            // Send SMS
            string jobStatus = cmbStatus.SelectedItem.ToString();
            bool messageSend = false;
            if (jobStatus == "Pending" && Settings.Default.pendingMessage != "")
            {
                messageSend = true;
            }
            else if (jobStatus == "Completed" && Settings.Default.completedMessage != "")
            {
                messageSend = true;
            }
            else if (jobStatus == "Work In Progress" && Settings.Default.workInProgressMessage != "")
            {
                messageSend = true;
            }
            if (messageSend)
            {
                if (txtMobileNo.Text.Length < 10)
                {
                    ShowMessage("Cannot send SMS, as Mobile Number is invalid");
                }
                else
                {
                    s.AddSMSToQueue(txtServiceTag.Text,txtCustName.Text,txtContactPerson.Text,
                        txtPrinterModel.Text,txtPrinterSerial.Text,txtProblem.Text,
                        txtWorkInProgress.Text,txtRemarks.Text,txtMobileNo.Text, jobStatus);
                }
            }
            printToolStripButton.Enabled = true;
            disableForm();
        }

        


        private void btnFilter_Click(object sender, EventArgs e)
        {
            lvJobs.Items.Clear();
            lvJobs.Items.AddRange(s.PopulateListView(cmbStatusFilter.SelectedItem.ToString(), dtpFilterFrom.Value.ToString("yyyy-MM-dd"),dtpFilterTo.Value.ToString("yyyy-MM-dd")));
        }

        Timer TmrMessageDelay = new Timer();
        short TmrCount;
        private void ShowMessage(String Message)
        {
            //displays Status Label for a while with Message
            lblStatusMessage.Text = Message;
            lblStatusMessage.Visible = true;

            TmrCount = 0;
            TmrMessageDelay.Interval = 1000;
            TmrMessageDelay.Tick += new EventHandler(TmrMessageDelay_Tick);
            TmrMessageDelay.Enabled = true;
        }

        void TmrMessageDelay_Tick(object sender, EventArgs e)
        {
            if (TmrCount >= 5)
            {
                TmrMessageDelay.Enabled = false;
                lblStatusMessage.Visible = false;
            }
            TmrCount++;
        }

    

        private void settingstoolStripButton_Click(object sender, EventArgs e)
        {
            frmSettings settings = new frmSettings();
            settings.ShowDialog();
        }

        private void txtCustName_TextChanged(object sender, EventArgs e)
        {
            CustomerDetailsEntry(txtCustName.Text);
        }

        private void CustomerDetailsEntry(string customerName) {
            int cId = s.GetCustomerId(customerName);
            if (-1 != cId)
            {
                string[] customerDetails = s.GetCustomerDetails(cId);
                txtContactPerson.ReadOnly = true;
                txtContactPerson.Text = customerDetails[2];
                txtAddress.ReadOnly = true;
                txtAddress.Text = customerDetails[3];
                txtPhoneNo.ReadOnly = true;
                txtPhoneNo.Text = customerDetails[4];
                txtMobileNo.ReadOnly = true;
                txtMobileNo.Text = customerDetails[5];
                // txtPrinterModel.Focus();
            }
            else
            {
                txtContactPerson.ReadOnly = false;
                txtContactPerson.Text = "";
                txtAddress.ReadOnly = false;
                txtAddress.Text = "";
                txtPhoneNo.ReadOnly = false;
                txtPhoneNo.Text = "";
                txtMobileNo.ReadOnly = false;
                txtMobileNo.Text = "";
            }
        }
        private void btnEditCustomers_Click(object sender, EventArgs e)
        {
            frmCustomers cstForm = new frmCustomers();
            cstForm.ShowDialog();
        }


        private void chkSelectAll_CheckedChanged(object sender, EventArgs e)
        {
            if (chkSelectAll.Checked)
            {
                foreach (ListViewItem item in lvJobs.Items)
                {
                    item.Checked = true;
                }
            }
            else
            {
                foreach (ListViewItem item in lvJobs.Items)
                {
                    item.Checked = false;
                }
            }
        }


        private void AbouttoolStripButton_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Developed By Madhur & Omkarnath\nContact : 9890575158/9970441814\n;-)", "About", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        #region "Print Invoice"
        private void printToolStripButton_Click(object sender, EventArgs e)
        {
            if (txtServiceTag.Text == "")
            {
                MessageBox.Show("Service Tag Empty", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else if (!s.ServiceTagExists(txtServiceTag.Text))
            {
                MessageBox.Show("Service Tag does not exists in records\nPlease save and try again", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            PrintDocument pd = new PrintDocument();
            PrintDialog pDialog = new PrintDialog();

            if (pDialog.ShowDialog() == DialogResult.OK)
            {
                //Declare a BGW and start printing
                BackgroundWorker bgwPrintInvoice = new BackgroundWorker();
                bgwPrintInvoice.DoWork += new DoWorkEventHandler(bgwPrintInvoice_DoWork);
                bgwPrintInvoice.ProgressChanged += new ProgressChangedEventHandler(bgwPrintInvoice_ProgressChanged);
                bgwPrintInvoice.WorkerReportsProgress = true;
                pd.PrinterSettings = pDialog.PrinterSettings;
                bgwPrintInvoice.RunWorkerAsync(pd);
            }
        }

        void bgwPrintInvoice_DoWork(object sender, DoWorkEventArgs e)
        {
            //Print Invoice for Currently selected record
            try
            {
                PrintDocument pd = (PrintDocument)e.Argument;
                pd.PrintPage += new PrintPageEventHandler(PrintPage);
                pd.Print();
                ((BackgroundWorker)sender).ReportProgress(100, "Print Completed");
            }
            catch (Exception ae)
            {
                ((BackgroundWorker)sender).ReportProgress(0, "Error");
                MessageBox.Show(ae.Message,"Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }

        }

        private void PrintPage(object sender, PrintPageEventArgs e)
        {
            Invoice invoice = s.GetInvoice(txtServiceTag.Text);

            Image img = Image.FromFile("Printer.jpg");
            e.Graphics.DrawImage(img, new Point(0, 0));

            //DrawString stuff goes here
            Font font = new Font("Verdana", 10);
            Pen pen = new Pen(System.Drawing.Color.Black);

            float x = 100;
            float y = 300;
            float spacing = 20;

            e.Graphics.DrawString("Service Tag : " + invoice.ServiceTag, font, pen.Brush, x, y);

            if (invoice.Status == "Pending")
            {
                e.Graphics.DrawString("Date : " + invoice.ArrivalDate.ToString("dd/MM/yyyy"), font, pen.Brush, 560, y);
            }
            else
            {
                if (invoice.DeliveryDate.Hour == 00)
                    e.Graphics.DrawString("Date : " + invoice.DeliveryDate.ToString("dd/MM/yyyy"), font, pen.Brush, 560, y);
                else
                    e.Graphics.DrawString("Date : " + invoice.DeliveryDate.ToString("dd/MM/yyyy HH:mm"), font, pen.Brush, 560, y);
            }

            y += spacing;

            e.Graphics.DrawString("M/s : " + invoice.CustomerName, font, pen.Brush, x, y);
            y += spacing;

            e.Graphics.DrawString("Contact Person : " + invoice.ContactPerson, font, pen.Brush, x, y);
            y += spacing;

            if (invoice.Address.Length > 75)
            {
                e.Graphics.DrawString("Address : " + invoice.Address.Substring(0,75), font, pen.Brush, x, y);
                y += spacing;
                e.Graphics.DrawString("          " + invoice.Address.Substring(76, invoice.Address.Length-1), font, pen.Brush, x, y);
            }
            else
            {
                e.Graphics.DrawString("Address : " + invoice.Address, font, pen.Brush, x, y);
            }
            y += spacing;
            

            e.Graphics.DrawString("Phone : " + invoice.Phone, font, pen.Brush, x, y);
            e.Graphics.DrawString("Mobile : " + invoice.MobileNo, font, pen.Brush, x + 200, y);
            y += spacing * 3;

            e.Graphics.DrawString("Printer : " + invoice.PrinterModel, font, pen.Brush, x, y);
            y += spacing;


            int tLength = invoice.Problem.Length;
            float tSpace = y;
            if (tLength > 75)
            {
                StringBuilder sbProblem = new StringBuilder("Problem : " + invoice.Problem.Substring(0, 74) + "\n");
                for (int i = 75; i < tLength; i=i+75)
                {
                    if (tLength - i > 75)
                    {
                        sbProblem.Append(invoice.Problem.Substring(i, 75) + "\n");
                        tSpace += spacing;
                    }
                    else
                    {
                        sbProblem.Append(invoice.Problem.Substring(i));
                        tSpace += spacing;
                        break;
                    }
                    
                }
                e.Graphics.DrawString(sbProblem.ToString(), font, pen.Brush, x, y);
                y = tSpace;
            }
            else
            {
                e.Graphics.DrawString("Problem : " + invoice.Problem, font, pen.Brush, x, y);
            }
            y += spacing;
            

            e.Graphics.DrawString("Status : " + invoice.Status, font, pen.Brush, x, y);
            y += spacing;


            
            if (invoice.Remarks != String.Empty)
            {
                tLength = invoice.Remarks.Length;
                tSpace = y;
                if (tLength > 75)
                {
                    StringBuilder sbRemarks = new StringBuilder("Remarks : " + invoice.Remarks.Substring(0, 74) + "\n");
                    for (int i = 75; i < tLength; i = i + 75)
                    {
                        if (tLength - i > 75)
                        {
                            sbRemarks.Append(invoice.Remarks.Substring(i, 75) + "\n");
                            tSpace += spacing;
                        }
                        else
                        {
                            sbRemarks.Append(invoice.Remarks.Substring(i));
                            tSpace += spacing;
                            break;
                        }

                    }
                    e.Graphics.DrawString(sbRemarks.ToString(), font, pen.Brush, x, y);
                    y = tSpace;
                }
                else
                {
                    e.Graphics.DrawString("Remarks : " + invoice.Remarks, font, pen.Brush, x, y);
                }
                y += spacing;
            }
        }

        void bgwPrintInvoice_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            ShowMessage(e.UserState.ToString());
            if (e.ProgressPercentage == 100)
                printToolStripButton.Enabled = false;

        }

        void pd_EndPrint(object sender, PrintEventArgs e)
        {
            ShowMessage("Print Completed");
        }
        #endregion

        #region "Print List of Records"

        private void btnPrint_Click(object sender, EventArgs e)
        {
            if (lvJobs.CheckedItems.Count == 0)
            {
                ShowMessage("No Record selected for Printing");
                return;
            }

            PrintDocument pd = new PrintDocument();
            PrintDialog pDialog = new PrintDialog();

            if (pDialog.ShowDialog() == DialogResult.OK)
            {
                //Declare a BGW and start printing List
                BackgroundWorker bgwPrintList = new BackgroundWorker();
                bgwPrintList.DoWork += new DoWorkEventHandler(bgwPrintList_DoWork);
                bgwPrintList.ProgressChanged += new ProgressChangedEventHandler(bgwPrintList_ProgressChanged);
                bgwPrintList.WorkerReportsProgress = true;
                pd.PrinterSettings = pDialog.PrinterSettings;
                bgwPrintList.RunWorkerAsync(pd);
            }

        }

        void bgwPrintList_DoWork(object sender, DoWorkEventArgs e)
        {
            PrintDocument pd = (PrintDocument)e.Argument;
            //Print list of selected records            
            try
            {
                Control.CheckForIllegalCrossThreadCalls = false;
                pd.PrintPage += new PrintPageEventHandler(PrintList);
                pd.Print();
                ((BackgroundWorker)sender).ReportProgress(100, "Print Completed");
                Control.CheckForIllegalCrossThreadCalls = true;
            }
            catch (Exception ae)
            {
                ((BackgroundWorker)sender).ReportProgress(0, ae.Message);
            }
        }


        private int listStart = 0;
        private void PrintList(object sender, PrintPageEventArgs e)
        {
            //DrawString stuff goes here
            Font font = new Font("Verdana", 10);
            Pen pen = new Pen(System.Drawing.Color.Black);

            float x = 20;
            float y = 20;
            float spacing = 20;
            String header = cmbStatusFilter.SelectedItem.ToString() + " Jobs for the period of " + dtpFilterFrom.Value.ToString("dd-MM-yyyy") + " To " + dtpFilterTo.Value.ToString("dd-MM-yyyy");

            e.Graphics.DrawString(header, font, pen.Brush, x, y);
            y += spacing;

            e.Graphics.DrawString("Name\t\t\tPrinter Model\t\tProblem", font, pen.Brush, x, y);
            y += spacing;

            //foreach (ListViewItem item in lvJobs.CheckedItems)
            for (int listIndex = listStart; listIndex < lvJobs.CheckedItems.Count; listIndex++)
            {
                StringBuilder sb = new StringBuilder("\n");
                sb.Append(lvJobs.CheckedItems[listIndex].SubItems[0].Text); //Customer name
                sb.Append("\t\t" + lvJobs.CheckedItems[listIndex].SubItems[2].Text); //Printer model
                sb.Append("\t\t" + lvJobs.CheckedItems[listIndex].SubItems[3].Text); //problem

                e.Graphics.DrawString(sb.ToString(), font, pen.Brush, x, y);
                y += spacing;
                if (listIndex < (lvJobs.CheckedItems.Count - 1) && y >= e.MarginBounds.Bottom)
                {
                    listStart = listIndex + 1;
                    e.HasMorePages = true;
                    break;
                }
                else
                {
                    e.HasMorePages = false;
                }
            }

        }

        private void bgwPrintList_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            ShowMessage(e.UserState.ToString());
        }
        #endregion

        #region "List View Operations"
        private void lvJobs_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Get Invoice Details for selected ListView Item
            if (lvJobs.SelectedItems.Count > 0)
                GetInvoice(lvJobs.SelectedItems[0].Text);
            flagNewRecord = false;
        }

        private ListViewColumnSorter lvwColumnSorter = new ListViewColumnSorter();
        private void lvJobs_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            // Determine if clicked column is already the column that is being sorted.
            if (e.Column == lvwColumnSorter.SortColumn)
            {
                // Reverse the current sort direction for this column.
                if (lvwColumnSorter.Order == SortOrder.Ascending)
                {
                    lvwColumnSorter.Order = SortOrder.Descending;
                }
                else
                {
                    lvwColumnSorter.Order = SortOrder.Ascending;
                }
            }
            else
            {
                // Set the column number that is to be sorted; default to ascending.
                lvwColumnSorter.SortColumn = e.Column;
                lvwColumnSorter.Order = SortOrder.Ascending;
            }

            // Perform the sort with these new sort options.
            this.lvJobs.Sort();
        }
        #endregion

        #region "Enable_Disable_Form"
        private void disableForm()
        {
            txtServiceTag.Enabled = false;
            dtpArrivalDate.Enabled = false;
            txtCustName.Enabled = false;
            txtContactPerson.Enabled = false;
            txtAddress.Enabled = false;
            txtPhoneNo.Enabled = false;
            txtMobileNo.Enabled = false;
            txtPrinterModel.Enabled = false;
            txtPrinterSerial.Enabled = false;
            txtProblem.Enabled = false;
            txtWorkInProgress.Enabled = false;
            dtpDeliverydate.Enabled = false;
            cmbDayHour.Enabled = false;
            cmbStatus.Enabled = false;
            txtSpareParts.Enabled = false;
            txtRemarks.Enabled = false;
            saveToolStripButton.Enabled = false;
            /*
            txtServiceTag.Text = "";
            dtpArrivalDate.Value = DateTime.Today;
            txtCustName.Text = "";
            txtContactPerson.Text = "";
            txtAddress.Text = "";
            txtPhoneNo.Text = "";
            txtMobileNo.Text = "";
            txtPrinterModel.Text = "";
            txtProblem.Text = "";
            txtWorkInProgress.Text = "";
            dtpDeliverydate.Value = DateTime.Today;
            cmbStatus.SelectedIndex = 0;
            txtSpareParts.Text = "";
            txtRemarks.Text = "";
             */
        }

        private void disableForSettings()
        {
            txtServiceTag.Enabled = false;
            dtpArrivalDate.Enabled = false;
            txtCustName.Enabled = false;
            txtContactPerson.Enabled = false;
            txtAddress.Enabled = false;
            txtPhoneNo.Enabled = false;
            txtMobileNo.Enabled = false;
            txtPrinterModel.Enabled = false;
            txtPrinterSerial.Enabled = false;
            txtProblem.Enabled = false;
            txtWorkInProgress.Enabled = false;
            dtpDeliverydate.Enabled = false;
            cmbDayHour.Enabled = false;
            cmbStatus.Enabled = false;
            txtSpareParts.Enabled = false;
            txtRemarks.Enabled = false;
            saveToolStripButton.Enabled = false;
            newToolStripButton.Enabled = false;
            printToolStripButton.Enabled = false;
            btnEditCustomers.Enabled = false;
            btnFilter.Enabled = false;
            dtpFilterFrom.Enabled = false;
            dtpFilterTo.Enabled = false;
            txtSearchServiceTag.Enabled = false;
            btnSearch.Enabled = false;
            cmbStatusFilter.Enabled = false;
            btnPrint.Enabled = false;
            chkSelectAll.Enabled = false;
        }

        private void enableForNew()
        {
            txtServiceTag.Text = "";
            txtServiceTag.Enabled = true;
            dtpArrivalDate.Value = DateTime.Today;
            dtpArrivalDate.Enabled = true;
            txtCustName.Text = "";
            txtCustName.Enabled = true;
            txtContactPerson.Text = "";
            txtContactPerson.Enabled = true;
            txtAddress.Text = "";
            txtAddress.Enabled = true;
            txtPhoneNo.Text = "";
            txtPhoneNo.Enabled = true;
            txtMobileNo.Text = "";
            txtMobileNo.Enabled = true;
            txtPrinterModel.Text = "";
            txtPrinterModel.Enabled = true;
            txtPrinterSerial.Text = "";
            txtPrinterSerial.Enabled = true;
            txtProblem.Text = "";
            txtProblem.Enabled = true;
            txtWorkInProgress.Text = "";
            txtWorkInProgress.Enabled = true;
            dtpDeliverydate.Value = DateTime.Today.AddDays(1);
            dtpDeliverydate.Enabled = true;
            cmbDayHour.Enabled = true;
            cmbDayHour.SelectedIndex = 0;
            cmbStatus.SelectedIndex = 0;
            cmbStatus.Enabled = true;
            txtSpareParts.Text = "";
            txtSpareParts.Enabled = true;
            txtRemarks.Text = "";
            txtRemarks.Enabled = true;
            saveToolStripButton.Enabled = true;
        }

        private void enableForUpdate()
        {
            txtServiceTag.Enabled = true;
            dtpArrivalDate.Value = DateTime.Today;
            dtpArrivalDate.Enabled = false;
            txtCustName.Enabled = false;
            txtContactPerson.Enabled = false;
            txtAddress.Enabled = false;
            txtPhoneNo.Enabled = false;
            txtMobileNo.Enabled = false;
            txtPrinterModel.Enabled = false;
            txtPrinterSerial.Enabled = false;
            txtProblem.Enabled = true;
            txtWorkInProgress.Enabled = true;
            dtpDeliverydate.Enabled = true;
            cmbDayHour.Enabled = true;
            cmbStatus.Enabled = true;
            txtSpareParts.Enabled = true;
            txtRemarks.Enabled = true;
            saveToolStripButton.Enabled = true;
        }
        #endregion

        #region "Search Tag"
        void txtSearchServiceTag_GotFocus(object sender, System.EventArgs e)
        {
            if(txtSearchServiceTag.Text == "Search")
                txtSearchServiceTag.Text = "";
        }

        void txtSearchServiceTag_LostFocus(object sender, System.EventArgs e)
        {
            if (txtSearchServiceTag.Text == "")
                txtSearchServiceTag.Text = "Search";
        }

        void txtSearchServiceTag_KeyUp(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode.ToString() == "Return")
                btnSearch_Click(null, null);
        }
        #endregion

        #region "SMS"
        Timer timerSMSPoll;
        private void initMessageSending()
        {
            timerSMSPoll = new Timer();
            timerSMSPoll.Interval = 60000;
            timerSMSPoll.Tick += new EventHandler(timerSMSPoll_Tick);
            timerSMSPoll.Start();
        }

        void timerSMSPoll_Tick(object sender, EventArgs e)
        {
            List<MessagePayload> listPayload = s.GetMessageQueue();
            if (listPayload.Count > 0)
            {
                System.Threading.Thread thread = new System.Threading.Thread(new System.Threading.ParameterizedThreadStart(sendMessage));
                thread.Start(listPayload);
                // sendMessage();
            }
        }

        private void sendMessage(object oListPayload)
        {
            Texting t = new Texting();
            List<MessagePayload> listPayload = (List < MessagePayload >)oListPayload;
            t.openPort();
            foreach (MessagePayload payload in listPayload)
            {
                t.sendMessage(payload.number, payload.message);
            }
            t.closePort();
        }
        #endregion

        private void cmbDayHour_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbDayHour.SelectedIndex == 0)
            {
                dtpDeliverydate.CustomFormat = "dd/MM/yyyy";
                lblDeliveryDateTime.Text = "Delivery Date";
                dtpDeliverydate.ShowUpDown = false;
            }
            else if (cmbDayHour.SelectedIndex == 1)
            {
                dtpDeliverydate.CustomFormat = "HH:mm";
                lblDeliveryDateTime.Text = "Delivery Time";
                dtpDeliverydate.ShowUpDown = true;
            }
        }

        private void dtpArrivalDate_ValueChanged(object sender, EventArgs e)
        {
            dtpDeliverydate.MinDate = dtpArrivalDate.Value;
        }

        


    }
}
