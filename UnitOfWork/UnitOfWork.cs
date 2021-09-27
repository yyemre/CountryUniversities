using System;
using System.Data;
using System.Data.SqlClient;
using UnitOfWork.Repositories;
using Npgsql;

namespace UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private IDbConnection _connection;
        private IDbTransaction _transaction;
        private IUniversityRepository _universityRepository;
        private bool _disposed;
        public UnitOfWork(string connectionString)
        {
            /* Bağlantı türü connection string yapısına göre otomatik olarak seçilmektedir. */
            try
            {
                /* MSSQL Bağlantısı */
                _connection = new SqlConnection(connectionString);

            }
            catch
            {
                /* PostgreSQL Bağlantısı */
                _connection = new NpgsqlConnection(connectionString);
            }
            _connection.Open();
            _transaction = _connection.BeginTransaction();
        }

        public IUniversityRepository UniversityRepository
        {
            get { return _universityRepository ?? (_universityRepository = new UniversityRepository(_transaction)); }
        }
        public void Commit()
        {
            try
            {
                _transaction.Commit();
            }
            catch
            {
                _transaction.Rollback();
                throw;
            }
            finally
            {
                /* Komut çalıştırıldıktan sonra yeni komutlar için bağlantının yenilenmesi ve eski verilerin temizlenmesi. */
                _transaction.Dispose();
                _transaction = _connection.BeginTransaction();
                resetRepositories();
            }
        }
        private void resetRepositories()
        {
            _universityRepository = null;
        }

        /* Using deyimi içerisinde kullandığımız için temizleme işlemini yapmamız gerekiyor. */
        public void Dispose()
        {
            dispose(true);
            /* Temizleme işlemini yaptıktan sonra bu nesnenin kalıcı olması performans sağlıyor. */
            GC.SuppressFinalize(this);
        }

        private void dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    if (_transaction != null)
                    {
                        _transaction.Dispose();
                        _transaction = null;
                    }
                    if (_connection != null)
                    {
                        _connection.Dispose();
                        _connection = null;
                    }
                }
                _disposed = true;
            }
        }
    }
}
