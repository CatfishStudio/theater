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
    public partial class Ticket : Form
    {
        public Ticket()
        {
            InitializeComponent();
        }

        public Tickets fTickets;
        public String ID;

        private void dataLoad()
        {
            SqlServerFull _sqlServerSpectacle = new SqlServerFull();
            DataSet _dataSetSpectacle = new DataSet();
            _sqlServerSpectacle.SelectSqlCommand = "SELECT * FROM spectacle";
            if (_sqlServerSpectacle.ExecuteFill(_dataSetSpectacle, "spectacle"))
            {
                comboBox1.DataSource = _dataSetSpectacle.Tables["spectacle"];
                comboBox1.DisplayMember = "spectacle_id";
            }
            else MessageBox.Show("Произошла ошибка загрузки данных о спектаклях.");

            SqlServerFull _sqlServerPlaces = new SqlServerFull();
            DataSet _dataSetPlaces = new DataSet();
            _sqlServerPlaces.SelectSqlCommand = "SELECT * FROM places";
            if (_sqlServerPlaces.ExecuteFill(_dataSetPlaces, "places"))
            {
                comboBox2.DataSource = _dataSetPlaces.Tables["places"];
                comboBox2.DisplayMember = "places_id";
            }
            else MessageBox.Show("Произошла ошибка загрузки данных о местах.");

            if (this.Text == "Изменить билет.")
            {
                SqlServerFull _sqlServerTicket = new SqlServerFull();
                DataSet _dataSetTicket = new DataSet();
                _sqlServerTicket.SelectSqlCommand = "SELECT * FROM ticket WHERE (ticket_id = " + ID.ToString() + ")";
                if (_sqlServerTicket.ExecuteFill(_dataSetTicket, "ticket"))
                {
                    comboBox1.Text = _dataSetTicket.Tables["ticket"].Rows[0]["ticket_spectacle"].ToString();
                    comboBox2.Text = _dataSetTicket.Tables["ticket"].Rows[0]["ticket_place"].ToString();
                    comboBox3.Text = _dataSetTicket.Tables["ticket"].Rows[0]["ticket_order"].ToString();
                    comboBox4.Text = _dataSetTicket.Tables["ticket"].Rows[0]["ticket_subscription"].ToString();
                }
                else MessageBox.Show("Произошла ошибка открытия данных о выбраном билете.");
            }

        }

        private void dataSave()
        {
            if (this.Text == "Создать билет.")
            {
                SqlServerShort _sqlServerShort = new SqlServerShort();
                _sqlServerShort.SqlCommand = "INSERT INTO ticket (ticket_spectacle, ticket_place, ticket_order, ticket_subscription) "+
                    "VALUES (" + comboBox1.Text + ", " + comboBox2.Text + ", " + comboBox3.Text + ", " + comboBox4.Text + ")";
                if (_sqlServerShort.ExecuteNonQuery())
                {
                    try
                    {
                        fTickets.TableUpdate();
                        this.Close();
                    }
                    catch
                    {
                        this.Close();
                    }
                }
                else MessageBox.Show("Произошла ошибка сохранения новых данных.");

            }

            if (this.Text == "Изменить билет.")
            {
                SqlServerShort _sqlServerShort = new SqlServerShort();
                _sqlServerShort.SqlCommand = "UPDATE ticket SET (ticket_spectacle = " + comboBox1.Text + 
                    ", ticket_place = " + comboBox2.Text + 
                    ", ticket_order = " + comboBox3.Text + 
                    ", ticket_subscription = " + comboBox4.Text + 
                    ") WHERE (ticket_id = " + ID.ToString() + ")";
                if (_sqlServerShort.ExecuteNonQuery())
                {
                    try
                    {
                        fTickets.TableUpdate();
                        this.Close();
                    }
                    catch
                    {
                        this.Close();
                    }
                }
                else MessageBox.Show("Произошла ошибка сохранения изменённых данных.");
            }
        }

        private void Ticket_Load(object sender, EventArgs e)
        {
            dataLoad();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            dataSave();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.Text != "System.Data.DataRowView")
            {
                SqlServerFull _sqlServerSpectacle = new SqlServerFull();
                DataSet _dataSetSpectacle = new DataSet();
                _sqlServerSpectacle.SelectSqlCommand = "SELECT * FROM spectacle WHERE (spectacle_id = " + comboBox1.Text + ")";
                if (_sqlServerSpectacle.ExecuteFill(_dataSetSpectacle, "spectacle"))
                {
                    label2.Text = "(" + _dataSetSpectacle.Tables["spectacle"].Rows[0]["spectacle_name"].ToString() + ")";
                }
                else MessageBox.Show("Произошла ошибка загрузки данных о спектаклях.");
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox2.Text != "System.Data.DataRowView")
            {
                SqlServerFull _sqlServerPlaces = new SqlServerFull();
                DataSet _dataSetPlaces = new DataSet();
                _sqlServerPlaces.SelectSqlCommand = "SELECT * FROM places WHERE (places_id = " + comboBox2.Text + ")";
                if (_sqlServerPlaces.ExecuteFill(_dataSetPlaces, "places"))
                {
                    label4.Text = "(Ряд: " + _dataSetPlaces.Tables["places"].Rows[0]["places_series"].ToString() + 
                        "   Место: " + _dataSetPlaces.Tables["places"].Rows[0]["places_place"].ToString() +
                        "   Цена: " + _dataSetPlaces.Tables["places"].Rows[0]["places_price"].ToString() + ")";
                }
                else MessageBox.Show("Произошла ошибка загрузки данных о местах.");
            }
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox3.Text != "System.Data.DataRowView")
            {
                if (comboBox3.Text == "0")
                    label6.Text = "(нет)";
                else label6.Text = "(да)";
            }
        }

        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox4.Text != "System.Data.DataRowView")
            {
                if (comboBox4.Text == "0")
                    label8.Text = "(нет)";
                else label8.Text = "(да)";
            }
        }
    }
}
