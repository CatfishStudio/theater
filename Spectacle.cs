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
    public partial class Spectacle : Form
    {
        public Spectacle()
        {
            InitializeComponent();
        }

        private SqlServerFull _sqlServer = new SqlServerFull();
        private DataSet _dataSet = new DataSet();
        private BindingSource _bindingSource = new BindingSource();

        private SqlServerFull _sqlServer2 = new SqlServerFull();
        private DataSet _dataSetAuthor = new DataSet();
        private DataSet _dataSetStaff = new DataSet();

        private SqlServerFull _sqlServer3 = new SqlServerFull();
        private DataSet _dataSetFromTable2 = new DataSet();

        /* Загрузка данных */
        private void TableLoad()
        {
            _dataSet.Clear();
            _dataSet.DataSetName = "spectacle";
            _sqlServer.SelectSqlCommand = "SELECT * FROM spectacle";

            _sqlServer.InsertSqlCommand = "INSERT INTO spectacle (spectacle_name, spectacle_genre, spectacle_author, spectacle_director, spectacle_producer, spectacle_designer, spectacle_conductor)" + 
                                    " VALUES (@spectacle_name, @spectacle_genre, @spectacle_author, @spectacle_director, @spectacle_producer, @spectacle_designer, @spectacle_conductor)";
            _sqlServer.InsertParametersAdd("@spectacle_name", SqlDbType.VarChar, 255, "spectacle_name");
            _sqlServer.InsertParametersAdd("@spectacle_genre", SqlDbType.VarChar, 50, "spectacle_genre");
            _sqlServer.InsertParametersAdd("@spectacle_author", SqlDbType.Int, 11, "spectacle_author");
            _sqlServer.InsertParametersAdd("@spectacle_director", SqlDbType.Int, 11, "spectacle_director");
            _sqlServer.InsertParametersAdd("@spectacle_producer", SqlDbType.Int, 11, "spectacle_producer");
            _sqlServer.InsertParametersAdd("@spectacle_designer", SqlDbType.Int, 11, "spectacle_designer");
            _sqlServer.InsertParametersAdd("@spectacle_conductor", SqlDbType.Int, 11, "spectacle_conductor");
            _sqlServer.InsertParametersAdd("@spectacle_id", SqlDbType.Int, 11, "spectacle_id");

            _sqlServer.UpdateSqlCommand = "UPDATE spectacle SET spectacle_name = @spectacle_name, spectacle_genre = @spectacle_genre, spectacle_author = @spectacle_author, spectacle_director = @spectacle_director, spectacle_producer = @spectacle_producer, spectacle_designer = @spectacle_designer, spectacle_conductor = @spectacle_conductor WHERE (spectacle_id = @spectacle_id)";
            _sqlServer.UpdateParametersAdd("@spectacle_name", SqlDbType.VarChar, 255, "spectacle_name");
            _sqlServer.UpdateParametersAdd("@spectacle_genre", SqlDbType.VarChar, 50, "spectacle_genre");
            _sqlServer.UpdateParametersAdd("@spectacle_author", SqlDbType.Int, 11, "spectacle_author");
            _sqlServer.UpdateParametersAdd("@spectacle_director", SqlDbType.Int, 11, "spectacle_director");
            _sqlServer.UpdateParametersAdd("@spectacle_producer", SqlDbType.Int, 11, "spectacle_producer");
            _sqlServer.UpdateParametersAdd("@spectacle_designer", SqlDbType.Int, 11, "spectacle_designer");
            _sqlServer.UpdateParametersAdd("@spectacle_conductor", SqlDbType.Int, 11, "spectacle_conductor");
            _sqlServer.UpdateParametersAdd("@spectacle_id", SqlDbType.Int, 11, "spectacle_id");

            _sqlServer.DeleteSqlCommand = "DELETE FROM spectacle WHERE (spectacle_id = @spectacle_id)";
            _sqlServer.DeleteParametersAdd("@spectacle_id", SqlDbType.Int, 11, "spectacle_id");

            if (_sqlServer.ExecuteFill(_dataSet, "spectacle"))
            {
                _bindingSource.DataSource = _dataSet;
                _bindingSource.DataMember = "spectacle";
                bindingNavigator1.BindingSource = _bindingSource;
                dataGridView1.DataSource = _bindingSource;
            }

            /* Загрузка списка идентификаторов всех авторов */
            DataGridViewComboBoxColumn cBox1 = new DataGridViewComboBoxColumn();
            _dataSetAuthor.Clear();
            _dataSetAuthor.DataSetName = "author";
            _sqlServer2.SelectSqlCommand = "SELECT * FROM author";
            if (_sqlServer2.ExecuteFill(_dataSetAuthor, "author"))
            {
                foreach (DataRow row in _dataSetAuthor.Tables["author"].Rows)
                {
                    cBox1.Items.Add(row["author_id"]);
                }
            }
            cBox1.HeaderText = "Автор ID";
            cBox1.DataPropertyName = "spectacle_author";
            cBox1.Width = 150;
            dataGridView1.Columns.Add(cBox1);

            /* Загрузка списка идентификаторов всех работников театра */
            DataGridViewComboBoxColumn cBox2 = new DataGridViewComboBoxColumn();
            _dataSetStaff.Clear();
            _dataSetStaff.DataSetName = "employee";
            _sqlServer2.SelectSqlCommand = "SELECT * FROM employee";
            if (_sqlServer2.ExecuteFill(_dataSetStaff, "employee"))
            {
                foreach (DataRow row in _dataSetStaff.Tables["employee"].Rows)
                {
                    cBox2.Items.Add(row["employee_id"]);
                }
            }
            cBox2.HeaderText = "Постановщик ID";
            cBox2.DataPropertyName = "spectacle_director";
            cBox2.Width = 150;
            dataGridView1.Columns.Add(cBox2);
            DataGridViewComboBoxColumn cBox3 = new DataGridViewComboBoxColumn();
            cBox3 = (DataGridViewComboBoxColumn)cBox2.Clone();
            cBox3.HeaderText = "Режиссер-постановщик ID";
            cBox3.DataPropertyName = "spectacle_producer";
            cBox3.Width = 150;
            dataGridView1.Columns.Add(cBox3);
            DataGridViewComboBoxColumn cBox4 = new DataGridViewComboBoxColumn();
            cBox4 = (DataGridViewComboBoxColumn)cBox2.Clone();
            cBox4.HeaderText = "Художник-постановщик ID";
            cBox4.DataPropertyName = "spectacle_designer";
            cBox4.Width = 150;
            dataGridView1.Columns.Add(cBox4);
            DataGridViewComboBoxColumn cBox5 = new DataGridViewComboBoxColumn();
            cBox5 = (DataGridViewComboBoxColumn)cBox2.Clone();
            cBox5.HeaderText = "Дирижер ID";
            cBox5.DataPropertyName = "spectacle_conductor";
            cBox5.Width = 150;
            dataGridView1.Columns.Add(cBox5);
        }

        /* Обновление данных в таблице */
        private void TableUpdate()
        {
            _dataSet.Clear();
            _dataSet.DataSetName = "spectacle";
            _sqlServer.SelectSqlCommand = "SELECT * FROM spectacle";
            if (_sqlServer.ExecuteFill(_dataSet, "spectacle"))
            {
                _bindingSource.DataSource = _dataSet;
                _bindingSource.DataMember = "spectacle";
                bindingNavigator1.BindingSource = _bindingSource;
                dataGridView1.DataSource = _bindingSource;
            }

        }

        /* Сохранение данных а таблице базы данных */
        private void TableSave()
        {
            if (_sqlServer.ExecuteUpdate(_dataSet, "spectacle"))
            {
                TableUpdate();
            }
        }

        /* Загрузка данных из других таблиц */
        private void ShowTable2(String _authorID, String _directorID, String _producerID, String _designerID, String _conductorID)
        {
            _dataSetFromTable2.Clear();
            _dataSetFromTable2.DataSetName = "employee";
            _sqlServer3.SelectSqlCommand = "SELECT employee.employee_id, employee.employee_name, author.author_id, author.author_name FROM employee, author" +
                " WHERE (author.author_id = " + _authorID + " AND (employee.employee_id = " + _directorID + " OR employee.employee_id = " + _producerID + " OR employee.employee_id = " + _designerID + " OR employee.employee_id = " + _conductorID + "))";
            if(_sqlServer3.ExecuteFill(_dataSetFromTable2, "employee"))
            {
                dataGridView2.DataSource = _dataSetFromTable2;
                dataGridView2.DataMember = "employee";
            }
        }

        private void Spectacle_Load(object sender, EventArgs e)
        {
            /* Подключение к базе данных, выбор данных из таблицы */
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

        private void dataGridView1_Click(object sender, EventArgs e)
        {
            String _authorID = dataGridView1[8, dataGridView1.CurrentCell.RowIndex].Value.ToString();
            String _directorID = dataGridView1[9, dataGridView1.CurrentCell.RowIndex].Value.ToString();
            String _producerID = dataGridView1[10, dataGridView1.CurrentCell.RowIndex].Value.ToString();
            String _designerID = dataGridView1[11, dataGridView1.CurrentCell.RowIndex].Value.ToString();
            String _conductorID = dataGridView1[12, dataGridView1.CurrentCell.RowIndex].Value.ToString();
            if ((_authorID != "") && (_directorID != "") && (_producerID != "") && (_designerID != "") && (_conductorID != ""))
            {
                ShowTable2(_authorID, _directorID, _producerID, _designerID, _conductorID);
            }
            else
            {
                _dataSetFromTable2.Clear();
            }
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            String _authorID = "";
            String _directorID = "";
            String _producerID = "";
            String _designerID = "";
            String _conductorID = "";
                
            if ((dataGridView1.ColumnCount > 10) && (dataGridView1.RowCount > 0))
            {
                try
                {
                    _authorID = dataGridView1[8, dataGridView1.CurrentCell.RowIndex].Value.ToString();
                    _directorID = dataGridView1[9, dataGridView1.CurrentCell.RowIndex].Value.ToString();
                    _producerID = dataGridView1[10, dataGridView1.CurrentCell.RowIndex].Value.ToString();
                    _designerID = dataGridView1[11, dataGridView1.CurrentCell.RowIndex].Value.ToString();
                    _conductorID = dataGridView1[12, dataGridView1.CurrentCell.RowIndex].Value.ToString();
                }
                catch
                {
                    TableUpdate();
                }
                if ((_authorID != "") && (_directorID != "") && (_producerID != "") && (_designerID != "") && (_conductorID != ""))
                {
                    ShowTable2(_authorID, _directorID, _producerID, _designerID, _conductorID);
                }
                else
                {
                    _dataSetFromTable2.Clear();
                }
            }
        }

        
        
        
        
    }
}
