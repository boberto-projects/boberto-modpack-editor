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
        LoadModPacksList();

        //string[] modpacksDir = Directory.GetDirectories(modpackDir);
        //foreach (var modpack in modpacksDir)
        //{
        //    var modpackFiles = Directory.EnumerateFiles(modpack, "*", SearchOption.AllDirectories);
        //    var files = new List<MinecraftFile>();

        //    foreach (var modpackFile in modpackFiles)
        //    {
        //        files.Add(new MinecraftFile(modpackFile, modpack));
        //    }
        //    LocalModPacks.Add(new ModPack()
        //    {
        //        Name = new DirectoryInfo(modpack).Name,
        //        Files = files
        //    });
        //}

        this.BindingContext = this;
    }
    public void LoadModPacksList()
    {
        LocalModPacks = new ObservableCollection<ModPack>();
        var modpackDir = Utils.GetModPacksDir();
        ConfigHelper.InitConfigFile();
        var modpacks = ConfigHelper.LoadConfigFile().ModPacks;
        if (Directory.Exists(modpackDir) == false)
        {
            Directory.CreateDirectory(modpackDir);
        }
        foreach (var item in modpacks)
        {
            LocalModPacks.Add(item);
        }
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

    private async void AddModPack(object sender, EventArgs e)
    {
        var modPack = new ModPack();
        modPack.Ignored = new List<string>();
        //modPack.Create();
        await Navigation.PushAsync(new ModPackPage(modPack));

    }
    private async void EditModPack(object sender, EventArgs e)
    {
        //modPack.Create();
        if (CurrentModPack == null)
        {
            DisplayAlert("Warning", "Modpack cant be found for this selectable value", "OK");
            return;
        }
        await Navigation.PushAsync(new ModPackPage(CurrentModPack));
    }
    private void RemoveModPack(object sender, EventArgs e)
    {

    }
}

