using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace D.YMX.Utils
{
    public static class ControlUtil
    {
        /// <summary>
        /// 设置DataGrid的默认样式
        /// </summary>
        /// <param name="dataGridView"></param>

        public static void SetGridDataViewStyle(this DataGridView dataGridView)
        {
            #region DataGridVeiw Style

            dataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            // 背景颜色
            dataGridView.BackgroundColor = Color.White;
            // 边框样式
            dataGridView.BorderStyle = BorderStyle.Fixed3D;
            // 列标题的高度根据所有列标题单元格的内容进行调整
            dataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            // 单元格颜色
            dataGridView.GridColor = Color.FromArgb(240, 242, 245);
            // 字体样式
            dataGridView.Font = new Font("思源黑体 CN Regular", 14F, FontStyle.Regular, GraphicsUnit.Pixel, ((byte)(134)));
            // 是否只读
            //dataGridView.ReadOnly = true;
            // 行头部是否显示
            dataGridView.RowHeadersVisible = false;
            //// 行头部宽度显示
            //dataGridView.RowHeadersWidth = 62;
            // 设置选择为单元格模式
            dataGridView.SelectionMode = DataGridViewSelectionMode.CellSelect;
            // 多了一行空白
            dataGridView.AllowUserToAddRows = false;
            // 禁止自动创建Column aurosizerowsmode
            dataGridView.AutoGenerateColumns = false;
            // 行高
            dataGridView.RowTemplate.Height = 28;
            //dataGridView.RowTemplate.ReadOnly = true;
            // 列填充满整个内容
            dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            // 列对齐方式为居左对齐
            dataGridView.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            // 设置该值为Flase可以取消grid的表头突出显示
            dataGridView.EnableHeadersVisualStyles = false;

            dataGridView.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;

            // 头部列的背景颜色
            dataGridView.RowHeadersDefaultCellStyle.SelectionForeColor = Color.FromArgb(30, 42, 120);
            dataGridView.RowHeadersDefaultCellStyle.SelectionBackColor = Color.FromArgb(213, 216, 230);
            dataGridView.RowHeadersDefaultCellStyle.ForeColor = Color.FromArgb(244, 244, 248);

            dataGridView.AllowUserToDeleteRows = false;

            // 每行的默认颜色
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            dataGridViewCellStyle1.BackColor = Color.FromArgb(244, 244, 248);
            dataGridView.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;

            // 隔行的样式
            DataGridViewCellStyle dataGridViewCellStyle = new DataGridViewCellStyle();
            dataGridViewCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;//211, 223, 240
            dataGridViewCellStyle.BackColor = Color.FromArgb(213, 216, 230);
            dataGridViewCellStyle.Font = new Font("思源黑体 CN Regular", 14, FontStyle.Regular, GraphicsUnit.Pixel, ((byte)(134)));
            dataGridViewCellStyle.ForeColor = Color.FromArgb(30, 42, 120);
            dataGridViewCellStyle.SelectionBackColor = Color.FromArgb(213, 216, 230);
            dataGridViewCellStyle.SelectionForeColor = Color.FromArgb(30, 42, 120);
            dataGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle;

            #endregion
        }

        public static void CreateColumn(string name, string dataPropertyName, List<DataGridView> dataGridViews)
        {
            foreach (var item in dataGridViews)
            {
                DataGridViewTextBoxColumn keyBoxColumn = new DataGridViewTextBoxColumn();
                keyBoxColumn.CellTemplate = new DataGridViewTextBoxCell();
                keyBoxColumn.ReadOnly = true;
                keyBoxColumn.Name = name;
                keyBoxColumn.HeaderText = name;
                keyBoxColumn.DataPropertyName = dataPropertyName;

                item.Columns.Add(keyBoxColumn);
            }
        }

    }
}
