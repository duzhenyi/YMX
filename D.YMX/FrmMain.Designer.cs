using System.Windows.Forms;

namespace D.YMX
{
    partial class FrmMain
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMain));
            tabContent = new TabControl();
            tabKeywords = new TabPage();
            btnExport = new Button();
            dgvKeyWordsProduct = new DataGridView();
            btnStop = new Button();
            btnStart = new Button();
            checkBoxProxy = new CheckBox();
            btnKeyWords = new Button();
            cboProdKeyWords = new ComboBox();
            cboCountry = new ComboBox();
            label2 = new Label();
            label1 = new Label();
            tabProductType = new TabPage();
            cboCountry2 = new ComboBox();
            label19 = new Label();
            dgvTypeProduct = new DataGridView();
            button2 = new Button();
            button3 = new Button();
            checkBox2 = new CheckBox();
            checkBox1 = new CheckBox();
            textBox1 = new TextBox();
            label3 = new Label();
            tabShop = new TabPage();
            textBox3 = new TextBox();
            dgvShopProduct = new DataGridView();
            button8 = new Button();
            button9 = new Button();
            cboCountry3 = new ComboBox();
            label15 = new Label();
            label16 = new Label();
            tabLinkUrl = new TabPage();
            textBox4 = new TextBox();
            dgvLinkProduct = new DataGridView();
            button10 = new Button();
            button11 = new Button();
            cboCountry4 = new ComboBox();
            label17 = new Label();
            label18 = new Label();
            tabResult = new TabPage();
            splitContainer1 = new SplitContainer();
            groupBox1 = new GroupBox();
            comboBox3 = new ComboBox();
            btnSearch = new Button();
            textBox2 = new TextBox();
            label14 = new Label();
            comboBox6 = new ComboBox();
            label13 = new Label();
            comboBox5 = new ComboBox();
            label12 = new Label();
            cboCountry5 = new ComboBox();
            label11 = new Label();
            label10 = new Label();
            comboBox2 = new ComboBox();
            label9 = new Label();
            comboBox1 = new ComboBox();
            label8 = new Label();
            numericUpDown3 = new NumericUpDown();
            label6 = new Label();
            numericUpDown4 = new NumericUpDown();
            label7 = new Label();
            numericUpDown2 = new NumericUpDown();
            label5 = new Label();
            numericUpDown1 = new NumericUpDown();
            label4 = new Label();
            groupBox2 = new GroupBox();
            dgvResult = new DataGridView();
            button7 = new Button();
            button6 = new Button();
            button5 = new Button();
            button4 = new Button();
            checkBox3 = new CheckBox();
            button1 = new Button();
            panleTop = new Panel();
            lblTitle = new Label();
            btnMin = new Button();
            btnClose = new Button();
            btnMax = new Button();
            tabLog = new TabPage();
            txtLogs = new RichTextBox();
            tabContent.SuspendLayout();
            tabKeywords.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvKeyWordsProduct).BeginInit();
            tabProductType.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvTypeProduct).BeginInit();
            tabShop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvShopProduct).BeginInit();
            tabLinkUrl.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvLinkProduct).BeginInit();
            tabResult.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numericUpDown3).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDown4).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDown2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDown1).BeginInit();
            groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvResult).BeginInit();
            panleTop.SuspendLayout();
            tabLog.SuspendLayout();
            SuspendLayout();
            // 
            // tabContent
            // 
            tabContent.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            tabContent.Controls.Add(tabKeywords);
            tabContent.Controls.Add(tabProductType);
            tabContent.Controls.Add(tabShop);
            tabContent.Controls.Add(tabLinkUrl);
            tabContent.Controls.Add(tabResult);
            tabContent.Controls.Add(tabLog);
            tabContent.Location = new Point(4, 51);
            tabContent.Margin = new Padding(2, 3, 2, 3);
            tabContent.Name = "tabContent";
            tabContent.SelectedIndex = 0;
            tabContent.Size = new Size(2303, 1297);
            tabContent.TabIndex = 0;
            // 
            // tabKeywords
            // 
            tabKeywords.Controls.Add(btnExport);
            tabKeywords.Controls.Add(dgvKeyWordsProduct);
            tabKeywords.Controls.Add(btnStop);
            tabKeywords.Controls.Add(btnStart);
            tabKeywords.Controls.Add(checkBoxProxy);
            tabKeywords.Controls.Add(btnKeyWords);
            tabKeywords.Controls.Add(cboProdKeyWords);
            tabKeywords.Controls.Add(cboCountry);
            tabKeywords.Controls.Add(label2);
            tabKeywords.Controls.Add(label1);
            tabKeywords.Location = new Point(4, 33);
            tabKeywords.Margin = new Padding(2, 3, 2, 3);
            tabKeywords.Name = "tabKeywords";
            tabKeywords.Padding = new Padding(2, 3, 2, 3);
            tabKeywords.Size = new Size(2295, 1260);
            tabKeywords.TabIndex = 0;
            tabKeywords.Text = "关键词采集";
            tabKeywords.UseVisualStyleBackColor = true;
            // 
            // btnExport
            // 
            btnExport.Location = new Point(1564, 15);
            btnExport.Margin = new Padding(2, 3, 2, 3);
            btnExport.Name = "btnExport";
            btnExport.Size = new Size(112, 35);
            btnExport.TabIndex = 9;
            btnExport.Text = "导出CSV";
            btnExport.UseVisualStyleBackColor = true;
            btnExport.Click += btnExport_Click;
            // 
            // dgvKeyWordsProduct
            // 
            dgvKeyWordsProduct.AllowUserToAddRows = false;
            dgvKeyWordsProduct.AllowUserToDeleteRows = false;
            dgvKeyWordsProduct.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dgvKeyWordsProduct.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvKeyWordsProduct.Location = new Point(6, 72);
            dgvKeyWordsProduct.Margin = new Padding(2, 3, 2, 3);
            dgvKeyWordsProduct.Name = "dgvKeyWordsProduct";
            dgvKeyWordsProduct.ReadOnly = true;
            dgvKeyWordsProduct.RowHeadersWidth = 62;
            dgvKeyWordsProduct.RowTemplate.Height = 32;
            dgvKeyWordsProduct.Size = new Size(2283, 1185);
            dgvKeyWordsProduct.TabIndex = 8;
            // 
            // btnStop
            // 
            btnStop.Location = new Point(1435, 15);
            btnStop.Margin = new Padding(2, 3, 2, 3);
            btnStop.Name = "btnStop";
            btnStop.Size = new Size(112, 35);
            btnStop.TabIndex = 7;
            btnStop.Text = "停止";
            btnStop.UseVisualStyleBackColor = true;
            btnStop.Click += btnStop_Click;
            // 
            // btnStart
            // 
            btnStart.Location = new Point(1298, 15);
            btnStart.Margin = new Padding(2, 3, 2, 3);
            btnStart.Name = "btnStart";
            btnStart.Size = new Size(112, 35);
            btnStart.TabIndex = 6;
            btnStart.Text = "开始";
            btnStart.UseVisualStyleBackColor = true;
            btnStart.Click += btnStart_Click;
            // 
            // checkBoxProxy
            // 
            checkBoxProxy.AutoSize = true;
            checkBoxProxy.Location = new Point(1110, 17);
            checkBoxProxy.Margin = new Padding(2, 3, 2, 3);
            checkBoxProxy.Name = "checkBoxProxy";
            checkBoxProxy.Size = new Size(124, 28);
            checkBoxProxy.TabIndex = 5;
            checkBoxProxy.Text = "开启代理IP";
            checkBoxProxy.UseVisualStyleBackColor = true;
            // 
            // btnKeyWords
            // 
            btnKeyWords.Location = new Point(968, 13);
            btnKeyWords.Margin = new Padding(2, 3, 2, 3);
            btnKeyWords.Name = "btnKeyWords";
            btnKeyWords.Size = new Size(112, 35);
            btnKeyWords.TabIndex = 4;
            btnKeyWords.Text = "拓展关键词";
            btnKeyWords.UseVisualStyleBackColor = true;
            btnKeyWords.Click += btnKeyWords_Click;
            // 
            // cboProdKeyWords
            // 
            cboProdKeyWords.FormattingEnabled = true;
            cboProdKeyWords.Location = new Point(419, 15);
            cboProdKeyWords.Margin = new Padding(2, 3, 2, 3);
            cboProdKeyWords.Name = "cboProdKeyWords";
            cboProdKeyWords.Size = new Size(543, 32);
            cboProdKeyWords.TabIndex = 3;
            // 
            // cboCountry
            // 
            cboCountry.FormattingEnabled = true;
            cboCountry.Location = new Point(81, 15);
            cboCountry.Margin = new Padding(2, 3, 2, 3);
            cboCountry.Name = "cboCountry";
            cboCountry.Size = new Size(182, 32);
            cboCountry.TabIndex = 2;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(295, 19);
            label2.Margin = new Padding(2, 0, 2, 0);
            label2.Name = "label2";
            label2.Size = new Size(118, 24);
            label2.TabIndex = 1;
            label2.Text = "产品关键词：";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(20, 19);
            label1.Margin = new Padding(2, 0, 2, 0);
            label1.Name = "label1";
            label1.Size = new Size(64, 24);
            label1.TabIndex = 0;
            label1.Text = "国家：";
            // 
            // tabProductType
            // 
            tabProductType.Controls.Add(cboCountry2);
            tabProductType.Controls.Add(label19);
            tabProductType.Controls.Add(dgvTypeProduct);
            tabProductType.Controls.Add(button2);
            tabProductType.Controls.Add(button3);
            tabProductType.Controls.Add(checkBox2);
            tabProductType.Controls.Add(checkBox1);
            tabProductType.Controls.Add(textBox1);
            tabProductType.Controls.Add(label3);
            tabProductType.Location = new Point(4, 33);
            tabProductType.Margin = new Padding(2, 3, 2, 3);
            tabProductType.Name = "tabProductType";
            tabProductType.Padding = new Padding(2, 3, 2, 3);
            tabProductType.Size = new Size(2295, 1260);
            tabProductType.TabIndex = 1;
            tabProductType.Text = "类目采集";
            tabProductType.UseVisualStyleBackColor = true;
            // 
            // cboCountry2
            // 
            cboCountry2.FormattingEnabled = true;
            cboCountry2.Location = new Point(75, 15);
            cboCountry2.Margin = new Padding(2, 3, 2, 3);
            cboCountry2.Name = "cboCountry2";
            cboCountry2.Size = new Size(182, 32);
            cboCountry2.TabIndex = 13;
            // 
            // label19
            // 
            label19.AutoSize = true;
            label19.Location = new Point(13, 19);
            label19.Margin = new Padding(2, 0, 2, 0);
            label19.Name = "label19";
            label19.Size = new Size(64, 24);
            label19.TabIndex = 12;
            label19.Text = "国家：";
            // 
            // dgvTypeProduct
            // 
            dgvTypeProduct.AllowUserToAddRows = false;
            dgvTypeProduct.AllowUserToDeleteRows = false;
            dgvTypeProduct.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dgvTypeProduct.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvTypeProduct.Location = new Point(6, 57);
            dgvTypeProduct.Margin = new Padding(2, 3, 2, 3);
            dgvTypeProduct.Name = "dgvTypeProduct";
            dgvTypeProduct.ReadOnly = true;
            dgvTypeProduct.RowHeadersWidth = 62;
            dgvTypeProduct.RowTemplate.Height = 32;
            dgvTypeProduct.Size = new Size(2283, 1185);
            dgvTypeProduct.TabIndex = 11;
            // 
            // button2
            // 
            button2.Location = new Point(1514, 11);
            button2.Margin = new Padding(2, 3, 2, 3);
            button2.Name = "button2";
            button2.Size = new Size(112, 35);
            button2.TabIndex = 10;
            button2.Text = "停止";
            button2.UseVisualStyleBackColor = true;
            // 
            // button3
            // 
            button3.Location = new Point(1377, 11);
            button3.Margin = new Padding(2, 3, 2, 3);
            button3.Name = "button3";
            button3.Size = new Size(112, 35);
            button3.TabIndex = 9;
            button3.Text = "开始";
            button3.UseVisualStyleBackColor = true;
            // 
            // checkBox2
            // 
            checkBox2.AutoSize = true;
            checkBox2.Location = new Point(1263, 13);
            checkBox2.Margin = new Padding(2, 3, 2, 3);
            checkBox2.Name = "checkBox2";
            checkBox2.Size = new Size(108, 28);
            checkBox2.TabIndex = 3;
            checkBox2.Text = "继续爬取";
            checkBox2.UseVisualStyleBackColor = true;
            // 
            // checkBox1
            // 
            checkBox1.AutoSize = true;
            checkBox1.Location = new Point(1126, 15);
            checkBox1.Margin = new Padding(2, 3, 2, 3);
            checkBox1.Name = "checkBox1";
            checkBox1.Size = new Size(131, 28);
            checkBox1.TabIndex = 2;
            checkBox1.Text = "启用Cookie";
            checkBox1.UseVisualStyleBackColor = true;
            // 
            // textBox1
            // 
            textBox1.Location = new Point(433, 13);
            textBox1.Margin = new Padding(2, 3, 2, 3);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(687, 30);
            textBox1.TabIndex = 1;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(273, 17);
            label3.Margin = new Padding(2, 0, 2, 0);
            label3.Name = "label3";
            label3.Size = new Size(154, 24);
            label3.TabIndex = 0;
            label3.Text = "亚马逊类目列表页";
            // 
            // tabShop
            // 
            tabShop.Controls.Add(textBox3);
            tabShop.Controls.Add(dgvShopProduct);
            tabShop.Controls.Add(button8);
            tabShop.Controls.Add(button9);
            tabShop.Controls.Add(cboCountry3);
            tabShop.Controls.Add(label15);
            tabShop.Controls.Add(label16);
            tabShop.Location = new Point(4, 33);
            tabShop.Margin = new Padding(2, 3, 2, 3);
            tabShop.Name = "tabShop";
            tabShop.Padding = new Padding(2, 3, 2, 3);
            tabShop.Size = new Size(2295, 1260);
            tabShop.TabIndex = 3;
            tabShop.Text = "店铺采集";
            tabShop.UseVisualStyleBackColor = true;
            // 
            // textBox3
            // 
            textBox3.Location = new Point(409, 16);
            textBox3.Margin = new Padding(2, 3, 2, 3);
            textBox3.Name = "textBox3";
            textBox3.Size = new Size(871, 30);
            textBox3.TabIndex = 15;
            // 
            // dgvShopProduct
            // 
            dgvShopProduct.AllowUserToAddRows = false;
            dgvShopProduct.AllowUserToDeleteRows = false;
            dgvShopProduct.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dgvShopProduct.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvShopProduct.Location = new Point(6, 63);
            dgvShopProduct.Margin = new Padding(2, 3, 2, 3);
            dgvShopProduct.Name = "dgvShopProduct";
            dgvShopProduct.ReadOnly = true;
            dgvShopProduct.RowHeadersWidth = 62;
            dgvShopProduct.RowTemplate.Height = 32;
            dgvShopProduct.Size = new Size(2283, 1185);
            dgvShopProduct.TabIndex = 14;
            // 
            // button8
            // 
            button8.Location = new Point(1435, 13);
            button8.Margin = new Padding(2, 3, 2, 3);
            button8.Name = "button8";
            button8.Size = new Size(112, 35);
            button8.TabIndex = 13;
            button8.Text = "停止";
            button8.UseVisualStyleBackColor = true;
            // 
            // button9
            // 
            button9.Location = new Point(1298, 13);
            button9.Margin = new Padding(2, 3, 2, 3);
            button9.Name = "button9";
            button9.Size = new Size(112, 35);
            button9.TabIndex = 12;
            button9.Text = "开始";
            button9.UseVisualStyleBackColor = true;
            // 
            // cboCountry3
            // 
            cboCountry3.FormattingEnabled = true;
            cboCountry3.Location = new Point(81, 13);
            cboCountry3.Margin = new Padding(2, 3, 2, 3);
            cboCountry3.Name = "cboCountry3";
            cboCountry3.Size = new Size(182, 32);
            cboCountry3.TabIndex = 11;
            // 
            // label15
            // 
            label15.AutoSize = true;
            label15.Location = new Point(295, 19);
            label15.Margin = new Padding(2, 0, 2, 0);
            label15.Name = "label15";
            label15.Size = new Size(100, 24);
            label15.TabIndex = 10;
            label15.Text = "店铺地址：";
            // 
            // label16
            // 
            label16.AutoSize = true;
            label16.Location = new Point(20, 17);
            label16.Margin = new Padding(2, 0, 2, 0);
            label16.Name = "label16";
            label16.Size = new Size(64, 24);
            label16.TabIndex = 9;
            label16.Text = "国家：";
            // 
            // tabLinkUrl
            // 
            tabLinkUrl.Controls.Add(textBox4);
            tabLinkUrl.Controls.Add(dgvLinkProduct);
            tabLinkUrl.Controls.Add(button10);
            tabLinkUrl.Controls.Add(button11);
            tabLinkUrl.Controls.Add(cboCountry4);
            tabLinkUrl.Controls.Add(label17);
            tabLinkUrl.Controls.Add(label18);
            tabLinkUrl.Location = new Point(4, 33);
            tabLinkUrl.Margin = new Padding(2, 3, 2, 3);
            tabLinkUrl.Name = "tabLinkUrl";
            tabLinkUrl.Padding = new Padding(2, 3, 2, 3);
            tabLinkUrl.Size = new Size(2295, 1260);
            tabLinkUrl.TabIndex = 4;
            tabLinkUrl.Text = "链接采集";
            tabLinkUrl.UseVisualStyleBackColor = true;
            // 
            // textBox4
            // 
            textBox4.Location = new Point(409, 13);
            textBox4.Margin = new Padding(2, 3, 2, 3);
            textBox4.Name = "textBox4";
            textBox4.Size = new Size(871, 30);
            textBox4.TabIndex = 22;
            // 
            // dgvLinkProduct
            // 
            dgvLinkProduct.AllowUserToAddRows = false;
            dgvLinkProduct.AllowUserToDeleteRows = false;
            dgvLinkProduct.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dgvLinkProduct.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvLinkProduct.Location = new Point(6, 59);
            dgvLinkProduct.Margin = new Padding(2, 3, 2, 3);
            dgvLinkProduct.Name = "dgvLinkProduct";
            dgvLinkProduct.ReadOnly = true;
            dgvLinkProduct.RowHeadersWidth = 62;
            dgvLinkProduct.RowTemplate.Height = 32;
            dgvLinkProduct.Size = new Size(2283, 1185);
            dgvLinkProduct.TabIndex = 21;
            // 
            // button10
            // 
            button10.Location = new Point(1435, 11);
            button10.Margin = new Padding(2, 3, 2, 3);
            button10.Name = "button10";
            button10.Size = new Size(112, 35);
            button10.TabIndex = 20;
            button10.Text = "停止";
            button10.UseVisualStyleBackColor = true;
            // 
            // button11
            // 
            button11.Location = new Point(1298, 11);
            button11.Margin = new Padding(2, 3, 2, 3);
            button11.Name = "button11";
            button11.Size = new Size(112, 35);
            button11.TabIndex = 19;
            button11.Text = "开始";
            button11.UseVisualStyleBackColor = true;
            // 
            // cboCountry4
            // 
            cboCountry4.FormattingEnabled = true;
            cboCountry4.Location = new Point(81, 11);
            cboCountry4.Margin = new Padding(2, 3, 2, 3);
            cboCountry4.Name = "cboCountry4";
            cboCountry4.Size = new Size(182, 32);
            cboCountry4.TabIndex = 18;
            // 
            // label17
            // 
            label17.AutoSize = true;
            label17.Location = new Point(295, 16);
            label17.Margin = new Padding(2, 0, 2, 0);
            label17.Name = "label17";
            label17.Size = new Size(100, 24);
            label17.TabIndex = 17;
            label17.Text = "链接地址：";
            // 
            // label18
            // 
            label18.AutoSize = true;
            label18.Location = new Point(20, 13);
            label18.Margin = new Padding(2, 0, 2, 0);
            label18.Name = "label18";
            label18.Size = new Size(64, 24);
            label18.TabIndex = 16;
            label18.Text = "国家：";
            // 
            // tabResult
            // 
            tabResult.Controls.Add(splitContainer1);
            tabResult.Location = new Point(4, 33);
            tabResult.Margin = new Padding(2, 3, 2, 3);
            tabResult.Name = "tabResult";
            tabResult.Padding = new Padding(2, 3, 2, 3);
            tabResult.Size = new Size(2295, 1260);
            tabResult.TabIndex = 2;
            tabResult.Text = "结果筛选";
            tabResult.UseVisualStyleBackColor = true;
            // 
            // splitContainer1
            // 
            splitContainer1.Dock = DockStyle.Fill;
            splitContainer1.Location = new Point(2, 3);
            splitContainer1.Name = "splitContainer1";
            splitContainer1.Orientation = Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            splitContainer1.Panel1.Controls.Add(groupBox1);
            // 
            // splitContainer1.Panel2
            // 
            splitContainer1.Panel2.Controls.Add(groupBox2);
            splitContainer1.Size = new Size(2291, 1254);
            splitContainer1.SplitterDistance = 129;
            splitContainer1.TabIndex = 2;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(comboBox3);
            groupBox1.Controls.Add(btnSearch);
            groupBox1.Controls.Add(textBox2);
            groupBox1.Controls.Add(label14);
            groupBox1.Controls.Add(comboBox6);
            groupBox1.Controls.Add(label13);
            groupBox1.Controls.Add(comboBox5);
            groupBox1.Controls.Add(label12);
            groupBox1.Controls.Add(cboCountry5);
            groupBox1.Controls.Add(label11);
            groupBox1.Controls.Add(label10);
            groupBox1.Controls.Add(comboBox2);
            groupBox1.Controls.Add(label9);
            groupBox1.Controls.Add(comboBox1);
            groupBox1.Controls.Add(label8);
            groupBox1.Controls.Add(numericUpDown3);
            groupBox1.Controls.Add(label6);
            groupBox1.Controls.Add(numericUpDown4);
            groupBox1.Controls.Add(label7);
            groupBox1.Controls.Add(numericUpDown2);
            groupBox1.Controls.Add(label5);
            groupBox1.Controls.Add(numericUpDown1);
            groupBox1.Controls.Add(label4);
            groupBox1.Dock = DockStyle.Fill;
            groupBox1.Location = new Point(0, 0);
            groupBox1.Margin = new Padding(2, 3, 2, 3);
            groupBox1.Name = "groupBox1";
            groupBox1.Padding = new Padding(2, 3, 2, 3);
            groupBox1.Size = new Size(2291, 129);
            groupBox1.TabIndex = 0;
            groupBox1.TabStop = false;
            groupBox1.Text = "查询条件";
            // 
            // comboBox3
            // 
            comboBox3.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox3.FormattingEnabled = true;
            comboBox3.Items.AddRange(new object[] { "", "未知", "是", "否" });
            comboBox3.Location = new Point(779, 29);
            comboBox3.Margin = new Padding(2, 3, 2, 3);
            comboBox3.Name = "comboBox3";
            comboBox3.Size = new Size(182, 32);
            comboBox3.TabIndex = 23;
            // 
            // btnSearch
            // 
            btnSearch.Location = new Point(1349, 89);
            btnSearch.Margin = new Padding(2, 3, 2, 3);
            btnSearch.Name = "btnSearch";
            btnSearch.Size = new Size(112, 35);
            btnSearch.TabIndex = 22;
            btnSearch.Text = "筛选查询";
            btnSearch.UseVisualStyleBackColor = true;
            // 
            // textBox2
            // 
            textBox2.Location = new Point(1349, 28);
            textBox2.Margin = new Padding(2, 3, 2, 3);
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(262, 30);
            textBox2.TabIndex = 21;
            // 
            // label14
            // 
            label14.AutoSize = true;
            label14.Location = new Point(1297, 29);
            label14.Margin = new Padding(2, 0, 2, 0);
            label14.Name = "label14";
            label14.Size = new Size(46, 24);
            label14.TabIndex = 20;
            label14.Text = "搜索";
            // 
            // comboBox6
            // 
            comboBox6.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox6.FormattingEnabled = true;
            comboBox6.Items.AddRange(new object[] { "", "关键词抓取", "类目抓取" });
            comboBox6.Location = new Point(1091, 89);
            comboBox6.Margin = new Padding(2, 3, 2, 3);
            comboBox6.Name = "comboBox6";
            comboBox6.Size = new Size(182, 32);
            comboBox6.TabIndex = 19;
            // 
            // label13
            // 
            label13.AutoSize = true;
            label13.Location = new Point(1040, 92);
            label13.Margin = new Padding(2, 0, 2, 0);
            label13.Name = "label13";
            label13.Size = new Size(46, 24);
            label13.TabIndex = 18;
            label13.Text = "类型";
            // 
            // comboBox5
            // 
            comboBox5.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox5.FormattingEnabled = true;
            comboBox5.Location = new Point(1091, 27);
            comboBox5.Margin = new Padding(2, 3, 2, 3);
            comboBox5.Name = "comboBox5";
            comboBox5.Size = new Size(182, 32);
            comboBox5.TabIndex = 17;
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.Location = new Point(986, 29);
            label12.Margin = new Padding(2, 0, 2, 0);
            label12.Name = "label12";
            label12.Size = new Size(100, 24);
            label12.TabIndex = 16;
            label12.Text = "任务关键词";
            // 
            // cboCountry5
            // 
            cboCountry5.DropDownStyle = ComboBoxStyle.DropDownList;
            cboCountry5.FormattingEnabled = true;
            cboCountry5.Location = new Point(779, 89);
            cboCountry5.Margin = new Padding(2, 3, 2, 3);
            cboCountry5.Name = "cboCountry5";
            cboCountry5.Size = new Size(182, 32);
            cboCountry5.TabIndex = 15;
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Location = new Point(693, 91);
            label11.Margin = new Padding(2, 0, 2, 0);
            label11.Name = "label11";
            label11.Size = new Size(82, 24);
            label11.TabIndex = 14;
            label11.Text = "所属国家";
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Location = new Point(693, 35);
            label10.Margin = new Padding(2, 0, 2, 0);
            label10.Name = "label10";
            label10.Size = new Size(82, 24);
            label10.TabIndex = 12;
            label10.Text = "自动发货";
            // 
            // comboBox2
            // 
            comboBox2.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox2.FormattingEnabled = true;
            comboBox2.Items.AddRange(new object[] { "", "未知", "是", "否" });
            comboBox2.Location = new Point(473, 88);
            comboBox2.Margin = new Padding(2, 3, 2, 3);
            comboBox2.Name = "comboBox2";
            comboBox2.Size = new Size(182, 32);
            comboBox2.TabIndex = 11;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new Point(423, 89);
            label9.Margin = new Padding(2, 0, 2, 0);
            label9.Name = "label9";
            label9.Size = new Size(46, 24);
            label9.TabIndex = 10;
            label9.Text = "僵尸";
            // 
            // comboBox1
            // 
            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox1.FormattingEnabled = true;
            comboBox1.Items.AddRange(new object[] { "", "未知", "未注册", "已注册" });
            comboBox1.Location = new Point(473, 31);
            comboBox1.Margin = new Padding(2, 3, 2, 3);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(182, 32);
            comboBox1.TabIndex = 9;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(387, 32);
            label8.Margin = new Padding(2, 0, 2, 0);
            label8.Name = "label8";
            label8.Size = new Size(82, 24);
            label8.TabIndex = 8;
            label8.Text = "注册商标";
            // 
            // numericUpDown3
            // 
            numericUpDown3.Location = new Point(227, 85);
            numericUpDown3.Margin = new Padding(2, 3, 2, 3);
            numericUpDown3.Maximum = new decimal(new int[] { 50, 0, 0, 65536 });
            numericUpDown3.Name = "numericUpDown3";
            numericUpDown3.Size = new Size(141, 30);
            numericUpDown3.TabIndex = 7;
            numericUpDown3.Value = new decimal(new int[] { 50, 0, 0, 65536 });
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(207, 88);
            label6.Margin = new Padding(2, 0, 2, 0);
            label6.Name = "label6";
            label6.Size = new Size(18, 24);
            label6.TabIndex = 6;
            label6.Text = "-";
            // 
            // numericUpDown4
            // 
            numericUpDown4.Location = new Point(97, 85);
            numericUpDown4.Margin = new Padding(2, 3, 2, 3);
            numericUpDown4.Maximum = new decimal(new int[] { 50, 0, 0, 65536 });
            numericUpDown4.Name = "numericUpDown4";
            numericUpDown4.Size = new Size(106, 30);
            numericUpDown4.TabIndex = 5;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(34, 89);
            label7.Margin = new Padding(2, 0, 2, 0);
            label7.Name = "label7";
            label7.Size = new Size(46, 24);
            label7.TabIndex = 4;
            label7.Text = "星级";
            // 
            // numericUpDown2
            // 
            numericUpDown2.Location = new Point(227, 29);
            numericUpDown2.Margin = new Padding(2, 3, 2, 3);
            numericUpDown2.Maximum = new decimal(new int[] { 99999, 0, 0, 0 });
            numericUpDown2.Name = "numericUpDown2";
            numericUpDown2.Size = new Size(141, 30);
            numericUpDown2.TabIndex = 3;
            numericUpDown2.Value = new decimal(new int[] { 99999, 0, 0, 0 });
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(207, 33);
            label5.Margin = new Padding(2, 0, 2, 0);
            label5.Name = "label5";
            label5.Size = new Size(18, 24);
            label5.TabIndex = 2;
            label5.Text = "-";
            // 
            // numericUpDown1
            // 
            numericUpDown1.Location = new Point(97, 29);
            numericUpDown1.Margin = new Padding(2, 3, 2, 3);
            numericUpDown1.Maximum = new decimal(new int[] { 99998, 0, 0, 0 });
            numericUpDown1.Name = "numericUpDown1";
            numericUpDown1.Size = new Size(106, 30);
            numericUpDown1.TabIndex = 1;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(16, 31);
            label4.Margin = new Padding(2, 0, 2, 0);
            label4.Name = "label4";
            label4.Size = new Size(64, 24);
            label4.TabIndex = 0;
            label4.Text = "评论数";
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(dgvResult);
            groupBox2.Controls.Add(button7);
            groupBox2.Controls.Add(button6);
            groupBox2.Controls.Add(button5);
            groupBox2.Controls.Add(button4);
            groupBox2.Controls.Add(checkBox3);
            groupBox2.Controls.Add(button1);
            groupBox2.Dock = DockStyle.Fill;
            groupBox2.Location = new Point(0, 0);
            groupBox2.Margin = new Padding(2, 3, 2, 3);
            groupBox2.Name = "groupBox2";
            groupBox2.Padding = new Padding(2, 3, 2, 3);
            groupBox2.Size = new Size(2291, 1121);
            groupBox2.TabIndex = 1;
            groupBox2.TabStop = false;
            groupBox2.Text = "筛选结果";
            // 
            // dgvResult
            // 
            dgvResult.AllowUserToAddRows = false;
            dgvResult.AllowUserToDeleteRows = false;
            dgvResult.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dgvResult.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvResult.Location = new Point(13, 84);
            dgvResult.Margin = new Padding(2, 3, 2, 3);
            dgvResult.Name = "dgvResult";
            dgvResult.ReadOnly = true;
            dgvResult.RowHeadersWidth = 62;
            dgvResult.RowTemplate.Height = 32;
            dgvResult.Size = new Size(2269, 1014);
            dgvResult.TabIndex = 29;
            // 
            // button7
            // 
            button7.Location = new Point(1604, 29);
            button7.Margin = new Padding(2, 3, 2, 3);
            button7.Name = "button7";
            button7.Size = new Size(112, 35);
            button7.TabIndex = 28;
            button7.Text = "导出筛选Asin";
            button7.UseVisualStyleBackColor = true;
            // 
            // button6
            // 
            button6.Location = new Point(1476, 29);
            button6.Margin = new Padding(2, 3, 2, 3);
            button6.Name = "button6";
            button6.Size = new Size(112, 35);
            button6.TabIndex = 27;
            button6.Text = "导出全部Asin";
            button6.UseVisualStyleBackColor = true;
            // 
            // button5
            // 
            button5.Location = new Point(1348, 29);
            button5.Margin = new Padding(2, 3, 2, 3);
            button5.Name = "button5";
            button5.Size = new Size(112, 35);
            button5.TabIndex = 26;
            button5.Text = "清空";
            button5.UseVisualStyleBackColor = true;
            // 
            // button4
            // 
            button4.Location = new Point(1220, 29);
            button4.Margin = new Padding(2, 3, 2, 3);
            button4.Name = "button4";
            button4.Size = new Size(112, 35);
            button4.TabIndex = 25;
            button4.Text = "删除选中";
            button4.UseVisualStyleBackColor = true;
            // 
            // checkBox3
            // 
            checkBox3.AutoSize = true;
            checkBox3.Location = new Point(1080, 33);
            checkBox3.Margin = new Padding(2, 3, 2, 3);
            checkBox3.Name = "checkBox3";
            checkBox3.Size = new Size(134, 28);
            checkBox3.TabIndex = 24;
            checkBox3.Text = "全选/不全选";
            checkBox3.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            button1.Location = new Point(6, 33);
            button1.Margin = new Padding(2, 3, 2, 3);
            button1.Name = "button1";
            button1.Size = new Size(112, 35);
            button1.TabIndex = 23;
            button1.Text = "刷新列表";
            button1.UseVisualStyleBackColor = true;
            // 
            // panleTop
            // 
            panleTop.BackColor = Color.WhiteSmoke;
            panleTop.Controls.Add(lblTitle);
            panleTop.Controls.Add(btnMin);
            panleTop.Controls.Add(btnClose);
            panleTop.Controls.Add(btnMax);
            panleTop.Dock = DockStyle.Top;
            panleTop.Location = new Point(0, 0);
            panleTop.Margin = new Padding(4, 3, 4, 3);
            panleTop.Name = "panleTop";
            panleTop.Size = new Size(2320, 48);
            panleTop.TabIndex = 3;
            // 
            // lblTitle
            // 
            lblTitle.BackColor = Color.Transparent;
            lblTitle.Font = new Font("思源黑体 CN Regular", 12F, FontStyle.Regular, GraphicsUnit.Pixel);
            lblTitle.Location = new Point(1060, 8);
            lblTitle.Margin = new Padding(4, 0, 4, 0);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(178, 28);
            lblTitle.TabIndex = 3;
            lblTitle.Text = "YMH";
            lblTitle.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // btnMin
            // 
            btnMin.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnMin.BackColor = Color.White;
            btnMin.BackgroundImage = Properties.Resources.min;
            btnMin.BackgroundImageLayout = ImageLayout.Zoom;
            btnMin.FlatAppearance.BorderSize = 0;
            btnMin.FlatStyle = FlatStyle.Flat;
            btnMin.Location = new Point(2156, 8);
            btnMin.Margin = new Padding(4, 3, 4, 3);
            btnMin.Name = "btnMin";
            btnMin.Size = new Size(37, 37);
            btnMin.TabIndex = 2;
            btnMin.UseVisualStyleBackColor = false;
            btnMin.Click += btnMin_Click;
            // 
            // btnClose
            // 
            btnClose.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnClose.BackColor = Color.White;
            btnClose.BackgroundImage = Properties.Resources.close;
            btnClose.BackgroundImageLayout = ImageLayout.Zoom;
            btnClose.FlatAppearance.BorderSize = 0;
            btnClose.FlatStyle = FlatStyle.Flat;
            btnClose.Location = new Point(2279, 8);
            btnClose.Margin = new Padding(4, 3, 4, 3);
            btnClose.Name = "btnClose";
            btnClose.Size = new Size(37, 37);
            btnClose.TabIndex = 1;
            btnClose.UseVisualStyleBackColor = false;
            btnClose.Click += buttonClose_Click;
            // 
            // btnMax
            // 
            btnMax.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnMax.BackColor = Color.White;
            btnMax.BackgroundImage = Properties.Resources.max;
            btnMax.BackgroundImageLayout = ImageLayout.Zoom;
            btnMax.FlatAppearance.BorderSize = 0;
            btnMax.FlatStyle = FlatStyle.Flat;
            btnMax.Location = new Point(2221, 8);
            btnMax.Margin = new Padding(4, 3, 4, 3);
            btnMax.Name = "btnMax";
            btnMax.Size = new Size(37, 37);
            btnMax.TabIndex = 0;
            btnMax.UseVisualStyleBackColor = false;
            btnMax.Click += btnMax_Click;
            // 
            // tabLog
            // 
            tabLog.Controls.Add(txtLogs);
            tabLog.Location = new Point(4, 33);
            tabLog.Name = "tabLog";
            tabLog.Padding = new Padding(3);
            tabLog.Size = new Size(2295, 1260);
            tabLog.TabIndex = 5;
            tabLog.Text = "系统日志";
            tabLog.UseVisualStyleBackColor = true;
            // 
            // txtLogs
            // 
            txtLogs.BackColor = Color.AntiqueWhite;
            txtLogs.BorderStyle = BorderStyle.None;
            txtLogs.Dock = DockStyle.Fill;
            txtLogs.Location = new Point(3, 3);
            txtLogs.Name = "txtLogs";
            txtLogs.ReadOnly = true;
            txtLogs.Size = new Size(2289, 1254);
            txtLogs.TabIndex = 0;
            txtLogs.Text = "";
            // 
            // FrmMain
            // 
            AutoScaleDimensions = new SizeF(11F, 24F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(2320, 1365);
            ControlBox = false;
            Controls.Add(panleTop);
            Controls.Add(tabContent);
            FormBorderStyle = FormBorderStyle.None;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Margin = new Padding(2, 3, 2, 3);
            Name = "FrmMain";
            StartPosition = FormStartPosition.CenterScreen;
            WindowState = FormWindowState.Maximized;
            tabContent.ResumeLayout(false);
            tabKeywords.ResumeLayout(false);
            tabKeywords.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvKeyWordsProduct).EndInit();
            tabProductType.ResumeLayout(false);
            tabProductType.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvTypeProduct).EndInit();
            tabShop.ResumeLayout(false);
            tabShop.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvShopProduct).EndInit();
            tabLinkUrl.ResumeLayout(false);
            tabLinkUrl.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvLinkProduct).EndInit();
            tabResult.ResumeLayout(false);
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numericUpDown3).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDown4).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDown2).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDown1).EndInit();
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvResult).EndInit();
            panleTop.ResumeLayout(false);
            tabLog.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private TabControl tabContent;
        private TabPage tabKeywords;
        private Label label1;
        private TabPage tabProductType;
        private TabPage tabResult;
        private Button btnKeyWords;
        private ComboBox cboProdKeyWords;
        private ComboBox cboCountry;
        private Label label2;
        private CheckBox checkBoxProxy;
        private Button btnStop;
        private Button btnStart;
        private DataGridView dgvKeyWordsProduct;
        private Button button2;
        private Button button3;
        private CheckBox checkBox2;
        private CheckBox checkBox1;
        private TextBox textBox1;
        private Label label3;
        private DataGridView dgvTypeProduct;
        private TabPage tabShop;
        private TabPage tabLinkUrl;
        private DataGridView dgvShopProduct;
        private Button button8;
        private Button button9;
        private Label label15;
        private TextBox textBox3;
        private TextBox textBox4;
        private DataGridView dgvLinkProduct;
        private Button button10;
        private Button button11;
        private ComboBox cboCountry4;
        private Label label17;
        private Label label18;
        private ComboBox cboCountry3;
        private Label label16;
        private ComboBox cboCountry2;
        private Label label19;
        private Button btnExport;
        private Panel panleTop;
        private Label lblTitle;
        private Button btnMin;
        private Button btnClose;
        private Button btnMax;
        private SplitContainer splitContainer1;
        private GroupBox groupBox2;
        private DataGridView dgvResult;
        private Button button7;
        private Button button6;
        private Button button5;
        private Button button4;
        private CheckBox checkBox3;
        private Button button1;
        private GroupBox groupBox1;
        private ComboBox comboBox3;
        private Button btnSearch;
        private TextBox textBox2;
        private Label label14;
        private ComboBox comboBox6;
        private Label label13;
        private ComboBox comboBox5;
        private Label label12;
        private ComboBox cboCountry5;
        private Label label11;
        private Label label10;
        private ComboBox comboBox2;
        private Label label9;
        private ComboBox comboBox1;
        private Label label8;
        private NumericUpDown numericUpDown3;
        private Label label6;
        private NumericUpDown numericUpDown4;
        private Label label7;
        private NumericUpDown numericUpDown2;
        private Label label5;
        private NumericUpDown numericUpDown1;
        private Label label4;
        private TabPage tabLog;
        private RichTextBox txtLogs;
    }
}