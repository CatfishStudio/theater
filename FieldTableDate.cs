using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Theater
{
    class FieldTableDate
    {
        private DateTimePicker _dateTimePicker;
        private DataGridView _dataGridView;
        private int _indexColumn;

        public FieldTableDate(DataGridView _dGridView, int _columnIndex)
        {
            /* Инициализация объекта ДатаВремя*/
            _dateTimePicker = new DateTimePicker();
            _dateTimePicker.Format = DateTimePickerFormat.Short;
            _dateTimePicker.Visible = false;
            _dateTimePicker.Width = 100;
            _dGridView.Controls.Add(_dateTimePicker);
            _dateTimePicker.ValueChanged += this.dateTimePicker_ValueChanged;
            _dGridView.CellBeginEdit += this.dataGridView_CellBeginEdit;
            _dGridView.CellEndEdit += this.dataGridView_CellEndEdit;
            _dataGridView = _dGridView;
            _indexColumn = _columnIndex;
        }

        /* Начало редактирование ячейки */
        private void dataGridView_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            try
            {
                if ((_dataGridView.Focused) && (_dataGridView.CurrentCell.ColumnIndex == _indexColumn))
                {
                    _dateTimePicker.Location = _dataGridView.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, false).Location;
                    _dateTimePicker.Visible = true;
                    if (_dataGridView.CurrentCell.Value != DBNull.Value)
                    {
                        _dateTimePicker.Value = (DateTime)_dataGridView.CurrentCell.Value;
                    }
                    else
                    {
                        _dateTimePicker.Value = DateTime.Today;
                    }
                }
                else
                {
                    _dateTimePicker.Visible = false;
                }
            }
            catch (Exception ex)
            {
                if (MessageBox.Show("Произошла ошибка!" + System.Environment.NewLine + "Показать полное сообщение?", "Ошибка:", MessageBoxButtons.YesNo) == DialogResult.Yes)	//Сообщение об ошибке
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        }

        /* Завершение редактирвоания ячейки */
        private void dataGridView_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if ((_dataGridView.Focused) && (_dataGridView.CurrentCell.ColumnIndex == _indexColumn))
                {
                    _dataGridView.CurrentCell.Value = _dateTimePicker.Value.Date;
                    _dateTimePicker.Visible = false;
                }
            }
            catch (Exception ex)
            {
                if (MessageBox.Show("Произошла ошибка!" + System.Environment.NewLine + "Показать полное сообщение?", "Ошибка:", MessageBoxButtons.YesNo) == DialogResult.Yes)	//Сообщение об ошибке
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        }

        /* Событие: выбор даты */
        private void dateTimePicker_ValueChanged(object sender, EventArgs e)
        {
            _dataGridView.CurrentCell.Value = _dateTimePicker.Text;
        }

    }
}
