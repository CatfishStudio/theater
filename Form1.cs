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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ConnectServer fConnectServer = new ConnectServer();
            fConnectServer.ShowDialog();
        }

        private void выходИзПрограммыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void ShowAuthor()
        {
            Authors fAurhors = new Authors();
            fAurhors.MdiParent = this;
            fAurhors.Show();
        }

        private void авторыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowAuthor();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            ShowAuthor();
        }

        private void ShowStaff()
        {
            Staff fStaff = new Staff();
            fStaff.MdiParent = this;
            fStaff.Show();
        }

        private void работникиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowStaff();
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            ShowStaff();
        }

        private void ShowSpectacle()
        {
            Spectacle fSpectacle = new Spectacle();
            fSpectacle.MdiParent = this;
            fSpectacle.Show();
        }

        private void спектаклиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowSpectacle();
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            ShowSpectacle();
        }

        private void ShowPlaces()
        {
            Places fPlaces = new Places();
            fPlaces.MdiParent = this;
            fPlaces.Show();
        }

        private void местаВЗалеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowPlaces();
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            ShowPlaces();
        }

        private void ShowTickets()
        {
            Tickets fTickets = new Tickets();
            fTickets.MdiParent = this;
            fTickets.fParent = this;
            fTickets.Show();
        }

        private void билетToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowTickets();
        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            ShowTickets();
        }

        private void ShowTroupe()
        {
            Troupe fTroupe = new Troupe();
            fTroupe.MdiParent = this;
            fTroupe.Show();
        }

        private void труппыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowTroupe();
        }

        private void toolStripButton6_Click(object sender, EventArgs e)
        {
            ShowTroupe();
        }

        private void ShowRepertoire()
        {
            Repertoire fRepertoire = new Repertoire();
            fRepertoire.MdiParent = this;
            fRepertoire.Show();
        
        }

        private void репертуарToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowRepertoire();
        }

        private void toolStripButton7_Click(object sender, EventArgs e)
        {
            ShowRepertoire();
        }

        private void ShowReport()
        {
            Report fReport = new Report();
            fReport.MdiParent = this;
            fReport.Show();
            
        }

        private void общийОтчётToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowReport();   
        }

        private void toolStripButton8_Click(object sender, EventArgs e)
        {
            ShowReport();
        }

        private void оПрограммеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            About fAbout = new About();
            fAbout.MdiParent = this;
            fAbout.Show();
        }

        
    }
}
