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
                   
        /* Загрузка данных */
        private void TableLoad()
        {
            _dataSet.Clear();
            _dataSet.DataSetName = "author";
            _sqlServer.SelectSqlCommand = "SELECT * FROM author";

            _sqlServer.InsertSqlCommand = "INSERT INTO author (author_name, author_year_birth, author_age, author_country) VALUES (@author_name, @author_year_birth, @author_age, @author_country)";
            _sqlServer.InsertParametersAdd("@author_name", SqlDbType.VarChar, 255, "author_name");
            _sqlServer.InsertParametersAdd("@author_year_birth", SqlDbType.DateTime, 10, "author_year_birth");
            _sqlServer.InsertParametersAdd("@author_age", SqlDbType.Int, 3, "author_age");
            _sqlServer.InsertParametersAdd("@author_country", SqlDbType.Char, 255, "author_country");
            _sqlServer.InsertParametersAdd("@author_id", SqlDbType.Int, 11, "author_id");

            _sqlServer.UpdateSqlCommand = "UPDATE author SET author_name = @author_name, author_year_birth = @author_year_birth, author_age = @author_age, author_country = @author_country WHERE (author_id = @author_id)";
            _sqlServer.UpdateParametersAdd("@author_name", SqlDbType.VarChar, 255, "author_name");
            _sqlServer.UpdateParametersAdd("@author_year_birth", SqlDbType.DateTime, 10, "author_year_birth");
            _sqlServer.UpdateParametersAdd("@author_age", SqlDbType.Int, 3, "author_age");
            _sqlServer.UpdateParametersAdd("@author_country", SqlDbType.Char, 255, "author_country");
            _sqlServer.UpdateParametersAdd("@author_id", SqlDbType.Int, 11, "author_id");

            _sqlServer.DeleteSqlCommand = "DELETE FROM author WHERE (author_id = @author_id)";
            _sqlServer.DeleteParametersAdd("@author_id", SqlDbType.Int, 11, "author_id");
            
            if (_sqlServer.ExecuteFill(_dataSet, "author"))
            {
                _bindingSource.DataSource = _dataSet;
                _bindingSource.DataMember = "author";
                bindingNavigator1.BindingSource = _bindingSource;
                dataGridView1.DataSource = _bindingSource;
            }
        }

        /* Обновление данных в таблице */
        private void TableUpdate()
        {
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

        /* Сохранение данных а таблице базы данных */
        private void TableSave()
        {
            if(_sqlServer.ExecuteUpdate(_dataSet, "author")){
                TableUpdate();
            }
        }

        private void Authors_Load(object sender, EventArgs e)
        {
            /* Инициализация объекта ДатаВремя*/
            FieldTableDate ftd = new FieldTableDate(dataGridView1, 2);

            /* Подключение к базе данных, выбор данных из таблицы */
            TableLoad();
        }

        /* Обновить информацию в таблице */
        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            TableUpdate();
        }

        /* Обновить данные в базе данных */
        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            TableSave();
        }

    }
}
