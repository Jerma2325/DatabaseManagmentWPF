using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace projectSchool
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            SklepDatabaseEntities db = new SklepDatabaseEntities();
            var products = from d in db.Produkts
                           select new
                           {
                               Id = d.Id,
                               Nazwa = d.Nazwa,
                               Data_Dostawy = d.Data_dostawy,
                               Ilość = d.Ilość_na_stanie,
                               Typ = d.Typ,



                           };
            foreach (var item in products)
            {
                Console.WriteLine(item.Id);
                Console.WriteLine(item.Nazwa);
                Console.WriteLine(item.Data_Dostawy);
                Console.WriteLine(item.Ilość);
                Console.WriteLine(item.Typ);

            }
            

            var skleps = from d in db.Skleps
                           select new
                           {
                               Id = d.Id,
                               Adres = d.Adres,
                               Data_Otwarcia = d.Data_otwarcia,
                               Ilość_Pracownikow = d.Ilość_pracówników,
                               Ilość_Etatów = d.Ilość_etatów,
                               Budżet = d.Budżet,
                               Powierzchnia = d.Powierzchnia_m_2_,
                           };
            foreach (var item in skleps)
            {
                Console.WriteLine(item.Id);
                Console.WriteLine(item.Adres);
                Console.WriteLine(item.Data_Otwarcia);
                Console.WriteLine(item.Ilość_Pracownikow);
                Console.WriteLine(item.Ilość_Etatów);
                Console.WriteLine(item.Budżet);
                Console.WriteLine(item.Powierzchnia);

            }
           

            var pracowniks = from d in db.Pracowniks
                             select new
                             {
                                 Id = d.Id,
                                 Imie = d.Imie_i_Nazwisko,
                                 Stanowisko = d.Stanowisko,
                                 Pesel = d.PESEL,
                                 DataUrodzenia = d.Data_Urodzenia,
                                 DataZatrudnienia = d.Data_zatrudnienia,
                             };
            foreach (var item in pracowniks)
            {
                Console.WriteLine(item.Id);
                Console.WriteLine(item.Imie);
                Console.WriteLine(item.Pesel);
                Console.WriteLine(item.DataUrodzenia);
                Console.WriteLine(item.Stanowisko);
                Console.WriteLine(item.DataZatrudnienia);

            }
            
            var wyniks = from d in db.Wyniks
                         select new
                         {
                             Id = d.Id,
                             Id_Sklepu = d.Id_Sklepu,
                             Budżet__na_ten_miesiąc = d.Budżet_na_ten_miesiąc,
                             Zrealizowano = d.Zrealizowano,
                             Wynik_budżetu = d.Wynik_budżetu,
                             Straty_rzeczywiste = d.Straty_rzeczywiste,
                             Straty_oczekiwane = d.Straty_oczekiwane,
                             Wynik_strat = d.Wynik_strat
                         };
            foreach (var item in wyniks)
            {
                Console.WriteLine(item.Id);
                Console.WriteLine(item.Id_Sklepu);
                Console.WriteLine(item.Budżet__na_ten_miesiąc);
                Console.WriteLine(item.Zrealizowano);
                Console.WriteLine(item.Wynik_budżetu);
                Console.WriteLine(item.Straty_oczekiwane);
                Console.WriteLine(item.Straty_rzeczywiste);
                Console.WriteLine(item.Wynik_strat);

            }
            this.wynikiGrid.ItemsSource = wyniks.ToList();
            this.pracownikiGrid.ItemsSource = pracowniks.ToList();
            this.produktyGrid.ItemsSource = products.ToList();
            this.sklepyGrid.ItemsSource = skleps.ToList();
        }

        private void btn_wyniks_Click(object sender, RoutedEventArgs e)
        {
            WyniksControl wyniksControl = new WyniksControl();
            wyniksControl.Show();
        }

        private void btn_sklepy_Click(object sender, RoutedEventArgs e)
        {
            SklepsControl sklepsControl = new SklepsControl();
            sklepsControl.Show();
        }

        private void btn_pracow_Click(object sender, RoutedEventArgs e)
        {
            WorkersControl workersControl = new WorkersControl();
            workersControl.Show();
        }

        private void btn_products_Click(object sender, RoutedEventArgs e)
        {
            ProductsControl productsControl = new ProductsControl();
            productsControl.Show();
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            SklepDatabaseEntities db = new SklepDatabaseEntities();

            var pracowniks = from d in db.Pracowniks
                             select new
                             {
                                 Id = d.Id,
                                 Imie = d.Imie_i_Nazwisko,
                                 Stanowisko = d.Stanowisko,
                                 Pesel = d.PESEL,
                                 DataUrodzenia = d.Data_Urodzenia,
                                 DataZatrudnienia = d.Data_zatrudnienia,
                             };
            var wyniks = from d in db.Wyniks
                         select new
                         {
                             Id = d.Id,
                             Id_Sklepu = d.Id_Sklepu,
                             Budżet__na_ten_miesiąc = d.Budżet_na_ten_miesiąc,
                             Zrealizowano = d.Zrealizowano,
                             Wynik_budżetu = d.Wynik_budżetu,
                             Straty_rzeczywiste = d.Straty_rzeczywiste,
                             Straty_oczekiwane = d.Straty_oczekiwane,
                             Wynik_strat = d.Wynik_strat
                         };
            var skleps = from d in db.Skleps
                         select new
                         {
                             Id = d.Id,
                             Adres = d.Adres,
                             Data_Otwarcia = d.Data_otwarcia,
                             Ilość_Pracownikow = d.Ilość_pracówników,
                             Ilość_Etatów = d.Ilość_etatów,
                             Budżet = d.Budżet,
                             Powierzchnia = d.Powierzchnia_m_2_,
                         };
            var products = from d in db.Produkts
                           select new
                           {
                               Id = d.Id,
                               Nazwa = d.Nazwa,
                               Data_Dostawy = d.Data_dostawy,
                               Ilość = d.Ilość_na_stanie,
                               Typ = d.Typ,
                           };
            this.wynikiGrid.ItemsSource = wyniks.ToList();
            this.pracownikiGrid.ItemsSource = pracowniks.ToList();
            this.produktyGrid.ItemsSource = products.ToList();
            this.sklepyGrid.ItemsSource = skleps.ToList();
        }
    }
}
