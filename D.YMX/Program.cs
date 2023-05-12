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
        /// 1. �õ��Ҷ�ͼ��ǰ���������ٽ�ֵ �����䷽��� 
        /// �����ǰ��/�����ķֽ�ֵ
        /// UnCodebase������һ��GetPicValidByValue(int dgGrayValue) ���������Եõ�ǰ������Ч����
        /// ����������ǰ��/�����ķֽ�ֵdgGrayValue�����ȷ���ģ����õ��ǻҶ�128����
        /// ���ֵ�Ļ�ȡ������ѧ�㷨���������䷽�����ͼ���ǰ�󾰵�ƽ����Ϊ���ʱ��ֵ�������ǹ��ĵķֽ�ֵ���Ը��� �����ϸ��ӵı����ǳ����ã�
        /// </summary>         
        /// <returns>ǰ���������ٽ�ֵ</returns>         
        public static int GetDgGrayValue(Bitmap bmpobj)
        {
            int[] pixelNum = new int[256];
            //ͼ��ֱ��ͼ����256����
            int n, n1, n2;
            int total;
            //totalΪ�ܺͣ��ۼ�ֵ
            double m1, m2, sum, csum, fmax, sb;
            //sbΪ��䷽�fmax�洢��󷽲�ֵ
            int k, t, q;
            int threshValue = 1;
            // ��ֵ
            int step = 1;
            //����ֱ��ͼ
            for (int i = 0; i < bmpobj.Width; i++)
            {
                for (int j = 0; j < bmpobj.Height; j++)
                {
                    //���ظ��������ɫ����RGB��ʾ
                    pixelNum[bmpobj.GetPixel(i, j).R]++;
                    //��Ӧ��ֱ��ͼ��1
                }
            }            //ֱ��ͼƽ����
            for (k = 0; k <= 255; k++)
            {
                total = 0; for (t = -2; t <= 2; t++)
                //�븽��2���Ҷ���ƽ������tֵӦȡ��С��ֵ
                {
                    q = k + t; if (q < 0)
                        //Խ�紦��
                        q = 0; if (q > 255) q = 255; total = total + pixelNum[q];
                    //totalΪ�ܺͣ��ۼ�ֵ
                }
                pixelNum[k] = (int)((float)total / 5.0 + 0.5);
                //ƽ���������2��+�м�1��+�ұ�2���Ҷȣ���5���������ܺͳ���5�������0.5��������ֵ
            }            //����ֵ
            sum = csum = 0.0; n = 0;
            //�����ܵ�ͼ��ĵ����������أ�Ϊ����ļ�����׼��
            for (k = 0; k <= 255; k++)
            {
                sum += (double)k * (double)pixelNum[k];
                //x*f(x)�����أ�Ҳ����ÿ���Ҷȵ�ֵ�������������һ����Ϊ���ʣ���sumΪ���ܺ�
                n += pixelNum[k];                       //nΪͼ���ܵĵ�������һ��������ۻ�����
            }
            fmax = -1.0;
            //��䷽��sb������Ϊ��������fmax��ʼֵΪ-1��Ӱ�����Ľ���
            n1 = 0; for (k = 0; k < 256; k++)
            //��ÿ���Ҷȣ���0��255������һ�ηָ�����䷽��sb
            {
                n1 += pixelNum[k];
                //n1Ϊ�ڵ�ǰ��ֵ��ǰ��ͼ��ĵ���
                if (n1 == 0) { continue; }            //û�зֳ�ǰ����
                n2 = n - n1;                        //n2Ϊ����ͼ��ĵ���
                if (n2 == 0) { break; }               //n2Ϊ0��ʾȫ�����Ǻ�ͼ����n1=0������ƣ�֮��ı���������ʹǰ���������ӣ����Դ�ʱ�����˳�ѭ��
                csum += (double)k * pixelNum[k];    //ǰ���ġ��Ҷȵ�ֵ*����������ܺ�
                m1 = csum / n1;                     //m1Ϊǰ����ƽ���Ҷ�
                m2 = (sum - csum) / n2;
                //m2Ϊ������ƽ���Ҷ�
                sb = (double)n1 * (double)n2 * (m1 - m2) * (m1 - m2);   //sbΪ��䷽��
                if (sb > fmax)                  //����������䷽�����ǰһ���������䷽��
                {
                    fmax = sb;                    //fmaxʼ��Ϊ�����䷽�otsu��
                    threshValue = k;              //ȡ�����䷽��ʱ��Ӧ�ĻҶȵ�k���������ֵ
                }
            }
            return threshValue;
        }


        /// <summary>         
        ///  ȥ���ӵ㣨�ʺ��ӵ�/���ߴ�Ϊ1��  
        ///  //���ȥ�����ŵ�/������
        /// ÿһ�㼰�ܱ�8������������һ�����ֱ�Ϊ1�㣬3�㣬8�㣩������һ�ָ�����Ϣ�����ȱ��ַ�������С�ĵ�����������ھͿ��Զ��ֱ�дȥ�Ӵ����ˡ�
        /// 2.2 �����ܱ���Ч����ȥ�뺯��
        /// </summary>        
        /// <param name="dgGrayValue">��ǰ����ɫ����</param>         
        /// <returns></returns>   

        public static void ClearNoise(Bitmap bmpobj, int dgGrayValue, int MaxNearPoints)
        {
            Color piexl; int nearDots = 0;
            int XSpan, YSpan, tmpX, tmpY;
            //����ж�
            for (int i = 0; i < bmpobj.Width; i++)
            {
                for (int j = 0; j < bmpobj.Height; j++)
                {
                    piexl = bmpobj.GetPixel(i, j);

                    if (piexl.R < dgGrayValue)
                    {
                        nearDots = 0;                        //�ж���Χ8�����Ƿ�ȫΪ��

                        if (i == 0 || i == bmpobj.Width - 1 || j == 0 || j == bmpobj.Height - 1)  //�߿�ȫȥ��
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
                        //ȥ������ && ��ϸС3�ڱߵ�       
                    }
                    else
                    {
                        //����
                        bmpobj.SetPixel(i, j, Color.FromArgb(255, 255, 255));
                    }
                }
            }
        }

        /// <summary>
        /// 3��3��ֵ�˲����ӣ�
        /// �˲��㷨ȥ�뺯�� 
        /// ͼ��Ԥ�������ж����˲��㷨����ԭ���뷽���ֱ�Ϊ
        /// 1�� ��ֵ�˲�
        /// ��ͨ����ͼ���е�ĳ����������ȡ�����������ݽ�������õ��Ľ��������˼�壬��ν��ֵ���Ǵ��������������ݰ���С˳�����к�������λ�õ��Ǹ�������ֵ�˲��Դ��ڵ���ֵ��Ϊ��������
        /// ʵ�������ܼ�
        /// 1���ȶԴ�������
        /// 2������������ֵȡ��Ҫ��������ݼ���
        /// ע�����
        /// 1��ע��ͼ���Ե���ݵĴ���
        /// 2�����ڲ�ͬ��Ŀ��ѡ�ò�ͬ�Ĵ��壬һ����3��3��5��5�ȵ�
        /// </summary>
        /// <param name="dgGrayValue"></param>
        public static void ClearNoise(Bitmap bmpobj, int dgGrayValue)
        {
            int x, y;
            byte[] p = new byte[9]; //��С������3*3
            byte s;
            //byte[] lpTemp=new BYTE[nByteWidth*nHeight];
            int i, j;

            //--!!!!!!!!!!!!!!���濪ʼ����Ϊ3��3��ֵ�˲�!!!!!!!!!!!!!!!!
            for (y = 1; y < bmpobj.Height - 1; y++) //--��һ�к����һ���޷�ȡ����
            {
                for (x = 1; x < bmpobj.Width - 1; x++)
                {
                    //ȡ9�����ֵ
                    p[0] = bmpobj.GetPixel(x - 1, y - 1).R;
                    p[1] = bmpobj.GetPixel(x, y - 1).R;
                    p[2] = bmpobj.GetPixel(x + 1, y - 1).R;
                    p[3] = bmpobj.GetPixel(x - 1, y).R;
                    p[4] = bmpobj.GetPixel(x, y).R;
                    p[5] = bmpobj.GetPixel(x + 1, y).R;
                    p[6] = bmpobj.GetPixel(x - 1, y + 1).R;
                    p[7] = bmpobj.GetPixel(x, y + 1).R;
                    p[8] = bmpobj.GetPixel(x + 1, y + 1).R;
                    //������ֵ
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
                    bmpobj.SetPixel(x, y, Color.FromArgb(p[4], p[4], p[4]));    //����Чֵ����ֵ
                }
            }
        }
    }
}