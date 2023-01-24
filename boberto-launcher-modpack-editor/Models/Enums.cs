using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace boberto_launcher_modpack_editor.Models
{
    public enum Type
    {
        [EnumMember(Value = "MOD")]
        MOD,
        [EnumMember(Value = "VERIONSCUSTOM")]
        VERSIONCUSTOM,
        [EnumMember(Value = "FILE")]
        FILE,
        [EnumMember(Value = "LIBRARY")]
        LIBRARY
    }
    public enum TypeEnviroment
    {
        [Description("client")]
        Client,
        [Description("server")]
        Server
    }
}
