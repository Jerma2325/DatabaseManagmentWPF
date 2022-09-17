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
using System.Windows.Shapes;

namespace projectSchool
{
    /// <summary>
    /// Interaction logic for SklepsControl.xaml
    /// </summary>
    public partial class SklepsControl : Window
    {


        /// <summary>
        /// Utwzarza tabelke i prezentuje dane z bazy danych w niej.
        /// </summary>
        public SklepsControl()
        {
            InitializeComponent();

            SklepDatabaseEntities db = new SklepDatabaseEntities();
            var skleps = from d in db.Skleps
                           select new
                           {
                               Id = d.Id,
                               Adres = d.Adres,
                               Data_Otwarcia = d.Data_otwarcia,
                               Ilość_Pracownikow= d.Ilość_pracówników,
                               Ilość_Etatów = d.Ilość_etatów,
                               Budżet = d.Budżet,
                               Powierzchnia= d.Powierzchnia_m_2_,



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
            this.gridSklepy.ItemsSource = skleps.ToList();
        }

        private int updatingSklepID = 0;
        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.gridSklepy.SelectedIndex >= 0)
            {
                if (this.gridSklepy.SelectedItems.Count >= 0)
                {
                    if (this.gridSklepy.SelectedItems[0].GetType() == typeof(Sklep))
                    {
                        Sklep d = (Sklep)this.gridSklepy.SelectedItems[0];
                        this.box_Adres.Text = d.Adres;
                        this.pick_Otwarcie.SelectedDate = d.Data_otwarcia;
                        this.box_Pracownik.Text =Convert.ToString(d.Ilość_pracówników );
                        this.updatingSklepID = d.Id;
                        this.box_Etaty.Text = Convert.ToString( d.Ilość_etatów);
                        this.box_budzet.Text = Convert.ToString(d.Budżet);
                        this.box_powierzch.Text = Convert.ToString(d.Powierzchnia_m_2_);


                    }
                }
            }
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            SklepDatabaseEntities db = new SklepDatabaseEntities();
            Sklep SklepObject = new Sklep()
            {
                Adres = box_Adres.Text,
                Data_otwarcia = pick_Otwarcie.SelectedDate,
                Ilość_pracówników = Convert.ToInt32(box_Pracownik.Text),
                Ilość_etatów = Convert.ToDouble(box_Etaty.Text),
                Budżet = Convert.ToDouble(box_budzet.Text),
                Powierzchnia_m_2_= Convert.ToDouble(box_powierzch.Text),


            };
            db.Skleps.Add(SklepObject);
            db.SaveChanges();
            this.gridSklepy.ItemsSource = db.Skleps.ToList();
        }


        private void btnLoadData_Click(object sender, RoutedEventArgs e)
        {
            SklepDatabaseEntities db = new SklepDatabaseEntities();
            this.gridSklepy.ItemsSource = db.Skleps.ToList();
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            SklepDatabaseEntities db = new SklepDatabaseEntities();

            var r = from d in db.Skleps
                    where d.Id == this.updatingSklepID
                    select d;
            Sklep obj = r.SingleOrDefault();
            if (obj != null)
            {
                obj.Adres = this.box_Adres.Text;
                obj.Data_otwarcia = this.pick_Otwarcie.SelectedDate;
                obj.Ilość_pracówników = Convert.ToInt32(this.box_Pracownik.Text);
                obj.Ilość_etatów = Convert.ToDouble(this.box_Etaty.Text);
                obj.Budżet = Convert.ToDouble(this.box_budzet.Text);
                obj.Powierzchnia_m_2_ = Convert.ToDouble(this.box_budzet.Text);


                db.SaveChanges();
                this.gridSklepy.ItemsSource = db.Skleps.ToList();
            }
        }

        private void btnRemove_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult msgBoxResult = MessageBox.Show("Czy na pewno chcesz usunąć?",
                "Usunąć Sklep ",
                 MessageBoxButton.YesNo,
                 MessageBoxImage.Warning,
                 MessageBoxResult.No);
            if (msgBoxResult == MessageBoxResult.Yes)
            {
                SklepDatabaseEntities db = new SklepDatabaseEntities();

                var r = from d in db.Skleps
                        where d.Id == this.updatingSklepID
                        select d;
                Sklep obj = r.SingleOrDefault();
                if (obj != null)
                {
                    db.Skleps.Remove(obj);
                    db.SaveChanges();
                    this.gridSklepy.ItemsSource = db.Skleps.ToList();
                }

            }
        }

        
    }
}
