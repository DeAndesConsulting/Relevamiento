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
            List<_ARTICULOS> listaArticulos = new List<_ARTICULOS>();

            List<ListaProductos> listaTemp = new List<ListaProductos>();
            using (SQLite.SQLiteConnection conexion = new SQLiteConnection(App.RutaBD))
            {
                listaArticulos = conexion.Query<_ARTICULOS>("select * from _ARTICULOS where FK_TIP_ART = ?", Fernet).ToList();

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
                        TipoProducto = obj.FK_TIP_ART
                    };
                    listaTemp.Add(producto);
                }

            }
            listaFernets = listaTemp;
            FernetListView.ItemsSource = listaFernets;
        }
    }
}