using System;
using System.Collections.Generic;
using System.IO;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace ImageProcessor
{
    class Program
    {
        static void Main(string[] args)
        {
            string sourceDir = @"D:\Home";
            string destDir = @"D:\Home\newfolder";
            CreateItems(sourceDir, destDir);
            
            Console.ReadKey();
        }

        private static void CreateItems(string sourceDir, string destDir)
        {
            DirectoryInfo dir = new DirectoryInfo(sourceDir);
            List<FileInfo> filesInfos = new List<FileInfo>(dir.GetFiles());
            Directory.CreateDirectory(destDir);
            Image newImage = Image.FromFile("SampImag.jpg");

            //foreach (FileInfo fileinfo in filesInfos)
            //{
            //    string withoutExtension = Path.GetFileNameWithoutExtension(fileinfo.Name);
            //    Console.WriteLine(Path.GetFileNameWithoutExtension(fileinfo.Name));
            //    string newName = $"{withoutExtension}[new]{fileinfo.Extension}";
            //    string combine = Path.Combine(destDir, newName);
            //    fileinfo.CopyTo(combine);
            //}
        }
    }
}
