using Relevamiento.Clases;
using Relevamiento.Services.Middleware;
using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel;

namespace Relevamiento.Vistas
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Principal : ContentPage, INotifyPropertyChanged
    {
        public Principal(Usuario usuario)
        {
            InitializeComponent();
            lblUsuario.Text = "¡Hola " + usuario.NombreUsuario + "!";

            BindingContext = this;
        }

        private bool _IsBusy;
        public bool IsBusy
        {
            get => _IsBusy;
            set
            {
                _IsBusy = value;
                OnPropertyChanged("IsBusy");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            var handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));
        }

        async private void BtnRelevamiento_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new BusquedaDistribuidor());
        }

        async private void BtnEstado_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new EstadoPage());
        }

        async private void SincronizarRegistros_Clicked(object sender, EventArgs e)
        {
            if (CheckNetworkState.hasConnectivity)
            {
                if (await DisplayAlert("Aviso", "Esta apunto de sincronizar los datos, ¿desea continuar?", "Si", "No"))
                {
                    try
                    {
                        IsBusy = true;

                        var articulosService = new ArticulosService(lblArticulosCreate, lblArticulosUpdate, lblArticulosDelete);
                        await articulosService.SynchronizeArticulos();
                        var asesoresService = new ErpAsesoresService(lblAsesoresCreate, lblAsesoresUpdate, lblAsesoresDelete);
                        await asesoresService.SynchronizeAsesores();
                        var empresasService = new ErpEmpresasService(lblEmpresasCreate, lblEmpresasUpdate, lblEmpresasDelete);
                        await empresasService.SynchronizeEmpresas();
                        var localidadesService = new ErpLocalidadesService(lblLocalidadesCreate, lblLocalidadesUpdate, lblLocalidadesDelete);
                        await localidadesService.SynchronizeLocalidades();

                        IsBusy = false;
                    }
                    catch (Exception)
                    {
                        throw;
                    }

                }
            }
            else
            {
                await DisplayAlert("Aviso", "Sin conexion a internet, no se pueden sincronizar los registros", "Ok");
            }
        }

        protected override void OnAppearing()
        {
        }
    }

}