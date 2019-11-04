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
    public partial class SodasRele : ContentPage
    {
        public SodasRele()
        {
            InitializeComponent();
            int Sodas = 4;
            SodasListView.ItemsSource = EstadoRelevamiento.listaArticulos.Where(c => c.FK_TIP_ART.ToString().ToLower().Contains(Sodas.ToString()));
        }
    }
}