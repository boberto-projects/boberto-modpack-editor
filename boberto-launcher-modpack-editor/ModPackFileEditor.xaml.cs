using boberto_launcher_modpack_editor.Models;
using System.Collections.ObjectModel;
using System.Diagnostics;

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

    private void DragGestureRecognizer_Original(object sender, DragStartingEventArgs e)
    {
        var item = (sender as BindableObject).BindingContext as MinecraftFile;
        e.Data.Properties.Add("Text", item);
        e.Data.Properties.Add("Origem", true);
    }
    private void DropGestureRecognizer_Drop_Server(object sender, DropEventArgs e)
    {
        var isOriginalListFile = e.Data.Properties.ContainsKey("Origem");
        var item = e.Data.Properties["Text"] as MinecraftFile;
        if (item != null)
        {
            if (isOriginalListFile == false && item.Enviroment.Equals(TypeEnviroment.Client))
            {
                item.Enviroment = TypeEnviroment.Server;
                ClientFiles.Remove(item);
            }
            ServerFiles.Add(item);
        }
    }
    private void DropGestureRecognizer_Drop_Client(object sender, DropEventArgs e)
    {
        var isOriginalListFile = e.Data.Properties.ContainsKey("Origem");
        var item = e.Data.Properties["Text"] as MinecraftFile;
        if (item != null)
        {
            if (isOriginalListFile == false && item.Enviroment.Equals(TypeEnviroment.Server))
            {
                item.Enviroment = TypeEnviroment.Client;
                ServerFiles.Remove(item);
            }
            ClientFiles.Add(item);
        }
    }
    private void DropGestureRecognizer_Drop_Delete(object sender, DropEventArgs e)
    {
        var item = e.Data.Properties["Text"] as MinecraftFile;
        if (item != null)
        {
            if (item.Enviroment.Equals(TypeEnviroment.Client))
            {
                ClientFiles.Remove(item);
            }
            if (item.Enviroment.Equals(TypeEnviroment.Server))
            {
                ServerFiles.Remove(item);
            }
            FilesToRemove.Add(item);
        }
    }
    private async void OnAddServerFilesClicked(object sender, EventArgs e)
    {
        var modpackPath = Utils.GetModPacksDir(CurrentModPack.Directory);
        PickOptions options = new()
        {
            PickerTitle = "Please select a comic file",
        };
        var files = await PickAndShow(options);
        foreach (var file in files)
        {
            Utils.MoveFileToServer(file.FullPath, modpackPath);
        }
    }

    private async void OnAddClientFilesClicked(object sender, EventArgs e)
    {

    }
    private async void OnShowModsFolder(object sender, EventArgs e)
    {
        var modpackPath = Utils.GetModPacksDir(CurrentModPack.Directory);
        Process.Start("explorer.exe", modpackPath);
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
    /// <summary>
    /// https://learn.microsoft.com/en-us/dotnet/maui/platform-integration/storage/file-picker?view=net-maui-7.0&tabs=windows
    /// </summary>
    /// <param name="options"></param>
    /// <returns></returns>
    private async Task<IEnumerable<FileResult>> PickAndShow(PickOptions options)
    {
        try
        {
            var result = await FilePicker.Default.PickMultipleAsync(options);

            return result;
        }
        catch (Exception ex)
        {
            // The user canceled or something went wrong
        }

        return null;
    }
    private async void CancelClicked(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }
}