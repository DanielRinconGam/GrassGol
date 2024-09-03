namespace GrassGol;


public partial class RegisterUserPage : ContentPage
{
	public RegisterUserPage()
	{
		InitializeComponent();
	}

    private async void LoginTapped(object sender, TappedEventArgs e)
    {
        await this.FadeTo(0.8, 200, Easing.CubicIn);
        await Shell.Current.GoToAsync("///login", true);
        await this.FadeTo(1, 200, Easing.CubicOut);
    }
}