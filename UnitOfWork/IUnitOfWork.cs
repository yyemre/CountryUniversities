using System;
using UnitOfWork.Repositories;

namespace UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IUniversityRepository UniversityRepository { get; }
        void Commit();
    }
}
