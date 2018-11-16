using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Drawing;

namespace ImageInfoLibrary

{
    public static class ImageInfoLib
    {
        public static class GetDate
        {
            public static DateTime AsDate(FileInfo fileInfo, Image image)
            {
                DateTime actualDate = DateGetter(fileInfo, image);
                return actualDate;
            }
            public static string AsString(FileInfo fileInfo, Image image)
            {
                DateTime actualDate = DateGetter(fileInfo, image);
                return DateParams(actualDate);
            }
            public static string Year(FileInfo file, Image image)
            {
                DateTime actualDate = AsDate(file, image);
                return actualDate.Year.ToString();
            }
        }
        
        internal static DateTime DateGetter(FileInfo fileInfo, Image image)
        {
            try
            {
                Regex r = new Regex(":");
                var dateTakenTime = DateTime.Parse(r.Replace(Encoding.UTF8.GetString(image.GetPropertyItem(36867).Value), "-", 2));
                return dateTakenTime;
            }
            catch
            {
                DateTime creationDate = fileInfo.CreationTime;
                return creationDate;
            }
        }
        public static string GetGPS(Image image)
        {
            try
            {
                string gpsLatitudeRef = BitConverter.ToChar(image.GetPropertyItem(1).Value, 0).ToString();
                string latitude = DecodeRational64u(image.GetPropertyItem(2));
                string gpsLongitudeRef = BitConverter.ToChar(image.GetPropertyItem(3).Value, 0).ToString();
                string longitude = DecodeRational64u(image.GetPropertyItem(4));
                return $"{gpsLatitudeRef} {latitude} {gpsLongitudeRef} {longitude}";
            }
            catch { return ""; }
        }
        private static string DecodeRational64u(System.Drawing.Imaging.PropertyItem propertyItem)
        {
            uint dN = BitConverter.ToUInt32(propertyItem.Value, 0);
            uint dD = BitConverter.ToUInt32(propertyItem.Value, 4);
            uint mN = BitConverter.ToUInt32(propertyItem.Value, 8);
            uint mD = BitConverter.ToUInt32(propertyItem.Value, 12);
            uint sN = BitConverter.ToUInt32(propertyItem.Value, 16);
            uint sD = BitConverter.ToUInt32(propertyItem.Value, 20);

            decimal deg;
            decimal min;
            decimal sec;
            // Found some examples where you could get a zero denominator and no one likes to devide by zero
            if (dD > 0) { deg = (decimal)dN / dD; } else { deg = dN; }
            if (mD > 0) { min = (decimal)mN / mD; } else { min = mN; }
            if (sD > 0) { sec = (decimal)sN / sD; } else { sec = sN; }

            if (sec == 0) return string.Format("{0}° {1:0.###}'", deg, min);
            else return string.Format("{0}° {1:0}' {2:0.#}\"", deg, min, sec);
        }
        private static string DateParams(DateTime date)
        {
            return $"_{date.Year}_{date.Month}_{date.Day}";
        }
    }
    
}
