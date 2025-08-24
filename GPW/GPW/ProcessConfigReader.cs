using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GPW
{
    /// <summary>
    /// Utility per la configurazione (solo file txt)
    /// </summary>
    public static class ProcessConfigReader
    {
        /// <summary>
        /// Se il file non esiste, lo crea. 
        /// Restituisce i processi letti come lista.
        /// </summary>
        public static List<string> ReadOrCreate(string filePath)
        {
            if (!File.Exists(filePath))
            {
                // crea file vuoto
                File.WriteAllText(filePath, "");
            }

            return File.ReadAllLines(filePath)
                       .Select(l => l.Trim())
                       .Where(l => !string.IsNullOrWhiteSpace(l))
                       .ToList();
        }
    }
}
