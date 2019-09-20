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
            int Sodas = 3;
            using (SQLite.SQLiteConnection conexion = new SQLiteConnection(App.RutaBD))
            {
                listaSodas = conexion.Query<ListaProductos>("select * from ListaProductos where TipoProducto = ?", Sodas).ToList();
                SodasListView.ItemsSource = listaSodas;
            }
        }
    }
}