using Relevamiento.Clases;
using Relevamiento.Repository;
using Relevamiento.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Relevamiento.Services.Middleware
{
    public class ErpEmpresasService
    {
        private readonly IGenericServiceRepository _genericServiceRepository;

        private readonly Label _lblEmpresasCreate;
        private readonly Label _lblEmpresasUpdate;
        private readonly Label _lblEmpresasDelete;

        public ErpEmpresasService()
        {
            _genericServiceRepository = new GenericServiceRepository();
        }

        public ErpEmpresasService(Label lblEmpresasCreate, Label lblEmpresasUpdate, Label lblEmpresasDelete)
        {
            _genericServiceRepository = new GenericServiceRepository();

            _lblEmpresasCreate = lblEmpresasCreate;
            _lblEmpresasUpdate = lblEmpresasUpdate;
            _lblEmpresasDelete = lblEmpresasDelete;
        }

        public async Task<EmpresasServiceModel> GetListEmpresas(List<ERP_EMPRESAS> lstEmpresas)
        {
            UriBuilder builder = new UriBuilder(ApiEndpoints.BaseApiUrl)
            {
                Path = ApiEndpoints.ErpAsesores
            };

            var result = await _genericServiceRepository.PostAsync<List<ERP_EMPRESAS>, EmpresasServiceModel>(builder.ToString(), lstEmpresas);

            return result;
        }

        public async void SynchronizeEmpresas()
        {
            var empresasServiceModel = await GetListEmpresas(CreateListAsesores());

            UpdateAsesores(empresasServiceModel);
            DeleteAsesores(empresasServiceModel);
            CreateAsesores(empresasServiceModel);
        }

        public void CreateAsesores(EmpresasServiceModel empresasServiceModel)
        {
            if (!isAlreadyCreated())
            {
                using (SQLite.SQLiteConnection conexion = new SQLite.SQLiteConnection(App.RutaBD))
                {
                    empresasServiceModel.listaCreate.ForEach(m => conexion.Insert(m));
                    _lblEmpresasCreate.Text = "Created";
                }
            }
        }

        public void UpdateAsesores(EmpresasServiceModel empresasServiceModel)
        {
            if (isAlreadyCreated())
            {
                if (empresasServiceModel.listaUpdate.Count() > 0)
                {
                    using (SQLite.SQLiteConnection conexion = new SQLite.SQLiteConnection(App.RutaBD))
                    {
                        empresasServiceModel.listaUpdate.ForEach(m => conexion.Update(m));
                        _lblEmpresasUpdate.Text = "Updated";
                    }
                }
            }
        }

        public void DeleteAsesores(EmpresasServiceModel empresasServiceModel)
        {
            if (isAlreadyCreated())
            {
                if (empresasServiceModel.listaDelete.Count() > 0)
                {
                    using (SQLite.SQLiteConnection conexion = new SQLite.SQLiteConnection(App.RutaBD))
                    {
                        empresasServiceModel.listaDelete.ForEach(m => conexion.Delete(m));
                        _lblEmpresasDelete.Text = "Deleted";
                    }
                }
            }
        }

        public bool isAlreadyCreated()
        {
            int countAsesores;

            using (SQLite.SQLiteConnection conexion = new SQLite.SQLiteConnection(App.RutaBD))
            {
                countAsesores = conexion.Table<ERP_EMPRESAS>().Count();
            }

            return countAsesores > 0 ? true : false;
        }

        public List<ERP_EMPRESAS> CreateListAsesores()
        {
            if (isAlreadyCreated())
            {
                using (SQLite.SQLiteConnection conexion = new SQLite.SQLiteConnection(App.RutaBD))
                {
                    return conexion.Table<ERP_EMPRESAS>().ToList();
                }
            }

            return new List<ERP_EMPRESAS>();
        }
    }
}
