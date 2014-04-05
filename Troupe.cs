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
    public partial class Troupe : Form
    {
        public Troupe()
        {
            InitializeComponent();
        }

        private SqlServerFull _sqlServer1 = new SqlServerFull();
        private DataSet _dataSet = new DataSet();
        private BindingSource _bindingSource = new BindingSource();

        private SqlServerFull _sqlServer2 = new SqlServerFull();
        private DataSet _dataSetSpectacle = new DataSet();
        private DataSet _dataSetEmployee = new DataSet();

        private SqlServerFull _sqlServer3 = new SqlServerFull();
        private DataSet _dataSetFromTable2 = new DataSet();


        /* Загрузка данных */
        private void TableLoad()
        {
            _dataSet.Clear();
            _dataSet.DataSetName = "troupe";
            _sqlServer1.SelectSqlCommand = "SELECT * FROM troupe";

            _sqlServer1.InsertSqlCommand = "INSERT INTO troupe (troupe_spectacle, troupe_party, troupe_role)" +
                                    " VALUES (@troupe_spectacle, @troupe_party, @troupe_role)";
            _sqlServer1.InsertParametersAdd("@troupe_spectacle", SqlDbType.Int, 4, "troupe_spectacle");
            _sqlServer1.InsertParametersAdd("@troupe_party", SqlDbType.Int, 4, "troupe_party");
            _sqlServer1.InsertParametersAdd("@troupe_role", SqlDbType.VarChar, 255, "troupe_role");
            _sqlServer1.InsertParametersAdd("@troupe_id", SqlDbType.Int, 4, "troupe_id");

            _sqlServer1.UpdateSqlCommand = "UPDATE troupe SET troupe_spectacle = @troupe_spectacle, troupe_party = @troupe_party, troupe_role = @troupe_role WHERE (troupe_id = @troupe_id)";
            _sqlServer1.UpdateParametersAdd("@troupe_spectacle", SqlDbType.Int, 4, "troupe_spectacle");
            _sqlServer1.UpdateParametersAdd("@troupe_party", SqlDbType.Int, 4, "troupe_party");
            _sqlServer1.UpdateParametersAdd("@troupe_role", SqlDbType.VarChar, 255, "troupe_role");
            _sqlServer1.UpdateParametersAdd("@troupe_id", SqlDbType.Int, 4, "troupe_id");

            _sqlServer1.DeleteSqlCommand = "DELETE FROM troupe WHERE (troupe_id = @troupe_id)";
            _sqlServer1.DeleteParametersAdd("@troupe_id", SqlDbType.Int, 4, "troupe_id");

            if (_sqlServer1.ExecuteFill(_dataSet, "troupe"))
            {
                _bindingSource.DataSource = _dataSet;
                _bindingSource.DataMember = "troupe";
                bindingNavigator1.BindingSource = _bindingSource;
                dataGridView1.DataSource = _bindingSource;
            }

            /* Загрузка списка идентификаторов всех Спектаклей */
            DataGridViewComboBoxColumn cBox1 = new DataGridViewComboBoxColumn();
            _dataSetSpectacle.Clear();
            _dataSetSpectacle.DataSetName = "spectacle";
            _sqlServer2.SelectSqlCommand = "SELECT * FROM spectacle";
            if (_sqlServer2.ExecuteFill(_dataSetSpectacle, "spectacle"))
            {
                foreach (DataRow row in _dataSetSpectacle.Tables["spectacle"].Rows)
                {
                    cBox1.Items.Add(row["spectacle_id"]);
                }
            }
            cBox1.HeaderText = "Спектакль ID";
            cBox1.DataPropertyName = "troupe_spectacle";
            cBox1.Width = 100;
            dataGridView1.Columns.Add(cBox1);

            /* Загрузка списка идентификаторов всех работников театра */
            DataGridViewComboBoxColumn cBox2 = new DataGridViewComboBoxColumn();
            _dataSetEmployee.Clear();
            _dataSetEmployee.DataSetName = "employee";
            _sqlServer2.SelectSqlCommand = "SELECT * FROM employee";
            if (_sqlServer2.ExecuteFill(_dataSetEmployee, "employee"))
            {
                foreach (DataRow row in _dataSetEmployee.Tables["employee"].Rows)
                {
                    cBox2.Items.Add(row["employee_id"]);
                }
            }
            cBox2.HeaderText = "Учасник ID";
            cBox2.DataPropertyName = "troupe_party";
            cBox2.Width = 100;
            dataGridView1.Columns.Add(cBox2);
        }

        /* Обновление данных в таблице */
        private void TableUpdate()
        {
            _dataSet.Clear();
            _dataSet.DataSetName = "troupe";
            _sqlServer1.SelectSqlCommand = "SELECT * FROM troupe";
            if (_sqlServer1.ExecuteFill(_dataSet, "troupe"))
            {
                _bindingSource.DataSource = _dataSet;
                _bindingSource.DataMember = "troupe";
                bindingNavigator1.BindingSource = _bindingSource;
                dataGridView1.DataSource = _bindingSource;
            }

        }

        /* Сохранение данных в таблице базы данных */
        private void TableSave()
        {
            if (_sqlServer1.ExecuteUpdate(_dataSet, "troupe"))
            {
                TableUpdate();
            }
        }

        /* Загрузка данных из других таблиц */
        private void ShowTable2(String _spectacleID, String _partyID)
        {
            _dataSetFromTable2.Clear();
            _dataSetFromTable2.DataSetName = "spectacle";
            _sqlServer3.SelectSqlCommand = "SELECT spectacle.spectacle_id, spectacle.spectacle_name, employee.employee_id, employee.employee_name FROM employee, spectacle" +
                " WHERE (spectacle.spectacle_id = " + _spectacleID + " AND (employee.employee_id = " + _partyID + "))";
            if (_sqlServer3.ExecuteFill(_dataSetFromTable2, "spectacle"))
            {
                dataGridView2.DataSource = _dataSetFromTable2;
                dataGridView2.DataMember = "spectacle";
            }
        }

        private void Troupe_Load(object sender, EventArgs e)
        {
            TableLoad();
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            TableUpdate();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            TableSave();
        }

        
        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            String _spectacleID = "";
            String _partyID = "";
                
            if ((dataGridView1.ColumnCount > 5) && (dataGridView1.RowCount > 0))
            {
                try
                {
                    _spectacleID = dataGridView1[4, dataGridView1.CurrentCell.RowIndex].Value.ToString();
                    _partyID = dataGridView1[5, dataGridView1.CurrentCell.RowIndex].Value.ToString();
                }
                catch
                {
                    //TableUpdate();
                }
                if ((_spectacleID != "") && (_partyID != ""))
                {
                    ShowTable2(_spectacleID, _partyID);
                }
                else
                {
                    _dataSetFromTable2.Clear();
                }
            }
        }
    }
}
