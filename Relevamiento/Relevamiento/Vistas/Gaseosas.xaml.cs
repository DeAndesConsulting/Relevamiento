using Relevamiento.Clases;
using SQLite;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Relevamiento.Vistas
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Gaseosas : ContentPage
    {

        public static List<ListaProductos> listaGaseosas;
        public Gaseosas()
        {

            List<_ARTICULOS> listaArticulos = new List<_ARTICULOS>();

            List<ListaProductos> listaGaseosasTemp = new List<ListaProductos>();

            InitializeComponent();
            int Gaseosas = 0;

            using (SQLite.SQLiteConnection conexion = new SQLiteConnection(App.RutaBD))
            {
                listaArticulos = conexion.Query<_ARTICULOS>("select * from _ARTICULOS where FK_TIP_ART = ?", Gaseosas).ToList();

            }
            ListaProductos producto;
            if (listaArticulos.Count != 0)
            {
                foreach (var obj in listaArticulos)
                {
                    producto = new ListaProductos()
                    {
                        Id = obj.ID,
                        Producto = obj.DESCRIPCION,
                        TipoProducto = obj.FK_TIP_ART,
                    };
                    listaGaseosasTemp.Add(producto);
                }

            }
            listaGaseosas = listaGaseosasTemp;
            GaseosasListView.ItemsSource = listaGaseosas;
        }
    }
}