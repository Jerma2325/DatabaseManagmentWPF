using Moq;
using projectSchool;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProject1.Services.DatabaseServices
{
    [TestFixture]
    public  class DatabaseWorkersServiceTest
    {
        public void btnAdd_Click()
        {
            
            
            SklepDatabaseEntities db = new SklepDatabaseEntities();
            
            Pracownik pracownikObject = new Pracownik()
            {
                Id = 0,
                Imie_i_Nazwisko = "Serg Bryankski",
                Stanowisko = "Sprzedawca-Kasjer",
                PESEL = "",
                Data_Urodzenia = DateTime.Now,
                Data_zatrudnienia = DateTime.MinValue,

            };
            db.Pracowniks.Add(pracownikObject);
            db.SaveChanges();
            foreach (var item in db.Pracowniks)
            {
                Console.WriteLine(item.Stanowisko);
            }
        }
    }
}
