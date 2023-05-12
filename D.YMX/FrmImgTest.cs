using D.YMX.Utils;
using Org.BouncyCastle.Asn1.Crmf;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace D.YMX
{
    public partial class FrmImgTest : Form
    {
        public FrmImgTest()
        {
            InitializeComponent();
        }

        private void btnTest_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "")
            {
                var bitMap = new Bitmap("C:\\Users\\29561\\Desktop\\YMX\\D.YMX\\bin\\Debug\\net7.0-windows\\Uploads\\Captcha\\20230512112902376.jpg");
                for (int i = 0; i < 6; i++)
                {
                    // store in the database : code, length= image.width , binarydata
                    string codePattern = ImgUtil.ScanImageUlong(bitMap);
                    StoreInDatabase(textBox1.Text[i] + "," + codePattern);
                }
            }
        }

        private void StoreInDatabase(string msg)
        {
            string pathFile = AppDomain.CurrentDomain.SetupInformation.ApplicationBase + "Uploads/Captcha/";

            //判断文件夹是否存在
            if (!Directory.Exists(pathFile))
            {
                Directory.CreateDirectory(pathFile);
            }
            pathFile += "template.txt";

            StreamWriter sw = new StreamWriter(pathFile, true);
            sw.WriteLine(msg);
            sw.Flush();
            sw.Close();
        }
    }
}
