using boberto_launcher_modpack_editor.Models;

namespace boberto_launcher_modpack_editor;

public partial class ModPackPage : ContentPage
{
    public ModPack CurrentModPack { get; set; }

    public ModPackPage(ModPack modpack)
    {
        InitializeComponent();
        CurrentModPack = modpack;
    }
    public void LoadFields()
    {

    }
    private void SaveClicked(object sender, EventArgs e)
    {
        CurrentModPack.Create();
    }
    private async void CancelClicked(object sender, EventArgs e)
    {
        await Navigation.PopAsync();

    }
}