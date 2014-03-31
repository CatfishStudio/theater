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
    public partial class Places : Form
    {
        public Places()
        {
            InitializeComponent();
        }

        private SqlServerFull _sqlServer = new SqlServerFull();
        private DataSet _dataSet = new DataSet();
        private BindingSource _bindingSource = new BindingSource();

        /* Загрузка данных */
        private void TableLoad()
        {
            _dataSet.Clear();
            _dataSet.DataSetName = "places";
            _sqlServer.SelectSqlCommand = "SELECT * FROM places";

            _sqlServer.InsertSqlCommand = "INSERT INTO places (places_place, places_series, places_price) VALUES (@places_place, @places_series, @places_price)";
            _sqlServer.InsertParametersAdd("@places_place", SqlDbType.Int, 4, "places_place");
            _sqlServer.InsertParametersAdd("@places_series", SqlDbType.Int, 4, "places_series");
            _sqlServer.InsertParametersAdd("@places_price", SqlDbType.Money, 4, "places_price");
            _sqlServer.InsertParametersAdd("@places_id", SqlDbType.Int, 11, "places_id");

            _sqlServer.UpdateSqlCommand = "UPDATE places SET places_place = @places_place, places_series = @places_series, places_price = @places_price  WHERE (places_id = @places_id)";
            _sqlServer.UpdateParametersAdd("@places_place", SqlDbType.Int, 4, "places_place");
            _sqlServer.UpdateParametersAdd("@places_series", SqlDbType.Int, 4, "places_series");
            _sqlServer.UpdateParametersAdd("@places_price", SqlDbType.Money, 4, "places_price");
            _sqlServer.UpdateParametersAdd("@places_id", SqlDbType.Int, 11, "places_id");

            _sqlServer.DeleteSqlCommand = "DELETE FROM places WHERE (places_id = @places_id)";
            _sqlServer.DeleteParametersAdd("@places_id", SqlDbType.Int, 11, "places_id");

            if (_sqlServer.ExecuteFill(_dataSet, "places"))
            {
                _bindingSource.DataSource = _dataSet;
                _bindingSource.DataMember = "places";
                bindingNavigator1.BindingSource = _bindingSource;
                dataGridView1.DataSource = _bindingSource;
            }
        }

        /* Обновление данных в таблице */
        private void TableUpdate()
        {
            _dataSet.Clear();
            _dataSet.DataSetName = "places";
            _sqlServer.SelectSqlCommand = "SELECT * FROM places";
            if (_sqlServer.ExecuteFill(_dataSet, "places"))
            {
                _bindingSource.DataSource = _dataSet;
                _bindingSource.DataMember = "places";
                bindingNavigator1.BindingSource = _bindingSource;
                dataGridView1.DataSource = _bindingSource;
            }
        }

        /* Сохранение данных а таблице базы данных */
        private void TableSave()
        {
            if (_sqlServer.ExecuteUpdate(_dataSet, "places"))
            {
                TableUpdate();
            }
        }


        private void Places_Load(object sender, EventArgs e)
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
    }
}
