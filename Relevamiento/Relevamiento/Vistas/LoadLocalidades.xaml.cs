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
                //LOCALIDADES DATA 1
                ILocalidadesData localidadesData = new LocalidadesData1();
                var listaLocalidades = localidadesData.TraerLocalidades();

                //3639
                Debug.WriteLine($"{"LOCALIDADES_1: " + listaLocalidades.Count()}");

                if (countLocalidades < listaLocalidades.Count())
                {
                    using (SQLiteConnection conexion = new SQLiteConnection(App.RutaBD))
                    {
                        conexion.InsertAll(listaLocalidades);
                        Debug.WriteLine($"{"LOCALIDADES_1: " + conexion.Table<ERP_LOCALIDADES>().Count().ToString()}");
                    }
                }

                localidadesData = null;
                listaLocalidades = null;

                //LOCALIDADES DATA 2
                localidadesData = new LocalidadesData2();
                listaLocalidades = localidadesData.TraerLocalidades();

                //3633
                Debug.WriteLine($"{"LOCALIDADES_2: " + listaLocalidades.Count()}");

                if (countLocalidades < listaLocalidades.Count())
                {
                    using (SQLiteConnection conexion = new SQLiteConnection(App.RutaBD))
                    {
                        conexion.InsertAll(listaLocalidades);
                        Debug.WriteLine($"{"LOCALIDADES_2: " + conexion.Table<ERP_LOCALIDADES>().Count().ToString()}");
                    }
                }

                localidadesData = null;
                listaLocalidades = null;

                //LOCALIDADES DATA 3
                localidadesData = new LocalidadesData3();
                listaLocalidades = localidadesData.TraerLocalidades();

                //3637
                Debug.WriteLine($"{"LOCALIDADES_3: " + listaLocalidades.Count()}");

                if (countLocalidades < listaLocalidades.Count())
                {
                    using (SQLiteConnection conexion = new SQLiteConnection(App.RutaBD))
                    {
                        conexion.InsertAll(listaLocalidades);
                        Debug.WriteLine($"{"LOCALIDADES_3: " + conexion.Table<ERP_LOCALIDADES>().Count().ToString()}");
                    }
                }

                localidadesData = null;
                listaLocalidades = null;

                //LOCALIDADES DATA 4
                localidadesData = new LocalidadesData4();
                listaLocalidades = localidadesData.TraerLocalidades();

                //3636
                Debug.WriteLine($"{"LOCALIDADES_4: " + listaLocalidades.Count()}");

                if (countLocalidades < listaLocalidades.Count())
                {
                    using (SQLiteConnection conexion = new SQLiteConnection(App.RutaBD))
                    {
                        conexion.InsertAll(listaLocalidades);
                        Debug.WriteLine($"{"LOCALIDADES_4: " + conexion.Table<ERP_LOCALIDADES>().Count().ToString()}");
                    }
                }

                localidadesData = null;
                listaLocalidades = null;
            }

            //System.Threading.Thread.Sleep(5000);

            Device.BeginInvokeOnMainThread(async () => await Navigation.PushAsync(new Login()));
        }
    }
}