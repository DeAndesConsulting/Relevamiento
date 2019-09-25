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

namespace Relevamiento
{
    public partial class App : Application
    {
        public static ItrisRelevamientoEntity releva = new ItrisRelevamientoEntity();
        public static List<ItrisComercioArticulo> comercios = new List<ItrisComercioArticulo>();
        public  static ERP_EMPRESAS distribuidorseleccionado;
        //public static List<ItrisComercioEntity> listacom = new List<ItrisComercioEntity>();
        //public static List<ItrisRelevamientoArticuloEntity> relarts = new List<ItrisRelevamientoArticuloEntity>();
        public static string RutaBD;
        public App(string rutaBD)
        {
            InitializeComponent();
            RutaBD = rutaBD;
            VersionTracking.Track();
            bool firsttime = VersionTracking.IsFirstLaunchForCurrentVersion;
            //if (firsttime == true)
            //{

                List<_ARTICULOS> lista_productos;
                lista_productos = TraerProductos();
                List<ERP_EMPRESAS> ListaDistribuidores;
                ListaDistribuidores = TraerDatos2();
                using (SQLite.SQLiteConnection conexion = new SQLite.SQLiteConnection(RutaBD))
                {
                    conexion.CreateTable<Provincia>();
                    conexion.CreateTable<TipoLocal>();
                    conexion.CreateTable<ERP_EMPRESAS>();
                    conexion.CreateTable<_COMERCIO>();
                    conexion.CreateTable<_TIP_ART>();
                    conexion.CreateTable<_ARTICULOS>();
                    conexion.CreateTable<Relevado>();
                }
                SQLiteAsyncConnection conn = new SQLiteAsyncConnection(App.RutaBD);
                conn.InsertAllAsync(lista_productos);
                conn.InsertAllAsync(ListaDistribuidores);

            //}


            //MainPage = new NavigationPage(new Vistas.Login());

            MainPage = new NavigationPage(new Login())
            {
                BarBackgroundColor = Color.FromHex("#F5DE8E"),
                BarTextColor = Color.Gray
            };
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

        public List<ERP_EMPRESAS> TraerDatos2()
        {
            List<ERP_EMPRESAS> listaDatos = new List<ERP_EMPRESAS>();

            ERP_EMPRESAS d1 = new ERP_EMPRESAS()
            {
                ID = 1,
                Z_FK_ERP_PROVINCIAS = "Buenos Aires",
                NOM_FANTASIA = "Hola"
            };
            listaDatos.Add(d1);

            d1 = new ERP_EMPRESAS()
            {
                ID = 2,
                Z_FK_ERP_PROVINCIAS = "Buenos Aires",
                NOM_FANTASIA = "REFRES NOW S.A"
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
