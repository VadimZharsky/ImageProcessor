using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageProcessor
{
    class Photo
    {
        public string name { get; set; }
        public Image image { get; set; }
        public FileInfo fileInfo { get; set;}
        public PropertyItem propertyItem { get; set; }
    }
}
