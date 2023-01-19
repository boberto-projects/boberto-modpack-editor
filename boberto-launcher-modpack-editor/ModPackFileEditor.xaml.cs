using boberto_launcher_modpack_editor.Models;
using System.Collections.ObjectModel;

namespace boberto_launcher_modpack_editor;

public partial class ModPackFileEditor : ContentPage
{
    public ModPack CurrentModPack { get; set; }
    public ObservableCollection<MinecraftFile> Files { get; set; }
    public ObservableCollection<MinecraftFile> ClientFiles { get; set; }
    public ObservableCollection<MinecraftFile> ServerFiles { get; set; }

    public ModPackFileEditor(ModPack modpack)
    {
        InitializeComponent();
        CurrentModPack = modpack;
        LoadModPackFiles();
    }
    public void LoadModPackFiles()
    {
        Files = new ObservableCollection<MinecraftFile>();
        ClientFiles = new ObservableCollection<MinecraftFile>();
        ServerFiles = new ObservableCollection<MinecraftFile>();
        var modpackDir = Path.Combine(Utils.GetModPacksDir(), CurrentModPack.Directory);
        var modpackFiles = Directory.EnumerateFiles(modpackDir, "*", SearchOption.AllDirectories);
        foreach (var modpack in modpackFiles)
        {
            Files.Add(new MinecraftFile(modpack, modpackDir));
        }
        this.BindingContext = this;
    }
}