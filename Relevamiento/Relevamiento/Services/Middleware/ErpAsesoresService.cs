using Relevamiento.Clases;
using Relevamiento.Repository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Relevamiento.Services.Middleware
{
    public class ErpAsesoresService 
    {
        private readonly IGenericServiceRepository _genericServiceRepository;

        public ErpAsesoresService()
        {
            _genericServiceRepository = new GenericServiceRepository();
        }

        public async Task<IEnumerable<ERP_ASESORES>> GetErpAsesoresAsync()
        {
            UriBuilder builder = new UriBuilder(ApiEndpoints.BaseApiUrl)
            {
                Path = ApiEndpoints.ErpAsesores
            };

            var erpAsesores = await _genericServiceRepository.GetAsync<List<ERP_ASESORES>>(builder.ToString());

            return erpAsesores;
        }

        public async Task<List<ERP_ASESORES>> PostGetAllErpAsesoresAsync()
        {
            UriBuilder builder = new UriBuilder(ApiEndpoints.BaseApiUrl)
            {
                Path = ApiEndpoints.ErpAsesores
            };

            var result = await _genericServiceRepository.PostGetAllAsync<List<ERP_ASESORES>>(builder.ToString(), new List<ERP_ASESORES>());

            return result;
        }
    }
}
