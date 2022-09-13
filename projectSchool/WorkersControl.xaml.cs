using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace projectSchool
{
    /// <summary>
    /// Interaction logic for WorkersControl.xaml
    /// </summary>
    public partial class WorkersControl : Window
    {
        

        
        public WorkersControl()
        {
            InitializeComponent();

            SklepDatabaseEntities db = new SklepDatabaseEntities ();
            var pracowniks = from d in db.Pracowniks
                       select new
                       {
                           Id = d.Id,
                           Imie = d.Imie_i_Nazwisko,
                           Stanowisko = d.Stanowisko,
                           Pesel = d.PESEL,
                           DataUrodzenia = d.Data_Urodzenia,
                           DataZatrudnienia= d.Data_zatrudnienia,
                          
                          
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
            this.gridWorkers.ItemsSource = pracowniks.ToList();
        }
        
        private int updatingPracownikID = 0;
        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.gridWorkers.SelectedIndex >= 0)
            {
                if (this.gridWorkers.SelectedItems.Count >= 0)
                {
                    if (this.gridWorkers.SelectedItems[0].GetType() == typeof(Pracownik))
                    {
                        Pracownik d = (Pracownik)this.gridWorkers.SelectedItems[0];
                        this.box_Name.Text = d.Imie_i_Nazwisko;
                        this.box_Position.Text = d.Stanowisko;
                        this.pick_BDate.SelectedDate = d.Data_Urodzenia;
                        this.updatingPracownikID = d.Id;
                        this.box_pesel.Text = d.PESEL;
                        this.pick_DateZatrud.SelectedDate = d.Data_zatrudnienia;
                       
                        
                    }
                }
            }
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            SklepDatabaseEntities db = new SklepDatabaseEntities();
            Pracownik pracownikObject = new Pracownik()
            {
                Imie_i_Nazwisko = box_Name.Text,
                Stanowisko = box_Position.Text,
                PESEL = box_pesel.Text,
                Data_Urodzenia= pick_BDate.SelectedDate,
                Data_zatrudnienia =pick_DateZatrud.SelectedDate,
                
            };
            db.Pracowniks.Add(pracownikObject);
            db.SaveChanges();
            this.gridWorkers.ItemsSource = db.Pracowniks.ToList();
        }

        
        private void btnLoadData_Click(object sender, RoutedEventArgs e)
        {
            SklepDatabaseEntities db = new SklepDatabaseEntities();
            this.gridWorkers.ItemsSource = db.Pracowniks.ToList();
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            SklepDatabaseEntities db = new SklepDatabaseEntities();

            var r = from d in db.Pracowniks
                    where d.Id == this.updatingPracownikID
                    select d;
            Pracownik obj = r.SingleOrDefault();
            if (obj != null)
            {
                obj.Imie_i_Nazwisko = this.box_Name.Text;
                obj.Stanowisko= this.box_Position.Text;
                obj.PESEL =this.box_pesel.Text;
                obj.Data_zatrudnienia = this.pick_DateZatrud.SelectedDate;
                obj.Data_Urodzenia = this.pick_BDate.SelectedDate;
                
                db.SaveChanges();
                this.gridWorkers.ItemsSource = db.Pracowniks.ToList();
            }
        }

        private void btnRemove_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult msgBoxResult = MessageBox.Show("Czy na pewno chcesz usunąć?",
                "Usunąć pracownika ",
                 MessageBoxButton.YesNo,
                 MessageBoxImage.Warning,
                 MessageBoxResult.No);
            if (msgBoxResult == MessageBoxResult.Yes)
            {
                SklepDatabaseEntities db = new SklepDatabaseEntities();

                var r = from d in db.Pracowniks
                        where d.Id == this.updatingPracownikID
                        select d;
                Pracownik obj = r.SingleOrDefault();
                if (obj != null)
                {
                    db.Pracowniks.Remove(obj);
                    db.SaveChanges();
                    this.gridWorkers.ItemsSource = db.Pracowniks.ToList();
                }

            }
        }

        
    }
}
