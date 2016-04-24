using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.IO;

namespace asg2_15208249
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private int input_Check(string id)
        {
            int temp = 0;
            try{
                if (id.Length == 8){
                    temp = Convert.ToInt32(id);
                }
                else{
                    resultLabel.Text = "Input must be 8 digit.";
                    temp = 0;
                }
            }
            catch{
                resultLabel.Text = "Input must be 8 digit.";
                temp = 0;
            }

            return temp;
        }

        private void checkBtn_Click(object sender, EventArgs e)
        {
            string ipAddress = "127.0.0.1";
            int portNum = 4321;

            BinaryReader readData;
            BinaryWriter writeData;
            NetworkStream inOutStream;
            TcpClient tcpConnection = new TcpClient();

            string textValue = resultTextBox.Text;
            int temp = input_Check(textValue);
            if (temp != 0)
            {
                tcpConnection.Connect(ipAddress, portNum);

                inOutStream = tcpConnection.GetStream();
                readData = new BinaryReader(inOutStream);
                writeData = new BinaryWriter(inOutStream);
                writeData.Write(temp);

                if(readData.ReadBoolean()){
                    resultLabel.Text = "The ID entered is a valid HKBU Student ID";
                }
                else{
                    resultLabel.Text = "The ID entered is not a valid HKBU StudentID";
                }
            }
        }
    }
}
