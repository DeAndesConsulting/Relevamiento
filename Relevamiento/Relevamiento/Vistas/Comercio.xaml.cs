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
        public _COMERCIO ComercioSeleccionado;
        public Comercio(ERP_EMPRESAS Distribuidor)
        {
            InitializeComponent();
            List<TipoLocal> lista_locales = new List<TipoLocal>();
            lista_locales = TraerLocales();
            pickerTipoLocal.ItemsSource = lista_locales.ToList();
            PickerProvincia.SelectedItem = Distribuidor.Z_FK_ERP_PROVINCIAS;
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

                _COMERCIO nuevoLocal = new _COMERCIO()
                {
                    //   FK_ERP_PROVINCIAS = PickerProvincia.Items[PickerProvincia.SelectedIndex],
                    //FK_TIP_COM = pickerTipoLocal.Items[pickerTipoLocal.SelectedIndex],
                    //  Distribuidor = App.distribuidorseleccionado.NOM_FANTASIA,
                    NOMBRE = entryNombreLocal.Text,
                    CALLE = entryCalleLocal.Text,
                    NUMERO = entryNumeroLocal.Text,
                    FK_ERP_LOCALIDADES = EntryLocalidad.Text,
                };
                await Navigation.PushAsync(new Tabbed(nuevoLocal));
            }
        }
    }
}