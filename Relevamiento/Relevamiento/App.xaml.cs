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
		//se agrega este objeto por politicas play;
		public static ERP_ASESORES globalAsesor = new ERP_ASESORES();
		//public static List<ItrisComercioEntity> listacom = new List<ItrisComercioEntity>();
		//public static List<ItrisRelevamientoArticuloEntity> relarts = new List<ItrisRelevamientoArticuloEntity>();
		public static string RutaBD;
		public App(string rutaBD)
		{
			InitializeComponent();

			//Linea para obtener permisos
			//Task.Run(async() => await ObtenerPermisos()).GetAwaiter().GetResult();

			RutaBD = rutaBD;
			VersionTracking.Track();
			bool firsttime = VersionTracking.IsFirstLaunchForCurrentVersion;

			List<_ARTICULOS> lista_productos;
			List<ERP_EMPRESAS> ListaDistribuidores;
			List<ERP_LOCALIDADES> ListaLocalidades;
			List<ERP_ASESORES> listaAsesores;

			using (SQLite.SQLiteConnection conexion = new SQLite.SQLiteConnection(RutaBD))
			{
				if (firsttime == true)
				{
					conexion.CreateTable<Provincia>();
					conexion.CreateTable<TipoLocal>();
					conexion.CreateTable<_COMERCIO>();
					conexion.CreateTable<_TIP_ART>();

					//Valido que las tablas maestras no existan porque arrojaba error en equipos que ya la habian
					//instalado. Si las tablas existen y es la primera ejecución de la version, se dropean y crean.
					//Si no existen es debido a que es la primera instalación en un equipo.
					if (!TableExists("_ARTICULOS"))
					{
						conexion.CreateTable<_ARTICULOS>();
						//lista_productos = TraerProductos();
						//conexion.InsertAll(lista_productos);
					}
					else
					{
						conexion.DropTable<_ARTICULOS>();
						conexion.CreateTable<_ARTICULOS>();
					}

					if (!TableExists("ERP_EMPRESAS"))
					{
						conexion.CreateTable<ERP_EMPRESAS>();
						//ListaDistribuidores = TraerEmpresas();
						//conexion.InsertAll(ListaDistribuidores);
					}
					else
					{
						conexion.DropTable<ERP_EMPRESAS>();
						conexion.CreateTable<ERP_EMPRESAS>();
					}

					if (!TableExists("ERP_ASESORES"))
					{
						conexion.CreateTable<ERP_ASESORES>();
						//listaAsesores = TraerAsesores();
						//conexion.InsertAll(listaAsesores);
					}
					else
					{
						conexion.DropTable<ERP_ASESORES>();
						conexion.CreateTable<ERP_ASESORES>();
					}

					if (!TableExists("ERP_LOCALIDADES"))
					{
						conexion.CreateTable<ERP_LOCALIDADES>();

						//INSERTO LOCALIDADES DE LA CLASE LOCALIDADES DATA
						LocalidadesData localidadesData = new LocalidadesData();
						var listaLocalidades = localidadesData.TraerLocalidades();
						conexion.InsertAll(listaLocalidades);

						//ListaLocalidades = TraerLocalidades();
						//conexion.InsertAll(ListaLocalidades);
					}
					else
					{
						conexion.DropTable<ERP_LOCALIDADES>();
						conexion.CreateTable<ERP_LOCALIDADES>();

						//INSERTO LOCALIDADES DE LA CLASE LOCALIDADES DATA
						LocalidadesData localidadesData = new LocalidadesData();
						var listaLocalidades = localidadesData.TraerLocalidades();
						conexion.InsertAll(listaLocalidades);
					}

					Debug.WriteLine($"{"LOCALIDADES: " + conexion.Table<ERP_LOCALIDADES>().Count().ToString()}");

					conexion.CreateTable<Relevado>();
					conexion.CreateTable<TbRequest>();

					//conexion.DropTable<GenericDataConfig>();
					//conexion.DropTable<SynchronizeDataConfig>();
					if (!TableExists("SynchronizeDataConfig"))
					{
						conexion.CreateTable<SynchronizeDataConfig>();
					}
					else
					{
						conexion.DropTable<SynchronizeDataConfig>();
						conexion.CreateTable<SynchronizeDataConfig>();
					}

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

			}



			//DROP DE TABLAS
			/*using (SQLite.SQLiteConnection conexion = new SQLite.SQLiteConnection(RutaBD))
			{
				//conexion.DropTable<_ARTICULOS>();
			}*/

			//SQLiteAsyncConnection conn = new SQLiteAsyncConnection(App.RutaBD);
			//         conn.InsertAllAsync(ListaDistribuidores);
			//         conn.InsertAllAsync(ListaLocalidades);

			//}


			//MainPage = new NavigationPage(new Vistas.Login());
			MainPage = new NavigationPage(new Login())
			{
				BarBackgroundColor = Color.FromHex("#F5DE8E"),
				BarTextColor = Color.Gray
			};
		}

		//public List<ERP_ASESORES> TraerAsesores()
		//{
		//	List<ERP_ASESORES> listaDatos = new List<ERP_ASESORES>();
		//	ERP_ASESORES d1 = new ERP_ASESORES()
		//	{
		//		ID = 1,
		//		DESCRIPCION = "BACHIECCA RUBEN",
		//		_EMAIL = "rbachiecca-manaos@hotmail.com",
		//		_IMEI = "",
		//		_IMEI_ADMIN = false
		//	};
		//	listaDatos.Add(d1);
		//	return listaDatos;
		//}

		public List<ERP_EMPRESAS> TraerEmpresas()
		{
			List<ERP_EMPRESAS> listaDatos = new List<ERP_EMPRESAS>();
			ERP_EMPRESAS d1 = new ERP_EMPRESAS()
			{
				ID = "10",
				NOM_FANTASIA = "JUAN BELAGARDE",
				Z_FK_ERP_LOCALIDADES = "MORENO",
				Z_FK_ERP_PARTIDOS = "MORENO",
				Z_FK_ERP_PROVINCIAS = "Buenos Aires",
				FK_ERP_ASESORES = 4,
				FK_ERP_ASESORES2 = 0,
				FK_ERP_ASESORES3 = 0
			};
			listaDatos.Add(d1);
			return listaDatos;
		}

		public List<_ARTICULOS> TraerProductos()
		{
			List<_ARTICULOS> listaProductos = new List<_ARTICULOS>();
			_ARTICULOS d2 = new _ARTICULOS()
			{
				ID = 1,
				DESCRIPCION = "Manaos 300",
				FK_TIP_ART = 0,
			};
			listaProductos.Add(d2);

			d2 = new _ARTICULOS()
			{
				ID = 1,
				DESCRIPCION = "Manaos 1250",
				FK_TIP_ART = 0,
			};
			listaProductos.Add(d2);

			d2 = new _ARTICULOS()
			{
				ID = 1,
				DESCRIPCION = "Manaos 2250",
				FK_TIP_ART = 0,
			};
			listaProductos.Add(d2);

			d2 = new _ARTICULOS()
			{
				ID = 1,
				DESCRIPCION = "Manaos 600",
				FK_TIP_ART = 0,
			};
			listaProductos.Add(d2);

			d2 = new _ARTICULOS()
			{
				ID = 1,
				DESCRIPCION = "Manaos sin Azucar",
				FK_TIP_ART = 0,
			};
			listaProductos.Add(d2);

			d2 = new _ARTICULOS()
			{
				ID = 1,
				DESCRIPCION = "Cantidad de sabores 2250",
				FK_TIP_ART = 0,
			};
			listaProductos.Add(d2);

			d2 = new _ARTICULOS()
			{
				ID = 1,
				DESCRIPCION = "Beach",
				FK_TIP_ART = 0,
			};
			listaProductos.Add(d2);

			d2 = new _ARTICULOS()
			{
				ID = 1,
				DESCRIPCION = "Cabalgata",
				FK_TIP_ART = 0,
			};
			listaProductos.Add(d2);

			d2 = new _ARTICULOS()
			{
				ID = 1,
				DESCRIPCION = "Caribe",
				FK_TIP_ART = 0,
			};
			listaProductos.Add(d2);

			d2 = new _ARTICULOS()
			{
				ID = 1,
				DESCRIPCION = "Cimes",
				FK_TIP_ART = 0,
			};
			listaProductos.Add(d2);

			d2 = new _ARTICULOS()
			{
				ID = 1,
				DESCRIPCION = "Coca Cola",
				FK_TIP_ART = 0,
			};
			listaProductos.Add(d2);

			d2 = new _ARTICULOS()
			{
				ID = 1,
				DESCRIPCION = "Cordoba",
				FK_TIP_ART = 0,
			};
			listaProductos.Add(d2);

			d2 = new _ARTICULOS()
			{
				ID = 1,
				DESCRIPCION = "Cunnington",
				FK_TIP_ART = 0,
			};
			listaProductos.Add(d2);

            d2 = new _ARTICULOS()
            {
                ID = 1,
                DESCRIPCION = "Doble Cola",
                FK_TIP_ART = 0,
            };
            listaProductos.Add(d2);

            d2 = new _ARTICULOS()
            {
                ID = 1,
                DESCRIPCION = "Frutafiel",
                FK_TIP_ART = 0,
            };
            listaProductos.Add(d2);

            d2 = new _ARTICULOS()
            {
                ID = 1,
                DESCRIPCION = "Goliat",
                FK_TIP_ART = 0,
            };
            listaProductos.Add(d2);

            d2 = new _ARTICULOS()
            {
                ID = 1,
                DESCRIPCION = "Marinaro",
                FK_TIP_ART = 0,
            };
            listaProductos.Add(d2);

            d2 = new _ARTICULOS()
            {
                ID = 1,
                DESCRIPCION = "Mocoreta",
                FK_TIP_ART = 0,
            };
            listaProductos.Add(d2);

            d2 = new _ARTICULOS()
            {
                ID = 1,
                DESCRIPCION = "Pepsi Cola",
                FK_TIP_ART = 0,
            };
            listaProductos.Add(d2);

            d2 = new _ARTICULOS()
            {
                ID = 1,
                DESCRIPCION = "Prity",
                FK_TIP_ART = 0,
            };
            listaProductos.Add(d2);

            d2 = new _ARTICULOS()
            {
                ID = 1,
                DESCRIPCION = "Rafting",
                FK_TIP_ART = 0,
            };
            listaProductos.Add(d2);

            d2 = new _ARTICULOS()
            {
                ID = 1,
                DESCRIPCION = "Secco",
                FK_TIP_ART = 0,
            };
            listaProductos.Add(d2);

            d2 = new _ARTICULOS()
            {
                ID = 1,
                DESCRIPCION = "Talca",
                FK_TIP_ART = 0,
            };
            listaProductos.Add(d2);

            d2 = new _ARTICULOS()
            {
                ID = 1,
                DESCRIPCION = "Torasso",
                FK_TIP_ART = 0,
            };
            listaProductos.Add(d2);

            d2 = new _ARTICULOS()
            {
                ID = 1,
                DESCRIPCION = "Otras",
                FK_TIP_ART = 0,
            };
            listaProductos.Add(d2);

            d2 = new _ARTICULOS()
            {
                ID = 1,
                DESCRIPCION = "Villamanaos 2000",
                FK_TIP_ART = 1,
            };
            listaProductos.Add(d2);

            d2 = new _ARTICULOS()
            {
                ID = 1,
                DESCRIPCION = "Villamanaos 6000",
                FK_TIP_ART = 1,
            };
            listaProductos.Add(d2);

            d2 = new _ARTICULOS()
            {
                ID = 1,
                DESCRIPCION = "Agua de misiones",
                FK_TIP_ART = 1,
            };
            listaProductos.Add(d2);

            d2 = new _ARTICULOS()
            {
                ID = 1,
                DESCRIPCION = "Celier",
                FK_TIP_ART = 1,
            };
            listaProductos.Add(d2);

            d2 = new _ARTICULOS()
            {
                ID = 1,
                DESCRIPCION = "Cimes",
                FK_TIP_ART = 1,
            };
            listaProductos.Add(d2);

            d2 = new _ARTICULOS()
            {
                ID = 1,
                DESCRIPCION = "Exaltacion de la cruz",
                FK_TIP_ART = 1,
            };
            listaProductos.Add(d2);

            d2 = new _ARTICULOS()
            {
                ID = 1,
                DESCRIPCION = "Glaciar",
                FK_TIP_ART = 1,
            };
            listaProductos.Add(d2);

            d2 = new _ARTICULOS()
            {
                ID = 1,
                DESCRIPCION = "Kin",
                FK_TIP_ART = 1,
            };
            listaProductos.Add(d2);

            d2 = new _ARTICULOS()
            {
                ID = 1,
                DESCRIPCION = "Sierra de los padres",
                FK_TIP_ART = 1,
            };
            listaProductos.Add(d2);

            d2 = new _ARTICULOS()
            {
                ID = 1,
                DESCRIPCION = "Villa del sur",
                FK_TIP_ART = 1,
            };
            listaProductos.Add(d2);

            d2 = new _ARTICULOS()
            {
                ID = 1,
                DESCRIPCION = "Villavicencio",
                FK_TIP_ART = 1,
            };
            listaProductos.Add(d2);

            d2 = new _ARTICULOS()
            {
                ID = 1,
                DESCRIPCION = "Otras",
                FK_TIP_ART = 1,
            };
            listaProductos.Add(d2);

            d2 = new _ARTICULOS()
            {
                ID = 1,
                DESCRIPCION = "Placer 1500",
                FK_TIP_ART = 2,
            };
            listaProductos.Add(d2);

            d2 = new _ARTICULOS()
            {
                ID = 1,
                DESCRIPCION = "Placer 500",
                FK_TIP_ART = 2,
            };
            listaProductos.Add(d2);

            d2 = new _ARTICULOS()
            {
                ID = 1,
                DESCRIPCION = "Baggio Fresh",
                FK_TIP_ART = 2,
            };
            listaProductos.Add(d2);

            d2 = new _ARTICULOS()
            {
                ID = 1,
                DESCRIPCION = "Levite",
                FK_TIP_ART = 2,
            };
            listaProductos.Add(d2);

            d2 = new _ARTICULOS()
            {
                ID = 1,
                DESCRIPCION = "Cimes",
                FK_TIP_ART = 2,
            };
            listaProductos.Add(d2);

            d2 = new _ARTICULOS()
            {
                ID = 1,
                DESCRIPCION = "Aquarius",
                FK_TIP_ART = 2,
            };
            listaProductos.Add(d2);

            d2 = new _ARTICULOS()
            {
                ID = 1,
                DESCRIPCION = "Bio Balance",
                FK_TIP_ART = 2,
            };
            listaProductos.Add(d2);

            d2 = new _ARTICULOS()
            {
                ID = 1,
                DESCRIPCION = "Celier",
                FK_TIP_ART = 2,
            };
            listaProductos.Add(d2);

            d2 = new _ARTICULOS()
            {
                ID = 1,
                DESCRIPCION = "Livra",
                FK_TIP_ART = 2,
            };
            listaProductos.Add(d2);

            d2 = new _ARTICULOS()
            {
                ID = 1,
                DESCRIPCION = "Ser",
                FK_TIP_ART = 2,
            };
            listaProductos.Add(d2);

            d2 = new _ARTICULOS()
            {
                ID = 1,
                DESCRIPCION = "Style",
                FK_TIP_ART = 2,
            };
            listaProductos.Add(d2);

            d2 = new _ARTICULOS()
            {
                ID = 1,
                DESCRIPCION = "Otras",
                FK_TIP_ART = 2,
            };
            listaProductos.Add(d2);

            d2 = new _ARTICULOS()
            {
                ID = 1,
                DESCRIPCION = "Manaos 2000",
                FK_TIP_ART = 3,
            };
            listaProductos.Add(d2);

            d2 = new _ARTICULOS()
            {
                ID = 1,
                DESCRIPCION = "Cimes",
                FK_TIP_ART = 3,
            };
            listaProductos.Add(d2);

            d2 = new _ARTICULOS()
            {
                ID = 1,
                DESCRIPCION = "Aubal",
                FK_TIP_ART = 3,
            };
            listaProductos.Add(d2);

            d2 = new _ARTICULOS()
            {
                ID = 1,
                DESCRIPCION = "Exaltacion de la cruz",
                FK_TIP_ART = 3,
            };
            listaProductos.Add(d2);

            d2 = new _ARTICULOS()
            {
                ID = 1,
                DESCRIPCION = "Ives",
                FK_TIP_ART = 3,
            };
            listaProductos.Add(d2);

            d2 = new _ARTICULOS()
            {
                ID = 1,
                DESCRIPCION = "Vitalisima",
                FK_TIP_ART = 3,
            };
            listaProductos.Add(d2);

            d2 = new _ARTICULOS()
            {
                ID = 1,
                DESCRIPCION = "Sierra de los padres",
                FK_TIP_ART = 3,
            };
            listaProductos.Add(d2);

            d2 = new _ARTICULOS()
            {
                ID = 1,
                DESCRIPCION = "Torasso",
                FK_TIP_ART = 3,
            };
            listaProductos.Add(d2);

            d2 = new _ARTICULOS()
            {
                ID = 1,
                DESCRIPCION = "Saldan",
                FK_TIP_ART = 3,
            };
            listaProductos.Add(d2);

            d2 = new _ARTICULOS()
            {
                ID = 1,
                DESCRIPCION = "Nihull",
                FK_TIP_ART = 3,
            };
            listaProductos.Add(d2);

            d2 = new _ARTICULOS()
            {
                ID = 1,
                DESCRIPCION = "Mass",
                FK_TIP_ART = 3,
            };
            listaProductos.Add(d2);

            d2 = new _ARTICULOS()
            {
                ID = 1,
                DESCRIPCION = "Penty",
                FK_TIP_ART = 3,
            };
            listaProductos.Add(d2);

            d2 = new _ARTICULOS()
            {
                ID = 1,
                DESCRIPCION = "Otras",
                FK_TIP_ART = 3,
            };
            listaProductos.Add(d2);

            d2 = new _ARTICULOS()
            {
                ID = 1,
                DESCRIPCION = "Pindapoy",
                FK_TIP_ART = 4,
            };
            listaProductos.Add(d2);

            d2 = new _ARTICULOS()
            {
                ID = 1,
                DESCRIPCION = "Baggio Pronto",
                FK_TIP_ART = 4,
            };
            listaProductos.Add(d2);

            d2 = new _ARTICULOS()
            {
                ID = 1,
                DESCRIPCION = "Cepita",
                FK_TIP_ART = 4,
            };
            listaProductos.Add(d2);

            d2 = new _ARTICULOS()
            {
                ID = 1,
                DESCRIPCION = "Bio Frut",
                FK_TIP_ART = 4,
            };
            listaProductos.Add(d2);

            d2 = new _ARTICULOS()
            {
                ID = 1,
                DESCRIPCION = "Capilla mendocina",
                FK_TIP_ART = 4,
            };
            listaProductos.Add(d2);

            d2 = new _ARTICULOS()
            {
                ID = 1,
                DESCRIPCION = "Tutti",
                FK_TIP_ART = 4,
            };
            listaProductos.Add(d2);

            d2 = new _ARTICULOS()
            {
                ID = 1,
                DESCRIPCION = "Puro Sol",
                FK_TIP_ART = 4,
            };
            listaProductos.Add(d2);

            d2 = new _ARTICULOS()
            {
                ID = 1,
                DESCRIPCION = "Citric",
                FK_TIP_ART = 4,
            };
            listaProductos.Add(d2);

            d2 = new _ARTICULOS()
            {
                ID = 1,
                DESCRIPCION = "Otros",
                FK_TIP_ART = 4,
            };
            listaProductos.Add(d2);

            d2 = new _ARTICULOS()
            {
                ID = 1,
                DESCRIPCION = "F-Nandito VII",
                FK_TIP_ART = 5,
            };
            listaProductos.Add(d2);

            d2 = new _ARTICULOS()
            {
                ID = 1,
                DESCRIPCION = "Fernando",
                FK_TIP_ART = 5,
            };
            listaProductos.Add(d2);
            return listaProductos;
        }

		public List<ERP_LOCALIDADES> TraerLocalidades()
		{
			List<ERP_LOCALIDADES> ListaLocalidades = new List<ERP_LOCALIDADES>();
			{
				ERP_LOCALIDADES l1 = new ERP_LOCALIDADES()
				{
					ID = 3,
					DESCRIPCION = "12 DE AGOSTO",
					FK_ERP_PARTIDOS = 105,
					Z_FK_ERP_PARTIDOS = "PELLEGRINI",
					FK_ERP_PROVINCIAS = 1,
					Z_FK_ERP_PROVINCIAS = "Buenos Aires"
				};
				ListaLocalidades.Add(l1);
			}
			return ListaLocalidades;
		}

		private async Task ObtenerPermisos()
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

		private bool TableExists(string tableName)
		{
			bool sw = false;
			try
			{
				using (SQLite.SQLiteConnection connection = new SQLite.SQLiteConnection(RutaBD))
				{
					string query = string.Format("SELECT name FROM sqlite_master WHERE type='table' AND name='{0}';", tableName);
					SQLiteCommand cmd = connection.CreateCommand(query);
					var item = connection.Query<object>(query);
					if (item.Count > 0)
						sw = true;
					return sw;
				}
			}
			catch (SQLiteException ex)
			{
				throw ex;
			}
		}

	}
}
