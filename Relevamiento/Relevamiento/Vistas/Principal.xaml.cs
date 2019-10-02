using Relevamiento.Clases;
using Relevamiento.Services.Middleware;
using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Linq;
using System.Threading.Tasks;

namespace Relevamiento.Vistas
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Principal : ContentPage
    {
        public Principal(Usuario usuario)
        {
            InitializeComponent();
            lblUsuario.Text = "¡Hola " + usuario.NombreUsuario + "!";
        }

        async private void BtnRelevamiento(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new BusquedaDistribuidor());
        }

        async private void BtnEstado_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new EstadoPage());
        }

		async private void SincronizarRegistros_Clicked(object sender, EventArgs e)
		{
            var asesoresService = new ErpAsesoresService(lblAsesoresCreate, lblAsesoresUpdate, lblAsesoresDelete);
            asesoresService.SynchronizeAsesores();
            var empresasService = new ErpEmpresasService(lblEmpresasCreate, lblEmpresasUpdate, lblEmpresasDelete);
            empresasService.SynchronizeEmpresas();
        }

        protected override void OnAppearing()
        {
        }
    }

}