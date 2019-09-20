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
            int Aguas = 1;
            using (SQLite.SQLiteConnection conexion = new SQLiteConnection(App.RutaBD))
            {
                listaAguas = conexion.Query<ListaProductos>("select * from ListaProductos where TipoProducto = ?", Aguas).ToList();
                AguasListView.ItemsSource = listaAguas;
            }
        }
    }
}