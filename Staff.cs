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
    public partial class Staff : Form
    {
        public Staff()
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
            _dataSet.DataSetName = "employee";
            _sqlServer.SelectSqlCommand = "SELECT * FROM employee";

            _sqlServer.InsertSqlCommand = "INSERT INTO employee (employee_name, employee_year_birth, employee_date_work, employee_sex, employee_amount_children, employee_salary, employee_position, employee_height, employee_vocal, employee_instrument, employee_genre, employee_title, employee_laureate)" +
                                        " VALUES (@employee_name, @employee_year_birth, @employee_date_work, @employee_sex, @employee_amount_children, @employee_salary, @employee_position, @employee_height, @employee_vocal, @employee_instrument, @employee_genre, @employee_title, @employee_laureate)";
            _sqlServer.InsertParametersAdd("@employee_name", SqlDbType.VarChar, 255, "employee_name");
            _sqlServer.InsertParametersAdd("@employee_year_birth", SqlDbType.DateTime, 10, "employee_year_birth");
            _sqlServer.InsertParametersAdd("@employee_date_work", SqlDbType.DateTime, 10, "employee_date_work");
            _sqlServer.InsertParametersAdd("@employee_sex", SqlDbType.VarChar, 50, "employee_sex");
            _sqlServer.InsertParametersAdd("@employee_amount_children", SqlDbType.Int, 3, "employee_amount_children");
            _sqlServer.InsertParametersAdd("@employee_salary", SqlDbType.Money, 10, "employee_salary");
            _sqlServer.InsertParametersAdd("@employee_position", SqlDbType.VarChar, 50, "employee_position");
            _sqlServer.InsertParametersAdd("@employee_height", SqlDbType.Int, 3, "employee_height");
            _sqlServer.InsertParametersAdd("@employee_vocal", SqlDbType.VarChar, 50, "employee_vocal");
            _sqlServer.InsertParametersAdd("@employee_instrument", SqlDbType.VarChar, 50, "employee_instrument");
            _sqlServer.InsertParametersAdd("@employee_genre", SqlDbType.VarChar, 50, "employee_genre");
            _sqlServer.InsertParametersAdd("@employee_title", SqlDbType.VarChar, 255, "employee_title");
            _sqlServer.InsertParametersAdd("@employee_laureate", SqlDbType.VarChar, 255, "employee_laureate");
            _sqlServer.InsertParametersAdd("@employee_id", SqlDbType.Int, 11, "employee_id");

            _sqlServer.UpdateSqlCommand = "UPDATE employee SET employee_name = @employee_name, employee_year_birth = @employee_year_birth, employee_date_work = @employee_date_work, employee_sex = @employee_sex, employee_amount_children = @employee_amount_children, employee_salary = @employee_salary, employee_position = @employee_position, employee_height = @employee_height, employee_vocal = @employee_vocal, employee_instrument = @employee_instrument, employee_genre = @employee_genre, employee_title = @employee_title, employee_laureate = @employee_laureate" +
                                        " WHERE (employee_id = @employee_id)";
            _sqlServer.UpdateParametersAdd("@employee_name", SqlDbType.VarChar, 255, "employee_name");
            _sqlServer.UpdateParametersAdd("@employee_year_birth", SqlDbType.DateTime, 10, "employee_year_birth");
            _sqlServer.UpdateParametersAdd("@employee_date_work", SqlDbType.DateTime, 10, "employee_date_work");
            _sqlServer.UpdateParametersAdd("@employee_sex", SqlDbType.VarChar, 50, "employee_sex");
            _sqlServer.UpdateParametersAdd("@employee_amount_children", SqlDbType.Int, 3, "employee_amount_children");
            _sqlServer.UpdateParametersAdd("@employee_salary", SqlDbType.Money, 10, "employee_salary");
            _sqlServer.UpdateParametersAdd("@employee_position", SqlDbType.VarChar, 50, "employee_position");
            _sqlServer.UpdateParametersAdd("@employee_height", SqlDbType.Int, 3, "employee_height");
            _sqlServer.UpdateParametersAdd("@employee_vocal", SqlDbType.VarChar, 50, "employee_vocal");
            _sqlServer.UpdateParametersAdd("@employee_instrument", SqlDbType.VarChar, 50, "employee_instrument");
            _sqlServer.UpdateParametersAdd("@employee_genre", SqlDbType.VarChar, 50, "employee_genre");
            _sqlServer.UpdateParametersAdd("@employee_title", SqlDbType.VarChar, 255, "employee_title");
            _sqlServer.UpdateParametersAdd("@employee_laureate", SqlDbType.VarChar, 255, "employee_laureate");
            _sqlServer.UpdateParametersAdd("@employee_id", SqlDbType.Int, 11, "employee_id");

            _sqlServer.DeleteSqlCommand = "DELETE FROM employee WHERE (employee_id = @employee_id)";
            _sqlServer.DeleteParametersAdd("@employee_id", SqlDbType.Int, 11, "employee_id");

            if (_sqlServer.ExecuteFill(_dataSet, "employee"))
            {
                _bindingSource.DataSource = _dataSet;
                _bindingSource.DataMember = "employee";
                bindingNavigator1.BindingSource = _bindingSource;
                dataGridView1.DataSource = _bindingSource;
            }
        }

        /* Обновление данных в таблице */
        private void TableUpdate()
        {
            _dataSet.Clear();
            _dataSet.DataSetName = "employee";
            _sqlServer.SelectSqlCommand = "SELECT * FROM employee";
            if (_sqlServer.ExecuteFill(_dataSet, "employee"))
            {
                _bindingSource.DataSource = _dataSet;
                _bindingSource.DataMember = "employee";
                bindingNavigator1.BindingSource = _bindingSource;
                dataGridView1.DataSource = _bindingSource;
            }
        }

        /* Сохранение данных а таблице базы данных */
        private void TableSave()
        {
            if (_sqlServer.ExecuteUpdate(_dataSet, "employee"))
            {
                TableUpdate();
            }
        }

        private void Staff_Load(object sender, EventArgs e)
        {
            /* Инициализация объекта ДатаВремя*/
            FieldTableDate ftd1 = new FieldTableDate(dataGridView1, 2);
            FieldTableDate ftd2 = new FieldTableDate(dataGridView1, 3);

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
