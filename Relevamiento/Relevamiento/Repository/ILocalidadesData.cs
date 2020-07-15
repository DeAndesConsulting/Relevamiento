using Relevamiento.Clases;
using System.Collections.Generic;

namespace Relevamiento.Repository
{
    public interface ILocalidadesData
    {
        List<ERP_LOCALIDADES> TraerLocalidades();
    }
}
