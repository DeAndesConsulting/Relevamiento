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
            InitializeComponent();
            int Gaseosas = 0;
            using (SQLite.SQLiteConnection conexion = new SQLiteConnection(App.RutaBD))
            {
                listaGaseosas = conexion.Query<ListaProductos>("select * from ListaProductos where TipoProducto = ?", Gaseosas).ToList();
                GaseosasListView.ItemsSource = listaGaseosas;
            }
        }
    }
}