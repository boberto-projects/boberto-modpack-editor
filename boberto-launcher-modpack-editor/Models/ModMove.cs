using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace boberto_launcher_modpack_editor.Models
{
    public class ModMove
    {
        public string Origin { get; set; }

        public string Destination { get; set; }

        public ModMove(string modPackDir, MinecraftFile minecraftFile, TypeEnviroment typeEnviroment)
        {
            var destinationPath = minecraftFile.RelativePath.Replace(minecraftFile.Enviroment.GetEnumDescription() + Path.DirectorySeparatorChar, "");
            Origin = minecraftFile.FullPath;
            Destination = Path.Combine(Utils.GetModPacksDir(modPackDir), typeEnviroment.GetEnumDescription(), destinationPath);
        }
    }
}
