using Relevamiento.Clases;
using SQLite;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Relevamiento.Vistas
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Fernet : ContentPage
    {
        public static List<ListaProductos> listaFernets;
        public Fernet()
        {
            InitializeComponent();
            int Fernet = 5;
            using (SQLite.SQLiteConnection conexion = new SQLiteConnection(App.RutaBD))
            {
                listaFernets = conexion.Query<ListaProductos>("select * from ListaProductos where TipoProducto = ?", Fernet).ToList();
                 FernetListView.ItemsSource = listaFernets;
            }
        }
    }
}