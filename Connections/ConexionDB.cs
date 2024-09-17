using MySql.Data.MySqlClient;
namespace GrassGol.Connections;

public partial class ConexionDB : ContentPage

{
    public ConexionDB()
    {

    }

    private string connectionString = "Server=35.188.49.212;Database=GrassDB;User ID=consulta-grass;Password=Grass%%#;Pooling=true";



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

    public async Task<bool> ValidateUsers(string email, string pass)
    {
        bool login = false;
        try
        {
            using (var connection = new MySqlConnection(connectionString))
            {
                await connection.OpenAsync();
                string query = "SELECT COUNT(1) FROM Usuario WHERE Correo = @email AND PasswordUser = @pass;";
                using (MySqlCommand cmd = new MySqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@email", email);
                    cmd.Parameters.AddWithValue("@pass", pass);

                    int count = Convert.ToInt32(await cmd.ExecuteScalarAsync());
                    login = count > 0;
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
        return login;
    }
}
