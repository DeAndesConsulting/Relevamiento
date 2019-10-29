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
        public static ERP_LOCALIDADES LocalidadSeleccionada = new ERP_LOCALIDADES();
        public ERP_EMPRESAS DistribuidorSeleccionado;
        public List<ERP_EMPRESAS> lista_distribuidores = new List<ERP_EMPRESAS>();
        public BusquedaDistribuidor()
        {
            InitializeComponent();
            using (SQLite.SQLiteConnection conexion = new SQLiteConnection(App.RutaBD))
            {
                //lista_distribuidores = conexion.Query<ERP_EMPRESAS>("select * from ERP_EMPRESAS").ToList();
                if (!App.globalAsesor.c_IMEI_ADMIN)
                {
                    lista_distribuidores = conexion.Table<ERP_EMPRESAS>()
                        .Where(m => m.FK_ERP_ASESORES == App.globalAsesor.ID
                        || m.FK_ERP_ASESORES2 == App.globalAsesor.ID
                        || m.FK_ERP_ASESORES3 == App.globalAsesor.ID)
                        .ToList();
                }
                else
                {
                    lista_distribuidores = conexion.Table<ERP_EMPRESAS>().ToList();
                }                
            }
        }

        public void DistribuidorSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            DistribuidorList.IsVisible = true;
            ToolbarItems.Clear();
            //DistribuidorList.ItemsSource = lista_distribuidores.Where(c => c.FormattedText.ToString().ToLower().Contains(DistribuidorSearch.Text));
            DistribuidorList.ItemsSource = lista_distribuidores.Where(c => c.FormattedText.IndexOf(DistribuidorSearch.Text, StringComparison.OrdinalIgnoreCase) != -1);
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