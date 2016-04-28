using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Traveler.Net;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Traveler
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class RegistroUsuarioPage : Page
    {
        public RegistroUsuarioPage()
        {
            this.InitializeComponent();
            SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = AppViewBackButtonVisibility.Visible;
            SystemNavigationManager.GetForCurrentView().BackRequested += DetailBusquedaPage_BackRequested;
        }

        private void DetailBusquedaPage_BackRequested(object sender, BackRequestedEventArgs e)
        {
            Frame root = Window.Current.Content as Frame;
            if (root.CanGoBack && e.Handled == false)
            {
                root.GoBack();

            }

        }
        private async void RegistrarUser(object sender, RoutedEventArgs e)
        {


            if (nombre.Text == "" || user.Text == "" || celular.Text == "" || contrasena.Password == "" || contrasenac.Password == "")
            {
                aviso.Text = "Campos requeridos incompletos";
            }

            else if (contrasena.Password != contrasenac.Password)
            {
                aviso.Text = "Confirme su contrasena";
            }

            else if(contrasena.Password==contrasenac.Password)
            {

                usuarioapi userapi = new usuarioapi();
                bool sucess = await userapi.usuarios(nombre.Text, email.Text, celular.Text, user.Text, contrasenac.Password );
                Frame root = Window.Current.Content as Frame;
                root.Navigate(typeof(MenuPage), null);
            }
           


               
            
            
        }
    }
}
