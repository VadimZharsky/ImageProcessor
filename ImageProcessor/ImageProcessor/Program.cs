using System;
using System.Collections.Generic;
using System.IO;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing.Imaging;

namespace ImageProcessor
{
    class Program
    {
        static void Main(string[] args)
        {
            string sourceDir = @"D:\c#works\it academy";
            CreateItems(sourceDir);

            Console.ReadKey();
        }

        private static void CreateItems(string sourceDir)
        {
            var dir = new DirectoryInfo(sourceDir);
            List<FileInfo> filesInfos = new List<FileInfo>(dir.GetFiles("*.jpg"));
            List<Photo> photos = new List<Photo>();
            DirectoryInfo parent = dir.Parent;
            //parent.CreateSubdirectory("newFolder");
            string destDir = $@"{parent.FullName}\newDossier";
            Directory.CreateDirectory(destDir);
            foreach (FileInfo file in filesInfos)
            {
                var getImage = Image.FromFile(file.FullName);
             
                
                photos.Add(new Photo() { fileInfo = file, name = file.Name, image = getImage });
            }
            foreach (Photo photo in photos)
            {

                try
                {
                    PropertyItem propItem = photo.image.GetPropertyItem(36867);
                    string dateTaken = Encoding.UTF8.GetString(photo.propertyItem.Value);
                    string newName = $"{Path.GetFileNameWithoutExtension(photo.fileInfo.Name)}_{dateTaken}{photo.fileInfo.Extension}";
                    File.Copy($@"{dir.FullName}\{photo.fileInfo.Name}", $@"{destDir}\{newName}");
                }
                catch
                {
                    string dateTaken = photo.fileInfo.CreationTime.ToString();
                    string newName = $"{Path.GetFileNameWithoutExtension(photo.fileInfo.Name)}{photo.fileInfo.Extension}";
                    File.Copy($@"{dir.FullName}\{photo.fileInfo.Name}", $@"{destDir}\{newName}");
                }
                
            }

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
