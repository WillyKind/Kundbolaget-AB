using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kundbolaget.Models.EntityModels;

namespace Kundbolaget.Interfaces
{
    interface IEntityRepository<T>
    {
        T[] GetEntities();
        T GetEntity(int id);
    }
}
