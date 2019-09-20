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
        public Distribuidora DistribuidorSeleccionado;
        public List<Distribuidora> lista_distribuidores;
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
            DistribuidorSeleccionado = e.Item as Distribuidora;
            LblNombreFantasia.Text = "Nombre: " + DistribuidorSeleccionado.Nombre.ToString();
            LblCodigo.Text = "Codigo: " + DistribuidorSeleccionado.Id.ToString();
        }

        public List<Distribuidora> TraerDatos2()
        {
            List<Distribuidora> listaDatos = new List<Distribuidora>();
            Distribuidora d1 = new Distribuidora()
            {
                Id = "99999",
                Provincia = "Buenos Aires",
                Nombre = "Prueba",
                Direccion = "Coronel Aguirre 2407"
            };
            listaDatos.Add(d1);

            d1 = new Distribuidora()
            {
                Id = "0003",
                Provincia = "Buenos Aires",
                Nombre = "REFRES NOW S.A",
                Direccion = "Brig. Juan M. de Rosas 25150"
            };
            listaDatos.Add(d1);
            d1 = new Distribuidora()
            {
                Id = "934",
                Provincia = "Buenos Aires",
                Nombre = "GASPAR CAMPOS",
                Direccion = "Gaspar Campos 4061"
            };
            listaDatos.Add(d1);

			d1 = new Distribuidora()
			{
				Id = "16009|200564|20507|20666|20789|20957",
				Provincia = "Buenos Aires",
				Nombre = "MAZUCA ROBERTO",
				Direccion = "Gaspar Campos 4061"
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