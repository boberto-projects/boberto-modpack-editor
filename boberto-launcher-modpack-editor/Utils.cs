using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace boberto_launcher_modpack_editor
{
    public static class Utils
    {
        public const string ConfigFile = "config.json";
        public const string ClientFolder = "client";
        public const string ServerFolder = "server";
        public const string ModPackDir = "modpacks";
        public static string GetDefaultAppDomain()
        {
            var localApp = AppDomain.CurrentDomain.BaseDirectory;
            return localApp;
        }
        public static string GetConfigFile() { return Path.Combine(GetDefaultAppDomain(), ConfigFile); }
        public static string GetModPacksDir()
        {
            var localApp = GetDefaultAppDomain();
            var modpackDir = Path.Combine(localApp, ModPackDir);
            return modpackDir;
        }
        public static string GetModPacksDir(string directory)
        {
            var localApp = GetDefaultAppDomain();
            var modpackDir = Path.Combine(localApp, ModPackDir, directory);
            return modpackDir;
        }
        public static void CreateClientDefaultFiles(string outPath)
        {
            if (Directory.Exists(outPath) == false)
            {
                Directory.CreateDirectory(outPath);
            }
            var path = Path.Combine(outPath, "README.MD");
            File.WriteAllText(path, "# Default client files");
        }

        public static void CreateClientModdedFiles(string outPath)
        {
            if (Directory.Exists(outPath) == false)
            {
                Directory.CreateDirectory(outPath);
            }
            var path = Path.Combine(outPath, "launcher_profiles.json");
            File.WriteAllText(path, "{}");
        }

        public static void CreateServerDefaultFiles(string outPath)
        {
            if (Directory.Exists(outPath) == false)
            {
                Directory.CreateDirectory(outPath);
            }
            var path = Path.Combine(outPath, "README.md");
            File.WriteAllText(path, "# Server files");
        }
        public static void DeleteAllFilesDir(string path)
        {
            if (Directory.Exists(path) == false)
            {
                return;
            }
            Directory.Delete(path, true);
        }
        public static bool MoveFileToClient(string path, string modPackDir)
        {
            if (File.Exists(path) == false)
            {
                return false;
            }
            var outputPath = Path.Combine(GetModPacksDir(), modPackDir, ClientFolder);
            var file = new FileInfo(path);
            var targetFilePath = Path.Combine(outputPath, file.Name);
            file.CopyTo(targetFilePath, true);
            return true;

        }
        public static bool MoveFileToServer(string path, string modPackDir)
        {
            if (File.Exists(path) == false)
            {
                return false;
            }
            var outputPath = Path.Combine(GetModPacksDir(), modPackDir, ServerFolder);
            var file = new FileInfo(path);
            var targetFilePath = Path.Combine(outputPath, file.Name);
            file.MoveTo(targetFilePath, true);
            return true;
        }

        /// <summary>
        /// https://learn.microsoft.com/en-us/dotnet/standard/io/how-to-copy-directories
        /// </summary>
        /// <param name="sourceDir"></param>
        /// <param name="destinationDir"></param>
        /// <param name="recursive"></param>
        /// <exception cref="DirectoryNotFoundException"></exception>
        public static void CopyDirectory(string sourceDir, string destinationDir, bool recursive)
        {
            // Get information about the source directory
            var dir = new DirectoryInfo(sourceDir);

            // Check if the source directory exists
            if (!dir.Exists)
                throw new Exception($"Diretório não encontrado: {dir.FullName}");

            // Cache directories before we start copying
            DirectoryInfo[] dirs = dir.GetDirectories();

            // Create the destination directory
            Directory.CreateDirectory(destinationDir);

            // Get the files in the source directory and copy to the destination directory
            foreach (FileInfo file in dir.GetFiles())
            {
                string targetFilePath = Path.Combine(destinationDir, file.Name);
                file.CopyTo(targetFilePath, true);
            }

            // If recursive and copying subdirectories, recursively call this method
            if (recursive)
            {
                foreach (DirectoryInfo subDir in dirs)
                {
                    string newDestinationDir = Path.Combine(destinationDir, subDir.Name);
                    CopyDirectory(subDir.FullName, newDestinationDir, true);
                }
            }
        }

    }
}
