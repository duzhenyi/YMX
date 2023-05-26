using D.YMX.Utils;
using Org.BouncyCastle.Asn1.Crmf;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
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
            //var path = "C:\\Users\\29561\\Desktop\\YMX\\D.YMX\\bin\\Debug\\net7.0-windows\\Uploads\\Captcha";
            //this.pictureBox1.Image = new Bitmap(path + "\\" + "AEJXLE.jpg");
        }

        private static Dictionary<string, List<string>> dic = new Dictionary<string, List<string>>();

        private void btnTest_Click(object sender, EventArgs e)
        {
            // 加载图片包地址 
            var path = "E:\\DL\\du-ling.2023\\YMX\\D.YMX\\bin\\Debug\\net7.0-windows\\Uploads\\Captcha";
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

                //var strs = fileName.ToCharArray().Distinct().ToList();
                //var count = 0;
                //foreach (var item in strs)
                //{
                //    if (dic.ContainsKey(item.ToString()))
                //    {
                //        count++;
                //    }
                //}
                //if (count == strs.Count)
                //{
                //    continue;
                //}
                using (var bitMap = new Bitmap(fileInfo.FullName))
                {
                    // 切割图片
                    var garyImg = ImgUtil.ToGray(bitMap);
                    //var img2 = ImgUtil.ConvertToBinaryImage(garyImg);
                    var list = ImgUtil.Cut(garyImg);
                    for (int i = 0; i < list.Count; i++)
                    {
                        string codePattern = ImgUtil.ScanImageUlong(list[i]);
                        var key = fileName[i].ToString();
                        if (!dic.ContainsKey(key))
                        {
                            dic.Add(key, new List<string>() { codePattern });
                        }
                        else
                        {
                            if (!dic[key].Any(m => m == codePattern))
                            {
                                dic[key].Add(codePattern);
                            }
                        }
                    }
                }
            }

            // 缓存字模到本地txt文本
            var str = new StringBuilder();
            foreach (var key in dic.Keys)
            {
                str.AppendLine(key + ":" + string.Join("|", dic[key]) + ")");
            }
            StoreInDatabase(str.ToString());
        }

        private void StoreInDatabase(string msg)
        {
            string filePath = AppDomain.CurrentDomain.SetupInformation.ApplicationBase + "Uploads/Captcha/";

            //判断文件夹是否存在
            if (!Directory.Exists(filePath))
            {
                Directory.CreateDirectory(filePath);
            }
            filePath += "template.txt";

            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }

            StreamWriter sw = new StreamWriter(filePath, true);
            sw.WriteLine(msg);
            sw.Flush();
            sw.Close();
        }


        /// <summary>
        /// 图片灰度
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnGray_Click(object sender, EventArgs e)
        {
            // 图片灰度
            Bitmap bmp = pictureBox1.Image as Bitmap;
            if (bmp != null)
            {
                pictureBox2.Image = ImgUtil.ToGray(bmp);
            }
        }

        /// <summary>
        /// 图片二值化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button3_Click(object sender, EventArgs e)
        {
            // 图片二值化
            Bitmap bmp = pictureBox2.Image as Bitmap;
            if (bmp != null)
            {
                pictureBox3.Image = ImgUtil.ConvertToBinaryImage(bmp);
            }
        }
        /// <summary>
        /// 图片切片
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            // 图片切片
            Bitmap bmp = pictureBox2.Image as Bitmap;
            if (bmp != null)
            {
                var list = ImgUtil.Cut(bmp);
                flowLayoutPanel1.Controls.Clear();
                for (int i = 0; i < list.Count; i++)
                {
                    var pic = new PictureBox();
                    //pic.Width = 100;
                    //pic.Height = 100;
                    pic.Image = list[i];
                    pic.Location = new Point(100 * i + 100, 0);
                    flowLayoutPanel1.Controls.Add(pic);
                }
            }
        }

        /// <summary>
        /// 字符扫描
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button4_Click(object sender, EventArgs e)
        {
            Bitmap bmp = pictureBox1.Image as Bitmap;
            var imgGary = ImgUtil.ToGray(bmp);
            var img2 = ImgUtil.ConvertToBinaryImage(imgGary);
            var list = ImgUtil.Cut(img2);

            // 字符扫描
            var str = new StringBuilder();
            foreach (var bitMap in list)
            {
                var code = ImgUtil.ScanImageUlong(bitMap);
                str.AppendLine(code);
                str.Append("\n");

                //for (int x = 0; x < bitMap.Width; x++) //行扫描，由x.0至x.图片宽度
                //{
                //    for (int y = 0; y < bitMap.Height; y++) //列扫描，由y.0至图片高度
                //    {
                //        if (bitMap.GetPixel(x, y).R == 0)  //对图片中的点进行判断，当x,y点中的R色为0的时候，//记录为1
                //        {
                //            Trace.Write("1");
                //        }
                //        else
                //        {
                //            Trace.Write("0");
                //        }
                //    }
                //    Trace.Write("\n");
                //}
            }

            richTextBox1.Text = str.ToString();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            // 字符匹配 
            //var path = "C:\\Users\\29561\\Desktop\\YMX\\D.YMX\\bin\\Debug\\net7.0-windows\\Uploads\\Captcha\\AEJXLE.jpg";
            //Bitmap bitMap = new Bitmap(path);
            //var garyImg = ImgUtil.ToGray(bitMap);
            //var img2 = ImgUtil.ConvertToBinaryImage(garyImg);

            //var bitmaps = ImgUtil.Cut(img2);

            // 字符扫描
            var codeList = new List<string>();
            for (int i = 0; i < flowLayoutPanel1.Controls.Count; i++)
            {
                var pic = flowLayoutPanel1.Controls[i] as PictureBox;
                var code = ImgUtil.ScanImageUlong(pic.Image as Bitmap);
                codeList.Add(code);
            }

            // 1. 加载字模
            string filePath = AppDomain.CurrentDomain.SetupInformation.ApplicationBase + "Uploads/Captcha/template.txt";
            StreamReader sr = new StreamReader(filePath);
            var str = sr.ReadToEnd();
            var files = str.Split(")");

            var result = new string[codeList.Count];
            for (int z = 0; z < codeList.Count; z++)
            {
                if (codeList[z] == "")
                {
                    continue;
                }
                // 分割后的字符
                string[] strs = codeList[z].Split(',');

                //  图片中的字符
                int len = int.Parse(strs[0]);

                // 3. 字符扫描的结果
                string[] code = new string[len];
                for (int i = 0; i < len; i++)
                {
                    code[i] = strs[i + 1];
                }

                foreach (var file in files)
                {
                    var lineText = file.Replace("\r", "").Replace("\n", "");
                    if (lineText == "")
                    {
                        continue;
                    }
                    // 分割后的字符
                    string[] templates = lineText.Split(':');

                    // 1. 图片中的字符,字母名称
                    string templateCharacter = templates[0];

                    // 后面的多个字符
                    var cahrts = templates[1].Split("|");

                    foreach (var chart in cahrts)
                    {
                        var charts2 = chart.Split(',');
                        // 2. 字符的宽度
                        int templateLen = int.Parse(charts2[0]);

                        // 3. 字符扫描的结果
                        string[] templateCode = new string[templateLen];
                        for (int i = 0; i < templateLen; i++)
                        {
                            templateCode[i] = charts2[i + 1];
                        }

                        // 进行字模比对
                        var matchRate = ImgUtil.CompareArr(templateCode, code);
                        Trace.WriteLine(templateCharacter + ":" + matchRate);
                        if (matchRate > 0.7)
                        {
                            result[z] = templateCharacter;
                        }
                    }
                }
            }
            this.textBox1.Text = string.Join(" ", result);
            sr.Dispose();
        }

        public static List<string> SortHashtable(Hashtable ht)
        {
            List<string> rtn = new List<string>();
            ArrayList keys = new ArrayList(ht.Keys);
            keys.Sort();
            rtn.Add((string)ht[keys[0]]);

            int temp = (int)keys[0];
            for (int i = 1; i < keys.Count; i++)
            {
                int next = ((int)keys[i]);
                if (next - temp > 10)
                {
                    rtn.Add((string)ht[keys[i]]);
                    temp = next;
                }
            }
            return rtn;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // 加载图片包地址 
            var path = "C:\\Users\\29561\\Desktop\\YMX\\D.YMX\\bin\\Debug\\net7.0-windows\\Uploads\\Captcha";
            Bitmap bmp = new Bitmap(path + "\\" + "AEJXLE.jpg");
            pictureBox1.Image = bmp;
            if (bmp != null)
            {
                // 图片二值化
                Bitmap bm = ImgUtil.ConvertToBinaryImage(bmp);
                pictureBox2.Image = bm;

                long lastTicks = DateTime.Now.Ticks;
                Hashtable table = new Hashtable();
                // if dbs is empty, load from file
                var dbs = new Database(path + "\\AEJXLE.txt");
                // 查找字符边界
                List<int> list = ImgUtil.RecordBlack(bm);
                List<int[]> edgeList = ImgUtil.FindEdge(list);

                // 将图片分割成四分或者更少
                foreach (int[] range in edgeList)
                {
                    // 图像分割
                    Bitmap temp = ImgUtil.CutImage(bm, range[0], range[1] - range[0]);
                    //  遍历所有样本库元素
                    for (int i = 0; i < dbs.codeArray.Length; i++)
                    {
                        for (int j = 0; j < dbs.codeArray[i].Count; j++)
                        {
                            // 图片扫描
                            int len = dbs.codeArray[i][j].len;
                            // 从图片中扫描制定宽度
                            for (int k = 0; k < temp.Width - len + 1; k++)
                            {
                                //ulong[] codeArray = ImgUtil.ScanImageUlong(temp, len, k);
                                //// 字符匹配
                                //double matchRate = ImgUtil.MatchTwoCodes(codeArray, dbs.codeArray[i][j].code);

                                //if (matchRate > 0.95) // 0.95 matchRateThreshold
                                //{
                                //    if (!table.Contains(k + range[0]))
                                //        table.Add(k + range[0], dbs.codeArray[i][j].character);
                                //}
                                //if (matchRate >= 0.99)  // 0.99 matchRateThreshold_PerfectMatch
                                //{
                                //    i = dbs.codeArray.Length - 1;
                                //    j = dbs.codeArray[i].Count; // break j first, then i                                   
                                //    break; // 成功找到匹配项，停止
                                //}
                            }
                        }
                    }
                    temp.Dispose();
                }
                bm.Dispose();
                // 对匹配结果进行删减
                List<string> resultList = SortHashtable(table);
                if (resultList.Count == 4)
                {
                    var code = resultList[0] + resultList[1] + resultList[2] + resultList[3];
                    long elapsedTicks = DateTime.Now.Ticks - lastTicks;
                    double diff = (new TimeSpan(elapsedTicks)).TotalMilliseconds;
                    MessageBox.Show("匹配成功，用时" + diff + "毫秒");
                }
                else
                {
                    MessageBox.Show("couldn't match!!!");
                }

            }
        }

        /// <summary>
        /// 解析HTML的商品详情
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button5_Click(object sender, EventArgs e)
        {
            var countryEntity = new CountryEntity()
            {
                CountryType = CountryEnum.America,
            };
            var filePath = "C:\\Users\\29561\\Documents\\HBuilderProjects\\all.html";
            var detailHtml = File.ReadAllText(filePath);
            var res = countryEntity.Instance().GetDetail(detailHtml);
        }

        /// <summary>
        /// 原始图
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button6_Click(object sender, EventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            DialogResult result = fileDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                this.pictureBox1.Image = Image.FromFile(fileDialog.FileName);
            } 
        }
    }
}
