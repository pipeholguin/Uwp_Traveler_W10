
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Traveler.Models;
using Windows.Devices.Geolocation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.Data.Json;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.Services.Maps;
using Windows.UI.Core;
using Windows.Storage.Pickers;
using Windows.Storage;
using Windows.Storage.Streams;
using Windows.UI.Xaml.Media.Imaging;
using Traveler.Net;
using System.Threading.Tasks;
using Windows.UI.Popups;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Traveler
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MenuPage : Page
    {
        HttpConnection con;
        string url;
        JsonArray json1;
        const String urlimage ="";
        string base64string;
        ApplicationDataContainer dato;



        public MenuPage()
        {
           // this.InitializeComponent();
            getPos();
            SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = AppViewBackButtonVisibility.Collapsed;
           
            con = new HttpConnection();
            url = "http://localhost/traveler/public/index.php/viajes";
            this.Loaded += MenuPage_Loaded;
            

        }

        private void MenuPage_Loaded(object sender, RoutedEventArgs e)
        {
            SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = AppViewBackButtonVisibility.Collapsed;
            getdata();

        }
        public async void getdata()
        {
            string rta = await con.requestByGet(url);
            json1 = JsonArray.Parse(rta);
            this.InitializeComponent();
            
        }


        
        private ObservableCollection<Viaje> data;
        public ObservableCollection<Viaje> Data
        {
            get {
                if (data == null)
                {
                    data = new ObservableCollection<Viaje>();

                    

                    foreach (JsonValue JO in json1)
                    {
                        Viaje v = new Viaje();
                        JsonObject viaje = JO.GetObject();
                        v.Id = viaje["id"].GetString();
                        v.Origen = viaje["origen"].GetString();
                        v.Destino = viaje["destino"].GetString();
                        v.Precio = viaje["precio"].GetString();
                        v.Asientos = viaje["asientos"].GetString();
                        v.Fecha = viaje["fecha"].GetString();
                        v.Hora = viaje["hora"].GetString();
                        v.Carro = viaje["carro"].GetString();
                        v.Contacto = viaje["contacto"].GetString();
                        v.Imagen = viaje["imagen"].GetString();

                        data.Add(v);
                    }
                }
                return data; }
            set { data = value; }
        }

    private async void SubirImagen_Click(object sender, RoutedEventArgs e)
        {
            

            FileOpenPicker openPicker = new FileOpenPicker();
            openPicker.ViewMode = PickerViewMode.Thumbnail;
            openPicker.SuggestedStartLocation = PickerLocationId.PicturesLibrary;
            openPicker.FileTypeFilter.Add(".jpg");
            openPicker.FileTypeFilter.Add(".jpeg");
            openPicker.FileTypeFilter.Add(".png");
            StorageFile file = await openPicker.PickSingleFileAsync();
            if (file != null)
            {

                using (IRandomAccessStream fileStream = await file.OpenAsync(Windows.Storage.FileAccessMode.Read))
                {
                    // Set the image source to the selected bitmap 
                    BitmapImage bitmapImage = new BitmapImage();
                   
                    await bitmapImage.SetSourceAsync(fileStream);
                    ImageS.Source = bitmapImage;
                    Byte[] picAttachment = new Byte[0];
                    var reader = new DataReader(fileStream.GetInputStreamAt(0));
                    picAttachment = new Byte[fileStream.Size];
                    await reader.LoadAsync((uint)fileStream.Size);
                    reader.ReadBytes(picAttachment);
                    base64string = Convert.ToBase64String(picAttachment);
                              
                }
        
            }
        }


        public async void getPos()
        {
            Geolocator geolocator = new Geolocator { DesiredAccuracyInMeters = 1 };

            Geoposition pos = await geolocator.GetGeopositionAsync();
            var longitud = (float)pos.Coordinate.Point.Position.Latitude;
            var latitud = (float)pos.Coordinate.Point.Position.Longitude;
            // origin.Text =longitud.ToString();

            BasicGeoposition location = new BasicGeoposition();
            location.Latitude = latitud;
            location.Longitude = longitud;
            Geopoint pointToReverseGeocode = new Geopoint(location);

            MapLocationFinderResult result =
            await MapLocationFinder.FindLocationsAtAsync(pointToReverseGeocode);
            if (result.Status == MapLocationFinderStatus.Success)
            {
                origengps.Text = result.Locations[0].Address.Country;
            }
        }

    private void list_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Viaje v = data.ElementAt(list.SelectedIndex);
            Frame root = Window.Current.Content as Frame;          
            root.Navigate(typeof(DetailBusquedaPage), v );
        }

    private async void registrar(object sender, RoutedEventArgs e)
        {
            if (contactop.Text == "" || preciop.Text=="" || destinop.SelectedItem==null || cuposp.SelectedItem==null || carrop.SelectedItem==null || horap.SelectedItem==null || jornadap.SelectedItem==null || ImageS. ) {
                MessageDialog dialog = new MessageDialog("Campos incompletos", "Error");
                await dialog.ShowAsync();
                
            }
            else {

                string carp = carrop.SelectedItem.ToString();
                string desp = destinop.SelectedItem.ToString();
                string cupp = cuposp.SelectedItem.ToString();
                string horp = horap.SelectedItem.ToString() +" "+ jornadap.SelectedItem.ToString();
                string fecp = fechap.Date.Year.ToString() + "-" + fechap.Date.Month.ToString() + "-" + fechap.Date.Day.ToString();
               


                viajesapi api = new viajesapi();
                bool success = await api.viaje(origenp.Text, desp, preciop.Text, cupp, horp, fecp, carp, base64string, contactop.Text);

                MessageDialog dialog = new MessageDialog("Viaje registrado con exito", "Guardado");
                await dialog.ShowAsync();

                Frame root = Window.Current.Content as Frame;
                root.Navigate(typeof(MenuPage), null);
            }

           
           
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
