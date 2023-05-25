using D.YMX.LogUtils;
using D.YMX.Models;
using System.Collections;
using System.Diagnostics;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Net;
using System.Reflection.Metadata;
using System.Runtime.InteropServices;
using System.Text;
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

        public static string SaveCaptchaImage(string imgUrl, bool openProxy = false)
        {
            string pathFile = AppDomain.CurrentDomain.SetupInformation.ApplicationBase + "Uploads/Captcha/";

            //判断文件夹是否存在
            if (!Directory.Exists(pathFile))
            {
                Directory.CreateDirectory(pathFile);
            }
            pathFile += $@"{DateTime.Now.ToString("yyyyMMddHHmmssfff")}.jpg";
            WriteBytesToFile(pathFile, HttpUtil.GetBytesFromUrl(imgUrl,openProxy));
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

        #region 3. 图像分割

        /// <summary>
        /// 二值化之后的图像进行字符边缘检测，并分割为四幅小的图片
        /// </summary>
        /// <param name="bm"></param>
        public static List<Bitmap> Cut(Bitmap bm)
        {
            List<int> list = RecordBlack(bm);
            List<int[]> edgeList = FindEdge(list);
            var bmList = new List<Bitmap>();

            foreach (int[] dou in edgeList)
            {
                Bitmap m1 = CutImage(bm, dou[0], dou[1] - dou[0]);
                bmList.Add(m1);
            }
            return bmList;
        }

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


        #region 字符边缘检测
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
        #endregion

        #region 4. 字符扫描
        /// <summary>
        /// 字符扫描
        /// 分割出来的单个字符按像素进行扫描。注意下面的代码中并没有将字符扫描的结果按0或者1存在string中，而是将每一列扫描的结果存在了一个类型为ulong的无符号长整型中。这里每一列的宽度是50，而ulong为64位，完全可以存下一列中所有的值
        /// </summary>
        /// <param name="bmp"></param>
        /// <returns></returns>
        public static string ScanImageUlong(Bitmap bmp)
        {
            //每行每列扫描获取图片数字编码字符
            string codeNumber = bmp.Width.ToString();  //定义一个字符串变量用于存储特征码

            //对图进行逐点扫描，当Ｒ值不等于255时则将CodeNumber记为1，否则记为0
            for (int x = 0; x < bmp.Width; x++) //行扫描，由x.0至x.图片宽度
            {
                StringBuilder code = new StringBuilder();
                for (int y = 0; y < bmp.Height; y++) //列扫描，由y.0至图片高度
                {
                    if (x == 1 && y == 33)
                    {
                        var r = bmp.GetPixel(x, y).R;
                    }

                    if (bmp.GetPixel(x, y).R == 0)  //对图片中的点进行判断，当x,y点中的R色为0的时候
                    {
                        code.Append("0");
                    }
                    else
                    {
                        code.Append("1");
                    }
                }
                codeNumber += "," + code.ToString();
            }

            //关闭图片
            return codeNumber;
        }
        #endregion

        #region 6. 字符匹配

        /// <summary>
        /// 我们定义了两个不同长度的字符数组 arr1 和 arr2，并使用 Math.Min 方法获取它们的最小长度。
        /// 然后，我们使用一个循环遍历两个数组中的元素，并计算它们的匹配率。最后，我们将匹配率输出到控制台。
        /// 请注意，上面的代码假设两个数组中的元素都是 0 或 1。
        /// </summary>
        /// <param name="arr1"></param>
        /// <param name="arr2"></param>
        /// <returns></returns>
        public static double CompareArr(string[] arr1, string[] arr2)
        {
            int matchCount = 0;
            int minLength = Math.Min(arr1.Length, arr2.Length);

            for (int i = 0; i < minLength; i++)
            {
                if (arr1[i] == arr2[i])
                {
                    matchCount++;
                }
            }

            double matchRate = (double)matchCount / minLength;
            return matchRate;
        }

        /// <summary>
        /// 计算匹配率 输出应该是“Match rate: 90%”。
        /// </summary>
        /// <param name="str1"></param>
        /// <param name="str2"></param>
        /// <returns></returns>
        public static double CompareArr(string str1, string str2)
        {
            int maxLength = Math.Max(str1.Length, str2.Length);
            int minLength = Math.Min(str1.Length, str2.Length);
            int matchingChars = 0;

            for (int i = 0; i < minLength; i++)
            {
                if (str1[i] == str2[i])
                {
                    matchingChars++;
                }
            }

            double matchRate = (double)matchingChars / maxLength * 100;
            return matchRate;
        }

        /// <summary>
        /// 字符匹配
        ///为了提高效率，如果一个冒号小于此阈值，则返回0
        /// </summary>
        /// <param name="code1"></param>
        /// <param name="code2"></param>
        /// <returns></returns>
        public static double MatchTwoCodes(ulong[] code1, ulong[] code2)
        {
            var ulongBit = 0.9;
            int count = 0;
            double total = code1.Length * ulongBit;

            for (int i = 0; i < code1.Length; i++)
            {
                ulong rtn = (code1[i] ^ code2[i]);  //如果相同，则结果为0
                int nMatch = BitCount(rtn);   // 计算不匹配
                count += nMatch;

                //if (ulongBit - nMatch < ulongBit * 0.9) 
                //    return 0;
            }
            return 1.0 - count / total;
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


        private static string[] files = null;
        /// <summary>
        /// 直接调用的匹配
        /// </summary>
        /// <param name="imgPath">单个验证码路径</param>
        /// <returns></returns>
        public static string GetCaptchaImage(string imgPath)
        {
            if (files == null)
            {
                // 1. 加载字模
                StreamReader sr = null;
                try
                {
                    string filePath = AppDomain.CurrentDomain.SetupInformation.ApplicationBase + "Uploads/Captcha/template.txt";
                    sr = new StreamReader(filePath);
                    var str = sr.ReadToEnd();
                    files = str.Split(")");
                }
                catch (Exception ex)
                {
                    NLogUtil.Log.Error(ex);
                }
                finally
                {
                    sr.Dispose();
                }
            }

            // 2. 灰度，二值化，切割图片
            using (var bitMap = new Bitmap(imgPath))
            {
                var garyImg = ImgUtil.ToGray(bitMap);
                var img2 = ImgUtil.ConvertToBinaryImage(garyImg);
                var imgs = ImgUtil.Cut(img2);
                var codeList = new List<string>();
                for (int i = 0; i < imgs.Count; i++)
                {
                    var code = ImgUtil.ScanImageUlong(imgs[i]);
                    codeList.Add(code);
                }


                var result = string.Empty;
                // 3. 匹配
                for (int z = 0; z < codeList.Count; z++)
                {

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
                                result += templateCharacter;
                            }
                        }
                    }
                }

                return result;
            }
        }
        #endregion 
    }
}
