namespace GrassGol;


public partial class LoginPage : ContentPage
{

    public LoginPage()
    {
        InitializeComponent();

    }
    

    private async void RegisterTapped(object sender, TappedEventArgs e)
    {

        await this.FadeTo(0.8, 200, Easing.CubicIn);
        await Shell.Current.GoToAsync("///register",true);
        await this.FadeTo(1, 200, Easing.CubicOut);

    }

    private async void Button_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("///paraBase", true);
    }
}