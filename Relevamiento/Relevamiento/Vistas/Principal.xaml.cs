using Relevamiento.Clases;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

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
    }
}