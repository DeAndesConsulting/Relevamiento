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
    public partial class EstadoRelevamiento : TabbedPage
    {
        public static List<_ARTICULOS> listaArticulos = new List<_ARTICULOS>();

        public EstadoRelevamiento(List<ItrisRelevamientoArticuloEntity> listadorg)
        {
            using (SQLite.SQLiteConnection conexion = new SQLiteConnection(App.RutaBD))
            {
                listaArticulos = conexion.Table<_ARTICULOS>().ToList();
            }
            foreach (var obj in listadorg)
            {
                for (int i = 0; i < listaArticulos.Count(); i++)
                {
                    if (listaArticulos[i].ID == obj.FK_ARTICULOS)
                    {
                        listaArticulos[i].Existe = obj.EXISTE;
                        listaArticulos[i].Precio = obj.PRECIO;
                    }
                }
            }
            InitializeComponent();



        }

        private async void Aceptar_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}