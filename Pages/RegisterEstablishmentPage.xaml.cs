using GrassGol.Connections;

namespace GrassGol.Pages;

public partial class RegisterEstablishmentPage : ContentPage
{
    private ConexionDB database;
    private string _wname, _wlastname, _wemail, _wpass0;


    public RegisterEstablishmentPage(string wname, string wlastname, string wemail, string wpass0)
    {
        InitializeComponent();
        _wname = wname;
        _wlastname = wlastname;
        _wemail = wemail;
        _wpass0 = wpass0;
        database = new ConexionDB();
    }

    private async void Button_Clicked(object sender, EventArgs e)
    {
        string bname = establishmentNameEntry.Text;
        string badress = establishmentAddressEntry.Text;
        string btel = establishmentPhoneEntry.Text;
        string bnit = establishmentNitEntry.Text;

        bool nitfree = await database.NewNit(bnit);
        if (!nitfree)
        {
            await DisplayAlert("Registro", $"{bnit} ya se encuentra registrado", "OK");
            return;
        }
        int empresaId = await database.CreateBusiness(bname, badress, bnit, btel);
        if (empresaId > 0) // Verificar si el ID es válido (empresa creada correctamente)
        {
            bool registerw = await database.RegisterWorkersAdmin(empresaId, _wname, _wlastname, _wemail, _wpass0, 1);
            if (registerw)
            {
                await DisplayAlert("Registro", $"Establecimiento registrado con éxito! \n El administrador será {_wname}", "OK");
            }
            else
            {
                await DisplayAlert("Error", "No se pudo registrar el administrador", "OK");
            }
        }
        else
        {
            await DisplayAlert("Error", "No se pudo registrar el establecimiento", "OK");
        }
    }
}