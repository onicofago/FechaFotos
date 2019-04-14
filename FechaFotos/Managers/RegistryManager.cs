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
        private RegistryKey RootLocalMachine { get; set; }
        private RegistryKey RegistryKeySoftware { get; set; }
        
        public RegistryManager()
        {
            RootLocalMachine = Registry.CurrentUser;
            RegistryKeySoftware = RootLocalMachine.OpenSubKey("Software", true);
        }

        /// <summary>
        /// Get a key value from the registry
        /// </summary>
        /// <param name="keyName"></param>
        /// <returns></returns>
        public string ReadKey(string keyName)
        {
            return RegistryKeySoftware?.GetValue(keyName) != null
                ? RegistryKeySoftware.GetValue(keyName).ToString().Trim()
                : Environment.CurrentDirectory;
        }

        /// <summary>
        /// Set a key value in the registry
        /// </summary>
        /// <param name="registryKey"></param>
        /// <param name="value"></param>
        public void WriteKey(string registryKey, string value)
        {
            if (RegistryKeySoftware == null) return;
            RegistryKeySoftware.SetValue(registryKey, value);
            RegistryKeySoftware.Flush();
            RegistryKeySoftware.Close();
        }
    }
}
