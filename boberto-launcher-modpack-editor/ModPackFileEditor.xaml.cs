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
        Files = new ObservableCollection<MinecraftFile>();
        ClientFiles = new ObservableCollection<MinecraftFile>();
        ServerFiles = new ObservableCollection<MinecraftFile>();
        InitializeComponent();

        CurrentModPack = modpack;
        this.BindingContext = this;

        LoadModPackFiles();
    }
    public void LoadModPackFiles()
    {
        var modpackDir = Path.Combine(Utils.GetModPacksDir(), CurrentModPack.Directory);
        var modpackFiles = Directory.EnumerateFiles(modpackDir, "*", SearchOption.AllDirectories);
        foreach (var modpack in modpackFiles)
        {
            Files.Add(new MinecraftFile(modpack, modpackDir));
        }
        foreach (var item in Files)
        {
            if (item.Enviroment.Equals(TypeEnviroment.Client))
            {
                ClientFiles.Add(item);
                continue;
            }
            ServerFiles.Add(item);
        }
    }
    private void DragGestureRecognizer_DragStarting(object sender, DragStartingEventArgs e)
    {
        var label = (sender as Element)?.Parent as Label;
        e.Data.Properties.Add("Text", label.Text);
    }

    private void DropGestureRecognizer_Drop(object sender, DropEventArgs e)
    {
        var data = e.Data.Properties["Text"].ToString();
        var frame = (sender as Element)?.Parent as Frame;
        frame.Content = new Label
        {
            Text = data
        };
    }
}