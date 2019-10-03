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

namespace Relevamiento.Vistas
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Login : ContentPage
	{
		public Login()
		{
			InitializeComponent();
			aiLogin.IsVisible = true;
			aiLogin.IsRunning = true;
			aiLogin.IsEnabled = true;
			lblMensaje.Text = "Sincronizando información... \nEste proceso puede demorar algunos minutos. \nAsegúrese de tener una buena conexión a internet.";

			//OK		
			Task.Run(async () => await CargaDeDatosInicial()).GetAwaiter().GetResult();
			//Task.Run(async() => await ValidarEquipo());

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
				Label lblAsesoresCreate = new Label();
				Label lblAsesoresUpdate = new Label();
				Label lblAsesoresDelete = new Label();
				Label lblEmpresasCreate = new Label();
				Label lblEmpresasUpdate = new Label();
				Label lblEmpresasDelete = new Label();

				var asesoresService = new ErpAsesoresService(lblAsesoresCreate, lblAsesoresUpdate, lblAsesoresDelete);
				Task.Run(async () => await  asesoresService.SynchronizeAsesores()).GetAwaiter().GetResult();
				//var empresasService = new ErpEmpresasService(lblEmpresasCreate, lblEmpresasUpdate, lblEmpresasDelete);
				//Task.Run(async () => await empresasService.SynchronizeEmpresas()).GetAwaiter().GetResult();

				//valido equipo
				Task.Run(async() => await ValidarEquipo()).GetAwaiter().GetResult();
				//ValidarEquipo();
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		//public void ValidarEquipo()
		public async Task ValidarEquipo()
		{
			try
			{
				//Get Imei
				//string imeiTelefono = DependencyService.Get<IServiceImei>().GetImei();
				string imeiTelefono = await GetImei();
				//DevieId.Text = "IMEI = " + imeiTelefono;

				List<ERP_ASESORES> listaAsesores = new List<ERP_ASESORES>();
				List<ERP_EMPRESAS> listaEmpresas = new List<ERP_EMPRESAS>();
				ERP_ASESORES asesor = new ERP_ASESORES();
				//validacion con asesores
				using (SQLite.SQLiteConnection conexion = new SQLiteConnection(App.RutaBD))
				{
					listaAsesores = conexion.Table<ERP_ASESORES>().ToList();
					listaEmpresas = conexion.Table<ERP_EMPRESAS>().ToList();
					asesor = conexion.Table<ERP_ASESORES>().Where(a => a.c_IMEI == imeiTelefono).FirstOrDefault();
				}

				if (asesor != null)
				{
					aiLogin.IsVisible = false;
					aiLogin.IsRunning = false;
					aiLogin.IsEnabled = false;

					Usuario User = new Usuario();
					User.NombreUsuario = asesor.DESCRIPCION;
					User.NumeroImei = asesor.c_IMEI;
					CheckNetworkState.isLoged = true;
					await Navigation.PushAsync(new Principal(User));
				}
				else
				{
					aiLogin.IsVisible = false;
					aiLogin.IsRunning = false;
					aiLogin.IsEnabled = false;
					lblMensaje.TextColor = Color.Red;
					lblMensaje.Text = "Este equipo no se encuentra habilitado para utilizar la aplicacion. \n Por favor contacte un administrador.";
				}
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		async Task<string> GetImei()
		{
			//Verify Permission
			var status = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Phone);
			if (status != PermissionStatus.Granted)
			{
				var results = await CrossPermissions.Current.RequestPermissionsAsync(Permission.Phone);
				//Best practice to always check that the key exists
				if (results.ContainsKey(Permission.Phone))
					status = results[Permission.Phone];
			}
			status = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Location);
			if (status != PermissionStatus.Granted)
			{
				var results = await CrossPermissions.Current.RequestPermissionsAsync(Permission.Location);
				//Best practice to always check that the key exists
				if (results.ContainsKey(Permission.Location))
					status = results[Permission.Location];
			}
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

		//async void Button_Clicked(object sender, EventArgs e)
		//{
		//    Usuario User = new Usuario();
		//    if (string.IsNullOrEmpty(entryName.Text))
		//    {
		//        lblusufail.Text = "Debe ingresar un usuario";
		//        lblusufail.IsVisible = true;
		//    }
		//    if (string.IsNullOrEmpty(entryName.Text))
		//    {
		//        lblpwfail.Text = "Debe ingresar una contraseña";
		//        lblusufail.IsVisible = true;
		//    }
		//    if (entryName.Text == "Admin" && entryPass.Text == "a" )
		//    {
		//        //await Navigation.PushAsync(new Busqueda());
		//        User.NombreUsuario = entryName.Text;
		//        User.NumeroImei = await GetImei();
		//        CheckNetworkState.isLoged = true;
		//        await Navigation.PushAsync(new Principal(User));
		//    }
		//}

		//private void EntryName_TextChanged(object sender, TextChangedEventArgs e)
		//{
		//    lblusufail.IsVisible = false;
		//}

		//private void EntryPass_TextChanged(object sender, TextChangedEventArgs e)
		//{
		//    lblpwfail.IsVisible = false;
		//}
	}
}