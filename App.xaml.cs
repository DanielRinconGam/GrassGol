using Microsoft.Maui.Controls;
using Microsoft.Maui.Handlers;


namespace GrassGol
{
    public partial class App : Application
    {
        public App()
        {

            InitializeComponent();

            MainPage = new AppShell();
            {
               // WidthRequest = 700,
               //HeightRequest = 450
            };


            // Personalizacion Entrys
            Microsoft.Maui.Handlers.EntryHandler.Mapper.AppendToMapping(nameof(EntryHandler), (handler, view) =>
            {
#if WINDOWS
                //Transparente
                var transparentBrush = new Microsoft.UI.Xaml.Media.SolidColorBrush(Microsoft.UI.Colors.Transparent);
                var platformView = handler.PlatformView;

                // Estado inicial
                platformView.Background = transparentBrush;
                platformView.BorderBrush = transparentBrush;

                // Elimina estilo
                platformView.Style = null;
                platformView.BorderThickness = new Microsoft.UI.Xaml.Thickness(0);

                // Elimina Enfoque
                platformView.FocusVisualPrimaryThickness = new Microsoft.UI.Xaml.Thickness(0);
                platformView.FocusVisualSecondaryThickness = new Microsoft.UI.Xaml.Thickness(0);

                // Evento mouse entra
                platformView.PointerEntered += (s, e) =>
                {
                    platformView.Background = transparentBrush;
                    platformView.BorderBrush = transparentBrush;
                };

                //Evento mouse selecciona
                platformView.PointerPressed += (s, e) =>
                {
                    platformView.Background = transparentBrush;
                    platformView.BorderBrush = transparentBrush;
                };

                // Evento mouse sale
                platformView.PointerExited += (s, e) =>
                {
                    platformView.Background = transparentBrush;
                    platformView.BorderBrush = transparentBrush;
                };

#endif
            });



        }


    }


}
