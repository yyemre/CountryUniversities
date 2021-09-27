using System;
using System.Collections.Generic;
using System.Data;
using UnitOfWork.Entities;
using Dapper;

namespace UnitOfWork.Repositories
{
    internal class UniversityRepository : BaseRepository, IUniversityRepository
    {
        public UniversityRepository( IDbTransaction transaction)
        : base(transaction)
        {
        }
        public char type;
        void IUniversityRepository.Add(University entity)
        {
            /* Null kontrol */
            if (entity == null)
                throw new ArgumentNullException("entity");

            /* Gelen verilerden bazıları string dizisi halinde geliyor. Veritabanına yazılması için string haline getiriyoruz. */
            /* Veri kaybı olmaması için dizideki verileri birleştiriyoruz. */
            string domains, web_Pages;
            domains = ArrayToString(entity.domains);
            web_Pages = ArrayToString(entity.web_pages);
            Connection.Execute(
                "Insert Into Universities(country, name, alpha_two_code, state_province, domains, web_pages)VALUES (@country, @name, @alpha_two_code, @state_province, @domains, @web_pages);",
                param: new { Country = entity.country, Name = entity.name, alpha_two_code = entity.alpha_two_code, state_province = entity.state_province, domains = domains, web_pages = web_Pages },
                transaction: Transaction
            );
        }
        public IEnumerable<UniversityRead> All()
        {
            return Connection.Query<UniversityRead>(
                "select * from Universities",
                transaction: Transaction
                );
        }
        public IEnumerable<UniversityRead> FindByName (string name)
        {
            return Connection.Query<UniversityRead>(
                "select * from Universities where name = @name",
                param: new { Name = name },
                transaction: Transaction
            );
        }
        void IUniversityRepository.RemoveByName(string name)
        {
            Connection.Execute(
                "delete from Universities where name = @name",
                param: new { Name = name },
                transaction: Transaction
            );
        }
        void IUniversityRepository.Update(University entity)
        {
            if (entity == null)
                throw new ArgumentNullException("entity");
            string domains, web_Pages;
            domains = ArrayToString(entity.domains);
            web_Pages = ArrayToString(entity.web_pages);
            Connection.Execute(
                "update Universities set country = @country, alpha_two_code = @alpha_two_code, state_province = @state_province, domains = @domains, web_pages = @web_pages where name = @name",
                param: new { Country = entity.country, alpha_two_code = entity.alpha_two_code, state_province = entity.state_province, domains = domains, web_pages = web_Pages, Name = entity.name},
                transaction: Transaction
            );
        }
        void IUniversityRepository.DeleteTable()
        {
            Connection.Execute(
                "delete from Universities",
                transaction: Transaction
            );
        }
        string ArrayToString(string[] strings)
        {
            string ret = "";
            for (int i = 0; i < strings.Length; i++)
            {
                if (i == 0) { ret = strings[i]; }
                else { ret = ret + " , " + strings[i]; }

            }
            return ret;
        }
    }
}
