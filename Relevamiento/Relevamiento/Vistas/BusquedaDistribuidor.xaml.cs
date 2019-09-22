using Relevamiento.Clases;
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
        public List<ERP_EMPRESAS> lista_distribuidores;
        public BusquedaDistribuidor()
        {
            InitializeComponent();

            lista_distribuidores = TraerDatos2();
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

        public List<ERP_EMPRESAS> TraerDatos2()
        {
            List<ERP_EMPRESAS> listaDatos = new List<ERP_EMPRESAS>();
            ERP_EMPRESAS d1 = new ERP_EMPRESAS()
            {
                ID = 99999,
                Z_FK_ERP_PROVINCIAS = "Buenos Aires",
                NOM_FANTASIA = "Prueba"
            };
            listaDatos.Add(d1);

            d1 = new ERP_EMPRESAS()
            {
                ID = 0003,
                Z_FK_ERP_PROVINCIAS = "Buenos Aires",
                NOM_FANTASIA = "REFRES NOW S.A"
            };
            listaDatos.Add(d1);
            d1 = new ERP_EMPRESAS()
            {
                ID = 934,
                Z_FK_ERP_PROVINCIAS = "Buenos Aires",
                NOM_FANTASIA = "GASPAR CAMPOS"
            };
            listaDatos.Add(d1);

			d1 = new ERP_EMPRESAS()
			{
				ID = 16009|200564|20507|20666|20789|20957,
                Z_FK_ERP_PROVINCIAS = "Buenos Aires",
                NOM_FANTASIA = "MAZUCA ROBERTO"
			};
			listaDatos.Add(d1);

			return listaDatos;
        }

        async public void BtnSiguiente_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Comercio(DistribuidorSeleccionado));
        }
    }
}