using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.IO;
using System.Drawing.Imaging;
using System.Diagnostics;
using SentraUtility;

namespace SentraSolutionFramework
{
    //[DebuggerNonUserCode]
    public static class Helper
    {
        public static byte[] ConvertImageToByteArray(Image imageToConvert)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                imageToConvert.Save(ms, ImageFormat.Png);
                //imageToConvert.Save(ms, imageToConvert.RawFormat);

                byte[] RetVal = ms.ToArray();
                ms.Close();
                return RetVal;
            }
        }
        public static Image ConvertByteArrayToImage(Byte[] Data)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                ms.Write(Data, 0, Data.Length);
                Bitmap Bmp = new Bitmap(ms);
                Bitmap Bmp2 = new Bitmap(Bmp.Width, Bmp.Height);
                Graphics gp = Graphics.FromImage(Bmp2);
                gp.DrawImage(Bmp, 0, 0, Bmp.Width, Bmp.Height);
                gp.Dispose();
                Bmp.Dispose();
                ms.Close();
                return Bmp2;
            }
        }
        public static Image ConvertStreamToImage(Stream ms)
        {
                Bitmap Bmp = new Bitmap(ms);
                Bitmap Bmp2 = new Bitmap(Bmp.Width, Bmp.Height);
                Graphics gp = Graphics.FromImage(Bmp2);
                gp.DrawImage(Bmp, 0, 0, Bmp.Width, Bmp.Height);
                gp.Dispose();
                Bmp.Dispose();
                return Bmp2;
        }

        public static DateTime GetRealDate(object DateValue)
        {
            if (DateValue.GetType() == typeof(DateTime))
                return (DateTime)DateValue;
            
            string tmpValue = DateValue.ToString();
            if (tmpValue.Substring(0, 5) == "Today")
            {
                if (tmpValue.Length == 5)
                    return BaseFramework.DefaultDp.GetDbDate();
                else
                    return BaseFramework.DefaultDp.GetDbDate().AddDays(double.Parse(
                        tmpValue.Substring(5)));
            }
            if (tmpValue.Substring(0, 3) == "Now")
                return BaseFramework.DefaultDp.GetDbDateTime();
            if (tmpValue.Substring(0, 10) == "StartMonth")
            {
                DateTime Today = BaseFramework.DefaultDp.GetDbDate();
                if (tmpValue.Length == 10)
                    return new DateTime(Today.Year, Today.Month, 1);
                else
                    return new DateTime(Today.Year, Today.Month, 1 +
                        int.Parse(tmpValue.Substring(10)));
            }
            if (tmpValue.Substring(0, 8) == "EndMonth")
            {
                DateTime Today = BaseFramework.DefaultDp.GetDbDate();
                if (tmpValue.Length == 10)
                    return new DateTime(Today.Year, Today.Month, 
                        DateTime.DaysInMonth(Today.Year, Today.Month));
                else
                    return new DateTime(Today.Year, Today.Month,
                        DateTime.DaysInMonth(Today.Year, Today.Month)).AddDays(
                        double.Parse(tmpValue.Substring(10)));
            }
            return BaseFramework.DefaultDp.GetDbDate();
        }
    }
}
