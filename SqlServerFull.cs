using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Theater
{
    class SqlServerFull
    {
        /* Переменные */
        private SqlConnection _connection;
        private SqlCommand _selectCommand;
        private SqlCommand _insertCommand;
        private SqlCommand _updateCommand;
        private SqlCommand _deleteCommand;
        private SqlDataAdapter _dataAdapter;
        private String _strSelect;
        private String _strInsert;
        private String _strUpdate;
        private String _strDelete;

        /* Конструктор */
        public SqlServerFull()
        {
            _connection = new SqlConnection();
            //_connection.ConnectionString = "server=SOMOV-PC\\SQLEXPRESS;uid=sa;password=12345;database=theater";
            _connection.ConnectionString = "server=" + ConnectConfig.server + ";uid=" + ConnectConfig.uid + ";password=" + ConnectConfig.password + ";database=" + ConnectConfig.database;
            _selectCommand = new SqlCommand("", _connection);
            _insertCommand = new SqlCommand("", _connection);
            _updateCommand = new SqlCommand("", _connection);
            _deleteCommand = new SqlCommand("", _connection);
            _dataAdapter = new SqlDataAdapter();
        }

        /* Свойства */
        public String SelectSqlCommand
        {
            get { return _strSelect; }
            set { _strSelect = value; }
        }

        public String InsertSqlCommand
        {
            get { return _strInsert; }
            set { _strInsert = value; }
        }

        public String UpdateSqlCommand
        {
            get { return _strUpdate; }
            set { _strUpdate = value; }
        }

        public String DeleteSqlCommand
        {
            get { return _strDelete; }
            set { _strDelete = value; }
        }

        /* Методы */
        public bool ExecuteFill(DataSet _DataSet, String _tableName)
        {
            try
            {
                _connection.Open();
                _selectCommand.CommandText = _strSelect;
                _insertCommand.CommandText = _strInsert;
                _updateCommand.CommandText = _strUpdate;
                _deleteCommand.CommandText = _strDelete;
                _dataAdapter.SelectCommand = _selectCommand;
                _dataAdapter.InsertCommand = _insertCommand;
                _dataAdapter.UpdateCommand = _updateCommand;
                _dataAdapter.DeleteCommand = _deleteCommand;
                _dataAdapter.Fill(_DataSet, _tableName);
                _connection.Close();
                return true;
            }
            catch (Exception ex)
            {
                _connection.Close();
                if(MessageBox.Show("Ошибка выполнения SQL запроса." + System.Environment.NewLine + "Показать полное сообщение?","Ошибка:", MessageBoxButtons.YesNo) == DialogResult.Yes)	//Сообщение об ошибке
                {
                    MessageBox.Show(ex.ToString());
                }
                return false; //произошла ошибка.
            }
        }

        public bool ExecuteUpdate(DataSet _DataSet, String _tableName)
        {
            try
            {
                _connection.Open();
                _selectCommand.CommandText = _strSelect;
                _insertCommand.CommandText = _strInsert;
                _updateCommand.CommandText = _strUpdate;
                _deleteCommand.CommandText = _strDelete;
                _dataAdapter.SelectCommand = _selectCommand;
                _dataAdapter.InsertCommand = _insertCommand;
                _dataAdapter.UpdateCommand = _updateCommand;
                _dataAdapter.DeleteCommand = _deleteCommand;
                _dataAdapter.Update(_DataSet, _tableName);
                _connection.Close();
                return true;
            }
            catch (Exception ex)
            {
                _connection.Close();
                if (MessageBox.Show("Ошибка выполнения SQL запроса." + System.Environment.NewLine + "Показать полное сообщение?", "Ошибка:", MessageBoxButtons.YesNo) == DialogResult.Yes)	//Сообщение об ошибке
                {
                    MessageBox.Show(ex.ToString());
                }
                return false; //произошла ошибка.

            }
        }

        public void SelectParametersAdd(String parameterName, SqlDbType dbType, int size, String sourceColumn)
        {
            _selectCommand.Parameters.Add(parameterName, dbType, size, sourceColumn);
        }

        public void InsertParametersAdd(String parameterName, SqlDbType dbType, int size, String sourceColumn)
        {
            _insertCommand.Parameters.Add(parameterName, dbType, size, sourceColumn);
        }

        public void UpdateParametersAdd(String parameterName, SqlDbType dbType, int size, String sourceColumn)
        {
            _updateCommand.Parameters.Add(parameterName, dbType, size, sourceColumn);
        }

        public void DeleteParametersAdd(String parameterName, SqlDbType dbType, int size, String sourceColumn)
        {
            _deleteCommand.Parameters.Add(parameterName, dbType, size, sourceColumn);
        }
		

    }
}
