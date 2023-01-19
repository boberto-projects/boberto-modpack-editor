using boberto_launcher_modpack_editor.Models;

namespace boberto_launcher_modpack_editor;

public partial class ModPackPage : ContentPage
{
    public ModPack CurrentModPack { get; set; }
    public ModPackPage(ModPack modpack)
    {
        InitializeComponent();
        CurrentModPack = modpack;
        LoadFields();
    }
    public void LoadFields()
    {
        if (CurrentModPack == null)
        {
            return;
        }
        Id.Text = CurrentModPack.Id;
        Name.Text = CurrentModPack.Name;
        isModded.IsChecked = CurrentModPack.IsModded;
        isVerifyMods.IsChecked = CurrentModPack.IsVerifyMods;
        isDefault.IsChecked = CurrentModPack.IsDefault;
        isJava.IsChecked = CurrentModPack.IsJava;
        serverIp.Text = CurrentModPack.ServerIp;
        serverPort.Text = CurrentModPack.ServerPort.ToString();
        directory.Text = CurrentModPack.Directory;
        minecraftVersion.Text = CurrentModPack.MinecraftVersion;
        Ignored.Text = string.Join(",", CurrentModPack.Ignored);
        Author.Text = CurrentModPack.Author;
        Description.Text = CurrentModPack.Description;
        img.Text = CurrentModPack.Img;
    }
    private async void SaveClicked(object sender, EventArgs e)
    {
        CurrentModPack.Name = Name.Text;
        CurrentModPack.IsModded = isModded.IsChecked;
        CurrentModPack.IsVerifyMods = isVerifyMods.IsChecked;
        CurrentModPack.IsDefault = isDefault.IsChecked;
        CurrentModPack.IsJava = isJava.IsChecked;
        CurrentModPack.ServerIp = serverIp.Text;
        CurrentModPack.ServerPort = int.Parse(serverPort.Text);
        CurrentModPack.MinecraftVersion = minecraftVersion.Text;
        CurrentModPack.Ignored = string.IsNullOrEmpty(Ignored.Text) == false ? Ignored.Text.Split(",") : new List<string>();
        CurrentModPack.Author = Author.Text;
        CurrentModPack.Description = Description.Text;
        CurrentModPack.Img = img.Text;
        //if (ConfigHelper.Exists(CurrentModPack))
        //{
        //    CurrentModPack.Create();
        //}
        if (CurrentModPack.Directory != directory.Text)
        {
            var newDirName = directory.Text;
            CurrentModPack.RemoveModPackFiles(newDirName);
            CurrentModPack.Directory = directory.Text;
        }
        CurrentModPack.Create();
        ConfigHelper.SaveModPack(CurrentModPack);
        await Navigation.PopAsync();

    }
    private async void CancelClicked(object sender, EventArgs e)
    {
        await Navigation.PopAsync();

    }
    private void ForgeInstallerAssistantClicked(object sender, EventArgs e)
    {

    }
    private async void FilesEditorClicked(object sender, EventArgs e)
    {
        if (CurrentModPack == null)
        {
            DisplayAlert("Warning", "Modpack cant be found for this selectable value", "OK");
            return;
        }
        await Navigation.PushAsync(new ModPackFileEditor(CurrentModPack));
    }


}