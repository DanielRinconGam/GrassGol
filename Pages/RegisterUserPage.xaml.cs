namespace GrassGol.Pages;

public partial class RegisterUserPage : ContentPage
{
	public RegisterUserPage()
	{
		InitializeComponent();
	}

    private async void RegisterClicked(object sender, EventArgs e)
    {
        if(radioUser.IsChecked)
        {

        }
        else if (radioEstablishment.IsChecked)
        {
            await Shell.Current.GoToAsync("//establishment", true);
        }

    }

    private async void LoginTapped(object sender, TappedEventArgs e)
    {
        await Shell.Current.GoToAsync("//login", true);
    }

    
}