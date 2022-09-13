using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace projectSchool
{
    /// <summary>
    /// Interaction logic for ProductsControl.xaml
    /// </summary>
    public partial class ProductsControl : Window
    {



        public ProductsControl()
        {
            InitializeComponent();

            SklepDatabaseEntities db = new SklepDatabaseEntities();
            var products = from d in db.Produkts
                             select new
                             {
                                 Id = d.Id,
                                 Nazwa= d.Nazwa,
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
            this.gridProducts.ItemsSource = products.ToList();
        }

        private int updatingProduktID = 0;
        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.gridProducts.SelectedIndex >= 0)
            {
                if (this.gridProducts.SelectedItems.Count >= 0)
                {
                    if (this.gridProducts.SelectedItems[0].GetType() == typeof(Produkt))
                    {
                        Produkt d = (Produkt)this.gridProducts.SelectedItems[0];
                        this.box_Nazwa.Text = d.Nazwa;
                        this.pick_Dostawa.SelectedDate = d.Data_dostawy;
                        this.box_Ilosc.Text=Convert.ToString( d.Ilość_na_stanie);
                        this.updatingProduktID = d.Id;
                        this.box_Typ.Text = d.Typ;


                    }
                }
            }
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            SklepDatabaseEntities db = new SklepDatabaseEntities();
            Produkt ProduktObject = new Produkt()
            {
                Nazwa = box_Nazwa.Text,
                Data_dostawy= pick_Dostawa.SelectedDate,
                Ilość_na_stanie = Convert.ToDouble(box_Ilosc.Text),
                Typ = box_Typ.Text,
                

            };
            db.Produkts.Add(ProduktObject);
            db.SaveChanges();
            this.gridProducts.ItemsSource = db.Produkts.ToList();
        }


        private void btnLoadData_Click(object sender, RoutedEventArgs e)
        {
            SklepDatabaseEntities db = new SklepDatabaseEntities();
            this.gridProducts.ItemsSource = db.Produkts.ToList();
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            SklepDatabaseEntities db = new SklepDatabaseEntities();

            var r = from d in db.Produkts
                    where d.Id == this.updatingProduktID
                    select d;
            Produkt obj = r.SingleOrDefault();
            if (obj != null)
            {
                obj.Nazwa = this.box_Nazwa.Text;
                obj.Data_dostawy = this.pick_Dostawa.SelectedDate;
                obj.Ilość_na_stanie = Convert.ToDouble(this.box_Ilosc.Text);
                obj.Typ = this.box_Typ.Text;
               

                db.SaveChanges();
                this.gridProducts.ItemsSource = db.Produkts.ToList();
            }
        }

        private void btnRemove_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult msgBoxResult = MessageBox.Show("Czy na pewno chcesz usunąć?",
                "Usunąć produkt ",
                 MessageBoxButton.YesNo,
                 MessageBoxImage.Warning,
                 MessageBoxResult.No);
            if (msgBoxResult == MessageBoxResult.Yes)
            {
                SklepDatabaseEntities db = new SklepDatabaseEntities();

                var r = from d in db.Produkts
                        where d.Id == this.updatingProduktID
                        select d;
                Produkt obj = r.SingleOrDefault();
                if (obj != null)
                {
                    db.Produkts.Remove(obj);
                    db.SaveChanges();
                    this.gridProducts.ItemsSource = db.Produkts.ToList();
                }

            }
        }

        private void box_Name_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
