using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FechaFotos.Managers
{
    public static class FileManager
    {
        public static bool IsAPictureFile(FileInfo file)
        {
            return (file.Name.ToUpper().EndsWith("JPG") | file.Name.ToUpper().EndsWith("AVI"));
        }

        public static bool IsValidCameraFile(FileInfo file)
        {
            return ((file.Name.ToUpper().StartsWith("P") | file.Name.ToUpper().StartsWith("DSC") |
                     file.Name.ToUpper().StartsWith("IMG_") | file.Name.ToUpper().StartsWith("20")) &
                    file.Name.ToUpper().EndsWith("JPG"));
        }

        public static string BuildNewFileNameFromExifData(FileInfo file)
        {
            using (var oEw = new ExifWorks(file.FullName))
            {
                return file.DirectoryName + @"\img" + oEw.DateTimeOriginal.ToString("yyyyMMdd_HHmmss") + ".jpg";
            }
        }

        public static bool RenameFile(FileInfo file, string newName)
        {
            if (file.FullName.Length <= 0) return false;
            try
            {
                Directory.Move(file.FullName, newName);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
