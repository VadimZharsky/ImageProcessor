using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ImageProcessor
{
    class Photo
    {
        //public string name { get; set; }
        //public Image image { get; set; }
        //public FileInfo fileInfo { get; set;}
        //public PropertyItem propertyItem { get; set; }
        //private string extension;

        //public string Extension
        //{
        //    get { return extension; }
        //    set
        //    {
        //        extension = Path.GetExtension(fileInfo.FullName);
        //    }

        //}
        public FileInfo fileInfo;
        public Image photoImage;
        public string name;
        public string extension;
        public string dateTaken;
        public string gpsCoords;

        public Photo(FileInfo fileInfo)
        {
            this.fileInfo = fileInfo;
            photoImage = Image.FromFile(fileInfo.FullName);
            name = Path.GetFileNameWithoutExtension(fileInfo.Name);
            extension = fileInfo.Extension;
            gpsCoords = GetGPS(photoImage);
            dateTaken = GetDate(photoImage);
            
        }

        private string GetDate(Image photoImage)
        {
            try
            {
                Regex r = new Regex(":");
                var dateTakenTime = DateTime.Parse(r.Replace(Encoding.UTF8.GetString(photoImage.GetPropertyItem(36867).Value), "-", 2));
                return$"-{DateParams(dateTakenTime)}";
            }
            catch
            {
                DateTime creationDate = fileInfo.CreationTime;
                return $"-created({DateParams(creationDate)})";
            }
        }

        private string GetGPS(Image photoImage)
        {
            try
            {
                string gpsLatitudeRef = BitConverter.ToChar(photoImage.GetPropertyItem(1).Value, 0).ToString();
                string latitude = DecodeRational64u(photoImage.GetPropertyItem(2));
                string gpsLongitudeRef = BitConverter.ToChar(photoImage.GetPropertyItem(3).Value, 0).ToString();
                string longitude = DecodeRational64u(photoImage.GetPropertyItem(4));
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

        private string DateParams(DateTime date)
        {
            return $"{date.Year}_{date.Month}_{date.Day}";
        }

    }

        

}
