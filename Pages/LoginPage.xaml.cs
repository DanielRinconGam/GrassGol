namespace GrassGol.Pages;
using GrassGol.Connections;

public partial class LoginPage : ContentPage
{
    private ConexionDB database;

    public LoginPage()
    {
        InitializeComponent();
        database = new ConexionDB();
    }

    private async void RegisterTapped(object sender, TappedEventArgs e)
    {
        await Shell.Current.GoToAsync("//register", true);
    }

    private async void LoginClicked(object sender, EventArgs e)
    {
        string email = emailEntry.Text;
        string pass = PasswordEntry.Text;

        bool isValidUser = await database.ValidateUsers(email, pass);

        if (isValidUser)
        {
            //await DisplayAlert("Login", "Correo y contraseÃ±a correctos", "OK");
            await Shell.Current.GoToAsync("//businessHome", true);
        }
        else
        {
            await DisplayAlert("Login", "Correo o contraseÃ±a incorrectos", "OK");

        }
    }

    private async void Button_Clicked(object sender, EventArgs e)
    {
        bool isConnected = await database.TestDatabaseConnectionAsync();

        if (isConnected)
        {
            await DisplayAlert("Conexión Exitosa", "Se ha conectado a la base de datos con exito.", "OK");
        }
        else
        {
            await DisplayAlert("Error de Conexión", "No se pudo conectar a la base de datos.", "OK");
        }
    }

}


