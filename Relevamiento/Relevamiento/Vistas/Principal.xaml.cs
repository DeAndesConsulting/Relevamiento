using Relevamiento.Clases;
using Relevamiento.Services.Middleware;
using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Linq;

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
		async private void SincronizarRegistros_Clicked(object sender, EventArgs e)
		{
            //await Navigation.PushAsync(new VitRel());
            UpdateListAsesores();
        }

        protected override void OnAppearing()
        {
            CreateListAsesores();
            CreateListEmpresas();
        }

        private async void CreateListEmpresas()
        {
            var erpEmpresasService = new ErpEmpresasService();
            List<ERP_EMPRESAS> erpEmpresas = await erpEmpresasService.PostGetAllErpEmpresasAsync(new List<ERP_EMPRESAS>(), "listaCreate");

            using (SQLite.SQLiteConnection conexion = new SQLite.SQLiteConnection(App.RutaBD))
            {
                var countEmpresas = conexion.Table<ERP_EMPRESAS>().Count();

                if (countEmpresas == 0)
                    erpEmpresas.ForEach(empresa => conexion.Insert(empresa));
            }
        }

        private async void CreateListAsesores()
        {
            var erpAsesoresService = new ErpAsesoresService();
            List<ERP_ASESORES> erpAsesores = await erpAsesoresService.PostGetAllErpAsesoresAsync(new List<ERP_ASESORES>(), "listaCreate");

            using (SQLite.SQLiteConnection conexion = new SQLite.SQLiteConnection(App.RutaBD))
            {
                var countAsesores = conexion.Table<ERP_ASESORES>().Count();

                if (countAsesores == 0)
                    erpAsesores.ForEach(asesor => conexion.Insert(asesor));
            }
        }

        private async void UpdateListAsesores()
        {
            List<ERP_ASESORES> lstAsesores;
            using (SQLite.SQLiteConnection conexion = new SQLite.SQLiteConnection(App.RutaBD))
            {
                lstAsesores = conexion.Table<ERP_ASESORES>().ToList();
            }

            var erpAsesoresService = new ErpAsesoresService();
            List<ERP_ASESORES> erpAsesores = await erpAsesoresService.PostGetAllErpAsesoresAsync(lstAsesores, "listaUpdate");

            if (erpAsesores.Count() > 0)
            {
                using (SQLite.SQLiteConnection conexion = new SQLite.SQLiteConnection(App.RutaBD))
                {
                    erpAsesores.ForEach(asesor => conexion.Update(asesor));
                }
            }
        }
    }

}