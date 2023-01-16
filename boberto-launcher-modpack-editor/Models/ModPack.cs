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
        public List<MinecraftFile> Files { get; set; }
        public ModPack()
        {
            if (Id is null)
                Id = Guid.NewGuid().ToString();
        }
        public void Create()
        {
            Name = "tmp_" + Id;
            var modpackDir = Path.Combine(Utils.GetModPacksDir(), Name);
            var clientFolder = Path.Combine(modpackDir, Utils.ClientFolder);
            var serverFolder = Path.Combine(modpackDir, Utils.ServerFolder);

            Utils.CreateClientDefaultFiles(clientFolder);
            Utils.CreateServerDefaultFiles(serverFolder);

        }
        public override string ToString()
        {
            return Name;
        }
    }
}
