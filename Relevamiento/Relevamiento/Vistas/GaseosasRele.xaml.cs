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
    public partial class GaseosasRele : ContentPage
    {
        public GaseosasRele()
        {
            InitializeComponent();
            int Gaseosas = 1;

            GaseosasListView.ItemsSource = EstadoRelevamiento.listaArticulos.Where(c => c.FK_TIP_ART.ToString().ToLower().Contains(Gaseosas.ToString()));

        }
    }
}