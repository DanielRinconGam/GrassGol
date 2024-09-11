using MySql.Data.MySqlClient;
namespace GrassGol.Connections;

public partial class ConexionDB : ContentPage

{
    public ConexionDB()
	{

    }

    private string connectionString = "Server=35.188.49.212;Database=Grass;User ID=consulta-grass;Password=Grass%%#;Pooling=true";

    public async void Button_Clicked(object sender, EventArgs e)
    {
        bool isConnected = await TestDatabaseConnectionAsync();
        if (isConnected)
        {
            await DisplayAlert("Conexión Exitosa", "Se ha conectado a la base de datos con éxito.", "OK");
        }
        else
        {
            await DisplayAlert("Error de Conexión", "No se pudo conectar a la base de datos.", "OK");
        }
    }
    public async Task<bool> TestDatabaseConnectionAsync()
    {
        try
        {
            using (var connection = new MySqlConnection(connectionString))
            {
                await connection.OpenAsync();
                return true;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error al conectar a la base de datos: {ex.Message}");
            return false;
        }
    }
}
