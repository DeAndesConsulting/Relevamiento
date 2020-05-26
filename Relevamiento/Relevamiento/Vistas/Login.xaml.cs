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
        private SynchronizeDataConfig _synchronizeDataConfig = new SynchronizeDataConfig();
        private bool isSynchronizing = false;

        public Login()
		{
            InitializeComponent();

			

            using (SQLite.SQLiteConnection conexion = new SQLite.SQLiteConnection(App.RutaBD))
            {
                _synchronizeDataConfig = conexion.Table<SynchronizeDataConfig>().FirstOrDefault();

                /*if (_synchronizeDataConfig.lastSynchronized.Day != DateTime.Today.Day)
                {
                    _synchronizeDataConfig.isSynchronized = false;
                }*/
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
            //if (!_isAlreadySynchronized)
            //{

            if (!isSynchronizing)
            {
                isSynchronizing = true;

                aiLogin.IsVisible = true;
                aiLogin.IsRunning = true;
                aiLogin.IsEnabled = true;
                lblMensaje.Text = "Sincronizando información... \nEste proceso puede demorar algunos minutos. \nAsegúrese de tener una buena conexión a internet.";

                Task.Run(async () => await CargaDeDatosInicial());
            }
            
            //_isAlreadySynchronized = true;
            //}
        }

        private async Task AskForPermissions()
        {
			Permission[] permisos = new Permission[] { Permission.Phone, Permission.Location };
            await CrossPermissions.Current.RequestPermissionsAsync(permisos);
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

        private async Task Synchronize()
		{
			try
			{
                if (CheckNetworkState.hasConnectivity)
                {
                    if (!_synchronizeDataConfig.isFirstTimeSynchronizedReady)
                    {
                        if (!_synchronizeDataConfig.isArticulosReady)
                        {
                            //TABLA ARTICULOS
                            var articulosService = new ArticulosService();
                            Task.Run(async () => await articulosService.SynchronizeArticulos()).GetAwaiter().GetResult();

                            //para debug Articulos
                            var countArticulos = 0;
                            using (SQLite.SQLiteConnection conexion = new SQLiteConnection(App.RutaBD))
                            {
                                //73
                                countArticulos = conexion.Table<_ARTICULOS>().Count();
                            }

                            using (SQLite.SQLiteConnection conexion = new SQLiteConnection(App.RutaBD))
                            {
                                _synchronizeDataConfig.isArticulosReady = true;
                                conexion.Update(_synchronizeDataConfig);
                            }
                        }

                        if (!_synchronizeDataConfig.isAsesoresReady)
                        {
                            //TABLA ASESORES
                            var asesoresService = new ErpAsesoresService();
                            Task.Run(async () => await asesoresService.SynchronizeAsesores()).GetAwaiter().GetResult();

                            //para debug Asesores
                            var countAsesores = 0;
                            using (SQLite.SQLiteConnection conexion = new SQLiteConnection(App.RutaBD))
                            {
                                //116
                                countAsesores = conexion.Table<ERP_ASESORES>().Count();
                            }

                            using (SQLite.SQLiteConnection conexion = new SQLiteConnection(App.RutaBD))
                            {
                                _synchronizeDataConfig.isAsesoresReady = true;
                                conexion.Update(_synchronizeDataConfig);
                            }
                        }


                        if (!_synchronizeDataConfig.isEmpresasReady)
                        {
                            //TABLA EMPRESAS
                            var empresasService = new ErpEmpresasService();
                            Task.Run(async () => await empresasService.SynchronizeEmpresas()).GetAwaiter().GetResult();

                            //para debug Empresas
                            var countEmpresas = 0;
                            using (SQLite.SQLiteConnection conexion = new SQLiteConnection(App.RutaBD))
                            {
                                //3133
                                countEmpresas = conexion.Table<ERP_EMPRESAS>().Count();
                            }

                            using (SQLite.SQLiteConnection conexion = new SQLiteConnection(App.RutaBD))
                            {
                                _synchronizeDataConfig.isEmpresasReady = true;
                                conexion.Update(_synchronizeDataConfig);
                            }
                        }

						//if (!_synchronizeDataConfig.isLocalidadesReady)
						//{
						//    var countLocalidades = 0;
						//    using (SQLite.SQLiteConnection conexion = new SQLiteConnection(App.RutaBD))
						//    {
						//        //21683
						//        countLocalidades = conexion.Table<ERP_LOCALIDADES>().Count();
						//    }

						//    if (countLocalidades < 21683)
						//    {
						//        var localidadesService = new ErpLocalidadesService();
						//        Task.Run(async () => await localidadesService.SynchronizeLocalidades()).GetAwaiter().GetResult();
						//    }

						//    using (SQLite.SQLiteConnection conexion = new SQLiteConnection(App.RutaBD))
						//    {
						//        _synchronizeDataConfig.isLocalidadesReady = true;
						//        conexion.Update(_synchronizeDataConfig);
						//    }
						//}

						//if (_synchronizeDataConfig.isArticulosReady &&
						//	_synchronizeDataConfig.isAsesoresReady &&
						//	_synchronizeDataConfig.isEmpresasReady &&
						//	_synchronizeDataConfig.isLocalidadesReady)

							if (_synchronizeDataConfig.isArticulosReady &&
								_synchronizeDataConfig.isAsesoresReady &&
								_synchronizeDataConfig.isEmpresasReady)
							{
								using (SQLite.SQLiteConnection conexion = new SQLiteConnection(App.RutaBD))
                            {
                                //_synchronizeDataConfig.isSynchronized = true;
                                //_synchronizeDataConfig.lastSynchronized = DateTime.Today;
                                _synchronizeDataConfig.isFirstTimeSynchronizedReady = true;
                                conexion.Update(_synchronizeDataConfig);
                            }
                        }
                    }
                    //valido equipo con IMEI. Por politicas no se usa por el momento, gestionarlas
                    //Task.Run(async () => await ValidarEquipo()).GetAwaiter().GetResult();
                    //Se valida por nombre de usuario y sin pass por el tema de arriba, descomentar y se usa la anterior
                    ValidarEquipo(true);
                }
                else
                {
                    if(_synchronizeDataConfig.isFirstTimeSynchronizedReady)
                    {
                        ValidarEquipo(true);
                    }
                    else
                    {
                        aiLogin.IsVisible = false;
                        aiLogin.IsRunning = false;
                        aiLogin.IsEnabled = false;

                        lblMensaje.Text = "";

                        await DisplayAlert("Aviso", "No se ha podido sincronizar por 1era vez. Sin conexión a internet.\nCierre la aplicación y vuelva a abrirla cuando tenga acceso a internet.", "Ok");

                        isSynchronizing = false;
                    }
                }
			}
			catch (Exception ex)
			{
                Device.BeginInvokeOnMainThread(async () => {

                    await DisplayAlert("Aviso", "Se ha perdido la conexión a datos. Asegurese de contar con buena señal para realizar el proceso de sincronización.\nCierre la aplicación y vuelva a intentar cuando tenga acceso a internet.", "Ok");

                    aiLogin.IsVisible = false;
                    aiLogin.IsRunning = false;
                    aiLogin.IsEnabled = false;

                    lblMensaje.Text = "";

                    isSynchronizing = false;
                });             
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
                int countAsesores = 0;
                using (SQLite.SQLiteConnection conexion = new SQLiteConnection(App.RutaBD))
				{
                    countAsesores = conexion.Table<ERP_ASESORES>().Count();

                    App.globalAsesor = conexion.Table<ERP_ASESORES>().Where(a => a.DESCRIPCION == entryName.Text.ToUpper()).FirstOrDefault();
				}

                if (countAsesores == 0)
                {
                    //lblMensaje.Text = "No se ha podido inicializar la aplicacion por falta de conexion a internet.";
                    await DisplayAlert("Aviso", "No se ha podido sincronizar por 1era vez. Sin conexión a internet.", "Ok");
                }

                if (App.globalAsesor != null)
				{
					if (!string.IsNullOrEmpty(App.globalAsesor.c_IMEI))
					{
						if (!_synchronizeDataConfig.isFirstTimeLoggedReady)
						{
							using (SQLite.SQLiteConnection conexion = new SQLiteConnection(App.RutaBD))
							{
								_synchronizeDataConfig.c_IMEI = App.globalAsesor.c_IMEI;
								_synchronizeDataConfig.isFirstTimeLoggedReady = true;
								conexion.Update(_synchronizeDataConfig);
							}
						}

						if (_synchronizeDataConfig.c_IMEI == App.globalAsesor.c_IMEI)
						{
							User.NombreUsuario = App.globalAsesor.DESCRIPCION;
							CheckNetworkState.isLoged = true;
							await Navigation.PushAsync(new Principal(User));
						}
						else
						{
							await DisplayAlert("Aviso", "El usuario no puede usar la aplicación.", "Ok");
							lblpwfail.Text = "Usuario incorrecto.";
							lblusufail.IsVisible = true;
						}
					}
					else
					{
						await DisplayAlert("Aviso", "El usuario no puede usar la aplicación.", "Ok");
						lblpwfail.Text = "Usuario incorrecto.";
						lblusufail.IsVisible = true;
					}
				}
				else
				{
					await DisplayAlert("Aviso", "Usuario incorrecto.", "Ok");
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