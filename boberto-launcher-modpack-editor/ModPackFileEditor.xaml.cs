using boberto_launcher_modpack_editor.Models;
using System.Collections.ObjectModel;

namespace boberto_launcher_modpack_editor;

public partial class ModPackFileEditor : ContentPage
{
    public ModPack CurrentModPack { get; set; }
    public ObservableCollection<MinecraftFile> Files { get; set; }
    public ObservableCollection<MinecraftFile> ClientFiles { get; set; }
    public ObservableCollection<MinecraftFile> ServerFiles { get; set; }
    public List<MinecraftFile> FilesToRemove { get; set; }
    public ModPackFileEditor(ModPack modpack)
    {
        Files = new ObservableCollection<MinecraftFile>();
        ClientFiles = new ObservableCollection<MinecraftFile>();
        ServerFiles = new ObservableCollection<MinecraftFile>();
        FilesToRemove = new List<MinecraftFile>();
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
        // var label = (sender as Element)?.Parent as Label;
        var item = (sender as BindableObject).BindingContext as MinecraftFile;
        e.Data.Properties.Add("Text", item);
    }

    private void DropGestureRecognizer_Drop_Server(object sender, DropEventArgs e)
    {
        // var item = (sender as BindableObject).BindingContext as MinecraftFile;
        var frame = (sender as Element)?.Parent as Frame;
        var item = e.Data.Properties["Text"] as MinecraftFile;
        if (item != null)
        {
            ServerFiles.Add(item);
        }
    }
    private void DropGestureRecognizer_Drop_Client(object sender, DropEventArgs e)
    {
        // var item = (sender as BindableObject).BindingContext as MinecraftFile;
        var frame = (sender as Element)?.Parent as Frame;
        var item = e.Data.Properties["Text"] as MinecraftFile;
        if (item != null)
        {
            ClientFiles.Add(item);
        }
    }
    private void DropGestureRecognizer_Drop_Delete(object sender, DropEventArgs e)
    {
        // var item = (sender as BindableObject).BindingContext as MinecraftFile;
        var frame = (sender as Element)?.Parent as Frame;
        var item = e.Data.Properties["Text"] as MinecraftFile;
        if (item != null && FilesToRemove.Contains(item) == false)
        {
            FilesToRemove.Add(item);
            if (item.Enviroment.Equals(TypeEnviroment.Client))
            {
                ClientFiles.Remove(item);
                return;
            }
            ServerFiles.Remove(item);
        }
    }
    private async void OnAddServerFilesClicked(object sender, EventArgs e)
    {

    }

    private async void OnAddClientFilesClicked(object sender, EventArgs e)
    {

    }
    private async void SaveClicked(object sender, EventArgs e)
    {
        foreach (var item in ClientFiles)
        {
            Utils.MoveFileToClient(item.FullPath, CurrentModPack.Directory);
        }
        foreach (var item in ServerFiles)
        {
            Utils.MoveFileToServer(item.FullPath, CurrentModPack.Directory);
        }
        foreach (var item in FilesToRemove)
        {
            if (File.Exists(item.FullPath))
            {
                File.Delete(item.FullPath);
            }
        }
        await Navigation.PopAsync();
    }
    private async void CancelClicked(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }
}