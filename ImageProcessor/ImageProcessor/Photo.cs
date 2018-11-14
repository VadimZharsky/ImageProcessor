using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;


namespace ImageProcessor
{
    class Photo
    {

        private FileInfo fileInfo;   
        private string name;
        private string extension;

        public Photo(FileInfo fileInfo)
        {
            this.fileInfo = fileInfo;
            name = Path.GetFileNameWithoutExtension(fileInfo.Name);
            extension = fileInfo.Extension;
        }    
        public FileInfo GetInfo
        {
            get { return fileInfo; }
        }
        public string GetExtension
        {
            get { return extension; }
        }
        public string GetName
        {
            get { return name; }
        }
    }
}
