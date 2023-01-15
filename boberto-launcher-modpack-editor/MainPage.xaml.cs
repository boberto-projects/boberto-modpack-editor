using System.Collections.ObjectModel;
using System.Runtime.Serialization;

namespace boberto_launcher_modpack_editor;

public partial class MainPage : ContentPage
{
    public ObservableCollection<ModPack> LocalModPacks { get; set; }
    public class MinecraftFile
    {
        public string Name { get; set; }
        public string Version { get; set; }
        public TypeEnviroment Enviroment { get; set; }
        public Type Type { get; set; }
        public string RelativePath { get; set; }

        public MinecraftFile(string path)
        {
            this.Name = Path.GetFileName(path);
            this.Type = GetFileType(path);
            this.Enviroment = GetEnviromentType(path);
        }
        private Type GetFileType(string path)
        {
            if (path.Contains("libraries"))
            {
                return Type.LIBRARY;
            }
            else if (path.Contains("mods"))
            {
                return Type.MOD;
            }
            else if (path.Contains("versions"))
            {
                return Type.VERSIONCUSTOM;
            }
            return Type.FILE;
        }

        private TypeEnviroment GetEnviromentType(string path)
        {
            if (path.EndsWith("client"))
            {
                return TypeEnviroment.Client;
            }
            return TypeEnviroment.Server;
        }
    }
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
    }
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
        Client,
        Server
    }
    public MainPage()
    {
        InitializeComponent();
        LocalModPacks = new ObservableCollection<ModPack>();
        var localApp = AppDomain.CurrentDomain.BaseDirectory;
        var modpackDir = Path.Combine(localApp, "modpacks");
        DirectoryInfo modPackDirInfo = new DirectoryInfo(modpackDir);
        string[] modpacksDir = Directory.GetDirectories(modpackDir);

        foreach (var modpack in modpacksDir)
        {
            var files = new List<MinecraftFile>();

            foreach (var modpackFile in Directory.GetFiles(modpack))
            {
                files.Add(new MinecraftFile(modpackFile));
            }
            LocalModPacks.Add(new ModPack()
            {
                Name = Path.GetDirectoryName(modpack),
                Files = files
            });
        }





        this.BindingContext = this;


    }
    private void DragGestureRecognizer_DragStarting_1(object sender, DragStartingEventArgs e)
    {
        var label = (sender as Element)?.Parent as Label;
        e.Data.Properties.Add("Text", label.Text);
    }

    private void DropGestureRecognizer_Drop_1(object sender, DropEventArgs e)
    {
        var data = e.Data.Properties["Text"].ToString();
        var frame = (sender as Element)?.Parent as Frame;
        frame.Content = new Label
        {
            Text = data
        };
    }
}

