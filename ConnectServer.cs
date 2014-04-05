using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Theater
{
    public partial class ConnectServer : Form
    {
        public ConnectServer()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ConnectConfig.test = true;
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            label5.Text = "Проверка соединения...";
            this.Update();
            ConnectConfig.server = comboBox1.Text;
            ConnectConfig.database = comboBox2.Text;
            ConnectConfig.uid = textBox1.Text;
            ConnectConfig.password = textBox2.Text;
            SqlServerShort testConnect = new SqlServerShort();
            testConnect.SqlCommand = "SELECT * FROM author";
            if (testConnect.ExecuteNonQuery())
            {
                label5.Text = "Проверка прошла успешно.";
                ConnectConfig.test = true;
                this.Close();
            }
            else
            {
                ConnectConfig.test = false;
                label5.Text = "Ошибка проверки соединения!";
            }
        }

        private void ConnectServer_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!ConnectConfig.test) e.Cancel = true;
        }

        
    }
}
