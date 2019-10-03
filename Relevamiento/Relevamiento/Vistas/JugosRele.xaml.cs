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
    public partial class JugosRele : ContentPage
    {
        public JugosRele()
        {
            InitializeComponent();
            int Jugos = 4;
            JugosListView.ItemsSource = EstadoRelevamiento.listaArticulos.Where(c => c.FK_TIP_ART.ToString().ToLower().Contains(Jugos.ToString()));
        }
    }
}