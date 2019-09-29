using Relevamiento.Models;
using Relevamiento.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Relevamiento.Services.Data
{
    public class TbRequestDataService
    {
        private IGenericRepository<TbRequest> _genericRepository; 
        public TbRequestDataService()
        {
            _genericRepository = new GenericRepository<TbRequest>();
        }

        public TbRequest getByReqCodigo(string reqCodigo)
        {
            return _genericRepository.GetAll()
                .Where(tbRequest => tbRequest.req_codigo == reqCodigo).FirstOrDefault();
        }

        public bool isReqCodigoAlreadyInserted(string reqCodigo)
        {
            return getByReqCodigo(reqCodigo) != null ? true : false;
        }
    }
}
