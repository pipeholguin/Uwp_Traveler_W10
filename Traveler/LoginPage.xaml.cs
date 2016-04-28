using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Traveler.Models;
using Traveler.Net;
using Windows.Data.Json;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
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
    public sealed partial class LoginPage : Page
    {
        ApplicationDataContainer data;
        string status;
        public LoginPage()
        {
            this.InitializeComponent();
            SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = AppViewBackButtonVisibility.Visible;
            SystemNavigationManager.GetForCurrentView().BackRequested += DetailBusquedaPage_BackRequested;
            data = ApplicationData.Current.LocalSettings;
           
        }

        private void DetailBusquedaPage_BackRequested(object sender, BackRequestedEventArgs e)
        {
            Frame root = Window.Current.Content as Frame;
            if (root.CanGoBack && e.Handled == false)
            {
                root.GoBack();

            }

        }

        private async void login(object sender, RoutedEventArgs e)
        {

            loginapi login = new loginapi();
            bool success = await login.usuarios(usr.Text, pass.Password);

            status = data.Values["log"] as string;

            if (status == "OK") {
                Frame root = Window.Current.Content as Frame;
                root.Navigate(typeof(MenuPage));
            }
            else if (status=="FAIL"){
                aviso1.Text = "Usuario y/o Contrasena incorrectos";
            }
           
           

        }
    }
}
