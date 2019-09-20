using Relevamiento.Clases;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Relevamiento.Vistas
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SodasRele : ContentPage
    {
        public static List<ListaProductos> listaSodas;
        public SodasRele()
        {
            InitializeComponent();
            int Sodas = 3;

                listaSodas = TraerProductos();
                SodasListView.ItemsSource = listaSodas.Where(c => c.TipoProducto.ToString().ToLower().Contains(Sodas.ToString()));
        }
        public List<ListaProductos> TraerProductos()
        {
            List<ListaProductos> listaProductos = new List<ListaProductos>();
            ListaProductos d2 = new ListaProductos()
            {
                Id = 1,
                Producto = "Manaos 300",
                Precio = 0,
                Existe = false,
                TipoProducto = 0,
            };
            listaProductos.Add(d2);

            d2 = new ListaProductos()
            {
                Id = 1,
                Producto = "Manaos 1250",
                Precio = 0,
                Existe = true,
                TipoProducto = 0,
            };
            listaProductos.Add(d2);

            d2 = new ListaProductos()
            {
                Id = 1,
                Producto = "Manaos 2250",
                Precio = 0,
                Existe = true,
                TipoProducto = 0,
            };
            listaProductos.Add(d2);

            d2 = new ListaProductos()
            {
                Id = 1,
                Producto = "Manaos 600",
                Precio = 0,
                Existe = false,
                TipoProducto = 0,
            };
            listaProductos.Add(d2);

            d2 = new ListaProductos()
            {
                Id = 1,
                Producto = "Manaos sin Azucar",
                Precio = 0,
                Existe = false,
                TipoProducto = 0,
            };
            listaProductos.Add(d2);

            d2 = new ListaProductos()
            {
                Id = 1,
                Producto = "Cantidad de sabores 2250",
                Precio = 5,
                Existe = true,
                TipoProducto = 0,
            };
            listaProductos.Add(d2);

            d2 = new ListaProductos()
            {
                Id = 1,
                Producto = "Beach",
                Precio = 0,
                Existe = false,
                TipoProducto = 0,
            };
            listaProductos.Add(d2);

            d2 = new ListaProductos()
            {
                Id = 1,
                Producto = "Cabalgata",
                Precio = 0,
                Existe = false,
                TipoProducto = 0,
            };
            listaProductos.Add(d2);

            d2 = new ListaProductos()
            {
                Id = 1,
                Producto = "Caribe",
                Precio = 0,
                Existe = false,
                TipoProducto = 0,
            };
            listaProductos.Add(d2);

            d2 = new ListaProductos()
            {
                Id = 1,
                Producto = "Cimes",
                Precio = 0,
                Existe = true,
                TipoProducto = 0,
            };
            listaProductos.Add(d2);

            d2 = new ListaProductos()
            {
                Id = 1,
                Producto = "Coca Cola",
                Precio = 0,
                Existe = true,
                TipoProducto = 0,
            };
            listaProductos.Add(d2);

            d2 = new ListaProductos()
            {
                Id = 1,
                Producto = "Cordoba",
                Precio = 0,
                Existe = true,
                TipoProducto = 0,
            };
            listaProductos.Add(d2);

            d2 = new ListaProductos()
            {
                Id = 1,
                Producto = "Cunnington",
                Precio = 0,
                Existe = true,
                TipoProducto = 0,
            };
            listaProductos.Add(d2);

            d2 = new ListaProductos()
            {
                Id = 1,
                Producto = "Doble Cola",
                Precio = 0,
                Existe = false,
                TipoProducto = 0,
            };
            listaProductos.Add(d2);

            d2 = new ListaProductos()
            {
                Id = 1,
                Producto = "Frutafiel",
                Precio = 0,
                Existe = false,
                TipoProducto = 0,
            };
            listaProductos.Add(d2);

            d2 = new ListaProductos()
            {
                Id = 1,
                Producto = "Goliat",
                Precio = 0,
                Existe = false,
                TipoProducto = 0,
            };
            listaProductos.Add(d2);

            d2 = new ListaProductos()
            {
                Id = 1,
                Producto = "Marinaro",
                Precio = 0,
                Existe = false,
                TipoProducto = 0,
            };
            listaProductos.Add(d2);

            d2 = new ListaProductos()
            {
                Id = 1,
                Producto = "Mocoreta",
                Precio = 0,
                Existe = false,
                TipoProducto = 0,
            };
            listaProductos.Add(d2);

            d2 = new ListaProductos()
            {
                Id = 1,
                Producto = "Pepsi Cola",
                Precio = 0,
                Existe = true,
                TipoProducto = 0,
            };
            listaProductos.Add(d2);

            d2 = new ListaProductos()
            {
                Id = 1,
                Producto = "Prity",
                Precio = 0,
                Existe = false,
                TipoProducto = 0,
            };
            listaProductos.Add(d2);

            d2 = new ListaProductos()
            {
                Id = 1,
                Producto = "Rafting",
                Precio = 0,
                Existe = false,
                TipoProducto = 0,
            };
            listaProductos.Add(d2);

            d2 = new ListaProductos()
            {
                Id = 1,
                Producto = "Secco",
                Precio = 0,
                Existe = true,
                TipoProducto = 0,
            };
            listaProductos.Add(d2);

            d2 = new ListaProductos()
            {
                Id = 1,
                Producto = "Talca",
                Precio = 0,
                Existe = false,
                TipoProducto = 0,
            };
            listaProductos.Add(d2);

            d2 = new ListaProductos()
            {
                Id = 1,
                Producto = "Torasso",
                Precio = 0,
                Existe = false,
                TipoProducto = 0,
            };
            listaProductos.Add(d2);

            d2 = new ListaProductos()
            {
                Id = 1,
                Producto = "Otras",
                Precio = 0,
                Existe = true,
                TipoProducto = 0,
            };
            listaProductos.Add(d2);

            d2 = new ListaProductos()
            {
                Id = 1,
                Producto = "Villamanaos 2000",
                Precio = 0,
                Existe = true,
                TipoProducto = 1,
            };
            listaProductos.Add(d2);

            d2 = new ListaProductos()
            {
                Id = 1,
                Producto = "Villamanaos 6000",
                Precio = 0,
                Existe = false,
                TipoProducto = 1,
            };
            listaProductos.Add(d2);

            d2 = new ListaProductos()
            {
                Id = 1,
                Producto = "Agua de misiones",
                Precio = 0,
                Existe = false,
                TipoProducto = 1,
            };
            listaProductos.Add(d2);

            d2 = new ListaProductos()
            {
                Id = 1,
                Producto = "Celier",
                Precio = 0,
                Existe = false,
                TipoProducto = 1,
            };
            listaProductos.Add(d2);

            d2 = new ListaProductos()
            {
                Id = 1,
                Producto = "Cimes",
                Precio = 0,
                Existe = false,
                TipoProducto = 1,
            };
            listaProductos.Add(d2);

            d2 = new ListaProductos()
            {
                Id = 1,
                Producto = "Exaltacion de la cruz",
                Precio = 0,
                Existe = false,
                TipoProducto = 1,
            };
            listaProductos.Add(d2);

            d2 = new ListaProductos()
            {
                Id = 1,
                Producto = "Glaciar",
                Precio = 0,
                Existe = true,
                TipoProducto = 1,
            };
            listaProductos.Add(d2);

            d2 = new ListaProductos()
            {
                Id = 1,
                Producto = "Kin",
                Precio = 0,
                Existe = false,
                TipoProducto = 1,
            };
            listaProductos.Add(d2);

            d2 = new ListaProductos()
            {
                Id = 1,
                Producto = "Sierra de los padres",
                Precio = 0,
                Existe = true,
                TipoProducto = 1,
            };
            listaProductos.Add(d2);

            d2 = new ListaProductos()
            {
                Id = 1,
                Producto = "Villa del sur",
                Precio = 0,
                Existe = true,
                TipoProducto = 1,
            };
            listaProductos.Add(d2);

            d2 = new ListaProductos()
            {
                Id = 1,
                Producto = "Villavicencio",
                Precio = 0,
                Existe = true,
                TipoProducto = 1,
            };
            listaProductos.Add(d2);

            d2 = new ListaProductos()
            {
                Id = 1,
                Producto = "Otras",
                Precio = 0,
                Existe = false,
                TipoProducto = 1,
            };
            listaProductos.Add(d2);

            d2 = new ListaProductos()
            {
                Id = 1,
                Producto = "Placer 1500",
                Precio = 0,
                Existe = true,
                TipoProducto = 2,
            };
            listaProductos.Add(d2);

            d2 = new ListaProductos()
            {
                Id = 1,
                Producto = "Placer 500",
                Precio = 0,
                Existe = false,
                TipoProducto = 2,
            };
            listaProductos.Add(d2);

            d2 = new ListaProductos()
            {
                Id = 1,
                Producto = "Baggio Fresh",
                Precio = 0,
                Existe = false,
                TipoProducto = 2,
            };
            listaProductos.Add(d2);

            d2 = new ListaProductos()
            {
                Id = 1,
                Producto = "Levite",
                Precio = 0,
                Existe = true,
                TipoProducto = 2,
            };
            listaProductos.Add(d2);

            d2 = new ListaProductos()
            {
                Id = 1,
                Producto = "Cimes",
                Precio = 0,
                Existe = false,
                TipoProducto = 2,
            };
            listaProductos.Add(d2);

            d2 = new ListaProductos()
            {
                Id = 1,
                Producto = "Aquarius",
                Precio = 0,
                Existe = true,
                TipoProducto = 2,
            };
            listaProductos.Add(d2);

            d2 = new ListaProductos()
            {
                Id = 1,
                Producto = "Bio Balance",
                Precio = 0,
                Existe = false,
                TipoProducto = 2,
            };
            listaProductos.Add(d2);

            d2 = new ListaProductos()
            {
                Id = 1,
                Producto = "Celier",
                Precio = 0,
                Existe = false,
                TipoProducto = 2,
            };
            listaProductos.Add(d2);

            d2 = new ListaProductos()
            {
                Id = 1,
                Producto = "Livra",
                Precio = 0,
                Existe = false,
                TipoProducto = 2,
            };
            listaProductos.Add(d2);

            d2 = new ListaProductos()
            {
                Id = 1,
                Producto = "Ser",
                Precio = 0,
                Existe = false,
                TipoProducto = 2,
            };
            listaProductos.Add(d2);

            d2 = new ListaProductos()
            {
                Id = 1,
                Producto = "Style",
                Precio = 0,
                Existe = false,
                TipoProducto = 2,
            };
            listaProductos.Add(d2);

            d2 = new ListaProductos()
            {
                Id = 1,
                Producto = "Otras",
                Precio = 0,
                Existe = true,
                TipoProducto = 2,
            };
            listaProductos.Add(d2);

            d2 = new ListaProductos()
            {
                Id = 1,
                Producto = "Manaos 2000",
                Precio = 0,
                Existe = true,
                TipoProducto = 3,
            };
            listaProductos.Add(d2);

            d2 = new ListaProductos()
            {
                Id = 1,
                Producto = "Cimes",
                Precio = 0,
                Existe = false,
                TipoProducto = 3,
            };
            listaProductos.Add(d2);

            d2 = new ListaProductos()
            {
                Id = 1,
                Producto = "Aubal",
                Precio = 0,
                Existe = false,
                TipoProducto = 3,
            };
            listaProductos.Add(d2);

            d2 = new ListaProductos()
            {
                Id = 1,
                Producto = "Exaltacion de la cruz",
                Precio = 0,
                Existe = false,
                TipoProducto = 3,
            };
            listaProductos.Add(d2);

            d2 = new ListaProductos()
            {
                Id = 1,
                Producto = "Ives",
                Precio = 0,
                Existe = true,
                TipoProducto = 3,
            };
            listaProductos.Add(d2);

            d2 = new ListaProductos()
            {
                Id = 1,
                Producto = "Vitalisima",
                Precio = 0,
                Existe = false,
                TipoProducto = 3,
            };
            listaProductos.Add(d2);

            d2 = new ListaProductos()
            {
                Id = 1,
                Producto = "Sierra de los padres",
                Precio = 0,
                Existe = false,
                TipoProducto = 3,
            };
            listaProductos.Add(d2);

            d2 = new ListaProductos()
            {
                Id = 1,
                Producto = "Torasso",
                Precio = 0,
                Existe = false,
                TipoProducto = 3,
            };
            listaProductos.Add(d2);

            d2 = new ListaProductos()
            {
                Id = 1,
                Producto = "Saldan",
                Precio = 0,
                Existe = false,
                TipoProducto = 3,
            };
            listaProductos.Add(d2);

            d2 = new ListaProductos()
            {
                Id = 1,
                Producto = "Nihull",
                Precio = 0,
                Existe = false,
                TipoProducto = 3,
            };
            listaProductos.Add(d2);

            d2 = new ListaProductos()
            {
                Id = 1,
                Producto = "Mass",
                Precio = 0,
                Existe = false,
                TipoProducto = 3,
            };
            listaProductos.Add(d2);

            d2 = new ListaProductos()
            {
                Id = 1,
                Producto = "Penty",
                Precio = 0,
                Existe = false,
                TipoProducto = 3,
            };
            listaProductos.Add(d2);

            d2 = new ListaProductos()
            {
                Id = 1,
                Producto = "Otras",
                Precio = 0,
                Existe = false,
                TipoProducto = 3,
            };
            listaProductos.Add(d2);

            d2 = new ListaProductos()
            {
                Id = 1,
                Producto = "Pindapoy",
                Precio = 0,
                Existe = true,
                TipoProducto = 4,
            };
            listaProductos.Add(d2);

            d2 = new ListaProductos()
            {
                Id = 1,
                Producto = "Baggio Pronto",
                Precio = 0,
                Existe = true,
                TipoProducto = 4,
            };
            listaProductos.Add(d2);

            d2 = new ListaProductos()
            {
                Id = 1,
                Producto = "Cepita",
                Precio = 0,
                Existe = true,
                TipoProducto = 4,
            };
            listaProductos.Add(d2);

            d2 = new ListaProductos()
            {
                Id = 1,
                Producto = "Bio Frut",
                Precio = 0,
                Existe = false,
                TipoProducto = 4,
            };
            listaProductos.Add(d2);

            d2 = new ListaProductos()
            {
                Id = 1,
                Producto = "Capilla mendocina",
                Precio = 0,
                Existe = false,
                TipoProducto = 4,
            };
            listaProductos.Add(d2);

            d2 = new ListaProductos()
            {
                Id = 1,
                Producto = "Tutti",
                Precio = 0,
                Existe = false,
                TipoProducto = 4,
            };
            listaProductos.Add(d2);

            d2 = new ListaProductos()
            {
                Id = 1,
                Producto = "Puro Sol",
                Precio = 0,
                Existe = false,
                TipoProducto = 4,
            };
            listaProductos.Add(d2);

            d2 = new ListaProductos()
            {
                Id = 1,
                Producto = "Citric",
                Precio = 0,
                Existe = false,
                TipoProducto = 4,
            };
            listaProductos.Add(d2);

            d2 = new ListaProductos()
            {
                Id = 1,
                Producto = "Otros",
                Precio = 0,
                Existe = false,
                TipoProducto = 4,
            };
            listaProductos.Add(d2);

            d2 = new ListaProductos()
            {
                Id = 1,
                Producto = "F-Nandito VII",
                Precio = 0,
                Existe = false,
                TipoProducto = 5,
            };
            listaProductos.Add(d2);

            d2 = new ListaProductos()
            {
                Id = 1,
                Producto = "Fernando",
                Precio = 0,
                Existe = true,
                TipoProducto = 5,
            };
            listaProductos.Add(d2);
            return listaProductos;
        }
    }
}