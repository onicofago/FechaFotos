using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Microsoft.Win32;
using FechaFotos.Managers;

namespace FechaFotos
{
    public partial class FrmFechaFotos : Form
    {
        private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private readonly RegistryManager _regMgr = new RegistryManager();
        private const string RegistryKey = "FechaFotos";

        public FrmFechaFotos()
        {
            InitializeComponent();
            txtCarpeta.Text = _regMgr.GetKey(RegistryKey);
        }

         private void btnCancelar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnCarpeta_Click(object sender, EventArgs e)
        {
            var oForm = new FolderBrowserDialog();

            oForm.SelectedPath = txtCarpeta.Text;
            oForm.ShowDialog();
            txtCarpeta.Text = oForm.SelectedPath;
            _regMgr.WriteKey(RegistryKey, oForm.SelectedPath.Trim());
        }

        private void btnProcesar_Click(object sender, EventArgs e)
        {
            Procesar(txtCarpeta.Text);
        }

        private void Procesar(string folderName)
        {
            var folder = new DirectoryInfo(folderName);
            FileInfo[] fileArray;
            var processed = 0;
            var read = 0;

            if (folderName.Length == 0) return;

            try
            {
                fileArray = folder.GetFiles();
            }
            catch
            {
                MessageBox.Show($"The specified folder cannot be opened: {folderName}", "Error", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                Log.Error($"The specified folder cannot be opened: {folderName}");
                fileArray = null;
            }

            if (fileArray == null || !fileArray.Any()) return;
            foreach (var file in fileArray)
            {
                if (FileManager.IsAPictureFile(file))
                {
                    var newName = "";

                    // Pictures (generic images)
                    if (FileManager.IsValidCameraFile(file))
                    {
                        try
                        {
                            newName = FileManager.BuildNewFileNameFromExifData(file);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"Error: Exception while reading EXIF metadata: {ex.Message} {file.FullName}", "Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                            Log.Warn($"Error: Exception while reading EXIF metadata: {ex.Message} {file.FullName}");
                            return;
                        }
                    }

                    if (FileManager.RenameFile(file, newName))
                    {
                        processed += 1;
                    }
                }

                read += 1;
                ProgressBar1.Value = Convert.ToInt32(read / (double) fileArray.Length * 100);
            }

            ProgressBar1.Value = 100;
            if (processed > 0)
            {
                MessageBox.Show($"Proceso finalizado, se procesaron {processed} archivos.", "Info", MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                Log.Info($"Proceso finalizado, Carpeta: {folderName}, {processed} archivos.");
            }
            else
            {
                MessageBox.Show("No se procesó ningún archivo en la carpeta especificada.", "Aviso", MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation);
                Log.Warn($"No se procesó ningún archivo en la carpeta: {folderName}");
            }
            Close();
        }
    }
}
