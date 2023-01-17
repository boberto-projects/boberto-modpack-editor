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

        Author.Text = CurrentModPack.Author;
        Description.Text = CurrentModPack.Description;
        img.Text = CurrentModPack.Img;
    }
    private void SaveClicked(object sender, EventArgs e)
    {
        CurrentModPack.Name = Name.Text;
        CurrentModPack.IsModded = isModded.IsChecked;
        CurrentModPack.IsVerifyMods = isVerifyMods.IsChecked;
        CurrentModPack.IsDefault = isDefault.IsChecked;
        CurrentModPack.IsJava = isJava.IsChecked;
        CurrentModPack.ServerIp = serverIp.Text;
        CurrentModPack.ServerPort = int.Parse(serverPort.Text);
        CurrentModPack.Directory = directory.Text;
        CurrentModPack.MinecraftVersion = minecraftVersion.Text;
        CurrentModPack.Ignored = Ignored.Text.Split(",");
        CurrentModPack.Author = Author.Text;
        CurrentModPack.Description = Description.Text;
        CurrentModPack.Img = img.Text;
        if (ConfigHelper.Exists(CurrentModPack))
        {
            CurrentModPack.Create();
        }
        ConfigHelper.SaveModPack(CurrentModPack);
    }
    private async void CancelClicked(object sender, EventArgs e)
    {
        await Navigation.PopAsync();

    }
    private void ForgeInstallerAssistantClicked(object sender, EventArgs e)
    {

    }


}