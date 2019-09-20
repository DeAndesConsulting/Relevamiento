using Relevamiento.Clases;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Relevamiento.Vistas
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Saborizadas : ContentPage
    {
        public static List<ListaProductos> listaSaborizadas;
        public Saborizadas()
        {
            InitializeComponent();
            int Saborizadas = 2;
            using (SQLite.SQLiteConnection conexion = new SQLiteConnection(App.RutaBD))
            {
                listaSaborizadas = conexion.Query<ListaProductos>("select * from ListaProductos where TipoProducto = ?", Saborizadas).ToList();
                SaborizadasListView.ItemsSource = listaSaborizadas;
            }
        }
    }
}