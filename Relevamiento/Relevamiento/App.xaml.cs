using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using Relevamiento.Clases;
using Relevamiento.Vistas;
using SQLite;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Relevamiento.Models;
using Relevamiento.Services.Middleware;
using System.Diagnostics;

namespace Relevamiento
{
	public partial class App : Application
	{
		public static ItrisRelevamientoEntity releva = new ItrisRelevamientoEntity();
		public static List<ItrisComercioArticulo> comercios = new List<ItrisComercioArticulo>();
		public static ERP_EMPRESAS distribuidorseleccionado;
		public static ERP_ASESORES globalAsesor = new ERP_ASESORES();
		public static string RutaBD;
		public App(string rutaBD)
		{
			InitializeComponent();

			RutaBD = rutaBD;
			VersionTracking.Track();
			bool firsttime = VersionTracking.IsFirstLaunchForCurrentVersion;

			using (SQLite.SQLiteConnection conexion = new SQLite.SQLiteConnection(RutaBD))
			{
                conexion.DropTable<Provincia>();
                conexion.CreateTable<Provincia>();

                conexion.DropTable<TipoLocal>();
                conexion.CreateTable<TipoLocal>();

                conexion.DropTable<_COMERCIO>();
                conexion.CreateTable<_COMERCIO>();

                conexion.DropTable<_TIP_ART>();
                conexion.CreateTable<_TIP_ART>();

                conexion.DropTable<_ARTICULOS>();
                conexion.CreateTable<_ARTICULOS>();

                conexion.DropTable<ERP_EMPRESAS>();
                conexion.CreateTable<ERP_EMPRESAS>();

                conexion.DropTable<ERP_ASESORES>();
                conexion.CreateTable<ERP_ASESORES>();

				conexion.DropTable<ERP_LOCALIDADES>();
				conexion.CreateTable<ERP_LOCALIDADES>();

                conexion.DropTable<Relevado>();
                conexion.CreateTable<Relevado>();

				conexion.DropTable<TbRequest>();
				conexion.CreateTable<TbRequest>();

				conexion.DropTable<SynchronizeDataConfig>();
				conexion.CreateTable<SynchronizeDataConfig>();

				//first time
				if (conexion.Table<SynchronizeDataConfig>().Count() == 0)
				{
					var synchronizeDataConfig = new SynchronizeDataConfig()
					{
						ID = 1,
						isSynchronized = false,
						lastSynchronized = DateTime.Today,
						isFirstTimeSynchronizedReady = false,
						isFirstTimeLoggedReady = false,
						c_IMEI = string.Empty,
						isArticulosReady = false,
						isAsesoresReady = false,
						isEmpresasReady = false,
						isLocalidadesReady = false
					};

					conexion.Insert(synchronizeDataConfig);
				}
			}

			MainPage = new NavigationPage(new LoadLocalidades())
			{
				BarBackgroundColor = Color.FromHex("#F5DE8E"),
				BarTextColor = Color.Gray
			};
		}

		protected override void OnStart()
		{
			// Handle when your app starts
			CheckNetworkState.StartListening();
		}

		protected override void OnSleep()
		{
			// Handle when your app sleeps
			CheckNetworkState.StopListening();
		}

		protected override void OnResume()
		{
			// Handle when your app resumes
			CheckNetworkState.StartListening();
		}
	}
}
