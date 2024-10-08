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
                string query = "SELECT PasswordUser FROM Usuario WHERE Correo = @email;";
                using (MySqlCommand cmd = new MySqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@email", email);
                    // Obtenemos el resultado de la consulta
                    var result = await cmd.ExecuteScalarAsync();
                    // Verificamos que el resultado no sea null y sea un string
                    if (result != null && result is string storedHashedPassword)
                    {
                        // Verificamos si la contraseña ingresada coincide con la hasheada
                        login = BCrypt.Net.BCrypt.Verify(pass, storedHashedPassword);
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
        return login;
    }

    public async Task<bool> RegisterUser(string rname, string rlastname, string remail, string rpass)
    {
        bool register = false;
        try
        {
            using (var connection = new MySqlConnection(connectionString))
            {
                await connection.OpenAsync();
                string ProPass = BCrypt.Net.BCrypt.HashPassword(rpass);
                string query = "CALL sp_RegistraUsuario (@name, @lastname, @email, @pass);";
                using (MySqlCommand cmd = new MySqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@name", rname);
                    cmd.Parameters.AddWithValue("@lastname", rlastname);
                    cmd.Parameters.AddWithValue("@email", remail);
                    cmd.Parameters.AddWithValue("@pass", ProPass);
                    int count = Convert.ToInt32(await cmd.ExecuteScalarAsync());
                    register = true;
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
        return register;
    }

    public async Task<bool> NewEmail(string email1)
    {
        bool emailfree = false;
        try
        {
            using (var connection = new MySqlConnection(connectionString))
            {
                await connection.OpenAsync();
                string query = "SELECT COUNT(1) FROM Usuario WHERE Correo=@email;";
                using (MySqlCommand cmd1 = new MySqlCommand(query, connection))
                {
                    cmd1.Parameters.AddWithValue("@email", email1);
                    var result = Convert.ToInt32(await cmd1.ExecuteScalarAsync());
                    emailfree = result == 0;
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
        return emailfree;
    }

    public async Task<bool> RegisterWorkersAdmin(int idemp, string rname, string rlastname, string remail, string rpass, int rrol)
    {
        bool register = false;
        try
        {
            using (var connection = new MySqlConnection(connectionString))
            {
                await connection.OpenAsync();
                string query = "INSERT INTO `GrassDB`.`Trabajador` (`IdEstablecimiento`, `NombreTrabajador`, `ApellidoTrabajador`, `CorreoTrabajador`, `PasswordTrabajador`, `RolEmpresa`) VALUES " +
                    "(@Idempresa, @name, @lastname, @email, @pass, @rol);";
                string ProPass = BCrypt.Net.BCrypt.HashPassword(rpass);
                using (MySqlCommand cmd = new MySqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@Idempresa", idemp);
                    cmd.Parameters.AddWithValue("@name", rname);
                    cmd.Parameters.AddWithValue("@lastname", rlastname);
                    cmd.Parameters.AddWithValue("@email", remail);
                    cmd.Parameters.AddWithValue("@pass", ProPass);
                    cmd.Parameters.AddWithValue("@rol", rrol);
                    int count = await cmd.ExecuteNonQueryAsync();
                    if (count > 0)
                    {
                        register = true;
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error al registrar al administrador: " + ex.Message);
            Console.WriteLine("Detalles del error: " + ex.ToString());
        }
        return register;
    }

    public async Task<int> CreateBusiness(string bname, string badress, string bnit, string bphone)
    {
        int idEmpresa = 0;
        try
        {
            using (var connection = new MySqlConnection(connectionString))
            {
                await connection.OpenAsync();
                string query = "INSERT INTO Establecimiento (NombreEstablecimiento, DireccionEstablecimiento, EstadoEstablecimiento, NitEstablecimiento, TelefonoEstablecimiento) VALUES (@bname, @badress, '1', @bnit, @bphone);";
                using (MySqlCommand cmd = new MySqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@bname", bname);
                    cmd.Parameters.AddWithValue("@badress", badress);
                    cmd.Parameters.AddWithValue("@bnit", bnit);
                    cmd.Parameters.AddWithValue("@bphone", bphone);
                    await cmd.ExecuteNonQueryAsync();
                }
                string getIdQuery = "SELECT LAST_INSERT_ID();";
                using (MySqlCommand cmd = new MySqlCommand(getIdQuery, connection))
                {
                    idEmpresa = Convert.ToInt32(await cmd.ExecuteScalarAsync());
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
        return idEmpresa;
    }


    public async Task<bool> NewNit(string bnit1)
    {
        bool nitfree = false;
        try
        {
            using (var connection = new MySqlConnection(connectionString))
            {
                await connection.OpenAsync();
                string query = "SELECT COUNT(1) FROM Establecimiento WHERE NitEstablecimiento=@nit1;";
                using (MySqlCommand cmd1 = new MySqlCommand(query, connection))
                {
                    cmd1.Parameters.AddWithValue("@nit1", bnit1);
                    var result = Convert.ToInt32(await cmd1.ExecuteScalarAsync());
                    nitfree = result == 0;
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
        return nitfree;
    }
}
