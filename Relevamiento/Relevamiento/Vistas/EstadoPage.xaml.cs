using Relevamiento.Clases;
using SQLite;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Relevamiento.Vistas
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EstadoPage : ContentPage
    {
        public EstadoPage()
        {
            InitializeComponent();
            List<Relevado> listaRelevados;
            using (SQLite.SQLiteConnection conexion = new SQLiteConnection(App.RutaBD))
            {
                listaRelevados = conexion.Table<Relevado>().ToList();
            }
            EstadoListView.ItemsSource = listaRelevados;
        }

        private async void EstadoListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            await Navigation.PushAsync(new EstadoRelevamiento());
        }
    }
}