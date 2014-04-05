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
    public partial class Tickets : Form
    {
        public Tickets()
        {
            InitializeComponent();
        }

        public Form fParent;
        private SqlServerFull _sqlServer = new SqlServerFull();
        private DataSet _dataSet = new DataSet();
        private BindingSource _bindingSource = new BindingSource();

        /* Обновление данных в таблице */
        public void TableUpdate()
        {
            _dataSet.Clear();
            _dataSet.DataSetName = "ticket";
            _sqlServer.SelectSqlCommand = "SELECT ticket.ticket_id, ticket.ticket_spectacle, ticket.ticket_place, " + 
                "spectacle.spectacle_id, spectacle.spectacle_name, "+
                "places.places_id, places.places_place, places.places_series, places.places_price  FROM ticket, spectacle, places "+
                "WHERE (ticket.ticket_spectacle = spectacle.spectacle_id AND ticket.ticket_place = places.places_id)";
            if (_sqlServer.ExecuteFill(_dataSet, "ticket"))
            {
                _bindingSource.DataSource = _dataSet;
                _bindingSource.DataMember = "ticket";
                bindingNavigator1.BindingSource = _bindingSource;
                dataGridView1.DataSource = _bindingSource;
            }
        }

        private void Tickets_Load(object sender, EventArgs e)
        {
            TableUpdate();
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            TableUpdate();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            Ticket fTicket = new Ticket();
            fTicket.MdiParent = fParent;
            fTicket.Text = "Создать билет.";
            fTicket.fTickets = this;
            fTicket.Show();
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            if (_dataSet.Tables["ticket"].Rows.Count > 0)
            {
                Ticket fTicket = new Ticket();
                fTicket.MdiParent = fParent;
                fTicket.Text = "Изменить билет.";
                fTicket.fTickets = this;
                fTicket.ID = dataGridView1[0, dataGridView1.CurrentCell.RowIndex].Value.ToString();
                fTicket.Show();
            }
        }
    }
}
