using System;
using System.Collections.Generic;
using System.IO;

using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing.Imaging;
using System.Text.RegularExpressions;


namespace ImageProcessor
{
    class Program
    {
        static void Main(string[] args)
        {
            string sourceDir = @"D:\Media\Media\photo\конец 2017-2018 год";
            //CreateItems(sourceDir);
            DateOntoImage(sourceDir);
            Console.ReadKey();
        }



        private static void DateOntoImage(string sourceDir)
        {

            var dir = new DirectoryInfo(sourceDir);
            List<FileInfo> filesInfos = new List<FileInfo>(dir.GetFiles("*.jpg"));
            List<Photo> photos = new List<Photo>();
            DirectoryInfo parent = dir.Parent;
            string destDir = $@"{parent.FullName}\newDossier";
            Directory.CreateDirectory(destDir);
            int count = 1;
            foreach (FileInfo file in filesInfos)
            {
                photos.Add(new Photo(file));
            }
            foreach (Photo photo in photos)
            {
                Console.WriteLine($"{count}/{photos.Count} successful");
                string destWay = $@"{destDir}\{photo.GetDestName}";
                FileStream stream = new FileStream(photo.GetInfo.FullName, FileMode.Open, FileAccess.Read);
                Graphics graphicImage = Graphics.FromImage(photo.GetImageInfo);
                graphicImage.DrawString(photo.GetDate, new Font("Arial", 18, FontStyle.Bold),
                Brushes.Black, new Point(photo.GetImageInfo.Width-250, 10));
                graphicImage.DrawString(photo.GetGPS, new Font("Arial", 18, FontStyle.Bold),
                Brushes.Black, new Point(10, 10));
                graphicImage.Save();
                photo.GetImageInfo.Save(destWay, ImageFormat.Jpeg); 
                Console.Clear();
                count++;
            }
            Console.WriteLine($"{count-1}/{photos.Count} successful");
        }

        private static void CreateItems(string sourceDir)
        {
            var dir = new DirectoryInfo(sourceDir);
            List<FileInfo> filesInfos = new List<FileInfo>(dir.GetFiles("*.jpg"));
            List<Photo> photos = new List<Photo>();
            DirectoryInfo parent = dir.Parent;
            string destDir = $@"{parent.FullName}\newDossier";
            Directory.CreateDirectory(destDir);
            int count = 1;
            foreach (FileInfo file in filesInfos)
            {
                photos.Add(new Photo(file));
            }
            foreach (Photo photo in photos)
            {
                string destWay = $@"{destDir}\{photo.GetDestName}";
                Console.WriteLine(photo.GetDate);
                File.Copy(photo.GetInfo.FullName, destWay);
                Console.WriteLine($"{count}/{photos.Count} successful");
                Console.Clear();
                count++;
            }
            Console.WriteLine($"{count-1}/{photos.Count} successful");



        }
    }
}
