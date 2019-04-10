using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Microsoft.Win32;

namespace FechaFotos
{
    public partial class FrmFechaFotos : Form
    {
        private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private const string RegistryKey = "FechaFotos";

        public FrmFechaFotos()
        {
            InitializeComponent();
            var rootLocalMachine = Registry.CurrentUser;
            var registryKeySoftware = rootLocalMachine.OpenSubKey("Software", true);
            txtCarpeta.Text = registryKeySoftware?.GetValue(RegistryKey) != null 
                ? registryKeySoftware.GetValue(RegistryKey).ToString().Trim() 
                : Environment.CurrentDirectory;
        }

         private void btnCancelar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnCarpeta_Click(object sender, EventArgs e)
        {
            var oForm = new FolderBrowserDialog();
            var rootLocalMachine = Registry.CurrentUser;
            var registryKeySoftware = rootLocalMachine.OpenSubKey("Software", true);

            oForm.SelectedPath = this.txtCarpeta.Text;
            oForm.ShowDialog();
            this.txtCarpeta.Text = oForm.SelectedPath;
            if (registryKeySoftware == null) return;
            registryKeySoftware.SetValue(RegistryKey, oForm.SelectedPath.Trim());
            registryKeySoftware.Flush();
            registryKeySoftware.Close();
        }

        private void btnProcesar_Click(object sender, EventArgs e)
        {
            var folder = new DirectoryInfo(txtCarpeta.Text);
            FileInfo[] fileArray;
            var processed = 0;
            var read = 0;

            if (txtCarpeta.Text.Length == 0) return;

            try
            {
                fileArray = folder.GetFiles();
            }
            catch
            {
                MessageBox.Show($"The specified folder cannot be opened: {txtCarpeta.Text}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Log.Error($"The specified folder cannot be opened: {txtCarpeta.Text}");
                fileArray = null;
            }

            // 20070608 * Changes filename format to handle NIKON (DSCN).
            // 20080528 * Changes filename format to handle PANASONIC (DMC-LZ8).
            // 20091014 * Changes filename format to handle either NIKON or PANASONIC.
            // 20110207 * Changes renaming to accept Apple iPhone format.
            // 20111228 * Changes renaming to accept GalaxyS format.
            if (fileArray == null || !fileArray.Any()) return;
            foreach (var file in fileArray)
            {
                if (file.Name.ToUpper().EndsWith("JPG") | file.Name.ToUpper().EndsWith("AVI"))
                {
                    var oldName = "";
                    var newName = "";

                    // Pictures (generic images)
                    if ((file.Name.ToUpper().StartsWith("P") | file.Name.ToUpper().StartsWith("DSC") | file.Name.ToUpper().StartsWith("IMG_") | file.Name.ToUpper().StartsWith("20")) & file.Name.ToUpper().EndsWith("JPG"))
                    {
                        oldName = file.FullName;
                        try
                        {
                            var oEw = new ExifWorks(oldName);
                            newName = file.DirectoryName + @"\img" + oEw.DateTimeOriginal.ToString("yyyyMMdd_HHmmss") + ".jpg";
                            oEw.Dispose();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"Error: Exception while reading EXIF metadata: {ex.Message} {oldName}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            Log.Warn($"Error: Exception while reading EXIF metadata: {ex.Message} {oldName}");
                            return;
                        }
                    }

                    // Videos (Disabled)
                    // if (file.Name.StartsWith("DSCN") && fFile.Name.EndsWith("AVI"))
                    // {
                    //  oldName = file.FullName
                    //  newName = file.DirectoryName & "\vid" & _
                    //  file.CreationTime.ToString("yyyyMMdd_HHmmss") + ".avi"
                    // }

                    // Actually rename files
                    if (oldName.Length > 0)
                    {
                        try
                        {
                            Directory.Move(oldName, newName);
                            processed += 1;
                        }
                        catch (Exception ex)
                        {
                            Log.Error(ex.Message);
                        }
                    }
                }

                read += 1;
                ProgressBar1.Value = Convert.ToInt32(read / (double)fileArray.Length * 100);
            }

            this.ProgressBar1.Value = 100;
            if (processed > 0)
            {
                MessageBox.Show($"Proceso finalizado, se procesaron {processed} archivos.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Log.Info($"Proceso finalizado, Carpeta: {txtCarpeta.Text}, {processed} archivos.");
            }
            else
            {
                MessageBox.Show("No se procesó ningún archivo en la carpeta especificada.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                Log.Warn($"No se procesó ningún archivo en la carpeta: {txtCarpeta.Text}");
            }
            Close();
        }
    }
}
