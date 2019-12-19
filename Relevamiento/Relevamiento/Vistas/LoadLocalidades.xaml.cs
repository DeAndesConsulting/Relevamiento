using Relevamiento.Clases;
using SQLite;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Relevamiento.Vistas
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoadLocalidades : ContentPage
    {
        public LoadLocalidades()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            aiLogin.IsVisible = true;
            aiLogin.IsRunning = true;
            aiLogin.IsEnabled = true;
            lblMensaje.Text = "Insertando Localidades... \nEste proceso puede demorar algunos minutos. \nAsegúrese de tener una buena conexión a internet.";

            Task.Run(() => InsertLocalidades());
        }

        private void InsertLocalidades()
        {
            var countLocalidades = 0;
            using (SQLiteConnection conexion = new SQLiteConnection(App.RutaBD))
            {
                //21683
                countLocalidades = conexion.Table<ERP_LOCALIDADES>().Count();
            }

            if (countLocalidades < 21683)
            {
                LocalidadesData localidadesData = new LocalidadesData();
                var listaLocalidades = localidadesData.TraerLocalidades();

                using (SQLiteConnection conexion = new SQLiteConnection(App.RutaBD))
                {
                    conexion.InsertAll(listaLocalidades);
                    Debug.WriteLine($"{"LOCALIDADES: " + conexion.Table<ERP_LOCALIDADES>().Count().ToString()}");
                }
            }

            //System.Threading.Thread.Sleep(5000);

            Device.BeginInvokeOnMainThread(async () => await Navigation.PushAsync(new Login()));
        }
    }
}