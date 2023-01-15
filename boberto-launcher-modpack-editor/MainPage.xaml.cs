using boberto_launcher_modpack_editor.Models;
using Microsoft.Maui.Storage;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;

namespace boberto_launcher_modpack_editor;

public partial class MainPage : ContentPage
{
    public ObservableCollection<ModPack> LocalModPacks { get; set; }
    public ModPack CurrentModPack { get; set; }

    public MainPage()
    {
        InitializeComponent();
        LocalModPacks = new ObservableCollection<ModPack>();
        var localApp = AppDomain.CurrentDomain.BaseDirectory;
        var modpackDir = Path.Combine(localApp, "modpacks");
        string[] modpacksDir = Directory.GetDirectories(modpackDir);
        foreach (var modpack in modpacksDir)
        {
            var modpackFiles = Directory.EnumerateFiles(modpack, "*", SearchOption.AllDirectories);
            var files = new List<MinecraftFile>();

            foreach (var modpackFile in modpackFiles)
            {
                files.Add(new MinecraftFile(modpackFile, modpack));
            }
            LocalModPacks.Add(new ModPack()
            {
                Name = new DirectoryInfo(modpack).Name,
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

