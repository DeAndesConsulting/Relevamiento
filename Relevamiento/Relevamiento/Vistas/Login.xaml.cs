using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Relevamiento.Clases;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using System.Threading.Tasks;

namespace Relevamiento.Vistas
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Login: ContentPage
    {
        public Login()
        {
            InitializeComponent();
        }

        void ObtenerImei(object sender, EventArgs e)
        {
            ////Verify Permission
            //var status = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Phone);
            //if (status != PermissionStatus.Granted)
            //{
            //    var results = await CrossPermissions.Current.RequestPermissionsAsync(Permission.Phone);
            //    //Best practice to always check that the key exists
            //    if (results.ContainsKey(Permission.Phone))
            //        status = results[Permission.Phone];
            //}

            //Get Imei
            DevieId.Text = "IMEI = " + DependencyService.Get<IServiceImei>().GetImei();
        }


        async Task<string> GetImei()
        {
            //Verify Permission
            var status = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Phone);
            if (status != PermissionStatus.Granted)
            {
                var results = await CrossPermissions.Current.RequestPermissionsAsync(Permission.Phone);
                //Best practice to always check that the key exists
                if (results.ContainsKey(Permission.Phone))
                    status = results[Permission.Phone];
            }
            //Get Imei
            string imei = DependencyService.Get<IServiceImei>().GetImei();
            return imei;
        }
        async void Button_Clicked(object sender, EventArgs e)
        {
            Usuario User = new Usuario();
            if (string.IsNullOrEmpty(entryName.Text))
            {
                lblusufail.Text = "Debe ingresar un usuario";
                lblusufail.IsVisible = true;
            }
            if (string.IsNullOrEmpty(entryName.Text))
            {
                lblpwfail.Text = "Debe ingresar una contraseña";
                lblusufail.IsVisible = true;
            }
            if (entryName.Text == "Admin" && entryPass.Text == "a" )
            {
                //await Navigation.PushAsync(new Busqueda());
                User.NombreUsuario = entryName.Text;
                User.NumeroImei = await GetImei();
                await Navigation.PushAsync(new Principal(User));
            }
        }

        private void EntryName_TextChanged(object sender, TextChangedEventArgs e)
        {
            lblusufail.IsVisible = false;
        }

        private void EntryPass_TextChanged(object sender, TextChangedEventArgs e)
        {
            lblpwfail.IsVisible = false;
        }
    }
}