using Relevamiento.Models;
using Relevamiento.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Relevamiento.Services.Data
{
    public class SynchronizeDataService
    {
        private IGenericRepository<SynchronizeDataConfig> _genericRepository;

        public SynchronizeDataService()
        {
            _genericRepository = new GenericRepository<SynchronizeDataConfig>();
        }

        public SynchronizeDataConfig GetSynchronizeDataConfig()
        {
            var result = _genericRepository.GetAll().FirstOrDefault();

            return result;
        }
    }
}
