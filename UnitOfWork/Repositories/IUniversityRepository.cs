using System.Collections.Generic;
using UnitOfWork.Entities;

namespace UnitOfWork.Repositories
{
    public interface IUniversityRepository
    {
        void Add(University entity);
        IEnumerable<UniversityRead> All();
        IEnumerable<UniversityRead> FindByName(string name);
        void RemoveByName(string name);
        void Update(University entity);
        void DeleteTable();
            
    }
}
