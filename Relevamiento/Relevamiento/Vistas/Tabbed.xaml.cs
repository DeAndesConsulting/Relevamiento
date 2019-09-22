/*
 * Falta guardar los datos de las listas de los productos en la base de datos(ya modificado)
 * que tienen que contener tambien la id del distribuidor, la id del local, la id del vendedor y 
 * el estado del relevamiento, por default va false, si se recibe un 200 cuando se quiere guardar en la nube sepone true y listo
 * Ver en que formato se va a guardar la longitud y la latitud de la geolocalizacion -------> string
 * Para el datetime.now mirar en https://stackoverflow.com/questions/34235152/xamarin-forms-how-do-i-get-the-entered-date-from-a-datepicker-to-a-format-of-yo
 * 
 */
using Newtonsoft.Json;
using Relevamiento.Clases;
using System;
using System.Collections.Generic;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Relevamiento.Vistas
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Tabbed : TabbedPage
    {
        public _COMERCIO localARelevar;
        public Tabbed(_COMERCIO nuevolocal)
        {
            InitializeComponent();
            localARelevar = nuevolocal;
        }

        async private void BtnFinalizar_Clicked(object sender, EventArgs e)
        {
            List<ListaProductos> ListaTotal = Aguas.listaAguas;
            ListaTotal.AddRange(Fernet.listaFernets);
            ListaTotal.AddRange(Gaseosas.listaGaseosas);
            ListaTotal.AddRange(Jugos.listaJugos);
            ListaTotal.AddRange(Saborizadas.listaSaborizadas);
            ListaTotal.AddRange(Sodas.listaSodas);

            var location = await Geolocation.GetLastKnownLocationAsync();
            if (location != null)
            {
                /*Guardar de alguna forma la ubicacion
                 * */
            }
            bool respuesta = await DisplayAlert("ATENCION", "Desea finalizar el relevamiento?", "Si", "No");
            if (respuesta)
            {
                ComerciO c3 = new ComerciO()
                {
                    fK_TIP_COM = 2,
                    nombre = localARelevar.NOMBRE,
                    calle = localARelevar.CALLE,
                    numero = localARelevar.NUMERO,
                    fK_ERP_LOCALIDADES = 3,
                    fK_ERP_PROVINCIAS = 1,
                    latitud = "999999.332",
                    longitud = "99999.963"
                };
                App.listacom.Add(c3);



                RelevamientoArticulO rla = new RelevamientoArticulO()
                {
                    fK_ARTICULOS = 1,
                    existe = true,
                    precio = 55.55

                };
                App.relarts.Add(rla);

                rla = new RelevamientoArticulO()
                {
                    fK_ARTICULOS = 2,
                    existe = true,
                    precio = 55.55

                };
                App.relarts.Add(rla);
                rla = new RelevamientoArticulO()
                {
                    fK_ARTICULOS = 3,
                    existe = true,
                    precio = 55.55

                };
                App.relarts.Add(rla);
                rla = new RelevamientoArticulO()
                {
                    fK_ARTICULOS = 4,
                    existe = true,
                    precio = 55.55

                };
                App.relarts.Add(rla);
                rla = new RelevamientoArticulO()
                {
                    fK_ARTICULOS = 5,
                    existe = true,
                    precio = 55.55

                };
                App.relarts.Add(rla);
                rla = new RelevamientoArticulO()
                {
                    fK_ARTICULOS = 6,
                    existe = true,
                    precio = 55.55

                };
                App.relarts.Add(rla);
                rla = new RelevamientoArticulO()
                {
                    fK_ARTICULOS = 7,
                    existe = true,
                    precio = 55.55

                };
                App.relarts.Add(rla);

                rla = new RelevamientoArticulO()
                {
                    fK_ARTICULOS = 8,
                    existe = true,
                    precio = 55.55

                };
                App.relarts.Add(rla);
                rla = new RelevamientoArticulO()
                {
                    fK_ARTICULOS = 9,
                    existe = true,
                    precio = 55.55

                };
                App.relarts.Add(rla);


                Comercios cs3 = new Comercios()
                {
                    comercio = c3,
                    relevamientoArticulo = App.relarts,
                };
                App.comercios.Add(cs3);




                await Navigation.PushAsync(new Comercio(App.distribuidorseleccionado));

                /*
                Relevado localRelevado = new Relevado();
                localRelevado.Longitud = location.Latitude.ToString();
                localRelevado.Latitud = location.Longitude.ToString();
                localRelevado.NombreDistribuidor = localARelevar.Distribuidor;
                localRelevado.TipoLocal = localARelevar.TipoLocal;
                localRelevado.Direccion = localARelevar.Direccion;
                localRelevado.Provincia = localARelevar.Provincia;
                localRelevado.FechaRelevado = DateTime.Now;
                localRelevado.Status = false;

                using (SQLite.SQLiteConnection conexion = new SQLite.SQLiteConnection(App.RutaBD))
                {
                    var result = conexion.Insert(localRelevado);
                    if (result > 0)
                    {
                        await DisplayAlert("ATENCION", "Relevamiento finalizado", "Ok");
                    }
                    else await DisplayAlert("ERROR", "Intente nuevamente", "Ok");
                }

                await Navigation.PushAsync(new Finalizado(localRelevado));
            */
            }
        }
        async private void BtnCancelar_Clicked(object sender, EventArgs e)
        {
            bool respuesta = await DisplayAlert("ATENCION", "Desea cancelar el relevamiento?", "Si", "No");
            if (respuesta)
            {
                PopUntilDestination(typeof(Comercio));
            }
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