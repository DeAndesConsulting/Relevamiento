/*
 * Falta ver porque no se puede setear en el picker de tipo de local
 * 
 */

using Newtonsoft.Json;
using Relevamiento.Clases;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Relevamiento.Models;
using Relevamiento.Services.Data;

namespace Relevamiento.Vistas
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Comercio : ContentPage
	{
		//public Distribuidora distribuidorseleccionado;
		public _COMERCIO ComercioSeleccionado = new _COMERCIO();
		public _TIP_COM TipoSeleccionado;
		public List<ERP_LOCALIDADES> ListaLocalidades = new List<ERP_LOCALIDADES>();

		public Comercio(ERP_EMPRESAS Distribuidor)
		{
			InitializeComponent();
			List<_TIP_COM> lista_locales = new List<_TIP_COM>();
			if (!string.IsNullOrEmpty(BusquedaDistribuidor.LocalidadSeleccionada.DESCRIPCION))
			{
				LocalidadSearch.Text = BusquedaDistribuidor.LocalidadSeleccionada.DESCRIPCION;
			}
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
			if (await DisplayAlert("Aviso", "Ud. va a cancelar todos los relevamientos de la localidad, ¿desea continuar?", "Si", "No"))
			{
				PopUntilDestination(typeof(Principal));
			}
		}

		private async void btnFinalizarClicked(object sender, EventArgs e)
		{
            try
            {
                bool respuesta = await DisplayAlert("ATENCION", "¿Desea finalizar el Relevamiento de este Pueblo?", "Si", "No");
                if (respuesta)
                {
                    //Primero se envia a la pantalla principal para que se sigan ejecutando los threads y evitar mandar 2 relevamientos
                    PopUntilDestination(typeof(Principal));
                    //Comento acceso al imei politicas android
                    //string imeiTelefono = DependencyService.Get<IServiceImei>().GetImei();

                    ERP_ASESORES asesor = new ERP_ASESORES();
                    int maxRequestId = 1;
                    using (SQLite.SQLiteConnection conexion = new SQLiteConnection(App.RutaBD))
                    {
                        //TbRequest maxRequest = new TbRequest();

                        //Se obtiene asesor mediante el imei del equipo. Por politicas de privacidad se obtiene mediante descripcion asesor.
                        //asesor = conexion.Table<ERP_ASESORES>().Where(a => a.c_IMEI == imeiTelefono).FirstOrDefault();
                        //Nuevo metodo de obtener asesore por descripcion (google play)				
                        //asesor = conexion.Table<ERP_ASESORES>().Where(a => a.c_IMEI == imeiTelefono).FirstOrDefault();
                        TbRequest maxRequest = conexion.Table<TbRequest>().OrderByDescending(r => r.ID).FirstOrDefault();
                        if (maxRequest != null)
                            maxRequestId = maxRequest.ID + 1;
                    }

                    App.releva.FK_ERP_EMPRESAS = App.distribuidorseleccionado.ID.ToString();

                    //Asesor previo politica			
                    //App.releva.FK_ERP_ASESORES = asesor.ID;
                    App.releva.FK_ERP_ASESORES = App.globalAsesor.ID;

                    App.releva.FECHA = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
                    //string codigoRequest = string.Format("{0}-{1}", App.globalAsesor.ID.ToString(), maxRequestId.ToString());
                    string codigoRequest = string.Format("{0}-{1}", App.globalAsesor.ID.ToString(), DateTime.Now.ToString("yyyyMMddHHmmss"));

                    //Obtengo el imei del equipo para el request

                    App.releva.CODIGO = "ASD123ADSASD";
                    ItrisPlanillaEntity relevamientos = new ItrisPlanillaEntity();
                    relevamientos.relevamiento = App.releva;
                    relevamientos.comercios = App.comercios;
                    relevamientos.codigoRequest = codigoRequest;// "123456789-9"; //<-- Usar este codigo para test (no va a itris)

                    string jsonRelevamiento = JsonConvert.SerializeObject(relevamientos);

                    var tbRequestDataService = new TbRequestDataService();

                    if (!tbRequestDataService.isInserted(relevamientos.codigoRequest))
                    {
                        TbRequest tbRequests = new TbRequest()
                        {
                            req_codigo = relevamientos.codigoRequest,
                            req_json = jsonRelevamiento,
                            req_estado = false
                        };

                        //Se updatea el estado del registro de la planilla enviada
                        try
                        {
                            tbRequestDataService.Insert(tbRequests);
                            App.comercios = new List<ItrisComercioArticulo>();
                        }
                        catch
                        {
                        }

                        //Se comenta codigo porque son mensajes debug
                        //if (tbRequestDataService.Insert(tbRequests))
                        //	await DisplayAlert("Aviso", "Se ha dado de alta un nuevo relevamiento", "Ok");
                        //else
                        //	await DisplayAlert("Aviso", "NO se ha podido dar de alta el relevamiento", "Ok");

                        if (CheckNetworkState.hasConnectivity)
                            await SendPostRelevamiento(jsonRelevamiento, tbRequests);
                        else
                            await DisplayAlert("Aviso", "Sin conexion a internet, no se podra enviar el relevamiento hasta que vuelva a tener conexion", "Ok");
                    }
                    else
                    {
                        await DisplayAlert("Aviso", "Ese relevamiento ya se encuentra dado de alta", "Ok");
                    }
                    //Se comenta esta linea porque permitia enviar dos formularios, se agrego arriba (si pasa test borrar).
                    //PopUntilDestination(typeof(Principal));
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
		}

		private async Task SendPostRelevamiento(string jsonRelevamiento, TbRequest tbRequestToUpdate)
		{
			try
			{
				//String content que serealiza la clase a string
				StringContent stringContent =
						new StringContent(jsonRelevamiento, Encoding.UTF8, "application/json");

				HttpClient httpClient = new HttpClient();
				httpClient.Timeout = TimeSpan.FromMinutes(30);

				//TEST
				//string urlPost = "http://iserver.itris.com.ar:7101/DACServicesTest/api/Relevamiento";

                //PROD
                string urlPost = "http://iserver.itris.com.ar:7101/DACServices/api/Relevamiento";

                //variable que se utiliza para tomar la respuesta
                HttpResponseMessage httpResponseMessage;

				//Se ejecuta el post y se lo asigna a la variable que contiene la respuesta
				httpResponseMessage = await httpClient.PostAsync(new Uri(urlPost), stringContent);

				if (httpResponseMessage.StatusCode == System.Net.HttpStatusCode.Created)
				{
					//Se comenta porque no es necesario informarle al usuario que se envio el relevamiento ok
					//Va a poder chequearlo desde el menu estados
					//await DisplayAlert("Aviso", "Se ha enviado el relevamiento", "Ok");

					//Obtengo el mensaje de respuesta del server
					var stringResponse = httpResponseMessage.Content.ReadAsStringAsync().Result;

					//Serializo la repsuesta que viene en formato json al tipo de clase
					//ACA TENES QUE TENER LA RESPUESTA DEL SERVICIO
					ItrisPlanillaEntity respuesta = JsonConvert.DeserializeObject<ItrisPlanillaEntity>(stringResponse);

					//Dato a guardar en tabla tbRequest
					string requestBody = JsonConvert.SerializeObject(respuesta);

					var tbRequestDataService = new TbRequestDataService();

					tbRequestToUpdate.req_json = requestBody;
					tbRequestToUpdate.req_estado = true;

					//Se updatea el estado del registro de la planilla enviada
					tbRequestDataService.Update(tbRequestToUpdate);

					//Se comenta codigo porque son mensajes debug
					//if (tbRequestDataService.Update(tbRequestToUpdate))
					//    await DisplayAlert("Aviso", "Se ha actualizado el relevamiento relevamiento", "Ok");
					//else
					//    await DisplayAlert("Aviso", "NO se ha podido actualizar el relevamiento relevamiento", "Ok");
				}
				else
					await DisplayAlert("Aviso", "NO se ha podido enviar el relevamiento", "Ok");
			}
			catch (Exception ex)
			{
				throw ex;
			}
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
					HORA_VISITA = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss")
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
			BusquedaDistribuidor.LocalidadSeleccionada = e.Item as ERP_LOCALIDADES;
			PickerProvincia.SelectedItem = BusquedaDistribuidor.LocalidadSeleccionada.Z_FK_ERP_PROVINCIAS;
			ComercioSeleccionado.FK_ERP_PROVINCIAS = BusquedaDistribuidor.LocalidadSeleccionada.FK_ERP_PROVINCIAS;
			ComercioSeleccionado.FK_ERP_LOCALIDADES = BusquedaDistribuidor.LocalidadSeleccionada.DESCRIPCION;
			LocalidadSearch.Text = BusquedaDistribuidor.LocalidadSeleccionada.DESCRIPCION;

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

		void PopUntilDestination(Type DestinationPage)
		{
			int LeastFoundIndex = 0;
			int PagesToRemove = 0;

			for (int index = Navigation.NavigationStack.Count - 2; index > 0; index--)
			{
				if (Navigation.NavigationStack[index].GetType().Equals(DestinationPage))
				{
					break;
				}
				else
				{
					LeastFoundIndex = index;
					PagesToRemove++;
				}
			}

			for (int index = 0; index < PagesToRemove; index++)
			{
				Navigation.RemovePage(Navigation.NavigationStack[LeastFoundIndex]);
			}

			Navigation.PopAsync();
		}

		#region Metodos para generar codigo del POST para el relevamiento

		/// <summary>
		/// Metodo de obtencion de IMEI
		/// </summary>
		/// <returns></returns>
		private async Task<string> GetImei()
		{
			//Verifico permisos en el equipo
			var status = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Phone);
			if (status != PermissionStatus.Granted)
			{
				var results = await CrossPermissions.Current.RequestPermissionsAsync(Permission.Phone);
				//como buena practica siempre es bueno chequear tener los permisos
				if (results.ContainsKey(Permission.Phone))
					status = results[Permission.Phone];
			}

			return DependencyService.Get<IServiceImei>().GetImei();
		}

		private string GetMaxIdTbRequest()
		{
			int maxId = 1;
			return maxId.ToString();
		}

		#endregion

	}
}