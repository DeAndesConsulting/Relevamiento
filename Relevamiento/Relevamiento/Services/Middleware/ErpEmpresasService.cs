using Relevamiento.Clases;
using Relevamiento.Repository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Relevamiento.Services.Middleware
{
    public class ErpEmpresasService
    {
        private readonly IGenericServiceRepository _genericServiceRepository;

        public ErpEmpresasService()
        {
            _genericServiceRepository = new GenericServiceRepository();
        }

        public async Task<List<ERP_EMPRESAS>> PostGetAllErpEmpresasAsync(List<ERP_EMPRESAS> lstEmpresas, string operationType)
        {
            UriBuilder builder = new UriBuilder(ApiEndpoints.BaseApiUrl)
            {
                Path = ApiEndpoints.ErpEmpresas
            };

            var result = await _genericServiceRepository.PostGetAllAsync<List<ERP_EMPRESAS>>(builder.ToString(), lstEmpresas, operationType);

            return result;
        }
    }
}
