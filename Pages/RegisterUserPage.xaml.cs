namespace GrassGol.Pages;
using GrassGol.Connections;
using GrassGol.Pages.Business;
public partial class RegisterUserPage : ContentPage
{
    private ConexionDB database;

    public RegisterUserPage()
	{
		InitializeComponent();
        database = new ConexionDB();
    }

    private async void RegisterClicked(object sender, EventArgs e)
    {
      
        if (radioUser.IsChecked)
        {
            string name = registerNameEntry.Text;
            string lastname = registerLastnameEntry.Text;
            string email = registerEmailEntry.Text;
            string pass0 = registerPasswordEntry.Text;
            string pass1 = registerRePasswordEntry.Text;
            if (pass0 != pass1)
            {
                await DisplayAlert("Registro", "Las contraseñas no coinciden", "OK");
                return;
            }
            else if (pass0.Length < 8)
            {
                await DisplayAlert("Registro", "La contraseña debe tener mínimo 8 caracteres", "OK");
                return;
            }
            bool emailfree = await database.NewEmail(email);
            if (!emailfree)
            {
                await DisplayAlert("Registro", $"{email} ya se encuentra registrado", "OK");
                return;
            }
            bool register = await database.RegisterUser(name, lastname, email, pass0);
            if (register)
            {
                await DisplayAlert("Registro", "Registrado con éxito!", "OK");
                await Shell.Current.GoToAsync("//login", true); 
            }
            else
            {
                await DisplayAlert("Registro", "Hubo un error en el registro", "OK");
            }
        }
        else if (radioEstablishment.IsChecked)
        {
            string wname = registerNameEntry.Text;
            string wlastname = registerLastnameEntry.Text;
            string wemail = registerEmailEntry.Text;
            string wpass0 = registerPasswordEntry.Text;
            string wpass1 = registerRePasswordEntry.Text;
            if (wpass0 != wpass1)
            {
                await DisplayAlert("Registro", "Las contraseñas no coinciden", "OK");
                return;
            }
            else if (wpass0.Length < 8)
            {
                await DisplayAlert("Registro", "La contraseña debe tener mínimo 8 caracteres", "OK");
                return;
            }
            bool emailfree = await database.NewEmail(wemail);
            if (!emailfree)
            {
                await DisplayAlert("Registro", $"{wemail} ya se encuentra registrado", "OK");
                return;
            }
            await Navigation.PushAsync(new RegisterEstablishmentPage(wname, wlastname, wemail, wpass0));
        }
    }
    private async void LoginTapped(object sender, TappedEventArgs e)
    {
        await Shell.Current.GoToAsync("//login", true);
    }    
}