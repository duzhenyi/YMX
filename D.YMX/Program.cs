namespace D.YMX
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            Application.Run(new FrmMain());
        }

        /// <summary>         
        /// 1. 得到灰度图像前景背景的临界值 最大类间方差法， 
        /// 如何设前景/背景的分界值
        /// UnCodebase类中有一个GetPicValidByValue(int dgGrayValue) 函数，可以得到前景的有效区域，
        /// 常有人问我前景/背景的分界值dgGrayValue是如何确定的（常用的是灰度128）。
        /// 这个值的获取是有数学算法，叫最大类间方差法，即图像的前后景的平方差为最大时的值就是我们关心的分界值，对付如 这样较复杂的背景非常管用，
        /// </summary>         
        /// <returns>前景背景的临界值</returns>         
        public static int GetDgGrayValue(Bitmap bmpobj)
        {
            int[] pixelNum = new int[256];
            //图象直方图，共256个点
            int n, n1, n2;
            int total;
            //total为总和，累计值
            double m1, m2, sum, csum, fmax, sb;
            //sb为类间方差，fmax存储最大方差值
            int k, t, q;
            int threshValue = 1;
            // 阈值
            int step = 1;
            //生成直方图
            for (int i = 0; i < bmpobj.Width; i++)
            {
                for (int j = 0; j < bmpobj.Height; j++)
                {
                    //返回各个点的颜色，以RGB表示
                    pixelNum[bmpobj.GetPixel(i, j).R]++;
                    //相应的直方图加1
                }
            }            //直方图平滑化
            for (k = 0; k <= 255; k++)
            {
                total = 0; for (t = -2; t <= 2; t++)
                //与附近2个灰度做平滑化，t值应取较小的值
                {
                    q = k + t; if (q < 0)
                        //越界处理
                        q = 0; if (q > 255) q = 255; total = total + pixelNum[q];
                    //total为总和，累计值
                }
                pixelNum[k] = (int)((float)total / 5.0 + 0.5);
                //平滑化，左边2个+中间1个+右边2个灰度，共5个，所以总和除以5，后面加0.5是用修正值
            }            //求阈值
            sum = csum = 0.0; n = 0;
            //计算总的图象的点数和质量矩，为后面的计算做准备
            for (k = 0; k <= 255; k++)
            {
                sum += (double)k * (double)pixelNum[k];
                //x*f(x)质量矩，也就是每个灰度的值乘以其点数（归一化后为概率），sum为其总和
                n += pixelNum[k];                       //n为图象总的点数，归一化后就是累积概率
            }
            fmax = -1.0;
            //类间方差sb不可能为负，所以fmax初始值为-1不影响计算的进行
            n1 = 0; for (k = 0; k < 256; k++)
            //对每个灰度（从0到255）计算一次分割后的类间方差sb
            {
                n1 += pixelNum[k];
                //n1为在当前阈值遍前景图象的点数
                if (n1 == 0) { continue; }            //没有分出前景后景
                n2 = n - n1;                        //n2为背景图象的点数
                if (n2 == 0) { break; }               //n2为0表示全部都是后景图象，与n1=0情况类似，之后的遍历不可能使前景点数增加，所以此时可以退出循环
                csum += (double)k * pixelNum[k];    //前景的“灰度的值*其点数”的总和
                m1 = csum / n1;                     //m1为前景的平均灰度
                m2 = (sum - csum) / n2;
                //m2为背景的平均灰度
                sb = (double)n1 * (double)n2 * (m1 - m2) * (m1 - m2);   //sb为类间方差
                if (sb > fmax)                  //如果算出的类间方差大于前一次算出的类间方差
                {
                    fmax = sb;                    //fmax始终为最大类间方差（otsu）
                    threshValue = k;              //取最大类间方差时对应的灰度的k就是最佳阈值
                }
            }
            return threshValue;
        }


        /// <summary>         
        ///  去掉杂点（适合杂点/杂线粗为1）  
        ///  //如何去除干扰点/干扰线
        /// 每一点及周边8个点的情况都不一样（分别为1点，3点，8点），这是一种干扰信息的粒度比字符的粒度小的典型情况。现在就可以动手编写去杂代码了。
        /// 2.2 根据周边有效点数去噪函数
        /// </summary>        
        /// <param name="dgGrayValue">背前景灰色界限</param>         
        /// <returns></returns>   

        public static void ClearNoise(Bitmap bmpobj, int dgGrayValue, int MaxNearPoints)
        {
            Color piexl; int nearDots = 0;
            int XSpan, YSpan, tmpX, tmpY;
            //逐点判断
            for (int i = 0; i < bmpobj.Width; i++)
            {
                for (int j = 0; j < bmpobj.Height; j++)
                {
                    piexl = bmpobj.GetPixel(i, j);

                    if (piexl.R < dgGrayValue)
                    {
                        nearDots = 0;                        //判断周围8个点是否全为空

                        if (i == 0 || i == bmpobj.Width - 1 || j == 0 || j == bmpobj.Height - 1)  //边框全去掉
                        {
                            bmpobj.SetPixel(i, j, Color.FromArgb(255, 255, 255));
                        }
                        else
                        {
                            if (bmpobj.GetPixel(i - 1, j - 1).R < dgGrayValue) nearDots++; if (bmpobj.GetPixel(i, j - 1).R < dgGrayValue) nearDots++;
                            if (bmpobj.GetPixel(i + 1, j - 1).R < dgGrayValue) nearDots++; if (bmpobj.GetPixel(i - 1, j).R < dgGrayValue) nearDots++;
                            if (bmpobj.GetPixel(i + 1, j).R < dgGrayValue) nearDots++; if (bmpobj.GetPixel(i - 1, j + 1).R < dgGrayValue) nearDots++;
                            if (bmpobj.GetPixel(i, j + 1).R < dgGrayValue) nearDots++; if (bmpobj.GetPixel(i + 1, j + 1).R < dgGrayValue) nearDots++;
                        }
                        if (nearDots < MaxNearPoints)
                            bmpobj.SetPixel(i, j, Color.FromArgb(255, 255, 255));
                        //去掉单点 && 粗细小3邻边点       
                    }
                    else
                    {
                        //背景
                        bmpobj.SetPixel(i, j, Color.FromArgb(255, 255, 255));
                    }
                }
            }
        }

        /// <summary>
        /// 3×3中值滤波除杂，
        /// 滤波算法去噪函数 
        /// 图像预处理中有多种滤波算法，其原理与方法分别为
        /// 1） 中值滤波
        /// 它通过从图像中的某个采样窗口取出奇数个数据进行排序得到的结果。顾名思义，所谓中值就是窗口中奇数个数据按大小顺序排列后处于中心位置的那个数。中值滤波以窗口的中值作为处理结果。
        /// 实现起来很简单
        /// 1：先对窗口排序
        /// 2：用排序后的中值取代要处理的数据即可
        /// 注意事项：
        /// 1：注意图像边缘数据的处理
        /// 2：对于不同的目的选用不同的窗体，一般有3×3，5×5等等
        /// </summary>
        /// <param name="dgGrayValue"></param>
        public static void ClearNoise(Bitmap bmpobj, int dgGrayValue)
        {
            int x, y;
            byte[] p = new byte[9]; //最小处理窗口3*3
            byte s;
            //byte[] lpTemp=new BYTE[nByteWidth*nHeight];
            int i, j;

            //--!!!!!!!!!!!!!!下面开始窗口为3×3中值滤波!!!!!!!!!!!!!!!!
            for (y = 1; y < bmpobj.Height - 1; y++) //--第一行和最后一行无法取窗口
            {
                for (x = 1; x < bmpobj.Width - 1; x++)
                {
                    //取9个点的值
                    p[0] = bmpobj.GetPixel(x - 1, y - 1).R;
                    p[1] = bmpobj.GetPixel(x, y - 1).R;
                    p[2] = bmpobj.GetPixel(x + 1, y - 1).R;
                    p[3] = bmpobj.GetPixel(x - 1, y).R;
                    p[4] = bmpobj.GetPixel(x, y).R;
                    p[5] = bmpobj.GetPixel(x + 1, y).R;
                    p[6] = bmpobj.GetPixel(x - 1, y + 1).R;
                    p[7] = bmpobj.GetPixel(x, y + 1).R;
                    p[8] = bmpobj.GetPixel(x + 1, y + 1).R;
                    //计算中值
                    for (j = 0; j < 5; j++)
                    {
                        for (i = j + 1; i < 9; i++)
                        {
                            if (p[j] > p[i])
                            {
                                s = p[j];
                                p[j] = p[i];
                                p[i] = s;
                            }
                        }
                    }
                    //      if (bmpobj.GetPixel(x, y).R < dgGrayValue)
                    bmpobj.SetPixel(x, y, Color.FromArgb(p[4], p[4], p[4]));    //给有效值付中值
                }
            }
        }
    }
}