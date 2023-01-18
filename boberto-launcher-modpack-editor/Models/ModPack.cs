using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace boberto_launcher_modpack_editor.Models
{
    public class ModPack
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public bool IsModded { get; set; }
        public bool IsVerifyMods { get; set; }
        public bool IsDefault { get; set; }
        public bool IsPremiumServer { get; set; }
        public bool IsJava { get; set; }
        public string ServerIp { get; set; }
        public int ServerPort { get; set; }

        public string Directory { get; set; }
        public string MinecraftVersion { get; set; }
        public string Author { get; set; }
        public string Description { get; set; }
        public string Img { get; set; }
        public IEnumerable<string> Ignored { get; set; }
        /// <summary>
        /// local launcher config
        /// </summary>
        public List<MinecraftFile> Files { get; set; }
        public ModPack()
        {
            if (Id is null)
                Id = Guid.NewGuid().ToString();
        }
        public void RemoveModPackFiles(string directory)
        {
            var modpackDir = Path.Combine(Utils.GetModPacksDir(), directory);
            var clientFolder = Path.Combine(modpackDir, Utils.ClientFolder);
            var serverFolder = Path.Combine(modpackDir, Utils.ServerFolder);
            Utils.DeleteAllFilesDir(clientFolder);
            Utils.DeleteAllFilesDir(serverFolder);
        }
        public void Create()
        {
            var modpackDir = Path.Combine(Utils.GetModPacksDir(), Directory);
            var clientFolder = Path.Combine(modpackDir, Utils.ClientFolder);
            var serverFolder = Path.Combine(modpackDir, Utils.ServerFolder);
            Utils.CreateClientDefaultFiles(clientFolder);
            Utils.CreateServerDefaultFiles(serverFolder);
        }

        public void CreateModded()
        {
            var modpackDir = Path.Combine(Utils.GetModPacksDir(), Directory);
            var clientFolder = Path.Combine(modpackDir, Utils.ClientFolder);
            var serverFolder = Path.Combine(modpackDir, Utils.ServerFolder);

            Utils.CreateClientModdedFiles(clientFolder);
            Utils.CreateServerDefaultFiles(serverFolder);
        }
        public override string ToString()
        {
            return Name;
        }
    }
}
