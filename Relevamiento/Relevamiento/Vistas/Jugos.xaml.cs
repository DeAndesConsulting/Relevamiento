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
    public partial class Jugos : ContentPage
    {
        public static List<ListaProductos> listaJugos;
        public Jugos()
        {
            InitializeComponent();
            int Jugos = 4;
            using (SQLite.SQLiteConnection conexion = new SQLiteConnection(App.RutaBD))
            {
                listaJugos = conexion.Query<ListaProductos>("select * from ListaProductos where TipoProducto = ?", Jugos).ToList();
                JugosListView.ItemsSource = listaJugos;
            }
        }
    }
}