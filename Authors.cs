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
    public partial class Authors : Form
    {
        public Authors()
        {
            InitializeComponent();
        }

        private SqlServerFull _sqlServer = new SqlServerFull();
        private DataSet _dataSet = new DataSet();
        private BindingSource _bindingSource = new BindingSource();
                    
        private void Authors_Load(object sender, EventArgs e)
        {
            /* Инициализация объекта ДатаВремя*/
             FieldTableDate ftd = new FieldTableDate(dataGridView1);

            /* Подключение к базе данных, выбор данных из таблицы */
            _dataSet.Clear();
            _dataSet.DataSetName = "author";
            _sqlServer.SelectSqlCommand = "SELECT * FROM author";
            if (_sqlServer.ExecuteFill(_dataSet, "author"))
            {
                _bindingSource.DataSource = _dataSet;
                _bindingSource.DataMember = "author";
                bindingNavigator1.BindingSource = _bindingSource;
                dataGridView1.DataSource = _bindingSource;
            }
        }

    }
}
