using System.Net;

namespace D.YMX.Utils
{
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
            WriteBytesToFile(pathFile, GetBytesFromUrl(imgUrl)); 
            return pathFile;
        }

        private static byte[] GetBytesFromUrl(string url)
        {
            byte[] b;
            HttpWebRequest myReq = (HttpWebRequest)WebRequest.Create(url);
            WebResponse myResp = myReq.GetResponse();

            Stream stream = myResp.GetResponseStream();
            using (BinaryReader br = new BinaryReader(stream))
            {
                b = br.ReadBytes(500000);
                br.Close();
            }
            myResp.Close();
            return b;

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

        #endregion


        #region 图片二值化

        /// <summary>
        /// THRESH_BINARY二值化算法。原理就是大于设定得值就设置为最大值
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
    }
}
