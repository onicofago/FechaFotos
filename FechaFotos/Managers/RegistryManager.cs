using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;

namespace FechaFotos.Managers
{
    public class RegistryManager
    {
        public string Key { get; set; }
        private RegistryKey RootLocalMachine { get; set; }
        private RegistryKey RegistryKeySoftware { get; set; }
        
        public RegistryManager()
        {
            RootLocalMachine = Registry.CurrentUser;
            RegistryKeySoftware = RootLocalMachine.OpenSubKey("Software", true);
        }

        public string GetKey(string keyName)
        {
            return RegistryKeySoftware?.GetValue(keyName) != null
                ? RegistryKeySoftware.GetValue(keyName).ToString().Trim()
                : Environment.CurrentDirectory;
        }

        public void WriteKey(string registryKey, string value)
        {
            if (RegistryKeySoftware == null) return;
            RegistryKeySoftware.SetValue(registryKey, value);
            RegistryKeySoftware.Flush();
            RegistryKeySoftware.Close();
        }
    }
}
