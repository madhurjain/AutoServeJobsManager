using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;
using System.Data;
using System.Windows.Forms;
using AutoServeJobsManager.Properties;

namespace AutoServeJobsManager
{
    class dbWork
    {
        private static MySqlConnection conn;
        private static MySqlCommand cmd;
        private static MySqlCommand selCmd;
        private static MySqlCommand sfCmd;
        private string lvCommandText = string.Empty;

        internal bool InitConnection()
        {

            string connectionString = "datasource=" + AutoServeJobsManager.Properties.Settings.Default.MySQLHost +
                                      ";username=" + AutoServeJobsManager.Properties.Settings.Default.MySQLUsername +
                                      ";password=" + AutoServeJobsManager.Properties.Settings.Default.MySQLPassword +
                                      ";database=" + AutoServeJobsManager.Properties.Settings.Default.MySQLDatabase;
            try
            {
                if (conn == null || conn.State == ConnectionState.Closed)
                {
                    conn = new MySqlConnection(connectionString);
                    cmd = new MySqlCommand();
                    cmd.Connection = conn;
                    selCmd = new MySqlCommand();
                    selCmd.Connection = conn;
                    sfCmd = new MySqlCommand();
                    sfCmd.Connection = conn;
                    sfCmd.CommandType = CommandType.StoredProcedure;
                    conn.Open();
                }
                return true;
            }
            catch (Exception ex){
                Console.WriteLine(ex.Message);
                return false;
            }
        }        

        internal void DestroyConnection()
        {
            if (conn != null && conn.State == ConnectionState.Open) {
                conn.Close();
                conn = null;
                cmd = null;
                selCmd = null;
                sfCmd = null;
            }
        }

        internal static void testConnection(string host, string username, string password, string database)
        {
            try
            {
                MySqlConnection tConnection = new MySqlConnection("datasource=" + host + ";username=" + username +
                                                              ";password=" + password + ";database=" + database);
                tConnection.Open();
                MessageBox.Show("Connection Test Succeeded!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                tConnection.Close();
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        internal AutoCompleteStringCollection PopulateCustomerNames()
        {
            AutoCompleteStringCollection CustomerNames = new AutoCompleteStringCollection();
            selCmd.CommandText = "SELECT CustomerName FROM Customer;";
            MySqlDataReader reader = selCmd.ExecuteReader();
            int i = 0;
            while (reader.Read())
            {
                CustomerNames.Add(reader.GetString(0));
                i++;
            }
            reader.Close();
            return CustomerNames;
        }

        internal bool ServiceTagExists(string ServiceTag)
        {
            selCmd.CommandText = "SELECT ServiceTag FROM Jobs WHERE ServiceTag='" + ServiceTag + "';";
            if (selCmd.ExecuteScalar() == null)
                return false;
            else
                return true;
        }

        internal Invoice GetInvoice(String ServiceTag)
        {
            selCmd.CommandText = "SELECT * FROM Jobs,Customer WHERE ServiceTag='" + ServiceTag + "' AND Jobs.CustomerId = Customer.CustomerId;";
            MySqlDataReader reader = selCmd.ExecuteReader();
            if (reader.HasRows)
            {
                Invoice invoice = new Invoice(ServiceTag);
                reader.Read();
                                
                invoice.ArrivalDate = new DateTime(reader.GetMySqlDateTime("arrivaldate").Year, reader.GetMySqlDateTime("arrivaldate").Month, reader.GetMySqlDateTime("arrivaldate").Day);
                invoice.PrinterModel = reader.GetString("printermodel");
                invoice.Problem = reader.GetString("problem");
                invoice.Status = reader.GetString("status");
                
                //possible Null returns
                invoice.DeliveryDate = GetData<DateTime>(reader, "deliverydate");                 
                invoice.WorkInProgress = GetData<String>(reader,"workinprogress");
                invoice.SpareParts = GetData<String>(reader,"spareparts");
                invoice.Remarks = GetData<String>(reader,"remarks");
                
                
                //Customer Details
                invoice.CustomerName = reader.GetString("customername");
                invoice.ContactPerson = GetData<String>(reader,"contactperson");
                invoice.Address = reader.GetString("address");
                invoice.Phone = GetData<String>(reader,"phone");
                invoice.MobileNo = reader.GetString("mobileno");

                reader.Close();
                return invoice;
            }//end if reader has rows
            else
            {
                if(!reader.IsClosed)
                    reader.Close();
                throw new ServiceTagNotFoundException(ServiceTag);
            }
        }

        public static T GetData<T>(IDataReader reader, string column)
        {
            //method to handle Null values
            // String columnType = reader.GetDataTypeName(reader.GetOrdinal(column));

            if (reader.IsDBNull(reader.GetOrdinal(column)))
            {
                  return default(T);
            }                
            return (T)reader[reader.GetOrdinal(column)];
        }        

        internal int GetCustomerId(String customerName)
        {
            selCmd.CommandText = "SELECT customerid FROM customer WHERE customername='" + customerName.Trim() + "'";
            Object id = selCmd.ExecuteScalar();
            if (id != null)
                return int.Parse(id.ToString());
            else
                return -1;
        }

        internal int CustomerNameExists(string customerName)
        {
            selCmd.CommandText = "SELECT COUNT(customerid),customerId FROM customer WHERE customername LIKE '%" + customerName + "%';";
            string idCount = selCmd.ExecuteScalar().ToString();
            if (idCount == "0")
            {
                return -1;
            }
            else if (idCount == "1")
            {
                selCmd.CommandText = "SELECT customerid FROM customer WHERE customername LIKE '%" + customerName + "%';";
                string id = selCmd.ExecuteScalar().ToString();
                return int.Parse(id);
            }
            else
            {
                return -2;
            }
        }

        internal string[] GetCustomerDetails(int customerId)
        {
            string[] customerDetails = {"","","","","",""};
            selCmd.CommandText = "SELECT * FROM Customer WHERE CustomerId = '" + customerId + "'";
            MySqlDataReader reader = selCmd.ExecuteReader();
            if(reader.HasRows)
            {
                reader.Read();
                for (int i = 0; i < customerDetails.Length; i++)
                {
                    customerDetails[i] = reader.GetString(i);
                }
                reader.Close();
            }
            return customerDetails;
        }

        internal int AddJob(string customerID, string arrivalDate, 
            string printerModel, string printerSerial,string problem,string status,string deliveryDate,string workInProgress,string spareParts, string remarks)
        {
            sfCmd.CommandText = "sf_AddJob";
            sfCmd.Parameters.AddWithValue("?CustomerID", customerID);
            sfCmd.Parameters.AddWithValue("?ArrivalDate", arrivalDate);
            sfCmd.Parameters.AddWithValue("?PrinterModel", printerModel);
            sfCmd.Parameters.AddWithValue("?PrinterSerial", printerSerial);
            sfCmd.Parameters.AddWithValue("?Problem", problem);
            sfCmd.Parameters.AddWithValue("?Status", status);
            sfCmd.Parameters.AddWithValue("?DeliveryDate", deliveryDate);
            sfCmd.Parameters.AddWithValue("?WorkInProgress", workInProgress);
            sfCmd.Parameters.AddWithValue("?SpareParts",spareParts);
            sfCmd.Parameters.AddWithValue("?Remarks", remarks);
            MySqlParameter retVal = new MySqlParameter();
            retVal.ParameterName = "?RetVal";
            retVal.Direction = ParameterDirection.ReturnValue;
            sfCmd.Parameters.Add(retVal);
            sfCmd.ExecuteNonQuery();
            int serviceTag = int.Parse(retVal.Value.ToString());
            sfCmd.Parameters.Clear();
            return serviceTag;
            
        }

        internal void UpdateJob(string serviceTag, string problem, string workInProgress,
            string deliveryDate,string status,string spareParts,string remarks)
        {
            cmd.CommandText = "UPDATE Jobs SET Problem=?Problem,WorkInProgress=?WorkInProgress,DeliveryDate=?DeliveryDate,Status=?Status,SpareParts=?SpareParts,Remarks=?Remarks WHERE ServiceTag=?ServiceTag;";
            cmd.Parameters.AddWithValue("?ServiceTag", serviceTag);
            cmd.Parameters.AddWithValue("?Problem", problem);
            cmd.Parameters.AddWithValue("?WorkInProgress",workInProgress);
            cmd.Parameters.AddWithValue("?DeliveryDate", deliveryDate);
            cmd.Parameters.AddWithValue("?Status", status);
            cmd.Parameters.AddWithValue("?SpareParts", spareParts);
            cmd.Parameters.AddWithValue("?Remarks", remarks);
            cmd.Prepare();
            Console.WriteLine(cmd.ExecuteNonQuery());
            cmd.Parameters.Clear();
        }

        internal int CreateCustomer(string Name, string contactPerson,
                                    string Address, string Phone, string MobileNo)
        {
            //MySqlCommand sfCmd = new MySqlCommand();
            //sfCmd.Connection = conn;
            //sfCmd.CommandType = CommandType.StoredProcedure;
            sfCmd.CommandText = "sf_CreateCustomer";
            sfCmd.Parameters.AddWithValue("?CustName", Name);
            sfCmd.Parameters.AddWithValue("?ContactPerson", contactPerson);
            sfCmd.Parameters.AddWithValue("?Address", Address);
            sfCmd.Parameters.AddWithValue("?Phone", Phone);
            sfCmd.Parameters.AddWithValue("?MobileNo", MobileNo);
            MySqlParameter cID = new MySqlParameter();
            cID.ParameterName = "?RetVal";
            cID.Direction = ParameterDirection.ReturnValue;
            sfCmd.Parameters.Add(cID);
            sfCmd.ExecuteNonQuery();
            int custID = int.Parse(cID.Value.ToString());
            sfCmd.Parameters.Clear();
            return custID;
        }
        internal void UpdateCustomer(string customerID, string Name, string contactPerson,
                                    string Address, string Phone, string MobileNo)
        {
            
            cmd.Connection = conn;
            cmd.CommandText = "UPDATE Customer SET CustomerName=?CustomerName,ContactPerson=?ContactPerson,Address=?Address,Phone=?Phone,MobileNo=?MobileNo WHERE CustomerId=?CustomerId;";
            cmd.Parameters.AddWithValue("?CustomerId", customerID);
            cmd.Parameters.AddWithValue("?CustomerName", Name);
            cmd.Parameters.AddWithValue("?ContactPerson", contactPerson);
            cmd.Parameters.AddWithValue("?Address", Address);
            cmd.Parameters.AddWithValue("?Phone", Phone);
            cmd.Parameters.AddWithValue("?MobileNo", MobileNo);
            cmd.Prepare();
            Console.WriteLine(cmd.ExecuteNonQuery());
            cmd.Parameters.Clear();
        }

        internal void DeleteCustomer(string customerID)
        {
            cmd.CommandText = "DELETE FROM Customer WHERE CustomerId=" + customerID;
            Console.WriteLine(cmd.ExecuteNonQuery());
        }

        internal bool SerialExists(string Serial)
        {
            selCmd.CommandText = "SELECT PrinterSerial FROM Jobs WHERE PrinterSerial='" + Serial + "';";
            if (selCmd.ExecuteScalar() == null)
                return false;
            else
                return true;
        }


        internal ListViewItem[] GetSerialListView(string serial)
        {
            string cmdText = string.Empty;
            cmdText = "SELECT ServiceTag,CustomerName,PrinterModel,PrinterSerial,Problem,Status,ArrivalDate,DeliveryDate,Remarks FROM Jobs,Customer WHERE Jobs.CustomerId = Customer.customerId AND PrinterSerial = '" + serial + "';";
            List<ListViewItem> items = new List<ListViewItem>();
            selCmd.CommandText = cmdText;
            MySqlDataReader dr = selCmd.ExecuteReader();
            ListViewItem tempItem;
            DateTime aDate = new DateTime();
            DateTime dDate = new DateTime();
            while (dr.Read())
            {
                tempItem = new ListViewItem(dr.GetString(0));
                for (int i = 1; i < dr.VisibleFieldCount; i++)
                {
                    if (dr.IsDBNull(i))
                    {
                        tempItem.SubItems.Add(String.Empty);
                    }
                    else
                    {
                        if (6 == i)
                        {
                            aDate = dr.GetDateTime(i);
                            tempItem.SubItems.Add(aDate.ToString("dd/MM/yyyy"));
                        }
                        else if (7 == i)
                        {
                            dDate = dr.GetDateTime(i);
                            if (aDate.Date == dDate.Date)
                                tempItem.SubItems.Add(dr.GetDateTime(i).ToString("dd/MM/yyyy HH:mm"));
                            else
                                tempItem.SubItems.Add(dr.GetDateTime(i).ToString("dd/MM/yyyy"));
                        }
                        else
                        {
                            tempItem.SubItems.Add(dr.GetString(i));
                        }
                    }
                }
                items.Add(tempItem);
            }
            dr.Close();
            return items.ToArray();
        }

        internal ListViewItem[] GetCustomerListView(string customerId)
        {
            string cmdText = string.Empty;
            cmdText = "SELECT ServiceTag,CustomerName,PrinterModel,PrinterSerial,Problem,Status,ArrivalDate,DeliveryDate,Remarks FROM Jobs,Customer WHERE Jobs.CustomerId = Customer.customerId AND Jobs.CustomerId = '" + customerId + "';";
            List<ListViewItem> items = new List<ListViewItem>();
            selCmd.CommandText = cmdText;
            MySqlDataReader dr = selCmd.ExecuteReader();
            ListViewItem tempItem;
            DateTime aDate = new DateTime();
            DateTime dDate = new DateTime();
            while (dr.Read())
            {
                tempItem = new ListViewItem(dr.GetString(0));
                for (int i = 1; i < dr.VisibleFieldCount; i++)
                {
                    if (dr.IsDBNull(i))
                    {
                        tempItem.SubItems.Add(String.Empty);
                    }
                    else
                    {
                        if (6 == i)
                        {
                            aDate = dr.GetDateTime(i);
                            tempItem.SubItems.Add(aDate.ToString("dd/MM/yyyy"));
                        }
                        else if (7 == i)
                        {
                            dDate = dr.GetDateTime(i);
                            if (aDate.Date == dDate.Date)
                                tempItem.SubItems.Add(dr.GetDateTime(i).ToString("dd/MM/yyyy HH:mm"));
                            else
                                tempItem.SubItems.Add(dr.GetDateTime(i).ToString("dd/MM/yyyy"));
                        }
                        else
                        {
                            tempItem.SubItems.Add(dr.GetString(i));
                        }
                    }
                }
                items.Add(tempItem);
            }
            dr.Close();
            return items.ToArray();
        }

        internal ListViewItem[] PopulateListView()
        {   
            List<ListViewItem> items = new List<ListViewItem>();
            selCmd.CommandText = lvCommandText;
            MySqlDataReader dr = selCmd.ExecuteReader();
            ListViewItem tempItem;
            DateTime aDate = new DateTime();
            DateTime dDate = new DateTime();
            while (dr.Read())
            {
                tempItem = new ListViewItem(dr.GetString(0));
                for (int i = 1; i < dr.VisibleFieldCount; i++)
                {
                    if (dr.IsDBNull(i))
                    {
                        tempItem.SubItems.Add(String.Empty);
                    }
                    else
                    {
                        if (6 == i)
                        {
                            aDate = dr.GetDateTime(i);
                            tempItem.SubItems.Add(aDate.ToString("dd/MM/yyyy"));
                        }
                        else if (7 == i)
                        {
                            dDate = dr.GetDateTime(i);
                            if (aDate.Date == dDate.Date)
                                tempItem.SubItems.Add(dr.GetDateTime(i).ToString("dd/MM/yyyy HH:mm"));
                            else
                                tempItem.SubItems.Add(dr.GetDateTime(i).ToString("dd/MM/yyyy"));
                        }
                        else
                        {
                            tempItem.SubItems.Add(dr.GetString(i));
                        }
                    }
                }
                items.Add(tempItem);
            }
            dr.Close();
            return items.ToArray();
        }

        internal ListViewItem[] PopulateListView(string Status, string arrivalDate, string deliveryDate)
        {
            
            if (arrivalDate == "")
                lvCommandText = "SELECT ServiceTag,CustomerName,PrinterModel,PrinterSerial,Problem,Status,ArrivalDate,DeliveryDate,Remarks FROM Jobs,Customer WHERE Jobs.CustomerId = Customer.customerId and Jobs.Status='" + Status + "' ORDER BY arrivalDate;";
            else
                lvCommandText = "SELECT ServiceTag,CustomerName,PrinterModel,PrinterSerial,Problem,Status,ArrivalDate,DeliveryDate,Remarks FROM Jobs,Customer WHERE Jobs.CustomerId = Customer.customerId and Jobs.Status='" + Status + "' AND ArrivalDate >= '" + arrivalDate + "' AND (DeliveryDate <= '" + deliveryDate + "' OR DeliveryDate IS NULL) ORDER BY arrivalDate;";

            List<ListViewItem> items = new List<ListViewItem>();
            selCmd.CommandText = lvCommandText;
            MySqlDataReader dr = selCmd.ExecuteReader();
            ListViewItem tempItem;
            DateTime aDate = new DateTime();
            DateTime dDate = new DateTime();
            while (dr.Read())
            {
                tempItem = new ListViewItem(dr.GetString(0));
                for (int i = 1; i < dr.VisibleFieldCount; i++)
                {
                    if (dr.IsDBNull(i))
                    {
                        tempItem.SubItems.Add(String.Empty);
                    }
                    else
                    {
                        if (6 == i)
                        {
                            aDate = dr.GetDateTime(i);
                            tempItem.SubItems.Add(aDate.ToString("dd/MM/yyyy"));
                        }
                        else if (7 == i)
                        {
                            dDate = dr.GetDateTime(i);
                            if(aDate.Date == dDate.Date)
                                tempItem.SubItems.Add(dr.GetDateTime(i).ToString("dd/MM/yyyy HH:mm"));
                            else
                                tempItem.SubItems.Add(dr.GetDateTime(i).ToString("dd/MM/yyyy"));
                        }
                        else
                        {
                            tempItem.SubItems.Add(dr.GetString(i));
                        }
                    }
                }
                items.Add(tempItem);
            }
            dr.Close();
            return items.ToArray();
        }

        internal DataTable GetCustomersList()
        {
            MySqlDataAdapter dataAdapter = new MySqlDataAdapter("SELECT * FROM Customer ORDER BY CustomerName",conn);
            DataTable customerTable = new DataTable();
            dataAdapter.Fill(customerTable);
            return customerTable;            
        }

        internal void AddSMSToQueue(string serviceTag, string customerName, string contactPerson,string printerModel,string printerSerial,
            string problem,string workInProgress,string remarks,string number,string status)
        {
            StringBuilder message = new StringBuilder();
            if (status == "Pending") {
                message.Insert(0,Settings.Default.pendingMessage);
            }
            else if (status == "Completed") {
                message.Insert(0, Settings.Default.pendingMessage);
            }
            else if (status == "Work In Progress") {
                message.Insert(0, Settings.Default.pendingMessage);
            }
            message = message.Replace("<ServiceTag>", serviceTag);
            message = message.Replace("<CustomerName>", customerName);
            message = message.Replace("<ContactPerson>", contactPerson);
            message = message.Replace("<PrinterModel>", printerModel);
            message = message.Replace("<PrinterSerial>", printerSerial);
            message = message.Replace("<Problem>", problem);
            message = message.Replace("<WorkInProgress>", workInProgress);
            message = message.Replace("<Status>", status);
            message = message.Replace("<Remarks>", remarks);
            cmd.CommandText = "INSERT INTO SMSQueue VALUES('" + number + "','" + message + "');";
            cmd.ExecuteNonQuery();
        }

        
        internal List<MessagePayload> GetMessageQueue()
        {
            MySqlCommand smsCommand = new MySqlCommand();
            smsCommand.Connection = conn;   
            List<MessagePayload> listPayload = new List<MessagePayload>();
            MessagePayload Payload = new MessagePayload();
            smsCommand.CommandText = "SELECT * FROM SMSQueue";
            MySqlDataReader reader = smsCommand.ExecuteReader();
            while (reader.Read())
            {
                Payload.number = reader.GetString(0);
                Payload.message = reader.GetString(1);
                listPayload.Add(Payload);
            }
            if (!reader.IsClosed)
                reader.Close();
            smsCommand.CommandText = "DELETE FROM SMSQueue";
            smsCommand.ExecuteNonQuery();
            smsCommand.Dispose();
            smsCommand = null;
            return listPayload;
        }
    }

    public struct MessagePayload
    {
        public string number;
        public string message;
    }
}

