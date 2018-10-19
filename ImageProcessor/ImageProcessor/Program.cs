using System;
using System.Collections.Generic;
using System.IO;



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
            List<FileInfo> filesInfos = new List<FileInfo>(dir.GetFiles("*.", SearchOption.AllDirectories));
            Directory.CreateDirectory(destDir);
            System.Drawing.Image newImage = System.Drawing.Image.FromFile(filesInfos[0].ToString());
            Console.WriteLine(newImage.Tag); 

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
