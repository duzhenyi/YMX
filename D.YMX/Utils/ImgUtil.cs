﻿using D.YMX.Models;
using System.Collections;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Net;
using System.Reflection.Metadata;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace D.YMX.Utils
{
    public class CodeList : List<codeStruct>
    {
        public List<codeStruct> GetItem(int length)
        {
            List<codeStruct> list = new List<codeStruct>();
            foreach (codeStruct cs in this)
            {
                if (cs.len == length)
                    list.Add(cs);
            }
            return list;
        }
    }
    public class codeStruct
    {
        public string character;
        public int len;
        public ulong[] code;

        public codeStruct(string databaseString)
        {
            string[] stringArray = databaseString.Split(',');
            character = stringArray[0];
            len = int.Parse(stringArray[1]);
            code = new ulong[len];
            for (int i = 0; i < len; i++)
            {
                code[i] = ulong.Parse(stringArray[i + 2]);
            }
        }

        public override string ToString()
        {
            string rtn = "";
            rtn += character;
            rtn += "," + len.ToString();
            foreach (ulong number in code)
                rtn += "," + number;
            return rtn;
        }
    }

    public class Database
    {
        public CodeList[] codeArray = new CodeList[36];

        public Database()
        {
            for (int i = 0; i < 36; i++)
            {
                codeArray[i] = new CodeList();
            }
        }

        public Database(string filePath)
        {
            for (int i = 0; i < 36; i++)
            {
                codeArray[i] = new CodeList();
            }

            StreamReader sr = new StreamReader(filePath);

            while (sr.Peek() > -1)
            {
                string lineText = sr.ReadLine();
                if (lineText == "") continue;
                codeStruct cs = new codeStruct(lineText.Trim());

                // 新的codeStruct来了，首先在表中找到索引。然后在旧数据库中找到相同长度的代码，如果匹配率高达95%，那么就放弃它
                int index = FindIndexInTable(cs.character);

                List<codeStruct> rtnList = codeArray[index].GetItem(cs.len);
                if (rtnList != null && rtnList.Count > 0)
                {
                    foreach (codeStruct codeStr in rtnList)
                    {
                        double matchRate = ImgUtil.MatchTwoCodes(cs.code, codeStr.code);
                        if (matchRate < 0.95) // 查找新模式，添加到数据库 matchRateThreshold
                        {
                            codeArray[index].Add(cs);
                            break;
                        }
                    }
                }
                else
                {
                    codeArray[index].Add(cs);
                }
            }
        }

        int FindIndexInTable(string character)
        {
            return 0;
        }

        public static readonly string[] characterTable =
        {
            "0","1","2","3","4","5","6","7","8","9",
            "A","B","C","D","E","F","G","H","I","J",
            "K","L","M","N","O","P","Q","R","S","T",
            "U","V","W","X","Y","Z"
        };
    }

    public class ImgUtil
    {
        #region 根据远程图片Url下载到本地

        public static string GetCaptchaImage(string imgUrl)
        {
            string pathFile = AppDomain.CurrentDomain.SetupInformation.ApplicationBase + "Uploads/Captcha/";

            //判断文件夹是否存在
            if (!Directory.Exists(pathFile))
            {
                Directory.CreateDirectory(pathFile);
            }
            pathFile += $@"{DateTime.Now.ToString("yyyyMMddHHmmssfff")}.jpg";
            WriteBytesToFile(pathFile, HttpUtil.GetBytesFromUrl(imgUrl));
            return pathFile;
        }


        private static void WriteBytesToFile(string fileName, byte[] content)
        {
            FileStream fs = new FileStream(fileName, FileMode.Create);
            BinaryWriter w = new BinaryWriter(fs);
            try
            {
                w.Write(content);
            }
            finally
            {
                fs.Close();
                w.Close();
            }
        }

        public static string SaveImg(Bitmap bitmap)
        {
            string pathFile = AppDomain.CurrentDomain.SetupInformation.ApplicationBase + "Uploads/Captcha/";

            //判断文件夹是否存在
            if (!Directory.Exists(pathFile))
            {
                Directory.CreateDirectory(pathFile);
            }
            pathFile += $@"{DateTime.Now.ToString("yyyyMMddHHmmssfff")}.jpg";
            bitmap.Save(pathFile);
            return pathFile;
        }

        #endregion

        #region 二维码识别


        private void button16_Click(Bitmap bmp)
        {

            //if (bmp != null)
            //{

            //    // 图片二值化
            //    Bitmap bm = ConvertToBinaryImage(bmp);


            //    long lastTicks = DateTime.Now.Ticks;

            //    Hashtable table = new Hashtable();

            //    // if dbs is empty, load from file
            //    var dbs = new Database("C:\\temp\\ImageDatabase\\database_modified.txt");


            //    // 查找字符边界
            //    List<int> list = RecordBlack(bm);
            //    List<int[]> edgeList = FindEdge(list);

            //    // 将图片分割成四分或者更少
            //    foreach (int[] range in edgeList)
            //    {
            //        // 图像分割
            //        Bitmap temp = CutImage(bm, range[0], range[1] - range[0]);
            //        //  遍历所有样本库元素
            //        for (int i = 0; i < dbs.codeArray.Length; i++)
            //        {
            //            for (int j = 0; j < dbs.codeArray[i].Count; j++)
            //            {
            //                // 图片扫描
            //                int len = dbs.codeArray[i][j].len;
            //                // 从图片中扫描制定宽度
            //                for (int k = 0; k < temp.Width - len + 1; k++)
            //                {
            //                    ulong[] codeArray = ScanImageUlong(temp, len, k);
            //                    // 字符匹配
            //                    double matchRate = MatchTwoCodes(codeArray, dbs.codeArray[i][j].code);

            //                    if (matchRate > 0.95) // 0.95 matchRateThreshold
            //                    {
            //                        if (!table.Contains(k + range[0]))
            //                            table.Add(k + range[0], dbs.codeArray[i][j].character);
            //                    }
            //                    if (matchRate >= 0.99)  // 0.99 matchRateThreshold_PerfectMatch
            //                    {
            //                        i = dbs.codeArray.Length - 1;
            //                        j = dbs.codeArray[i].Count; // break j first, then i                                   
            //                        break; // 成功找到匹配项，停止
            //                    }
            //                }
            //            }
            //        }
            //        temp.Dispose();
            //    }
            //    bm.Dispose();
            //    // 对匹配结果进行删减
            //    List<string> resultList = SortHashtable(table);
            //    if (resultList.Count == 4)
            //    {
            //        var code = resultList[0] + resultList[1] + resultList[2] + resultList[3];
            //        long elapsedTicks = DateTime.Now.Ticks - lastTicks;
            //        double diff = (new TimeSpan(elapsedTicks)).TotalMilliseconds;
            //        //MessageBox.Show("匹配成功，用时" + diff + "毫秒");
            //    }
            //    //else
            //    //MessageBox.Show("couldn't match!!!");
            //}
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

        #region 1. 图片灰度处理
        /// <summary>
        /// 图像灰度化
        /// 由于RGB这种表示方式的数据量太大了，不便于处理，我们要选用另外一种图像属性：图像的亮度，或称为灰度。灰度的计算公式为Y=0.3R+0.59G+0.11B。
        /// 在进行了灰度化处理之后，图像中的每个象素只有一个值，那就是象素的灰度值。它的大小决定了象素的亮暗程度。下面的图片是对第一张验证码进行灰度化的结果 
        /// </summary>
        /// <param name="bmp"></param>
        /// <returns></returns>
        public static Bitmap ToGray(Bitmap bmp)
        {
            Bitmap bm = new Bitmap(bmp.Width, bmp.Height);

            for (int i = 0; i < bmp.Width; i++)
            {
                for (int j = 0; j < bmp.Height; j++)
                {
                    // 获取该点的像素的RGB的颜色
                    Color color = bmp.GetPixel(i, j);
                    // 利用公式计算灰度值
                    // 根据YUV的颜色空间中，Y的分量的物理意义是点的亮度，由该值反映亮度等级，
                    // 根据RGB和YUV颜色空间的变化关系可建立亮度Y与R、G、B三个颜色分量的对应：
                    // Y=0.3R+0.59G+0.11B，以这个亮度值表达图像的灰度值
                    int gray = (int)(color.R * 0.3 + color.G * 0.59 + color.B * 0.11);
                    Color newColor = Color.FromArgb(gray, gray, gray);
                    bm.SetPixel(i, j, newColor);
                }
            }
            return bm;
        }
        #endregion

        #region 2. 图片二值化

        /// <summary>
        /// 图像二值化1取图片的平均灰度作为阈值，低于该值的全都为0，高于该值的全都为255
        /// 在进行了灰度化处理之后，图像中的每个象素只有一个值，那就是象素的灰度值。它的大小决定了象素的亮暗程度。
        /// 为了更加便利的开展下面的图像处理操作，还需要对已经得到的灰度图像做一个二值化处理。图像的二值化就是把
        /// 图像中的象素根据一定的标准分化成两种颜色。在系统中是根据象素的灰度值处理成黑白两种颜色。和灰度化相似的，
        /// 图像的二值化也有很多成熟的算法。它可以采用自适应阀值法，也可以采用给定阀值法。
        /// </summary>
        /// <param name="bmp"></param>
        /// <returns></returns>
        public static Bitmap ConvertToBinaryImage(Bitmap bmp)
        {
            Bitmap bm = new Bitmap(bmp.Width, bmp.Height);
            int average = 0;
            for (int i = 0; i < bmp.Width; i++)
            {
                for (int j = 0; j < bmp.Height; j++)
                {
                    Color color = bmp.GetPixel(i, j);
                    average += color.B;
                }
            }
            average = (int)average / (bmp.Width * bmp.Height);

            for (int i = 0; i < bmp.Width; i++)
            {
                for (int j = 0; j < bmp.Height; j++)
                {
                    //获取该点的像素的RGB的颜色
                    Color color = bmp.GetPixel(i, j);
                    int value = 255 - color.B;
                    Color newColor = value > average ? Color.FromArgb(0, 0, 0) : Color.FromArgb(255, 255, 255);
                    bm.SetPixel(i, j, newColor);
                }
            }
            return bm;
        }

        /// <summary>
        /// THRESH_BINARY二值化算法。原理就是大于设定得值就设置为最大值
        /// 图像的二值化就是把图像中的象素根据一定的标准划分成两种颜色。在系统中是根据象素的灰度值处理成黑白两种颜色，和灰度化相似。二值化之后每一个点的像素就可以只用一个bit来表示了，即0或者1. 
        /// </summary>
        public static Bitmap ImgTo2(Bitmap bt)
        {
            Color p1;
            Bitmap bt1 = bt.Clone() as Bitmap;
            for (int x1 = 0; x1 < bt.Width; x1++)
            {
                for (int x2 = 0; x2 < bt.Height; x2++)
                {
                    p1 = bt.GetPixel(x1, x2);
                    int temp = (p1.R + p1.B + p1.G) / 3;
                    bt1.SetPixel(x1, x2, Color.FromArgb(temp, temp, temp));
                }
            }

            Color p2;
            for (int x1 = 0; x1 < bt1.Width; x1++)
            {
                for (int x2 = 0; x2 < bt1.Height; x2++)
                {
                    p2 = bt1.GetPixel(x1, x2);
                    if (p2.G > 150)
                    {
                        bt1.SetPixel(x1, x2, Color.FromArgb(255, 255, 255));
                    }
                    else
                    {
                        bt1.SetPixel(x1, x2, Color.FromArgb(0, 0, 0));
                    }
                }
            }

            return bt1;
        }
        #endregion

        #region 3. 字符边缘检测
        /// <summary>
        /// 字符边缘检测
        /// 符边缘检测是非常关键的一步，这里采用了一种比较简单的方法：首先，在二值化之后操作, 对图从左到右进行逐点扫描，找出其中有值等于0的列数（即找出图片中验证码字符（为黑色）所在的列数），记录到列表中
        /// </summary>
        /// <param name="bmp"></param>
        /// <returns></returns>
        public static List<int> RecordBlack(Bitmap bmp)
        {
            List<int> list = new List<int>();

            for (int x = 0; x < bmp.Width; x++) //200
            {
                for (int y = 0; y < bmp.Height; y++) //50
                {
                    if (bmp.GetPixel(x, y).R == 0)
                    {
                        if (!list.Contains(x)) // occur for the first time
                            list.Add(x);
                    }
                }
            }
            return list;
        }
        /// <summary>
        /// 字符边缘检测
        /// 然后对列表中记录的数值进行两两比较，若值大于1，则认为是边界，并记录到边界列表中。另外边界列表中还将记录第一个列表中的首尾两个值。这样每两个数一组就可以代表验证码中每一个字符的前后边界（列数）了
        /// </summary>
        /// <param name="Deslist"></param>
        /// <returns></returns>
        public static List<int[]> FindEdge(List<int> Deslist)
        {
            List<int[]> rtnList = new List<int[]>();
            if (Deslist.Count == 0) return rtnList;
            List<int> tempList = new List<int>();
            for (int i = 1; i < Deslist.Count; i++)
            {
                if (Deslist[i] - Deslist[i - 1] > 1)
                {
                    tempList.Add(Deslist[i - 1]); // 
                    tempList.Add(Deslist[i]);
                }
            }
            tempList.Add(Deslist[Deslist.Count - 1]);  // add head and tail
            tempList.Insert(0, Deslist[0]);
            for (int i = 0; i < tempList.Count; i++)
            {
                if (i % 2 == 1)
                {
                    int[] temp = new int[2];
                    temp[0] = tempList[i - 1];  // start
                    temp[1] = tempList[i];     // end
                    rtnList.Add(temp);
                }
            }
            return rtnList;
        }
        #endregion

        #region 4. 图像分割
        /// <summary>
        /// 图像分割
        /// </summary>
        /// <param name="sourceMap"></param>
        /// <param name="pos"></param>
        /// <param name="width"></param>
        /// <returns></returns>
        public static Bitmap CutImage(Bitmap sourceMap, int pos, int width)
        {
            Bitmap rtnMap = new Bitmap(width, sourceMap.Height);
            Rectangle srcReg = new Rectangle(pos, 0, width, sourceMap.Height);
            //Rectangle destReg = new Rectangle(0, 0, width, sourceMap.Height);
            rtnMap = sourceMap.Clone(srcReg, sourceMap.PixelFormat);
            return rtnMap;
        }

        // 二值化之后的图像进行字符边缘检测，并分割为四幅小的图片
        //public static void Cut(Bitmap bm)
        //{
        //    List<int> list = RecordBlack(bm);
        //    List<int[]> edgeList = FindEdge(list);
        //    List<Bitmap> bmList = new List<Bitmap>();

        //    foreach (int[] dou in edgeList)
        //    {
        //        Bitmap m1 = CutImage(bm, dou[0], dou[1] - dou[0]);
        //        bmList.Add(m1);
        //    }
        //}
        #endregion

        #region 5. 字符扫描
        /// <summary>
        /// 字符扫描
        /// 分割出来的单个字符按像素进行扫描。注意下面的代码中并没有将字符扫描的结果按0或者1存在string中，而是将每一列扫描的结果存在了一个类型为ulong的无符号长整型中。这里每一列的宽度是50，而ulong为64位，完全可以存下一列中所有的值
        /// </summary>
        /// <param name="bmp"></param>
        /// <returns></returns>
        public static string ScanImageUlong(Bitmap bmp)
        {
            //每行每列扫描获取图片数字编码字符
            string CodeNumber = bmp.Width.ToString();  //定义一个字符串变量用于存储特征码
            ulong code;

            //对图进行逐点扫描，当Ｒ值不等于２５５时则将CodeNumber记为1，否则记为0
            for (int x = 0; x < bmp.Width; x++) //行扫描，由x.0至x.图片宽度
            {
                code = 0;
                for (int y = 0; y < bmp.Height; y++) //列扫描，由y.0至图片高度
                {
                    if (bmp.GetPixel(x, y).R == 0)  //对图片中的点进行判断，当x,y点中的R色为0的时候
                    {
                        //记录为1
                        if (y < 64)
                            code += (ulong)1 << y;
                    }
                }
                CodeNumber += "," + code.ToString();
            }

            //关闭图片
            //bmp.Dispose();
            return CodeNumber;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bmp"></param>
        /// <param name="len">图片扫描长度</param>
        /// <param name="k">第几个宽度</param>
        /// <returns></returns>
        public static string ScanImageUlong(Bitmap bmp, int len, int k)
        {
            //每行每列扫描获取图片数字编码字符
            string CodeNumber = bmp.Width.ToString();  //定义一个字符串变量用于存储特征码
            ulong code;

            //对图进行逐点扫描，当Ｒ值不等于２５５时则将CodeNumber记为1，否则记为0
            for (int x = 0; x < bmp.Width; x++) //行扫描，由x.0至x.图片宽度
            {
                code = 0;
                for (int y = 0; y < bmp.Height; y++) //列扫描，由y.0至图片高度
                {
                    if (bmp.GetPixel(x, y).R == 0)  //对图片中的点进行判断，当x,y点中的R色为0的时候
                    {
                        //记录为1
                        if (y < 64)
                            code += (ulong)1 << y;
                    }
                }
                CodeNumber += "," + code.ToString();
            }

            //关闭图片
            //bmp.Dispose();
            return CodeNumber;
        }
        #endregion

        #region 6. 字符匹配

        /// <summary>
        /// 字符匹配
        ///为了提高效率，如果一个冒号小于此阈值，则返回0
        /// </summary>
        /// <param name="code1"></param>
        /// <param name="code2"></param>
        /// <returns></returns>
        public static double MatchTwoCodes(ulong[] code1, ulong[] code2)
        {
            //// record the match cases
            //int count = 0;
            //double total = code1.Length * ulongBit;

            //for (int i = 0; i < code1.Length; i++)
            //{
            //    ulong rtn = (code1[i] ^ code2[i]);  // if same, the result will be 0
            //    int nMatch = BitCount(rtn);   // 计算不匹配
            //    count += nMatch;

            //    if (ulongBit - nMatch < ulongBit * matchRateThreshold_colomn)  // 0.9
            //        return 0;
            //}
            //return 1.0 - count / total;
            return 0;
        }
        /// <summary>
        /// 任意给定一个32位无符号整数n，求n的二进制表示中1的个数，
        /// 比如n = 5（0101）时，返回2，n = 15（1111）时，返回4
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        private static int BitCount(ulong n)
        {
            int c = 0; // 计数器
            while (n > 0)
            {
                if ((n & 1) == 1) // 当前位是1
                    ++c; // 计数器加1
                n >>= 1; // 移位
            }
            return c;
        }
        #endregion

        #endregion 
    }
}