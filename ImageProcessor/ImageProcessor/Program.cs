using System;
using System.Collections.Generic;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using ImageInfoLibrary;
using System.Threading;

namespace ImageProcessor
{
    class Program
    {
        static void Main(string[] args)
        {
            string sourceDir = @"F:\samsungA5Transfer\Phone\DCIM\Camera";
            DateOntoImage(sourceDir);
            Console.ReadKey();
        }



        private static void DateOntoImage(string sourceDir)
        {

            var dir = new DirectoryInfo(sourceDir);
            List<FileInfo> filesInfos = new List<FileInfo>(dir.GetFiles("*.jpg"));
            List<Photo> photos = new List<Photo>();
            DirectoryInfo parent = dir.Parent;
            string destDir = $@"{parent.FullName}\PhotoFolder";
            Directory.CreateDirectory(destDir);
            int count = 1;
            foreach (FileInfo file in filesInfos)
                photos.Add(new Photo(file));
            foreach (Photo photo in photos)
            {
                Image image = Image.FromFile(photo.GetInfo.FullName);
                string actualDate = ImageInfoLib.GetDate.AsString(photo.GetInfo, image);
                string actualYear = ImageInfoLib.GetDate.Year(photo.GetInfo, image);
                GraphicsOnImage(image, actualDate);
                string destWay = $@"{destDir}\{SetDestName(photo, actualDate, actualYear, destDir)}";
                image.Save(destWay, ImageFormat.Jpeg);
                Console.WriteLine($"{count}/{photos.Count} successful");
                image.Dispose();
                Thread.Sleep(100);
                Console.Clear();
                count++;
            }
            Console.WriteLine($"{count-1}/{photos.Count} successful");
        }

        private static void GraphicsOnImage(Image image, string actualDate)
        {
            Graphics graphicImage = Graphics.FromImage(image);
            graphicImage.DrawString(actualDate, new Font("Arial", 18, FontStyle.Bold),
            Brushes.Black, new Point(image.Width - 250, 10));
            graphicImage.DrawString(ImageInfoLib.GetGPS(image), new Font("Arial", 18, FontStyle.Bold),
            Brushes.Black, new Point(10, 10));
            graphicImage.Save();
        }

        private static string SetDestName(Photo photo, string dateTaken, string actualYear, string destDir)
        {
            CheckFolder(actualYear, destDir);
            return $"{actualYear}\\{photo.GetName}{dateTaken}{photo.GetExtension}";
        }

        private static void CheckFolder(string actualYear, string destDir)
        {
            string checkExistence = $@"{destDir}\{actualYear}";
            if(!Directory.Exists(checkExistence))
                Directory.CreateDirectory(checkExistence);
        }
    }
}
