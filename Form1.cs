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
            //...
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

        
    }
}
