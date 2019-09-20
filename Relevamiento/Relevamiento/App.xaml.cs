using System;
using System.Collections.Generic;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using Relevamiento.Clases;
using Relevamiento.Vistas;
using SQLite;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Relevamiento
{
    public partial class App : Application
    {
        public static RelevamientO releva = new RelevamientO();
        public static List<Comercios> comercios = new List<Comercios>();
        public  static Distribuidora distribuidorseleccionado;
        public static List<ComerciO> listacom = new List<ComerciO>();
        public static List<RelevamientoArticulO> relarts = new List<RelevamientoArticulO>();
        public static string RutaBD;
        public App(string rutaBD)
        {
            InitializeComponent();
            RutaBD = rutaBD;
            VersionTracking.Track();
            bool firsttime = VersionTracking.IsFirstLaunchForCurrentVersion;
            if (firsttime == true)
            {

                List<ListaProductos> lista_productos;
                lista_productos = TraerProductos();
                List<Distribuidora> ListaDistribuidores;
                ListaDistribuidores = TraerDatos2();
                using (SQLite.SQLiteConnection conexion = new SQLite.SQLiteConnection(RutaBD))
                {
                    conexion.CreateTable<Provincia>();
                    conexion.CreateTable<TipoLocal>();
                    conexion.CreateTable<Distribuidora>();
                    conexion.CreateTable<Local>();
                    conexion.CreateTable<TipoProductos>();
                    conexion.CreateTable<ListaProductos>();
                    conexion.CreateTable<Relevado>();
                }
                SQLiteAsyncConnection conn = new SQLiteAsyncConnection(App.RutaBD);
                conn.InsertAllAsync(lista_productos);
                conn.InsertAllAsync(ListaDistribuidores);

            }


            //MainPage = new NavigationPage(new Vistas.Login());

            MainPage = new NavigationPage(new Login())
            {
                BarBackgroundColor = Color.FromHex("#F5DE8E"),
                BarTextColor = Color.Gray
            };
        }

        async void ObtenerPermisos(object sender, EventArgs e)
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
        }
        public List<ListaProductos> TraerProductos()
        {
            List<ListaProductos> listaProductos = new List<ListaProductos>();
            ListaProductos d2 = new ListaProductos()
            {
                Id = 1,
                Producto = "Manaos 300",
                Precio = 0,
                Existe = false,
                TipoProducto = 0,
            };
            listaProductos.Add(d2);

            d2 = new ListaProductos()
            {
                Id = 1,
                Producto = "Manaos 1250",
                Precio = 0,
                Existe = false,
                TipoProducto = 0,
            };
            listaProductos.Add(d2);

            d2 = new ListaProductos()
            {
                Id = 1,
                Producto = "Manaos 2250",
                Precio = 0,
                Existe = false,
                TipoProducto = 0,
            };
            listaProductos.Add(d2);

            d2 = new ListaProductos()
            {
                Id = 1,
                Producto = "Manaos 600",
                Precio = 0,
                Existe = false,
                TipoProducto = 0,
            };
            listaProductos.Add(d2);

            d2 = new ListaProductos()
            {
                Id = 1,
                Producto = "Manaos sin Azucar",
                Precio = 0,
                Existe = false,
                TipoProducto = 0,
            };
            listaProductos.Add(d2);

            d2 = new ListaProductos()
            {
                Id = 1,
                Producto = "Cantidad de sabores 2250",
                Precio = 0,
                Existe = false,
                TipoProducto = 0,
            };
            listaProductos.Add(d2);

            d2 = new ListaProductos()
            {
                Id = 1,
                Producto = "Beach",
                Precio = 0,
                Existe = false,
                TipoProducto = 0,
            };
            listaProductos.Add(d2);

            d2 = new ListaProductos()
            {
                Id = 1,
                Producto = "Cabalgata",
                Precio = 0,
                Existe = false,
                TipoProducto = 0,
            };
            listaProductos.Add(d2);

            d2 = new ListaProductos()
            {
                Id = 1,
                Producto = "Caribe",
                Precio = 0,
                Existe = false,
                TipoProducto = 0,
            };
            listaProductos.Add(d2);

            d2 = new ListaProductos()
            {
                Id = 1,
                Producto = "Cimes",
                Precio = 0,
                Existe = false,
                TipoProducto = 0,
            };
            listaProductos.Add(d2);

            d2 = new ListaProductos()
            {
                Id = 1,
                Producto = "Coca Cola",
                Precio = 0,
                Existe = false,
                TipoProducto = 0,
            };
            listaProductos.Add(d2);

            d2 = new ListaProductos()
            {
                Id = 1,
                Producto = "Cordoba",
                Precio = 0,
                Existe = false,
                TipoProducto = 0,
            };
            listaProductos.Add(d2);

            d2 = new ListaProductos()
            {
                Id = 1,
                Producto = "Cunnington",
                Precio = 0,
                Existe = false,
                TipoProducto = 0,
            };
            listaProductos.Add(d2);

            d2 = new ListaProductos()
            {
                Id = 1,
                Producto = "Doble Cola",
                Precio = 0,
                Existe = false,
                TipoProducto = 0,
            };
            listaProductos.Add(d2);

            d2 = new ListaProductos()
            {
                Id = 1,
                Producto = "Frutafiel",
                Precio = 0,
                Existe = false,
                TipoProducto = 0,
            };
            listaProductos.Add(d2);

            d2 = new ListaProductos()
            {
                Id = 1,
                Producto = "Goliat",
                Precio = 0,
                Existe = false,
                TipoProducto = 0,
            };
            listaProductos.Add(d2);

            d2 = new ListaProductos()
            {
                Id = 1,
                Producto = "Marinaro",
                Precio = 0,
                Existe = false,
                TipoProducto = 0,
            };
            listaProductos.Add(d2);

            d2 = new ListaProductos()
            {
                Id = 1,
                Producto = "Mocoreta",
                Precio = 0,
                Existe = false,
                TipoProducto = 0,
            };
            listaProductos.Add(d2);

            d2 = new ListaProductos()
            {
                Id = 1,
                Producto = "Pepsi Cola",
                Precio = 0,
                Existe = false,
                TipoProducto = 0,
            };
            listaProductos.Add(d2);

            d2 = new ListaProductos()
            {
                Id = 1,
                Producto = "Prity",
                Precio = 0,
                Existe = false,
                TipoProducto = 0,
            };
            listaProductos.Add(d2);

            d2 = new ListaProductos()
            {
                Id = 1,
                Producto = "Rafting",
                Precio = 0,
                Existe = false,
                TipoProducto = 0,
            };
            listaProductos.Add(d2);

            d2 = new ListaProductos()
            {
                Id = 1,
                Producto = "Secco",
                Precio = 0,
                Existe = false,
                TipoProducto = 0,
            };
            listaProductos.Add(d2);

            d2 = new ListaProductos()
            {
                Id = 1,
                Producto = "Talca",
                Precio = 0,
                Existe = false,
                TipoProducto = 0,
            };
            listaProductos.Add(d2);

            d2 = new ListaProductos()
            {
                Id = 1,
                Producto = "Torasso",
                Precio = 0,
                Existe = false,
                TipoProducto = 0,
            };
            listaProductos.Add(d2);

            d2 = new ListaProductos()
            {
                Id = 1,
                Producto = "Otras",
                Precio = 0,
                Existe = false,
                TipoProducto = 0,
            };
            listaProductos.Add(d2);

            d2 = new ListaProductos()
            {
                Id = 1,
                Producto = "Villamanaos 2000",
                Precio = 0,
                Existe = false,
                TipoProducto = 1,
            };
            listaProductos.Add(d2);

            d2 = new ListaProductos()
            {
                Id = 1,
                Producto = "Villamanaos 6000",
                Precio = 0,
                Existe = false,
                TipoProducto = 1,
            };
            listaProductos.Add(d2);

            d2 = new ListaProductos()
            {
                Id = 1,
                Producto = "Agua de misiones",
                Precio = 0,
                Existe = false,
                TipoProducto = 1,
            };
            listaProductos.Add(d2);

            d2 = new ListaProductos()
            {
                Id = 1,
                Producto = "Celier",
                Precio = 0,
                Existe = false,
                TipoProducto = 1,
            };
            listaProductos.Add(d2);

            d2 = new ListaProductos()
            {
                Id = 1,
                Producto = "Cimes",
                Precio = 0,
                Existe = false,
                TipoProducto = 1,
            };
            listaProductos.Add(d2);

            d2 = new ListaProductos()
            {
                Id = 1,
                Producto = "Exaltacion de la cruz",
                Precio = 0,
                Existe = false,
                TipoProducto = 1,
            };
            listaProductos.Add(d2);

            d2 = new ListaProductos()
            {
                Id = 1,
                Producto = "Glaciar",
                Precio = 0,
                Existe = false,
                TipoProducto = 1,
            };
            listaProductos.Add(d2);

            d2 = new ListaProductos()
            {
                Id = 1,
                Producto = "Kin",
                Precio = 0,
                Existe = false,
                TipoProducto = 1,
            };
            listaProductos.Add(d2);

            d2 = new ListaProductos()
            {
                Id = 1,
                Producto = "Sierra de los padres",
                Precio = 0,
                Existe = false,
                TipoProducto = 1,
            };
            listaProductos.Add(d2);

            d2 = new ListaProductos()
            {
                Id = 1,
                Producto = "Villa del sur",
                Precio = 0,
                Existe = false,
                TipoProducto = 1,
            };
            listaProductos.Add(d2);

            d2 = new ListaProductos()
            {
                Id = 1,
                Producto = "Villavicencio",
                Precio = 0,
                Existe = false,
                TipoProducto = 1,
            };
            listaProductos.Add(d2);

            d2 = new ListaProductos()
            {
                Id = 1,
                Producto = "Otras",
                Precio = 0,
                Existe = false,
                TipoProducto = 1,
            };
            listaProductos.Add(d2);

            d2 = new ListaProductos()
            {
                Id = 1,
                Producto = "Placer 1500",
                Precio = 0,
                Existe = false,
                TipoProducto = 2,
            };
            listaProductos.Add(d2);

            d2 = new ListaProductos()
            {
                Id = 1,
                Producto = "Placer 500",
                Precio = 0,
                Existe = false,
                TipoProducto = 2,
            };
            listaProductos.Add(d2);

            d2 = new ListaProductos()
            {
                Id = 1,
                Producto = "Baggio Fresh",
                Precio = 0,
                Existe = false,
                TipoProducto = 2,
            };
            listaProductos.Add(d2);

            d2 = new ListaProductos()
            {
                Id = 1,
                Producto = "Levite",
                Precio = 0,
                Existe = false,
                TipoProducto = 2,
            };
            listaProductos.Add(d2);

            d2 = new ListaProductos()
            {
                Id = 1,
                Producto = "Cimes",
                Precio = 0,
                Existe = false,
                TipoProducto = 2,
            };
            listaProductos.Add(d2);

            d2 = new ListaProductos()
            {
                Id = 1,
                Producto = "Aquarius",
                Precio = 0,
                Existe = false,
                TipoProducto = 2,
            };
            listaProductos.Add(d2);

            d2 = new ListaProductos()
            {
                Id = 1,
                Producto = "Bio Balance",
                Precio = 0,
                Existe = false,
                TipoProducto = 2,
            };
            listaProductos.Add(d2);

            d2 = new ListaProductos()
            {
                Id = 1,
                Producto = "Celier",
                Precio = 0,
                Existe = false,
                TipoProducto = 2,
            };
            listaProductos.Add(d2);

            d2 = new ListaProductos()
            {
                Id = 1,
                Producto = "Livra",
                Precio = 0,
                Existe = false,
                TipoProducto = 2,
            };
            listaProductos.Add(d2);

            d2 = new ListaProductos()
            {
                Id = 1,
                Producto = "Ser",
                Precio = 0,
                Existe = false,
                TipoProducto = 2,
            };
            listaProductos.Add(d2);

            d2 = new ListaProductos()
            {
                Id = 1,
                Producto = "Style",
                Precio = 0,
                Existe = false,
                TipoProducto = 2,
            };
            listaProductos.Add(d2);

            d2 = new ListaProductos()
            {
                Id = 1,
                Producto = "Otras",
                Precio = 0,
                Existe = false,
                TipoProducto = 2,
            };
            listaProductos.Add(d2);

            d2 = new ListaProductos()
            {
                Id = 1,
                Producto = "Manaos 2000",
                Precio = 0,
                Existe = false,
                TipoProducto = 3,
            };
            listaProductos.Add(d2);

            d2 = new ListaProductos()
            {
                Id = 1,
                Producto = "Cimes",
                Precio = 0,
                Existe = false,
                TipoProducto = 3,
            };
            listaProductos.Add(d2);

            d2 = new ListaProductos()
            {
                Id = 1,
                Producto = "Aubal",
                Precio = 0,
                Existe = false,
                TipoProducto = 3,
            };
            listaProductos.Add(d2);

            d2 = new ListaProductos()
            {
                Id = 1,
                Producto = "Exaltacion de la cruz",
                Precio = 0,
                Existe = false,
                TipoProducto = 3,
            };
            listaProductos.Add(d2);

            d2 = new ListaProductos()
            {
                Id = 1,
                Producto = "Ives",
                Precio = 0,
                Existe = false,
                TipoProducto = 3,
            };
            listaProductos.Add(d2);

            d2 = new ListaProductos()
            {
                Id = 1,
                Producto = "Vitalisima",
                Precio = 0,
                Existe = false,
                TipoProducto = 3,
            };
            listaProductos.Add(d2);

            d2 = new ListaProductos()
            {
                Id = 1,
                Producto = "Sierra de los padres",
                Precio = 0,
                Existe = false,
                TipoProducto = 3,
            };
            listaProductos.Add(d2);

            d2 = new ListaProductos()
            {
                Id = 1,
                Producto = "Torasso",
                Precio = 0,
                Existe = false,
                TipoProducto = 3,
            };
            listaProductos.Add(d2);

            d2 = new ListaProductos()
            {
                Id = 1,
                Producto = "Saldan",
                Precio = 0,
                Existe = false,
                TipoProducto = 3,
            };
            listaProductos.Add(d2);

            d2 = new ListaProductos()
            {
                Id = 1,
                Producto = "Nihull",
                Precio = 0,
                Existe = false,
                TipoProducto = 3,
            };
            listaProductos.Add(d2);

            d2 = new ListaProductos()
            {
                Id = 1,
                Producto = "Mass",
                Precio = 0,
                Existe = false,
                TipoProducto = 3,
            };
            listaProductos.Add(d2);

            d2 = new ListaProductos()
            {
                Id = 1,
                Producto = "Penty",
                Precio = 0,
                Existe = false,
                TipoProducto = 3,
            };
            listaProductos.Add(d2);

            d2 = new ListaProductos()
            {
                Id = 1,
                Producto = "Otras",
                Precio = 0,
                Existe = false,
                TipoProducto = 3,
            };
            listaProductos.Add(d2);

            d2 = new ListaProductos()
            {
                Id = 1,
                Producto = "Pindapoy",
                Precio = 0,
                Existe = false,
                TipoProducto = 4,
            };
            listaProductos.Add(d2);

            d2 = new ListaProductos()
            {
                Id = 1,
                Producto = "Baggio Pronto",
                Precio = 0,
                Existe = false,
                TipoProducto = 4,
            };
            listaProductos.Add(d2);

            d2 = new ListaProductos()
            {
                Id = 1,
                Producto = "Cepita",
                Precio = 0,
                Existe = false,
                TipoProducto = 4,
            };
            listaProductos.Add(d2);

            d2 = new ListaProductos()
            {
                Id = 1,
                Producto = "Bio Frut",
                Precio = 0,
                Existe = false,
                TipoProducto = 4,
            };
            listaProductos.Add(d2);

            d2 = new ListaProductos()
            {
                Id = 1,
                Producto = "Capilla mendocina",
                Precio = 0,
                Existe = false,
                TipoProducto = 4,
            };
            listaProductos.Add(d2);

            d2 = new ListaProductos()
            {
                Id = 1,
                Producto = "Tutti",
                Precio = 0,
                Existe = false,
                TipoProducto = 4,
            };
            listaProductos.Add(d2);

            d2 = new ListaProductos()
            {
                Id = 1,
                Producto = "Puro Sol",
                Precio = 0,
                Existe = false,
                TipoProducto = 4,
            };
            listaProductos.Add(d2);

            d2 = new ListaProductos()
            {
                Id = 1,
                Producto = "Citric",
                Precio = 0,
                Existe = false,
                TipoProducto = 4,
            };
            listaProductos.Add(d2);

            d2 = new ListaProductos()
            {
                Id = 1,
                Producto = "Otros",
                Precio = 0,
                Existe = false,
                TipoProducto = 4,
            };
            listaProductos.Add(d2);

            d2 = new ListaProductos()
            {
                Id = 1,
                Producto = "F-Nandito VII",
                Precio = 0,
                Existe = false,
                TipoProducto = 5,
            };
            listaProductos.Add(d2);

            d2 = new ListaProductos()
            {
                Id = 1,
                Producto = "Fernando",
                Precio = 0,
                Existe = false,
                TipoProducto = 5,
            };
            listaProductos.Add(d2);
            return listaProductos;
        }

        public List<Distribuidora> TraerDatos2()
        {
            List<Distribuidora> listaDatos = new List<Distribuidora>();

            Distribuidora d1 = new Distribuidora()
            {
                Id = "1",
                Provincia = "Buenos Aires",
                Nombre = "Hola",
                Direccion = "Av lola 2211"
            };
            listaDatos.Add(d1);

            d1 = new Distribuidora()
            {
                Id = "2",
                Provincia = "Buenos Aires",
                Nombre = "REFRES NOW S.A",
                Direccion = "Brig. Juan M. de Rosas 25150"
            };
            listaDatos.Add(d1);
            return listaDatos;
        }


        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
