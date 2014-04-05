using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Theater
{
    class SqlServerShort
    {
        /* Переменные */
        private SqlConnection _connection;
        private SqlCommand _command;
        private String _sqlCommand;

        /* Конструктор */
        public SqlServerShort()
        {
            _connection = new SqlConnection();
            //_connection.ConnectionString = "server=SOMOV-PC\\SQLEXPRESS;uid=sa;password=12345;database=theater";
            _connection.ConnectionString = "server=" + ConnectConfig.server + ";uid=" + ConnectConfig.uid + ";password=" + ConnectConfig.password + ";database=" + ConnectConfig.database;
            _command = new SqlCommand("", _connection);
        }

        /* свойства */
        public String SqlCommand
        {
            get { return _sqlCommand; }
            set { _sqlCommand = value; }
        }

        /* Методы */
        public bool ExecuteNonQuery()
        {
            try
            {
                _connection.Open();
                _command.CommandText = _sqlCommand;
                _command.ExecuteNonQuery();	//выполнение запроса
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
    }
}
