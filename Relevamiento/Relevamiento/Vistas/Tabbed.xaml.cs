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
        public List<ItrisComercioEntity> listacom = new List<ItrisComercioEntity>();
        public List<ItrisRelevamientoArticuloEntity> relarts = new List<ItrisRelevamientoArticuloEntity>();
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

            if (ListaTotal.Count != 0)
            {
                ItrisRelevamientoArticuloEntity rla = new ItrisRelevamientoArticuloEntity();
                foreach (var obj in ListaTotal)
                {
					if (obj.Existe)
					{
						rla = new ItrisRelevamientoArticuloEntity()
						{
							FK_ARTICULOS = obj.Id,
							EXISTE = obj.Existe,
							PRECIO = obj.Precio
						};
						relarts.Add(rla);
					}
                }

            }

            var location = await Geolocation.GetLastKnownLocationAsync();
            if (location != null)
            {
                /*Guardar de alguna forma la ubicacion
                 * */
            }
            bool respuesta = await DisplayAlert("ATENCION", "Desea finalizar el relevamiento?", "Si", "No");
            if (respuesta)
            {
                ItrisComercioEntity c3 = new ItrisComercioEntity()
                {
					FK_TIP_COM = 2,
                    NOMBRE = localARelevar.NOMBRE,
                    CALLE = localARelevar.CALLE,
                    NUMERO = localARelevar.NUMERO,
                    FK_ERP_LOCALIDADES = 3,
                    FK_ERP_PROVINCIAS = 1,
                    LATITUD = "999999.332",
                    LONGITUD = "99999.963"
                };
                listacom.Add(c3);



                //ItrisRelevamientoArticuloEntity rla = new ItrisRelevamientoArticuloEntity()
                //{
                //    FK_ARTICULOS = 1,
                //    EXISTE = true,
                //    PRECIO = 55.55

                //};
                //relarts.Add(rla);

                //rla = new ItrisRelevamientoArticuloEntity()
                //{
                //    FK_ARTICULOS = 2,
                //    EXISTE = true,
                //    PRECIO = 55.55

                //};
                //relarts.Add(rla);
                //rla = new ItrisRelevamientoArticuloEntity()
                //{
                //    FK_ARTICULOS = 3,
                //    EXISTE = true,
                //    PRECIO = 55.55

                //};
                //relarts.Add(rla);
                //rla = new ItrisRelevamientoArticuloEntity()
                //{
                //    FK_ARTICULOS = 4,
                //    EXISTE = true,
                //    PRECIO = 55.55

                //};
                //relarts.Add(rla);
                //rla = new ItrisRelevamientoArticuloEntity()
                //{
                //    FK_ARTICULOS = 5,
                //    EXISTE = true,
                //    PRECIO = 55.55

                //};
                //relarts.Add(rla);
                //rla = new ItrisRelevamientoArticuloEntity()
                //{
                //    FK_ARTICULOS = 6,
                //    EXISTE = true,
                //    PRECIO = 55.55

                //};
                //relarts.Add(rla);
                //rla = new ItrisRelevamientoArticuloEntity()
                //{
                //    FK_ARTICULOS = 7,
                //    EXISTE = true,
                //    PRECIO = 55.55

                //};
                //relarts.Add(rla);

                //rla = new ItrisRelevamientoArticuloEntity()
                //{
                //    FK_ARTICULOS = 8,
                //    EXISTE = true,
                //    PRECIO = 55.55

                //};
                //relarts.Add(rla);
                //rla = new ItrisRelevamientoArticuloEntity()
                //{
                //    FK_ARTICULOS = 9,
                //    EXISTE = true,
                //    PRECIO = 55.55

                //};
                //relarts.Add(rla);


                ItrisComercioArticulo cs3 = new ItrisComercioArticulo()
                {
                    comercio = c3,
                    relevamientoArticulo = relarts,
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