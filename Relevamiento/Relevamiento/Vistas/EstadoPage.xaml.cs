using Newtonsoft.Json;
using Relevamiento.Clases;
using Relevamiento.Models;
using SQLite;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Relevamiento.Vistas
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EstadoPage : ContentPage
    {
        public List<EstadosRel> Lista1 = new List<EstadosRel>();
        public EstadoPage()
        {
            InitializeComponent();

            List<TbRequest> respuesta = new List<TbRequest>();
            respuesta = Listas2();
            foreach (var obj in respuesta)
            {
                EstadosRel respuesta2 = JsonConvert.DeserializeObject<EstadosRel>(obj.req_json);
                respuesta2.req_estado = obj.req_estado;
                using (SQLiteConnection conexion = new SQLiteConnection(App.RutaBD))
                {
                    respuesta2.Empresa = conexion.Table<ERP_EMPRESAS>()
                       .Where(m => m.ID == respuesta2.relevamiento.FK_ERP_EMPRESAS).First();

                }
                Lista1.Add(respuesta2);
            }
            EstadoListView.ItemsSource = Lista1;
        }
        public List<TbRequest> Listas2()
        {
            List<TbRequest> respuesta = new List<TbRequest>();
            using (SQLiteConnection conexion = new SQLiteConnection(App.RutaBD))
            {
                respuesta = conexion.Query<TbRequest>("select * from TbRequest").ToList();

            }
            return respuesta;
        }
        private async void EstadoListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            //var asd = e.Item as ItrisComercioArticulo;
            await Navigation.PushAsync(new ComerciosRelevados(e.Item as EstadosRel));
        }
    }
}