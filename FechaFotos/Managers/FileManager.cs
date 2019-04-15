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
        /// <summary>
        /// Validate a picture file
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public static bool IsAPictureFile(FileInfo file)
        {
            return (file.Name.ToUpper().EndsWith("JPG") | file.Name.ToUpper().EndsWith("AVI"));
        }

        /// <summary>
        /// Validate a proper camera file
        /// 20070608 * Changes filename format to handle NIKON (DSCN).
        /// 20080528 * Changes filename format to handle PANASONIC (DMC-LZ8).
        /// 20091014 * Changes filename format to handle either NIKON or PANASONIC.
        /// 20110207 * Changes renaming to accept Apple iPhone format.
        /// 20111228 * Changes renaming to accept GalaxyS format.
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public static bool IsValidCameraFile(FileInfo file)
        {
            return ((file.Name.ToUpper().StartsWith("P") | file.Name.ToUpper().StartsWith("DSC") |
                     file.Name.ToUpper().StartsWith("IMG_") | file.Name.ToUpper().StartsWith("20")) &
                    file.Name.ToUpper().EndsWith("JPG"));
        }

        /// <summary>
        /// Return a valid filename from EXIF data
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public static string BuildNewFileNameFromExifData(FileInfo file)
        {
            using (var oEw = new ExifWorks(file.FullName))
            {
                return file.DirectoryName + @"\img" + oEw.DateTimeOriginal.ToString("yyyyMMdd_HHmmss") + ".jpg";
            }
        }

        /// <summary>
        /// Actually rename the file
        /// </summary>
        /// <param name="file"></param>
        /// <param name="newName"></param>
        /// <returns></returns>
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
