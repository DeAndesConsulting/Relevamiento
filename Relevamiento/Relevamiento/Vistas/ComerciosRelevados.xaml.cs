using Relevamiento.Clases;
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
    public partial class ComerciosRelevados : ContentPage
    {
        public ComerciosRelevados(EstadosRel RelevamientoVer)
        {
            InitializeComponent();
            ListadoRel.ItemsSource = RelevamientoVer.comercios;

        }

        async void ListadoRel_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var asd = e.Item as ItrisComercioArticulo;
            await Navigation.PushAsync(new EstadoRelevamiento(asd.relevamientoArticulo));
        }

    }
}
