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
        public _COMERCIO ComercioSeleccionado = new _COMERCIO();
        public ERP_LOCALIDADES LocalidadSeleccionada;
        public _TIP_COM TipoSeleccionado;
        public List<ERP_LOCALIDADES> ListaLocalidades = new List<ERP_LOCALIDADES>();
        public Comercio(ERP_EMPRESAS Distribuidor)
        {
            InitializeComponent();
            List<_TIP_COM> lista_locales = new List<_TIP_COM>();
            lista_locales = TraerLocales();
            using (SQLite.SQLiteConnection conexion = new SQLiteConnection(App.RutaBD))
            {
                ListaLocalidades = conexion.Query<ERP_LOCALIDADES>("select * from ERP_LOCALIDADES where Z_FK_ERP_PROVINCIAS = ?", Distribuidor.Z_FK_ERP_PROVINCIAS).ToList();

            }
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
            if (string.IsNullOrEmpty(LocalidadSearch.Text))
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



        public List<_TIP_COM> TraerLocales()
        {
            List<_TIP_COM> ListaComercios = new List<_TIP_COM>();

            _TIP_COM d3 = new _TIP_COM()
            {
                ID = 1,
                DESCRIPCION = "Almacen"
            };
            ListaComercios.Add(d3);

            d3 = new _TIP_COM()
            {
                ID = 2,
                DESCRIPCION = "Chino"
            };
            ListaComercios.Add(d3);
            d3 = new _TIP_COM()
            {
                ID = 3,
                DESCRIPCION = "Despensa",
            };
            ListaComercios.Add(d3);
            d3 = new _TIP_COM()
            {
                ID = 4,
                DESCRIPCION = "Kiosco",
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
            App.releva.FK_ERP_EMPRESAS = App.distribuidorseleccionado.ID.ToString();
            App.releva.FK_ERP_ASESORES = App.distribuidorseleccionado.FK_ERP_ASESORES;
            App.releva.FECHA = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
            App.releva.CODIGO = "ASD123ADSASD";
            ItrisPlanillaEntity relevamientos = new ItrisPlanillaEntity();
            relevamientos.relevamiento = App.releva;
            relevamientos.comercios = App.comercios;
            relevamientos.codigoRequest = "1123456789-8";
            var post = relevamientos;
            var myHttpClient = new HttpClient();

			//URL para hacer el post
			string urlPost = "http://iserver.itris.com.ar:7101/DACServicesTest/api/Relevamiento";

			//String content que serealiza la clase a string
			StringContent stringContent =
				new StringContent(JsonConvert.SerializeObject(relevamientos), Encoding.UTF8, "application/json");

			//variable que se utiliza para tomar la respuesta
			HttpResponseMessage httpResponseMessage;

			//Se ejecuta el post y se lo asigna a la variable que contiene la respuesta
			httpResponseMessage = await myHttpClient.PostAsync(new Uri(urlPost), stringContent);

			//Obtengo el mensaje de respuesta del server
			var stringResponse = httpResponseMessage.Content.ReadAsStringAsync().Result;

			//Serializo la repsuesta que viene en formato json al tipo de clase
			//ACA TENES QUE TENER LA RESPUESTA DEL SERVICIO DACServiceTest
			ItrisPlanillaEntity respuesta = JsonConvert.DeserializeObject<ItrisPlanillaEntity>(stringResponse);

			//Dato a guardar en tabla tbRequest
			string requestBody = JsonConvert.SerializeObject(respuesta);

			//-------------- CODIGO LEO POST -------------
			//httpClient = new HttpClient();
			//StringContent stringContent =
			//	new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");

			//httpResponseMessage = await httpClient.PostAsync(new Uri(urlRequest), stringContent);

			//var stringResponse = httpResponseMessage.Content.ReadAsStringAsync().Result;
			//response = JsonConvert.DeserializeObject<ItrisPlanillaEntity>(stringResponse);
			//-------------- CODIGO LEO POST -------------
		}

		private async void btnSiguienteClicked(object sender, EventArgs e)
        {
            if (ValidarDatos())
            {

                _COMERCIO nuevoLocal = new _COMERCIO()
                {
                    FK_ERP_PROVINCIAS = 1,
                    FK_TIP_COM = TipoSeleccionado.ID,
                    NOMBRE = entryNombreLocal.Text,
                    CALLE = entryCalleLocal.Text,
                    NUMERO = entryNumeroLocal.Text,
                    FK_ERP_LOCALIDADES = LocalidadSearch.Text,
                };
                await Navigation.PushAsync(new Tabbed(nuevoLocal));
            }
        }

        private void PickerTipoLocal_SelectedIndexChanged(object sender, EventArgs e)
        {
            var picker = sender as Picker;

            var selectedTipo = picker.ItemsSource[picker.SelectedIndex] as _TIP_COM;
            TipoSeleccionado = selectedTipo;
        }

        public void LocalidadSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(LocalidadSearch.Text))
            {
                List<ERP_LOCALIDADES> temp = new List<ERP_LOCALIDADES>();

                temp = ListaLocalidades.Where(c => c.DESCRIPCION.ToString().ToLower().Contains(LocalidadSearch.Text)).ToList();
                if (temp.Count != 0)
                {
                    LocalidadList.IsVisible = true;
                    LocalidadList.ItemsSource = temp;
                }
                else LocalidadList.IsVisible = false;
            }
            else LocalidadList.IsVisible = false;
        }

        public void LocalidadList_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            LocalidadList.IsVisible = false;
            LocalidadSeleccionada = e.Item as ERP_LOCALIDADES;
            PickerProvincia.SelectedItem = LocalidadSeleccionada.Z_FK_ERP_PROVINCIAS;
            ComercioSeleccionado.FK_ERP_PROVINCIAS = LocalidadSeleccionada.FK_ERP_PROVINCIAS;
            ComercioSeleccionado.FK_ERP_LOCALIDADES = LocalidadSeleccionada.DESCRIPCION;
            LocalidadSearch.Text = LocalidadSeleccionada.DESCRIPCION;

        }

        private void PickerProvincia_SelectedIndexChanged(object sender, EventArgs e)
        {
            var picker = sender as Picker;
            var provincia = picker.SelectedItem.ToString();
            using (SQLite.SQLiteConnection conexion = new SQLiteConnection(App.RutaBD))
            {
                ListaLocalidades = conexion.Query<ERP_LOCALIDADES>("select * from ERP_LOCALIDADES where Z_FK_ERP_PROVINCIAS = ?", provincia).ToList();

            }
        }
    }
}