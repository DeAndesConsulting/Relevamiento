/*
 * Pagina temporal, no definitiva, se va a evaluar si queda o que accion se toma una vez finalizado el relevamiento
 * Terminado
 */


using Relevamiento.Clases;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Relevamiento.Vistas
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Finalizado : ContentPage
    {
        public Finalizado(Relevado LocalRelevado)
        {
            InitializeComponent();
            LblDistribuidor.Text = "Distribuidor:" + LocalRelevado.NombreDistribuidor;
            LblLocal.Text = "Direccion:" + LocalRelevado.Direccion;
        }

        private void BtnVolver_Clicked(object sender, EventArgs e)
        {
            PopUntilDestination(typeof(Comercio));
        }

        void PopUntilDestination(Type DestinationPage)
        {
            int LeastFoundIndex = 0;
            int PagesToRemove = 0;

            for (int index = Navigation.NavigationStack.Count - 2; index > 0; index--)
            {
                if (Navigation.NavigationStack[index].GetType().Equals(DestinationPage))
                {
                    break;
                }
                else
                {
                    LeastFoundIndex = index;
                    PagesToRemove++;
                }
            }

            for (int index = 0; index < PagesToRemove; index++)
            {
                Navigation.RemovePage(Navigation.NavigationStack[LeastFoundIndex]);
            }

            Navigation.PopAsync();
        }

    }
}