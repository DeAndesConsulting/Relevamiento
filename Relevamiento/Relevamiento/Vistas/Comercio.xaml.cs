/*
 * Falta ver porque no se puede setear en el picker de tipo de local
 * 
 */

using Newtonsoft.Json;
using Relevamiento.Clases;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Relevamiento.Vistas
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Comercio : ContentPage
    {
        //public Distribuidora distribuidorseleccionado;
        public Local ComercioSeleccionado;
        public Comercio(Distribuidora Distribuidor)
        {
            InitializeComponent();
            List<TipoLocal> lista_locales = new List<TipoLocal>();
            lista_locales = TraerLocales();
            pickerTipoLocal.ItemsSource = lista_locales.ToList();
            PickerProvincia.SelectedItem = Distribuidor.Provincia;
            App.distribuidorseleccionado = Distribuidor;
        }
            public bool ValidarDatos()
        {
            bool validar = true;
            if (string.IsNullOrEmpty(entryCalleLocal.Text))
            {
                LabelCalleLocal.IsVisible = true;
                validar = false;
            }
            if (string.IsNullOrEmpty(EntryLocalidad.Text))
            {
                LabelLocalidad.IsVisible = true;
                validar = false;
            }
            if (string.IsNullOrEmpty(entryNombreLocal.Text))
            {
                LabelNombreLocal.IsVisible = true;
                validar = false;
            }
            if (string.IsNullOrEmpty(entryNumeroLocal.Text))
            {
                LabelNumero.IsVisible = true;
                validar = false;
            }
            if (pickerTipoLocal.SelectedIndex == -1)
            {
                LabelTipoLocal.IsVisible = true;
              validar = false;
            }
            return validar;
        }
        public List<TipoLocal> TraerLocales()
        {
            List<TipoLocal> ListaComercios = new List<TipoLocal>();

            TipoLocal d3 = new TipoLocal()
            {
                Id = 1,
                Tipo = "Almacen"
            };
            ListaComercios.Add(d3);

            d3 = new TipoLocal()
            {
                Id = 2,
                Tipo = "Chino"
            };
            ListaComercios.Add(d3);
            d3 = new TipoLocal()
            {
                Id = 3,
                Tipo = "Despensa",
            };
            ListaComercios.Add(d3);
            d3 = new TipoLocal()
            {
                Id = 4,
                Tipo = "Kiosco",
            };
            ListaComercios.Add(d3);

            return ListaComercios;
        }

        public List<Local> TraerLocales2(Distribuidora Distribuidor)
        {
            List<Local> listaLocales = new List<Local>();
            using (SQLite.SQLiteConnection conexion = new SQLiteConnection(App.RutaBD))
            {

                listaLocales = conexion.Query<Local>("select * from Local where Distribuidor = ?", Distribuidor.Nombre).ToList();

            }
            return listaLocales;
        }
        private async void btnCancelarClicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }

        private async void btnFinalizarClicked(object sender, EventArgs e)
        {

            string URL = "https://jsonplaceholder.typicode.com/posts";
            App.releva.fK_ERP_EMPRESAS = "2";
            App.releva.fK_ERP_ASESORES = 2;
            App.releva.fecha = DateTime.Now;
            App.releva.codigo = "ASD123ADSASD";
            Relevamientos relevamientos = new Relevamientos();
            relevamientos.relevamiento = App.releva;
            relevamientos.comercios = App.comercios;
            relevamientos.codigoRequest = "123456789-8";
            var post = relevamientos;
            var myHttpClient = new HttpClient();
 //           var response = await myHttpClient.PostAsync(URL, relevamientos);

            //var json = await response.Content.ReadAsStringAsync();
               var json = JsonConvert.SerializeObject(relevamientos, Formatting.Indented);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await myHttpClient.PostAsync(URL, content);
        }

        private async void btnSiguienteClicked(object sender, EventArgs e)
        {
            if (ValidarDatos())
            {

                Local nuevoLocal = new Local()
                {
                    Provincia = PickerProvincia.Items[PickerProvincia.SelectedIndex],
                    TipoLocal = pickerTipoLocal.Items[pickerTipoLocal.SelectedIndex],
                    Distribuidor = App.distribuidorseleccionado.Nombre,
                    Nombre = entryNombreLocal.Text,
                    Calle = entryCalleLocal.Text,
                    Numero = entryNumeroLocal.Text,
                    Localidad = EntryLocalidad.Text,
                };
                await Navigation.PushAsync(new Tabbed(nuevoLocal));
            }
        }
    }
}