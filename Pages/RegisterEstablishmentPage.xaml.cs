namespace GrassGol.Pages;

public partial class RegisterEstablishmentPage : ContentPage
{
	public RegisterEstablishmentPage()
	{
		InitializeComponent();
	}

    private async void Button_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("//businessHome", true);
    }
}