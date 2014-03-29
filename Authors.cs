﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Theater
{
    public partial class Authors : Form
    {
        public Authors()
        {
            InitializeComponent();
        }

        private SqlServerFull _sqlServer = new SqlServerFull();
        private DataSet _dataSet = new DataSet();
            
        private void Authors_Load(object sender, EventArgs e)
        {
            _dataSet.Clear();
            _dataSet.DataSetName = "author";
            _sqlServer.SelectSqlCommand = "SELECT * FROM author";
            if (_sqlServer.ExecuteFill(_dataSet, "author"))
            {
                bindingSource1.DataSource = _dataSet;
                bindingSource1.DataMember = "author";
            }
        }

    }
}