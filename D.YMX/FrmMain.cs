using D.YMX.Models;
using D.YMX.Properties;
using D.YMX.Utils;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security.Policy;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Forms;

namespace D.YMX
{
    public partial class FrmMain : Form
    {
        #region 主界面操作


        #region 窗体缩放，移动

        /// <summary>
        /// 双击放大缩小
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void panleTop_DoubleClick(object sender, EventArgs e)
        {
            MaxMinForm();
        }

        /// <summary>
        /// 拖动窗体
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PanleTop_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (e.Clicks == 1)
            {
                //窗体移动
                if (e.Button == MouseButtons.Left)
                {
                    ReleaseCapture(); //释放鼠标捕捉
                    //发送左键点击的消息至该窗体(标题栏)
                    SendMessage(this.Handle, 0xA1, 0x02, 0);
                }
            }
        }

        /// <summary>
        /// 窗体放大缩小
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FrmMain_SizeChanged(object sender, EventArgs e)
        {
            if (this.WindowState != FormWindowState.Maximized)
                btnMax.BackgroundImage = Resources.max;

            TitleAutoResize();
        }

        #endregion

        #region 关闭

        private void Main_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        #endregion

        #region 窗体移动

        //窗体移动
        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();

        [DllImport("user32.dll")]
        public static extern bool SendMessage(IntPtr hwnd, int wMsg, int wParam, int lParam);

        #endregion

        #region 窗体缩放

        //窗体缩放
        const int Guying_HTLEFT = 10;
        const int Guying_HTRIGHT = 11;
        const int Guying_HTTOP = 12;
        const int Guying_HTTOPLEFT = 13;
        const int Guying_HTTOPRIGHT = 14;
        const int Guying_HTBOTTOM = 15;
        const int Guying_HTBOTTOMLEFT = 0x10;
        const int Guying_HTBOTTOMRIGHT = 17;

        protected override void WndProc(ref Message m)
        {
            switch (m.Msg)
            {
                case 0x0084:
                    base.WndProc(ref m);
                    Point vPoint = new Point((int)m.LParam & 0xFFFF,
                        (int)m.LParam >> 16 & 0xFFFF);
                    vPoint = PointToClient(vPoint);
                    if (this.WindowState != FormWindowState.Normal)
                        break; //源代码在窗体最大化状态下，鼠标移到窗口边缘也会出现拖动标识，添加这句代码可以避免
                    if (vPoint.X <= 5)
                        if (vPoint.Y <= 5)
                            m.Result = (IntPtr)Guying_HTTOPLEFT;
                        else if (vPoint.Y >= ClientSize.Height - 5)
                            m.Result = (IntPtr)Guying_HTBOTTOMLEFT;
                        else m.Result = (IntPtr)Guying_HTLEFT;
                    else if (vPoint.X >= ClientSize.Width - 5)
                        if (vPoint.Y <= 5)
                            m.Result = (IntPtr)Guying_HTTOPRIGHT;
                        else if (vPoint.Y >= ClientSize.Height - 5)
                            m.Result = (IntPtr)Guying_HTBOTTOMRIGHT;
                        else m.Result = (IntPtr)Guying_HTRIGHT;
                    else if (vPoint.Y <= 2)
                        m.Result = (IntPtr)Guying_HTTOP;
                    else if (vPoint.Y >= ClientSize.Height - 5)
                        m.Result = (IntPtr)Guying_HTBOTTOM;
                    break;
                default:
                    base.WndProc(ref m);
                    break;
            }
        }

        #endregion

        #region 最大化-快捷键

        private void btnMax_Click(object sender, EventArgs e)
        {
            MaxMinForm();
        }

        public void MaxMinForm()
        {
            if (this.WindowState == FormWindowState.Maximized)
            {
                //如果处于最大化，则还原

                this.WindowState = FormWindowState.Normal;
                Image backImage = Resources.max;
                btnMax.BackgroundImage = backImage;
            }
            else
            {
                //如果处于普通状态，则最大化

                this.WindowState = FormWindowState.Maximized;
                Image backImage = Resources.revert;
                btnMax.BackgroundImage = backImage;
            }

            TitleAutoResize();
        }

        #endregion

        #region 最小化-快捷键

        private void btnMin_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized; //最小化
        }

        #endregion

        #region 关闭窗体

        private void buttonClose_Click(object sender, EventArgs e)
        {
            this.Close(); //关闭窗口
        }

        #endregion

        #region 关闭弹窗-快捷键-ESC

        /// <summary>
        /// 快捷键
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="keyData"></param>
        /// <returns></returns>
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (msg.Msg == 256 | msg.Msg == 260)
            {
                if (keyData == Keys.Escape)
                {
                    //this.Close();
                }
            }

            return false;
        }

        #endregion

        #region 四周圆角

        private void Form_Resize(object sender, EventArgs e)
        {
            SetWindowRegion();
        }

        public void SetWindowRegion()
        {
            GraphicsPath formPath = new GraphicsPath();
            Rectangle rect = new Rectangle(0, 0, this.Width, this.Height);
            formPath = GetRoundedRectPath(rect, 5);
            this.Region = new Region(formPath);
        }

        private GraphicsPath GetRoundedRectPath(Rectangle rect, int radius)
        {
            int diameter = radius;
            Rectangle arcRect = new Rectangle(rect.Location, new Size(diameter, diameter));
            GraphicsPath path = new GraphicsPath();

            // 左上角
            path.AddArc(arcRect, 180, 90);

            // 右上角
            arcRect.X = rect.Right - diameter;
            path.AddArc(arcRect, 270, 90);

            // 右下角
            arcRect.Y = rect.Bottom - diameter;
            path.AddArc(arcRect, 0, 90);

            // 左下角
            arcRect.X = rect.Left;
            path.AddArc(arcRect, 90, 90);
            path.CloseFigure(); //闭合曲线
            return path;
        }

        #endregion

        #region Title居中

        private void TitleAutoResize()
        {
            int x = (int)(0.5 * (this.Width - lblTitle.Width));
            int y = lblTitle.Location.Y;
            lblTitle.Location = new Point(x, y);
        }

        #endregion

        #endregion

        #region 初始化


        public FrmMain()
        {
            InitializeComponent();

            this.MaximizedBounds = Screen.PrimaryScreen.WorkingArea;
            // 设置无边框模式
            this.Text = string.Empty;
            this.ControlBox = false;
            this.FormBorderStyle = FormBorderStyle.None;
            this.FormClosed += Main_FormClosed;
            // 四周圆角重绘
            this.Resize += Form_Resize;

            // 拖动窗体
            this.panleTop.MouseDown += PanleTop_MouseDown;
            this.panleTop.DoubleClick += panleTop_DoubleClick;

            // 窗体大小改变，放大还原
            this.SizeChanged += FrmMain_SizeChanged;

            Win32Util.SetFormRoundRectRgn(this);
            Win32Util.SetFormShadow(this);

            this.btnStart.Enabled = true;
            this.btnStop.Enabled = false;
            this.btnExport.Enabled = false;

            foreach (var item in YaMaXunUtil.Countries)
            {
                this.cboCountry.Items.Add(item.Name);
                this.cboCountry2.Items.Add(item.Name);
                this.cboCountry3.Items.Add(item.Name);
                this.cboCountry4.Items.Add(item.Name);
                this.cboCountry5.Items.Add(item.Name);
            }

            this.dgvKeyWordsProduct.SetGridDataViewStyle();
            this.dgvTypeProduct.SetGridDataViewStyle();
            this.dgvShopProduct.SetGridDataViewStyle();
            this.dgvLinkProduct.SetGridDataViewStyle();
            this.dgvResult.SetGridDataViewStyle();

            foreach (var item in Cache.Keywords)
            {
                this.cboProdKeyWords.Items.Add(item);
            }

            // 绑定列
            foreach (var item in columnArry)
            {
                ControlUtil.CreateColumn(item.Item1, item.Item2, new List<DataGridView>()
                {
                    dgvKeyWordsProduct,dgvTypeProduct,dgvShopProduct,dgvLinkProduct,dgvResult
                });
            }

            this.cboProdKeyWords.Text = "chairs";
        }
        private static List<Tuple<string, string>> columnArry = new List<Tuple<string, string>>()
        {
            new Tuple<string, string>("Asin","Asin"),
            new Tuple<string, string>("商品地址","Domain"),
            new Tuple<string, string>("商品标题","Name"),
            new Tuple<string, string>("图片","Img"),
            new Tuple<string, string>("颜色","Color"),
            new Tuple<string, string>("尺寸","Size"),
            new Tuple<string, string>("价格","Price"),
            new Tuple<string, string>("评论数","CommentTotal"),
            new Tuple<string, string>("星级","StartLevel"),
            new Tuple<string, string>("排名","Ranking"),
            new Tuple<string, string>("从哪里发货","Address"),
            new Tuple<string, string>("品牌名","Brand"),
            new Tuple<string, string>("是否注册","IsRegister"),
            new Tuple<string, string>("是否自发货","AutomaticShipping"),
            new Tuple<string, string>("是否僵尸产品","IsOffline"),
            new Tuple<string, string>("BSR","BSR"),
            new Tuple<string, string>("上架时间","ListingTime"),

        };
        #endregion

        #region 关键词采集
        /// <summary>
        /// 拓展关键词
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void btnKeyWords_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.cboProdKeyWords.Text))
            {
                MessageBox.Show("请输入产品关键词"); return;
            }
            FrmKeyWords frmKeyWords = new FrmKeyWords(this.cboProdKeyWords.Text, this.cboCountry.Text);
            frmKeyWords.ShowDialog();
        }

        private CancellationToken cancellationToken;
        private CancellationTokenSource cts;

        /// <summary>
        /// 开始
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void btnStart_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.cboProdKeyWords.Text))
            {
                MessageBox.Show("请输入产品关键词"); return;

            }

            //if (this.checkBoxOpenAuth.Checked)
            //{
            //    MessageBox.Show("验证开启时请先获取Cookie!"); return;
            //}

            this.btnStart.Enabled = false;
            this.btnStop.Enabled = true;

            cts = new CancellationTokenSource();
            cancellationToken = cts.Token;

            CountryEntity yaMaXunCountry = YaMaXunUtil.GetCountryCode(this.cboCountry.Text);
            var keyWrods = this.cboProdKeyWords.Text;

            // 获取最大页数量
            var total = await ApiUtil.GetTotalAsync(yaMaXunCountry, keyWrods);

            // 列表页面的Asin
            var asins = new List<string>();
            for (int i = 0; i < total; i++)
            {
                var res = await ApiUtil.GetAsinListAsync(yaMaXunCountry, keyWrods, (i + 1).ToString());
                if (res != null)
                {
                    asins.AddRange(res);
                }
            }

            // 获取所有商品的Asins
            List<string> allAsins = new List<string>();
            List<Task<List<string>>> tasks = new List<Task<List<string>>>();
            for (int i = 0; i < asins.Count; i++)
            {
                //var asin = asins[i];
                //tasks.Add(ApiUtil.GetAllAsins(yaMaXunCountry, asin));

                var colorAsins = await ApiUtil.GetAllAsins(yaMaXunCountry, asins[i]);
                if (colorAsins != null)
                {
                    allAsins.AddRange(colorAsins);
                }
                else
                {
                    allAsins.Add(asins[i]);
                }
            }

            //await Task.WhenAll(tasks);

            // 采集产品详情
            var list = new List<Product>();
            foreach (var asin in allAsins)
            {
                var product = await ApiUtil.GetDetailAsync(yaMaXunCountry, asin);
                if (product != null)
                {
                    product.Asin = asin;
                    list.Add(product);
                }
            }

            this.dgvKeyWordsProduct.DataSource = list;
            this.btnStart.Enabled = true;
            this.btnStop.Enabled = true;
            this.btnExport.Enabled = true;
        }

        /// <summary>
        /// 停止
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnStop_Click(object sender, EventArgs e)
        {
            cts.CancelAfter(3000);
            this.btnStart.Enabled = true;
            this.btnStop.Enabled = true;
            this.btnExport.Enabled = true;
        }

        /// <summary>
        /// 导出CSV
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnExport_Click(object sender, EventArgs e)
        {
            try
            {
                ExportCsvByDataGridView(this.dgvKeyWordsProduct);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        //private async void BrowerEnd(object sender, FrameLoadEndEventArgs e)
        //{
        //    this.BeginInvoke(new Action(() =>
        //    {

        //    }));
        //}
        #endregion

        #region 类目采集

        #endregion

        #region 店铺采集

        #endregion

        #region 链接采集

        #endregion

        #region 结果筛选

        #endregion 

        #region 数据导出CSV
        /// <summary>
        /// 数据导出
        /// </summary>
        /// <param name="dataGridView"></param>
        /// <returns></returns>
        private bool ExportCsvByDataGridView(DataGridView dataGridView)
        {
            if (dataGridView.Rows.Count == 0)
            {
                MessageBox.Show("没有数据可导出!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "CSV files (*.csv)|*.csv";
            saveFileDialog.FilterIndex = 0;
            saveFileDialog.RestoreDirectory = true;
            saveFileDialog.CreatePrompt = true;
            saveFileDialog.FileName = null;
            saveFileDialog.Title = "保存";
            DateTime now = DateTime.Now;
            saveFileDialog.FileName = $"{DateTime.Now.ToString("yyyyMMddhhmmssfff")}.csv";
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                Stream stream = saveFileDialog.OpenFile();
                StreamWriter sw = new StreamWriter(stream, System.Text.Encoding.UTF8);
                string strLine = "";
                try
                {
                    //表头
                    for (int i = 0; i < dataGridView.ColumnCount; i++)
                    {
                        if (i > 0) strLine += ",";
                        strLine += dataGridView.Columns[i].HeaderText;
                    }
                    strLine.Remove(strLine.Length - 1);
                    sw.WriteLine(strLine);
                    strLine = "";
                    //表的内容
                    for (int j = 0; j < dataGridView.Rows.Count; j++)
                    {
                        strLine = "";
                        int colCount = dataGridView.Columns.Count;
                        for (int k = 0; k < colCount; k++)
                        {
                            if (k > 0 && k < colCount) strLine += ",";
                            if (dataGridView.Rows[j].Cells[k].Value == null) strLine += "";
                            else
                            {
                                string cell = dataGridView.Rows[j].Cells[k].Value.ToString().Trim();
                                //防止里面含有特殊符号
                                cell = cell.Replace("\"", "\"\"");
                                cell = "\"" + cell + "\"";
                                strLine += cell;
                            }
                        }
                        sw.WriteLine(strLine);
                    }
                    sw.Close();
                    stream.Close();
                    MessageBox.Show("数据被导出到：" + saveFileDialog.FileName.ToString(), "导出完毕", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "导出错误", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return false;
                }
            }
            return true;
        }
        #endregion

    }
}