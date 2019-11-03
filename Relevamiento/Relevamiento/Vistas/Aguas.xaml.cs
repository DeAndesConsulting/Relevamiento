using Relevamiento.Clases;
using SQLite;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Relevamiento.Vistas
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Aguas : ContentPage
    {
        public static List<ListaProductos> listaAguas;

        public Aguas()
        {
            InitializeComponent();
            List<_ARTICULOS> listaArticulos = new List<_ARTICULOS>();

            List<ListaProductos> listaAguasTemp = new List<ListaProductos>();
            int Aguas = 2;
            using (SQLite.SQLiteConnection conexion = new SQLiteConnection(App.RutaBD))
            {
                listaArticulos = conexion.Query<_ARTICULOS>("select * from _ARTICULOS where FK_TIP_ART = ?", Aguas).ToList();

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
                    listaAguasTemp.Add(producto);
                }

            }


            listaAguas = listaAguasTemp;
            AguasListView.ItemsSource = listaAguas;
        }
    }
}