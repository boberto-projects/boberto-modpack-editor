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
    }
}
