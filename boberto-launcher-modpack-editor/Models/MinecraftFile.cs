using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace boberto_launcher_modpack_editor.Models
{
    public class MinecraftFile
    {
        public string Name { get; set; }
        public string Version { get; set; }
        public TypeEnviroment Enviroment { get; set; }
        public Type Type { get; set; }
        public string RelativePath { get; set; }
        public string FullPath { get; set; }

        public MinecraftFile(string path, string relativePath, string? version = "1.0.0")
        {
            this.Name = Path.GetFileName(path);
            this.FullPath = path;
            this.RelativePath = Path.GetRelativePath(relativePath, path);
            this.Version = version;
            this.Type = GetFileType();
            this.Enviroment = GetEnviromentType();
        }
        public MinecraftFile() { }
        private Type GetFileType()
        {
            if (this.FullPath.Contains("libraries"))
            {
                return Type.LIBRARY;
            }
            else if (this.FullPath.Contains("mods"))
            {
                return Type.MOD;
            }
            else if (this.FullPath.Contains("versions"))
            {
                return Type.VERSIONCUSTOM;
            }
            return Type.FILE;
        }

        private TypeEnviroment GetEnviromentType()
        {
            if (this.RelativePath.StartsWith("client"))
            {
                return TypeEnviroment.Client;
            }
            return TypeEnviroment.Server;
        }
        public override string ToString()
        {
            return this.Name;
        }
    }
}
