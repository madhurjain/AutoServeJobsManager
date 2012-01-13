using System;
using System.Windows.Forms;
using System.Text;
using System.IO.Ports;
using System.Threading;
using AutoServeJobsManager.Properties;

namespace AutoServeJobsManager
{
    class Texting
    {
        static SerialPort tP;
        static SerialPort p;

        public Texting() {
            p = new SerialPort(Settings.Default.portName, 115200, Parity.None, 8, StopBits.One);
            p.DataReceived += new SerialDataReceivedEventHandler(p_DataReceived);
            p.ErrorReceived += new SerialErrorReceivedEventHandler(p_ErrorReceived);
            p.Handshake = Handshake.RequestToSend;
            p.DtrEnable = true;
            p.RtsEnable = true;
            p.NewLine = Environment.NewLine;
            p.ReadTimeout = 2000;
            p.WriteTimeout = 2000;
        }

        internal static string[] GetPorts()
        {
            return SerialPort.GetPortNames();
        }

        internal static void QueryDevice(string port)
        {
            try
            {
                tP = new SerialPort(port, 115200, Parity.None, 8, StopBits.One);
                tP.DataReceived += new SerialDataReceivedEventHandler(tP_DataReceived);
                tP.ErrorReceived += new SerialErrorReceivedEventHandler(tP_ErrorReceived);
                tP.Handshake = Handshake.RequestToSend;
                tP.DtrEnable = true;
                tP.RtsEnable = true;
                tP.NewLine = Environment.NewLine;
                tP.ReadTimeout = 2000;
                tP.WriteTimeout = 2000;
                tP.Open();
                Thread.Sleep(200);
                tP.Write("AT+GMM" + (char)13);
                Thread.Sleep(500);
                tP.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (tP.IsOpen)
                    tP.Close();
            }
        }

        static void tP_ErrorReceived(object sender, SerialErrorReceivedEventArgs e)
        {
            MessageBox.Show("Error Communicating with the device","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
        }

        static void tP_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            MessageBox.Show(tP.ReadExisting());
        }
        internal void openPort()
        {
            try
            {
                p.Open();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        internal void closePort()
        { 
            if (p.IsOpen)
                p.Close();
        }
        internal void sendMessage(string number, string message)
        {
            try
            {
                Thread.Sleep(4000);
                p.Write("AT+CMGF=1" + (char)13);
                Thread.Sleep(200);
                p.Write("AT+CMGS=" + (char)34 + number + (char)34 + (char)13);
                Thread.Sleep(200);
                p.Write(message + (char)13 + (char)26);
                Thread.Sleep(5000);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        void p_ErrorReceived(object sender, SerialErrorReceivedEventArgs e)
        {
            Console.WriteLine("ERROR");
        }

        void p_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            Console.Write(p.ReadExisting());
        }

    }
}
