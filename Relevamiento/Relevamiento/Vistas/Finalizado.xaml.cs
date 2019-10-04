/*
 * Pagina temporal, no definitiva, se va a evaluar si queda o que accion se toma una vez finalizado el relevamiento
 * Terminado
 */


using Newtonsoft.Json;
using Relevamiento.Clases;
using Relevamiento.Models;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Relevamiento.Vistas
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Finalizado : ContentPage
    {
        public Finalizado(TbRequest tbRequests)
        {
            InitializeComponent();
            ItrisPlanillaEntity respuesta2 = JsonConvert.DeserializeObject<ItrisPlanillaEntity>(tbRequests.req_json);
            LblDistribuidor.Text = "Distribuidor:" +respuesta2.relevamiento.FK_ERP_EMPRESAS;
            LblLocal.Text = "Fecha" + respuesta2.relevamiento.FECHA;
            if (!tbRequests.req_estado)
            {
                LblEmail.Text = "pendiente";
            }
            else LblEmail.Text = "enviado";
        }
        
        private void BtnVolver_Clicked(object sender, EventArgs e)
        {
            PopUntilDestination(typeof(Principal));
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