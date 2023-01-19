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

