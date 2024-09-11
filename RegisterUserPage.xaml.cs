namespace GrassGol;


public partial class RegisterUserPage : ContentPage
{
	public RegisterUserPage()
	{
		InitializeComponent();
	}
    private void RadioButtonChecked(object sender, CheckedChangedEventArgs e)
    {
        if (e.Value)
        {
            var radioButton = sender as RadioButton;

            if (radioButton != null)
            {
                if (radioButton.Value.ToString() == "cuentaPersonal")
                {
                    // Placeholders para Usuario.
                    registerNameEntry.Placeholder = "Ingrese su nombre";
                    registerLastnameEntry.Placeholder = "Ingrese su apellido";
                    registerEmailEntry.Placeholder = "Ingrese su correo";
                }
                else if (radioButton.Value.ToString() == "cuentaEmpresa")
                {
                    // Placeholders para Empresa.
                    registerNameEntry.Placeholder = "Ingrese el nombre de su empresa";
                    registerLastnameEntry.Placeholder = "Ingrese la dirección de su empresa";
                    registerEmailEntry.Placeholder = "Ingrese el correo de su empresa";

                }
            }
        }
    }

    private async void LoginTapped(object sender, TappedEventArgs e)
    {
        await this.FadeTo(0.8, 200, Easing.CubicIn);
        await Shell.Current.GoToAsync("///login", true);
        await this.FadeTo(1, 200, Easing.CubicOut);
    }


}