using Relevamiento.Clases;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Relevamiento.Vistas
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BusquedaDistribuidor : ContentPage
    {
        public ERP_EMPRESAS DistribuidorSeleccionado;
        public List<ERP_EMPRESAS> lista_distribuidores = new List<ERP_EMPRESAS>();
        public BusquedaDistribuidor()
        {
            InitializeComponent();
            using (SQLite.SQLiteConnection conexion = new SQLiteConnection(App.RutaBD))
            {
                lista_distribuidores = conexion.Query<ERP_EMPRESAS>("select * from ERP_EMPRESAS").ToList();
            }
        }

        public void DistribuidorSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            DistribuidorList.IsVisible = true;
            ToolbarItems.Clear();
            DistribuidorList.ItemsSource = lista_distribuidores.Where(c => c.FormattedText.ToString().ToLower().Contains(DistribuidorSearch.Text));
        }

        public void DistribuidorList_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            DistribuidorList.IsVisible = false;
            DatosDistribuidor.IsVisible = true;
            DistribuidorSeleccionado = e.Item as ERP_EMPRESAS;
            LblNombreFantasia.Text = "Nombre: " + DistribuidorSeleccionado.NOM_FANTASIA.ToString();
            LblCodigo.Text = "Codigo: " + DistribuidorSeleccionado.ID.ToString();
        }

        async public void BtnSiguiente_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Comercio(DistribuidorSeleccionado));
        }
    }
}