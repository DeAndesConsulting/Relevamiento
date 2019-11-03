using Relevamiento.Clases;
using SQLite;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Relevamiento.Vistas
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Sodas : ContentPage
    {
        public static List<ListaProductos> listaSodas;
        public Sodas()
        {
            InitializeComponent();
            int Sodas = 4;
            List<_ARTICULOS> listaArticulos = new List<_ARTICULOS>();

            List<ListaProductos> listaTemp = new List<ListaProductos>();
            using (SQLite.SQLiteConnection conexion = new SQLiteConnection(App.RutaBD))
            {
                listaArticulos = conexion.Query<_ARTICULOS>("select * from _ARTICULOS where FK_TIP_ART = ?", Sodas).ToList();

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
                    listaTemp.Add(producto);
                }

            }
            listaSodas = listaTemp;
            SodasListView.ItemsSource = listaSodas;
        }
    }
}