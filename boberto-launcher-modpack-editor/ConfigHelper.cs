using boberto_launcher_modpack_editor.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace boberto_launcher_modpack_editor
{
    public static class ConfigHelper
    {
        public static void InitConfigFile()
        {
            var configFile = Utils.GetConfigFile();
            if (File.Exists(configFile))
            {
                return;
            }
            var defaultConfig = new ConfigModel();
            defaultConfig.Version = "1.0.0";
            defaultConfig.ApiUrl = "http://localhost";
            defaultConfig.ModPacks = new List<ModPack>();
            var configJson = JsonSerializer.Serialize(defaultConfig);
            File.WriteAllText(configFile, configJson);
        }
        public static ConfigModel LoadConfigFile()
        {
            var configFile = Utils.GetConfigFile();
            var configJson = File.ReadAllText(configFile);
            return JsonSerializer.Deserialize<ConfigModel>(configJson);
        }
        public static void SaveConfigFile(ConfigModel configModel)
        {
            var configFile = Utils.GetConfigFile();
            var configJson = JsonSerializer.Serialize(configModel);
            File.WriteAllText(configFile, configJson);
        }
        public static bool Exists(ModPack modpack)
        {
            var config = LoadConfigFile();
            var modPackList = config.ModPacks;
            var modPackExists = modPackList.FirstOrDefault(x => x.Id.Equals(modpack.Id)) != null;
            return modPackExists;
        }
        public static void SaveModPack(ModPack modpack)
        {
            var config = LoadConfigFile();
            var modPackList = config.ModPacks;
            var modPackElement = modPackList.FirstOrDefault(x => x.Id.Equals(modpack.Id));
            if (modPackElement != null)
            {
                modPackList.Remove(modPackElement);
                modPackList.Add(modpack);
            }
            else
            {
                modPackList.Add(modpack);
            }
            config.ModPacks = modPackList;
            SaveConfigFile(config);
        }
        public class ConfigModel
        {
            public string Version { get; set; }
            public string ApiKey { get; set; }
            public string ApiUrl { get; set; }
            public List<ModPack> ModPacks { get; set; }
        }
    }
}
