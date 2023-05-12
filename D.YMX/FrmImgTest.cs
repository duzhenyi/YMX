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
        private static Dictionary<string, string> dic = new Dictionary<string, string>();

        private void btnTest_Click(object sender, EventArgs e)
        {
            // 加载图片包地址 
            var path = "C:\\Users\\29561\\Desktop\\YMX\\D.YMX\\bin\\Debug\\net7.0-windows\\Uploads\\Captcha";
            //C#遍历指定文件夹中的所有文件 
            DirectoryInfo TheFolder = new DirectoryInfo(path);
            //遍历文件夹
            //foreach (DirectoryInfo NextFolder in TheFolder.GetDirectories())
            //遍历所有图片
            foreach (FileInfo fileInfo in TheFolder.GetFiles())
            {
                if (fileInfo.Name.Split(".")[1] != "jpg" && fileInfo.Name.Split(".")[1] != "png")
                {
                    continue;
                }
                var fileName = fileInfo.Name.Split(".")[0];

                var strs = fileName.ToCharArray().Distinct().ToList();
                var count = 0;
                foreach (var item in strs)
                {
                    if (dic.ContainsKey(item.ToString()))
                    {
                        count++;
                    }
                }
                if (count == strs.Count)
                {
                    continue;
                }
                using (var bitMap = new Bitmap(fileInfo.FullName))
                {
                    for (int i = 0; i < fileName.Length; i++)
                    {
                        // store in the database : code, length= image.width , binarydata
                        string codePattern = ImgUtil.ScanImageUlong(bitMap);
                        var key = fileName[i].ToString();
                        if (!dic.ContainsKey(key))
                        {
                            dic.Add(key, codePattern);
                        }
                    }
                }
                if (dic.Count == 36)
                {
                    break;
                }
            }

            // 缓存字模到本地txt文本
            var str = new StringBuilder();
            foreach (var item in dic.Keys)
            {
                str.AppendLine(item + ":" + dic[item].ToString());
            }
            StoreInDatabase(str.ToString());
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
