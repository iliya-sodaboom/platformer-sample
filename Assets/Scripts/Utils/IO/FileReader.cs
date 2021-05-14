using UnityEngine;

namespace TinyPlay {
    using System.IO;

    /// <summary>
    /// Base File Reader Module (Binary)
    /// Developed by Ilya Rastorguev
    /// </summary>
    public static class FileReader {
        /// <summary>
        /// Read Text File from Path
        ///
        /// Please, DO NOT USE THIS FOR LARGE FILES.
        /// MAKE IT ASYNC!
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static string ReadTextFile(string path) {
            // Check File Exists
            if (!File.Exists(path)) return null;
            return File.ReadAllText(path);
        }

        /// <summary>
        /// Save Text file to Path
        ///
        /// Please, DO NOT USE THIS FOR LARGE FILES.
        /// MAKE IT ASYNC!
        /// </summary>
        /// <param name="path"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public static bool SaveTextFile(string path, string data) {
            if (!File.Exists(path)) return false;
            File.WriteAllText(path, data);
            return true;
        }
    }
}