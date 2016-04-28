using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Traveler.Models;
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

// La plantilla de elemento Página en blanco está documentada en http://go.microsoft.com/fwlink/?LinkId=234238

namespace Traveler
{
    /// <summary>
    /// Una página vacía que se puede usar de forma independiente o a la que se puede navegar dentro de un objeto Frame.
    /// </summary>
    public sealed partial class DetailBusquedaPage : Page

    {
        ApplicationDataContainer dato;
        public DetailBusquedaPage()
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
        public Viaje viaje { get; set; }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            Viaje v = e.Parameter as Viaje;
            viaje = v;

            string identificador = v.Id.ToString();
            
        }

        private void salir(object sender, RoutedEventArgs e)
        {
            Frame root = Window.Current.Content as Frame;
            root.Navigate(typeof(MainPage), null);

            dato = ApplicationData.Current.LocalSettings;
            dato.Values["log"] = "";
        }
    }
}
