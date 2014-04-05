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
    public partial class Repertoire : Form
    {
        public Repertoire()
        {
            InitializeComponent();
        }

        private SqlServerFull _sqlServer1 = new SqlServerFull();
        private DataSet _dataSet = new DataSet();
        private BindingSource _bindingSource = new BindingSource();

        private SqlServerFull _sqlServer2 = new SqlServerFull();
        private DataSet _dataSetSpectacle = new DataSet();

        private SqlServerFull _sqlServer3 = new SqlServerFull();
        private DataSet _dataSetFromTable2 = new DataSet();
        
        /* Загрузка данных */
        private void TableLoad()
        {
            _dataSet.Clear();
            _dataSet.DataSetName = "repertoire";
            _sqlServer1.SelectSqlCommand = "SELECT * FROM repertoire";

            _sqlServer1.InsertSqlCommand = "INSERT INTO repertoire (repertoire_spectacle, repertoire_season, repertoire_play_out, repertoire_theater, repertoire_tour, repertoire_date_show, repertoire_date_premiere)" +
                                    " VALUES (@repertoire_spectacle, @repertoire_season, @repertoire_play_out, @repertoire_theater, @repertoire_tour, @repertoire_date_show, @repertoire_date_premiere)";
            _sqlServer1.InsertParametersAdd("@repertoire_spectacle", SqlDbType.Int, 4, "repertoire_spectacle");
            _sqlServer1.InsertParametersAdd("@repertoire_season", SqlDbType.VarChar, 50, "repertoire_season");
            _sqlServer1.InsertParametersAdd("@repertoire_play_out", SqlDbType.Int, 4, "repertoire_play_out");
            _sqlServer1.InsertParametersAdd("@repertoire_theater", SqlDbType.VarChar, 255, "repertoire_theater");
            _sqlServer1.InsertParametersAdd("@repertoire_tour", SqlDbType.Int, 4, "repertoire_tour");
            _sqlServer1.InsertParametersAdd("@repertoire_date_show", SqlDbType.DateTime, 10, "repertoire_date_show");
            _sqlServer1.InsertParametersAdd("@repertoire_date_premiere", SqlDbType.DateTime, 10, "repertoire_date_premiere");
            _sqlServer1.InsertParametersAdd("@repertoire_id", SqlDbType.Int, 4, "repertoire_id");

            _sqlServer1.UpdateSqlCommand = "UPDATE repertoire SET repertoire_spectacle = @repertoire_spectacle, repertoire_season = @repertoire_season, repertoire_play_out = @repertoire_play_out, repertoire_theater = @repertoire_theater, repertoire_tour = @repertoire_tour, repertoire_date_show = @repertoire_date_show, repertoire_date_premiere = @repertoire_date_premiere " +
                "WHERE (repertoire_id = @repertoire_id)";
            _sqlServer1.UpdateParametersAdd("@repertoire_spectacle", SqlDbType.Int, 4, "repertoire_spectacle");
            _sqlServer1.UpdateParametersAdd("@repertoire_season", SqlDbType.VarChar, 50, "repertoire_season");
            _sqlServer1.UpdateParametersAdd("@repertoire_play_out", SqlDbType.Int, 4, "repertoire_play_out");
            _sqlServer1.UpdateParametersAdd("@repertoire_theater", SqlDbType.VarChar, 255, "repertoire_theater");
            _sqlServer1.UpdateParametersAdd("@repertoire_tour", SqlDbType.Int, 4, "repertoire_tour");
            _sqlServer1.UpdateParametersAdd("@repertoire_date_show", SqlDbType.DateTime, 10, "repertoire_date_show");
            _sqlServer1.UpdateParametersAdd("@repertoire_date_premiere", SqlDbType.DateTime, 10, "repertoire_date_premiere");
            _sqlServer1.UpdateParametersAdd("@repertoire_id", SqlDbType.Int, 4, "repertoire_id");

            _sqlServer1.DeleteSqlCommand = "DELETE FROM repertoire WHERE (repertoire_id = @repertoire_id)";
            _sqlServer1.DeleteParametersAdd("@repertoire_id", SqlDbType.Int, 4, "repertoire_id");

            if (_sqlServer1.ExecuteFill(_dataSet, "repertoire"))
            {
                _bindingSource.DataSource = _dataSet;
                _bindingSource.DataMember = "repertoire";
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
            cBox1.DataPropertyName = "repertoire_spectacle";
            cBox1.Width = 100;
            dataGridView1.Columns.Add(cBox1);
        }

        /* Обновление данных в таблице */
        private void TableUpdate()
        {
            _dataSet.Clear();
            if (_sqlServer1.ExecuteFill(_dataSet, "repertoire"))
            {
                _bindingSource.DataSource = _dataSet;
                _bindingSource.DataMember = "repertoire";
                bindingNavigator1.BindingSource = _bindingSource;
                dataGridView1.DataSource = _bindingSource;
            }
        }

        /* Сохранение данных в таблице базы данных */
        private void TableSave()
        {
            if (_sqlServer1.ExecuteUpdate(_dataSet, "repertoire"))
            {
                TableUpdate();
            }
        }

        /* Загрузка данных из других таблиц */
        private void ShowTable2(String _spectacleID)
        {
            _dataSetFromTable2.Clear();
            _dataSetFromTable2.DataSetName = "spectacle";
            _sqlServer3.SelectSqlCommand = "SELECT spectacle.spectacle_id, spectacle.spectacle_name FROM spectacle" +
                " WHERE (spectacle.spectacle_id = " + _spectacleID + ")";
            if (_sqlServer3.ExecuteFill(_dataSetFromTable2, "spectacle"))
            {
                dataGridView2.DataSource = _dataSetFromTable2;
                dataGridView2.DataMember = "spectacle";
            }
        }

        private void Repertoire_Load(object sender, EventArgs e)
        {
            /* Инициализация объекта ДатаВремя*/
            FieldTableDate ftd1 = new FieldTableDate(dataGridView1, 6);
            FieldTableDate ftd2 = new FieldTableDate(dataGridView1, 7);

            /* Загрузка таблицы */
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
            
            if ((dataGridView1.ColumnCount > 7) && (dataGridView1.RowCount > 0))
            {
                try
                {
                    _spectacleID = dataGridView1[8, dataGridView1.CurrentCell.RowIndex].Value.ToString();
                }
                catch
                {
                    //TableUpdate();
                }
                if ((_spectacleID != ""))
                {
                    ShowTable2(_spectacleID);
                }
                else
                {
                    _dataSetFromTable2.Clear();
                }
            }
            

        }
        

    }
}
