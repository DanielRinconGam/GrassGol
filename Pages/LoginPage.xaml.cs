namespace GrassGol.Pages;

using GrassGol.Connections;
using System.Diagnostics;


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
        try
        {
            await Shell.Current.GoToAsync("//businessHome", true);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error conexión: {ex.Message}");
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


