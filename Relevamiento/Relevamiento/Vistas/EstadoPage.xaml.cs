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
        public List<ItrisPlanillaEntity> Lista1 = new List<ItrisPlanillaEntity>();
        public EstadoPage()
        {
            InitializeComponent();

            List<TbRequest> respuesta = new List<TbRequest>();
            respuesta = Listas2();
            foreach (var obj in respuesta)
            {
                ItrisPlanillaEntity respuesta2 = JsonConvert.DeserializeObject<ItrisPlanillaEntity>(obj.req_json);
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
            await Navigation.PushAsync(new ComerciosRelevados(e.Item as ItrisPlanillaEntity));
        }
    }
}