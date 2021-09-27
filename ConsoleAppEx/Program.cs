using System;
using RestSharp;
using Newtonsoft.Json;
using UnitOfWork.Entities;
using System.Collections.Generic;
using System.Configuration;
using Dapper;



namespace CountryUniversities
{
    class Program
    {
        /* Program bir veya birden fazla veritabanı üzerinde üniversite bilgilerini gönderen bir 
         * rest apiyi baz alarak oluşturulan bir tablo üzerinde işlemler yapmaktadır.
         * Bu işlemler şunlardır : Select, Insert, Update, Delete, Select All, Delete All */
        static void Main(string[] args)
        {
            /* Config dosyası üzerinden girilen bağlantı bilgilerinin hepsi için çalışmaktadır. */
            /* Bağlantının MSSQL veya PostgreSQL olması gerekmektedir. */
            for (int i = 1; i < ConfigurationManager.ConnectionStrings.Count; i++)
            {
                using (var uow = new UnitOfWork.UnitOfWork(ConfigurationManager.ConnectionStrings[i].ConnectionString))
                {

                    /* Veritabanındaki bütün bilginin alınması. */
                    var allDatas = uow.UniversityRepository.All().AsList<UniversityRead>();

                    /* Alınan bütün ünversitelerinin isminin ekrana yazdırılması. */
                    allDatas.ForEach(data =>
                    {
                        Console.WriteLine(data.name);
                    });

                    /* Veritabanındaki verinin silinmesi. */
                    uow.UniversityRepository.DeleteTable();

                    /* Verinin tekrar servisten çekilip veritabanına yazılması */
                    var client = new RestClient("http://universities.hipolabs.com/search");
                    var request = new RestRequest(Method.GET);
                    IRestResponse resp = client.Execute(request);
                    List<University> obj = JsonConvert.DeserializeObject<List<University>>(resp.Content);
                    obj.ForEach(data =>
                    {
                        uow.UniversityRepository.Add(data);
                    });

                    /* Örnek bir verinin veritabanında bulunması ve ekrana yazılması. */
                    var unit = uow.UniversityRepository.FindByName("Istanbul University").AsList();
                    unit.ForEach(data =>
                    {
                        Console.WriteLine(data.name);
                    });

                    /* Örnek bir verinin güncellenmesi. */
                    University example = new University { country = "Turkey", name = "Istanbul University", alpha_two_code = "TR", state_province = true, domains = new string[] { "istanbulc.edu.tr" }, web_pages = new string[] { "" } };
                    uow.UniversityRepository.Update(example);

                    /* Örnek bir verinin silinmesi. */
                    uow.UniversityRepository.RemoveByName("Istanbul University");

                    uow.Commit();

                }
               
            }



        }
    }
}
