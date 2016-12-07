using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kundbolaget.EntityFramework.Context;
using Kundbolaget.Models.EntityModels;

namespace Kundbolaget.Interfaces
{
    interface IEntityRepository<T> : IDisposable
    {
        T[] GetEntities();
        T GetEntity(int id);
        void CreateEntity(T newEntity);
        void DeleteEntity(int id);
        void UpdateEntity(T updatedEntity);
    }
}
