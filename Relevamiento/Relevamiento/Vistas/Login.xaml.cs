using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Relevamiento.Clases;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using System.Threading.Tasks;
using Xamarin.Essentials;
using System.ComponentModel;
using SQLite;
using System.Collections.Generic;
using Relevamiento.Services.Middleware;
using System.Linq;
using Relevamiento.Models;

namespace Relevamiento.Vistas
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Login : ContentPage
	{
        private bool _isAlreadySynchronized = false;
		private bool _mostrarControles = false;
        private GenericDataConfig genericDataConfig = new GenericDataConfig();

        public Login()
		{
            InitializeComponent();

			aiLogin.IsVisible = true;
			aiLogin.IsRunning = true;
			aiLogin.IsEnabled = true;
			lblMensaje.Text = "Sincronizando información... \nEste proceso puede demorar algunos minutos. \nAsegúrese de tener una buena conexión a internet.";


            using (SQLite.SQLiteConnection conexion = new SQLite.SQLiteConnection(App.RutaBD))
            {
                genericDataConfig = conexion.Table<GenericDataConfig>().FirstOrDefault();

                if (genericDataConfig.lastSynchronized.Day != DateTime.Today.Day)
                {
                    genericDataConfig.isSynchronized = false;
                }
            }

            
            //OK		
            //Task.Run(async () => await CargaDeDatosInicial()).GetAwaiter().GetResult();

            //Task.Run(async () => await AskForPermissions());

            //TEST
            //Task.Run(async() => await CargaDeDatosInicial());

            //test formulario principal
            //ERP_ASESORES asesor = new ERP_ASESORES();
            //using (SQLite.SQLiteConnection conexion = new SQLiteConnection(App.RutaBD))
            //{
            //	var listaAsesores = conexion.Table<ERP_ASESORES>().ToList();
            //	//listaEmpresas = conexion.Table<ERP_EMPRESAS>().ToList();
            //	asesor = conexion.Table<ERP_ASESORES>().Where(a => a.c_IMEI == "358240051111110").FirstOrDefault();
            //}

            //Usuario User = new Usuario();
            //User.NombreUsuario = asesor.DESCRIPCION;
            //User.NumeroImei = asesor.c_IMEI;
            //CheckNetworkState.isLoged = true;
            //Navigation.PushAsync(new Principal(User));
            //test formulario principal
        }

        protected async override void OnAppearing()
        {
            //base.OnAppearing();
            //PermissionStatus status = await CrossPermissions.Current.RequestPermissionAsync<CalendarPermission>();
            //await CrossPermissions.Current.RequestPermissionsAsync(Permission.Phone);
            //await CrossPermissions.Current.RequestPermissionsAsync(Permission.Location);
            //ActivityCompat.RequestPermissions(this, new String[] { Manifest.Permission.Camera }, REQUEST_LOCATION);
            
            await AskForPermissions();
            if (!_isAlreadySynchronized)
            {
                Task.Run(async () => await CargaDeDatosInicial());
                _isAlreadySynchronized = true;
            }

           
            
        }

        private async Task AskForPermissions()
        {
            await CrossPermissions.Current.RequestPermissionsAsync(Permission.Phone);
            await CrossPermissions.Current.RequestPermissionsAsync(Permission.Location);
        }

        private async Task CargaDeDatosInicial()
		{
            try
			{
                await Synchronize();
				//Task.Run(async() => await Synchronize()).GetAwaiter();
				//await ValidarEquipo();
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

        private bool isMonday()
        {
            //Se trato de hacerlo usando FindSystemTimeZoneById pero en xamarin no funciona
            //DateTime timeUtc = DateTime.UtcNow;
            //TimeZoneInfo argZone = TimeZoneInfo.FindSystemTimeZoneById("Argentina Standard Time");
            //DateTime argDateTime = TimeZoneInfo.ConvertTimeFromUtc(timeUtc, fooZone);

            DateTime now = DateTime.Now;
            DateTime gmt = now.ToUniversalTime();
            DateTime local = gmt.ToLocalTime();
            DateTime argDateTime = local.AddHours(-3);

            return argDateTime.DayOfWeek == DayOfWeek.Monday;
        }

		private async Task Synchronize()
		{
			try
			{
                //if (isMonday() && CheckNetworkState.hasConnectivity && !genericDataConfig.isSynchronized)
                if(true)
                {
                    var articulosService = new ArticulosService();
                    Task.Run(async () => await articulosService.SynchronizeArticulos()).GetAwaiter().GetResult();
                    /*var asesoresService = new ErpAsesoresService();
                    Task.Run(async () => await asesoresService.SynchronizeAsesores()).GetAwaiter().GetResult();
					var empresasService = new ErpEmpresasService();
					Task.Run(async () => await empresasService.SynchronizeEmpresas()).GetAwaiter().GetResult();

					var countLocalidades = 0;
                    using (SQLite.SQLiteConnection conexion = new SQLiteConnection(App.RutaBD))
                    {
                        countLocalidades = conexion.Table<ERP_LOCALIDADES>().Count();
                    }

					if (countLocalidades < 21683)
					{
						var localidadesService = new ErpLocalidadesService();
						Task.Run(async () => await localidadesService.SynchronizeLocalidades()).GetAwaiter().GetResult();
					}

                    using (SQLite.SQLiteConnection conexion = new SQLiteConnection(App.RutaBD))
                    {
                        genericDataConfig.isSynchronized = true;
                        genericDataConfig.lastSynchronized = DateTime.Today;
                        conexion.Update(genericDataConfig);
                    }*/
                }
                
                //valido equipo con IMEI. Por politicas no se usa por el momento, gestionarlas
                //Task.Run(async () => await ValidarEquipo()).GetAwaiter().GetResult();
				//Se valida por nombre de usuario y sin pass por el tema de arriba, descomentar y se usa la anterior
                ValidarEquipo(true);
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		//public void ValidarEquipo()
		public async Task ValidarEquipo()
		{
            //await AskForPermissions();

            try
			{
				//Get Imei
				//string imeiTelefono = DependencyService.Get<IServiceImei>().GetImei();
				string imeiTelefono = Task.Run(async() => await GetImei()).GetAwaiter().GetResult();
				//DevieId.Text = "IMEI = " + imeiTelefono;

				List<ERP_ASESORES> listaAsesores = new List<ERP_ASESORES>();
				//List<ERP_EMPRESAS> listaEmpresas = new List<ERP_EMPRESAS>();
				ERP_ASESORES asesor = new ERP_ASESORES();
				//validacion con asesores
				using (SQLite.SQLiteConnection conexion = new SQLiteConnection(App.RutaBD))
				{
					listaAsesores = conexion.Table<ERP_ASESORES>().ToList();
					//listaEmpresas = conexion.Table<ERP_EMPRESAS>().ToList();
					asesor = conexion.Table<ERP_ASESORES>().Where(a => a.c_IMEI == imeiTelefono).FirstOrDefault();
				}

				if (asesor != null)
				{
					Usuario User = new Usuario();
					User.NombreUsuario = asesor.DESCRIPCION;
					User.NumeroImei = asesor.c_IMEI;
					CheckNetworkState.isLoged = true;

					//aiLogin.IsVisible = false;
					//aiLogin.IsRunning = false;
					//aiLogin.IsEnabled = false;

					//await Navigation.PushAsync(new Principal(User));
					Device.BeginInvokeOnMainThread(async () => await Navigation.PushAsync(new Principal(User)));
				}
				else
				{
                    if(listaAsesores.Count() == 0)
                        lblMensaje.Text = "No se ha podido inicializar la aplicacion por falta de conexion a internet.";
                    else
                        lblMensaje.Text = "Este equipo no se encuentra habilitado para utilizar la aplicacion. \n Por favor contacte un administrador.";

					lblMensaje.TextColor = Color.Red;
					aiLogin.IsRunning = false;
					aiLogin.IsEnabled = false;
					aiLogin.IsVisible = false;
				}
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		async Task<string> GetImei()
		{
			////Verify Permission
			var status = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Phone);
			//if (status != PermissionStatus.Granted)
			//{
			//	var results = await CrossPermissions.Current.RequestPermissionsAsync(Permission.Phone);
			//	//Best practice to always check that the key exists
			//	if (results.ContainsKey(Permission.Phone))
			//		status = results[Permission.Phone];
			//}
			//status = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Location);
			//if (status != PermissionStatus.Granted)
			//{
			//	var results = await CrossPermissions.Current.RequestPermissionsAsync(Permission.Location);
			//	//Best practice to always check that the key exists
			//	if (results.ContainsKey(Permission.Location))
			//		status = results[Permission.Location];
			//}

			//Get Imei
			string imei = DependencyService.Get<IServiceImei>().GetImei();
			return imei;
		}

		//void ObtenerImei(object sender, EventArgs e)
		//{
		//	//Get Imei
		//	string imeiTelefono = DependencyService.Get<IServiceImei>().GetImei();
		//	//DevieId.Text = "IMEI = " + imeiTelefono;

		//	List<ERP_ASESORES> listaAsesores = new List<ERP_ASESORES>();
		//	ERP_ASESORES asesor = new ERP_ASESORES();
		//	//validacion con asesores
		//	using (SQLite.SQLiteConnection conexion = new SQLiteConnection(App.RutaBD))
		//	{
		//		listaAsesores = conexion.Table<ERP_ASESORES>().ToList();
		//		asesor = conexion.Table<ERP_ASESORES>().Where(a => a.c_IMEI == imeiTelefono).FirstOrDefault();
		//	}

		//	if (asesor != null)
		//	{
		//		Usuario User = new Usuario();
		//		User.NombreUsuario = asesor.DESCRIPCION;
		//		User.NumeroImei = asesor.c_IMEI;
		//		CheckNetworkState.isLoged = true;
		//		Navigation.PushAsync(new Principal(User));
		//	}
		//	else
		//	{
		//		aiLogin.IsVisible = false;
		//		aiLogin.IsRunning = false;
		//		aiLogin.IsEnabled = false;
		//		lblMensaje.TextColor = Color.Red;
		//		lblMensaje.Text = "Este equipo no se encuentra habilitado para utilizar la aplicacion. \n Por favor contacte un administrador.";
		//	}

		//}

		#region Validacion usuario y password
		public void ValidarEquipo(bool parametro)
		{
			try
			{
				//Device.BeginInvokeOnMainThread(() => { GridAct.IsVisible = false; });
				Device.BeginInvokeOnMainThread(() =>
				{
					//Muestro los controles de login
					sloLogin.IsVisible = true;
					//Oculto controles de sync
					lblMensaje.IsVisible = false;
					aiLogin.IsVisible = false;
				});
			}
			catch (Exception ex)
			{
				throw ex;
			}

		}

		async void Button_Clicked(object sender, EventArgs e)
		{
			Usuario User = new Usuario();
			if (string.IsNullOrEmpty(entryName.Text))
			{
				lblusufail.Text = "Debe ingresar un usuario";
				lblusufail.IsVisible = true;
			}
			//if (string.IsNullOrEmpty(entryName.Text))
			//{
			//	lblpwfail.Text = "Debe ingresar una contraseña";
			//	lblusufail.IsVisible = true;
			//}
			if (!string.IsNullOrEmpty(entryName.Text))
			{
				//validacion con asesores
				//ERP_ASESORES asesor = new ERP_ASESORES();
				using (SQLite.SQLiteConnection conexion = new SQLiteConnection(App.RutaBD))
				{
					App.globalAsesor = conexion.Table<ERP_ASESORES>().Where(a => a.DESCRIPCION == entryName.Text.ToUpper()).FirstOrDefault();
				}

				if (App.globalAsesor != null)
				{
					User.NombreUsuario = App.globalAsesor.DESCRIPCION;
					CheckNetworkState.isLoged = true;
					await Navigation.PushAsync(new Principal(User));
				}
				else
				{
					lblpwfail.Text = "Usuario incorrecto.";
					lblusufail.IsVisible = true;
				}
			}
		}

		private void EntryName_TextChanged(object sender, TextChangedEventArgs e)
		{
			lblusufail.IsVisible = false;
		}

		private void EntryPass_TextChanged(object sender, TextChangedEventArgs e)
		{
			lblpwfail.IsVisible = false;
		}
		#endregion

	}
}