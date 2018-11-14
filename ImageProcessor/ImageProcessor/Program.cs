using System;
using System.Collections.Generic;
using System.IO;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing.Imaging;
using ImageInfoLibrary;


namespace ImageProcessor
{
    class Program
    {
        static void Main(string[] args)
        {
            string sourceDir = @"F:\mes fichiers\DCIM\Camera";
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
                photos.Add(new Photo(file));
            foreach (Photo photo in photos)
            {
                Image image = Image.FromFile(photo.GetInfo.FullName);
                Console.WriteLine($"{count}/{photos.Count} successful");
                string actualDate = ImageInfoLib.GetDate.AsString(photo.GetInfo, image);
                FileStream stream = new FileStream(photo.GetInfo.FullName, FileMode.Open, FileAccess.Read);
                Graphics graphicImage = Graphics.FromImage(image);
                graphicImage.DrawString(actualDate, new Font("Arial", 18, FontStyle.Bold),
                Brushes.Black, new Point(image.Width-250, 10));
                graphicImage.DrawString(ImageInfoLib.GetGPS(image), new Font("Arial", 18, FontStyle.Bold),
                Brushes.Black, new Point(10, 10));
                graphicImage.Save();
                string destWay = $@"{destDir}\{SetDestName(photo, actualDate)}";
                image.Save(destWay, ImageFormat.Jpeg);
                image.Dispose();
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
                Image image = Image.FromFile(photo.GetInfo.FullName);
                string actualDate = ImageInfoLib.GetDate.AsString(photo.GetInfo, image);
                Console.WriteLine(actualDate);
                image.Dispose();
                string destWay = $@"{destDir}\{SetDestName(photo, actualDate)}";
                File.Copy(photo.GetInfo.FullName, destWay);
                Console.WriteLine($"{count}/{photos.Count} successful");
                Console.Clear();
                count++;
            }
            Console.WriteLine($"{count-1}/{photos.Count} successful");
        }
        private static string SetDestName(Photo photo, string dateTaken)
        {
            return $"{photo.GetName}{dateTaken}{photo.GetExtension}";
        }
    }
}
